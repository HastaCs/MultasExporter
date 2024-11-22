using API.Modelos;
using System.IO.Compression;

namespace API
{
  public class Utils
  {
    public static List<PdfExtraido> ExtractPdfsFromZip(byte[] zipData)
    {
      var pdfList = new List<PdfExtraido>();

      using (var zipStream = new MemoryStream(zipData))
      {
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
          foreach (var entry in archive.Entries)
          {
            if (Path.GetExtension(entry.FullName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
              using (var entryStream = entry.Open())
              {
                using (var memoryStream = new MemoryStream())
                {
                  entryStream.CopyTo(memoryStream);

                  // Añadir a la lista con el nombre y el contenido del archivo
                  pdfList.Add(new PdfExtraido
                  {
                    NombreFichero = entry.FullName,
                    Contenido = memoryStream.ToArray()
                  });
                }
              }
            }
          }
        }
      }

      return pdfList;
    }
  }
}
