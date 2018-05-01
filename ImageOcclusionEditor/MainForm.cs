using Hjg.Pngcs;
using Hjg.Pngcs.Chunks;
using Svg;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ImageOcclusionEditor
{
  public partial class MainForm : Form
  {
    const string SvgEditorPath = "svg-edit/svg-editor.html";

    public string OriginalSvg { get; }
    public string OcclusionFilePath { get; }
    public int OcclusionWidth { get; }
    public int OcclusionHeight { get; }

    public MainForm(string backgroundFilePath, string occlusionFilePath)
    {
      InitializeComponent();

      OcclusionFilePath = occlusionFilePath;
      OriginalSvg = ReadSvgFromChunk();

      int width, height;
      GetImageSize(backgroundFilePath, out width, out height);

      OcclusionWidth = width;
      OcclusionHeight = height;

      wb.DocumentCompleted += Wb_DocumentCompleted;
      wb.Navigate(String.Format("{0}?{1}", GetSvgEditorUri(), GenerateUrlParams(backgroundFilePath, width, height)));
    }

    private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (String.IsNullOrWhiteSpace(OriginalSvg) == false)
        SetSvgInBrowser(OriginalSvg);
    }

    protected override void OnLoad(EventArgs e)
    {
      if (Properties.Settings.Default.Maximized)
      {
        WindowState = FormWindowState.Maximized;
        Location = Properties.Settings.Default.Location;
        Size = Properties.Settings.Default.Size;
      }
      else if (Properties.Settings.Default.Minimized)
      {
        WindowState = FormWindowState.Minimized;
        Location = Properties.Settings.Default.Location;
        Size = Properties.Settings.Default.Size;
      }
      else
      {
        Location = Properties.Settings.Default.Location;
        Size = Properties.Settings.Default.Size;
      }

      base.OnLoad(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (WindowState == FormWindowState.Maximized)
      {
        Properties.Settings.Default.Location = RestoreBounds.Location;
        Properties.Settings.Default.Size = RestoreBounds.Size;
        Properties.Settings.Default.Maximized = true;
        Properties.Settings.Default.Minimized = false;
      }
      else if (WindowState == FormWindowState.Normal)
      {
        Properties.Settings.Default.Location = Location;
        Properties.Settings.Default.Size = Size;
        Properties.Settings.Default.Maximized = false;
        Properties.Settings.Default.Minimized = false;
      }
      else
      {
        Properties.Settings.Default.Location = RestoreBounds.Location;
        Properties.Settings.Default.Size = RestoreBounds.Size;
        Properties.Settings.Default.Maximized = false;
        Properties.Settings.Default.Minimized = true;
      }

      Properties.Settings.Default.Save();

      base.OnClosing(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == (Keys.Escape))
      {
        Close();

        return true;
      }

      else if (keyData == (Keys.Control | Keys.Shift | Keys.S))
      {
        SaveOcclusion();

        return true;
      }

      else if (keyData == (Keys.Control | Keys.S))
      {
        SaveOcclusionAndExit();

        return true;
      }

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private Uri GetSvgEditorUri()
    {
      string appFolder = Application.StartupPath;

      return new Uri(String.Format("file:///{0}", Path.Combine(appFolder, SvgEditorPath)));
    }

    private string GenerateUrlParams(string backgroundFilePath, int width, int height)
    {
      var urlParams = HttpUtility.ParseQueryString(string.Empty);

      urlParams.Add("bkgd_url", backgroundFilePath);
      urlParams.Add("dimensions", String.Format("{0},{1}", width, height));
      urlParams.Add("initFill[color]", Properties.Settings.Default.FillColor);
      urlParams.Add("initFill[opacity]", "1");
      urlParams.Add("initStroke[color]", Properties.Settings.Default.StrokeColor);
      urlParams.Add("initStroke[width]", Properties.Settings.Default.StrokeWidth);
      urlParams.Add("initStroke[opacity]", "1");

      return urlParams.ToString();
    }

    private void GetImageSize(string filePath, out int width, out int height)
    {
      using (Image img = Image.FromFile(filePath))
      {
        width = img.Width;
        height = img.Height;
      }
    }

    private void SetSvgInBrowser(string svg)
    {
      svg = svg.Replace("\r", "").Replace("\n", "");

      wb.Document.InvokeScript("eval", new[] { String.Format("svgCanvas.setSvgString('{0}')", svg) });
    }

    private string GetSvgFromBrowser()
    {
      return wb.Document.InvokeScript("eval", new[] { "svgCanvas.svgCanvasToString()" }).ToString();
    }

    private Bitmap ConvertSvgToImage(string svg, int width, int height)
    {
      var svgDoc = SvgDocument.FromSvg<SvgDocument>(svg);

      return svgDoc.Draw(width, height);
    }

    private void CreateChunk(PngWriter pngw, string svg)
    {
      PngChunkSVGI chunk = new PngChunkSVGI(pngw.ImgInfo);
      chunk.SetSVG(svg);
      chunk.Priority = true;

      pngw.GetChunksList().Queue(chunk);
    }

    private Stream ToMemoryStream(string filePath)
    {
      MemoryStream memStream = new MemoryStream();

      using (Stream inStream = File.OpenRead(filePath))
      {
        byte[] buffer = new byte[8192];

        while (inStream.Read(buffer, 0, buffer.Length) > 0)
          memStream.Write(buffer, 0, buffer.Length);
      }

      memStream.Seek(0, SeekOrigin.Begin);

      return memStream;
    }

    private void WriteSvgToChunk(string tmpOcclusionFilePath, string svg)
    {
      using (Stream inStream = ToMemoryStream(tmpOcclusionFilePath))
      {
        PngReader pngr = new PngReader(inStream);
        PngWriter pngw = FileHelper.CreatePngWriter(tmpOcclusionFilePath, pngr.ImgInfo, true);

        pngw.CopyChunksFirst(pngr, ChunkCopyBehaviour.COPY_ALL_SAFE);

        CreateChunk(pngw, svg);

        for (int row = 0; row < pngr.ImgInfo.Rows; row++)
        {
          ImageLine l1 = pngr.ReadRow(row);
          pngw.WriteRow(l1, row);
        }

        pngw.CopyChunksLast(pngr, ChunkCopyBehaviour.COPY_ALL);

        pngr.End();
        pngw.End();
      }
    }

    private string ReadSvgFromChunk()
    {
      var pngr = FileHelper.CreatePngReader(OcclusionFilePath);

      PngChunkSVGI chunk = (PngChunkSVGI)pngr.GetChunksList().GetById1(PngChunkSVGI.ID);

      pngr.End();

      return chunk?.GetSVG();
    }

    private void SaveOcclusion()
    {
      string tmpOcclusionFilePath = Path.GetTempFileName();
      string svg = GetSvgFromBrowser();

      using (Bitmap img = ConvertSvgToImage(svg, OcclusionWidth, OcclusionHeight))
      {
        img.Save(tmpOcclusionFilePath, ImageFormat.Png);
      }

      WriteSvgToChunk(tmpOcclusionFilePath, svg);

      if (File.Exists(OcclusionFilePath))
        File.Delete(OcclusionFilePath);

      File.Move(tmpOcclusionFilePath, OcclusionFilePath);
    }

    private void SaveOcclusionAndExit()
    {
      SaveOcclusion();
      Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      SaveOcclusion();
    }

    private void btnSaveExit_Click(object sender, EventArgs e)
    {
      SaveOcclusionAndExit();
    }
  }
}
