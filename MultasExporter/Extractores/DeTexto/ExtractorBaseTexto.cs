using MultasExporter.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Graphics.Operations.TextState;

namespace MultasExporter.Extractores.DeWords
{
  public abstract class ExtractorBaseTexto
  {
    private IEnumerable<Word> _palabras { get; set; }

    public void Set(IEnumerable<Word> palabras)
    {
      _palabras = palabras;
    }

    public bool Match(string match)
    {
      var str = string.Join(" ", _palabras.Select(x => x.Text));
      return str.Contains(match);
    }
    public string GetDato(PdfRectangle rect)
    {
      var palabras = _palabras.Where(word =>
                      word.BoundingBox.BottomLeft.X >= rect.BottomLeft.X &&
                      word.BoundingBox.BottomLeft.Y >= rect.BottomLeft.Y &&
                      word.BoundingBox.TopRight.X <= rect.TopRight.X &&
                      word.BoundingBox.TopRight.Y <= rect.TopRight.Y)
                  .Select(word => word.Text);
      return string.Join(" ", palabras).Trim();
    }
  }
}
