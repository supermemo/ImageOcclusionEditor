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
        return;

      string backgroundImg = args[0];
      string occlusionImg = args[1];

      if (!File.Exists(backgroundImg) || !File.Exists(occlusionImg))
        return;

      if (!ValidateImage(backgroundImg) || !ValidateImage(occlusionImg))
        return;

      PngChunk.FactoryRegister(PngChunkSVGI.ID, typeof(PngChunkSVGI));

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm(backgroundImg, occlusionImg));
    }

    private static bool ValidateImage(string filePath)
    {
      using (Stream stream = File.OpenRead(filePath))
      {
        var insp = new FileFormatInspector();

        return insp.DetermineFileFormat(stream) is Image;
      }
    }
  }
}
