using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using UglyToad.PdfPig.Content;

namespace MultasExporter.Interfaces
{
    public interface IFromTexto
    {
        public void SetPalabras(IEnumerable<Word> palabras);
    }
}
