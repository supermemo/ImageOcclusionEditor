using FileSignatures;
using FileSignatures.Formats;
using Hjg.Pngcs.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageOcclusionEditor
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      if (args.Length != 2)
      {
        MessageBox.Show("Invalid number of parameters. Usage: ImageOcclusionEditor.exe <background-image-path> <occlusion-image-path>");
        return;
      }

      string backgroundImg = args[0];
      string occlusionImg = args[1];

      if (!File.Exists(backgroundImg))
      {
        MessageBox.Show(String.Format("Background file {0} doesn't exist", backgroundImg));
        return;
      }

      if (!File.Exists(occlusionImg))
      {
        MessageBox.Show(String.Format("Occlusion file {0} doesn't exist", occlusionImg));
        return;
      }

      if (!ValidateImage(backgroundImg))
      {
        MessageBox.Show(String.Format("Background file {0} isn't a known Image Format", backgroundImg));
        return;
      }

      if (!ValidateImage(occlusionImg))
      {
        MessageBox.Show(String.Format("Occlusion file {0} isn't a known Image Format", occlusionImg));
        return;
      }

      PngChunk.FactoryRegister(PngChunkSVGI.ID, typeof(PngChunkSVGI));

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm(backgroundImg, occlusionImg));
    }

    private static bool ValidateImage(string filePath)
    {
      try
      {
        using (Stream stream = File.OpenRead(filePath))
        {
          var insp = new FileFormatInspector();

          return insp.DetermineFileFormat(stream) is Image;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("An exception was thrown while opening file {0}.\n\nException message: {1}", filePath, ex.Message));
        Application.Exit();

        return false;
      }
    }
  }
}
