using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter
{
  internal interface IFromPix
  {
    public void SetPix(Pix pixImagen);
  }
}
