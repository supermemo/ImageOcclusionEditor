using Hjg.Pngcs;
using Hjg.Pngcs.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOcclusionEditor
{
  class PngChunkSVGI : PngChunkSingle
  {
    // ID must follow the PNG conventions: four ascii letters,
    // ID[0] : lowercase (ancillary)
    // ID[1] : lowercase if private, upppecase if public
    // ID[3] : uppercase if "safe to copy"
    public readonly static String ID = "svGi";

    private string svg;

    public PngChunkSVGI(ImageInfo info)
        : base(ID, info)
    {
      svg = string.Empty;
    }

    public override ChunkOrderingConstraint GetOrderingConstraint()
    {
      // change this if you don't require this chunk to be before IDAT, etc
      return ChunkOrderingConstraint.BEFORE_IDAT;
    }

    // in this case, we have that the chunk data corresponds to the serialized object
    public override ChunkRaw CreateRawChunk()
    {
      ChunkRaw c = null;

      byte[] arr = Encoding.UTF8.GetBytes(svg);
      c = createEmptyChunk(arr.Length, true);
      c.Data = arr;
        
      return c;
    }

    public override void ParseFromRaw(ChunkRaw c)
    {
      svg = Encoding.UTF8.GetString(c.Data);
    }

    public override void CloneDataFromRead(PngChunk other)
    {
      PngChunkSVGI otherx = (PngChunkSVGI)other;
      this.svg = otherx.svg; // shallow clone, we could implement other copying
    }

    public string GetSVG()
    {
      return svg;
    }

    public void SetSVG(string osvg)
    {
      this.svg = osvg;
    }

  }
}
