using MultasExporter;
using MultasExporter.Extractores.DeTexto;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Tesseract;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Graphics;

namespace MultasConsola
{
  public class Program
  {
  

    public async static Task SacaRectangulos( string file,int pagina)
    {
      using (var pdfDocument = PdfDocument.Open(file))
      {
        // Seleccionar la primera página
        var page = pdfDocument.GetPage(pagina);
        var annotations = page.ExperimentalAccess.GetAnnotations().ToList();


        var texto = "";
        foreach (var item in annotations)
        {


          if (item.AnnotationDictionary.Data.TryGetValue("Subj", out var valor))
          {
            texto += $"var rec{valor.ToString().Replace("(", "_").Replace(")", "")}=new PdfRectangle({item.Rectangle.BottomLeft.X.ToString().Replace(",", ".")}, {item.Rectangle.BottomLeft.Y.ToString().Replace(",", ".")},{item.Rectangle.TopRight.X.ToString().Replace(",", ".")}, {item.Rectangle.TopRight.Y.ToString().Replace(",", ".")});{Environment.NewLine}";
            Console.WriteLine(valor?.ToString());
          }


        }
        await File.WriteAllTextAsync(file+"_rectangulos.txt", texto);
      }
    }

      public static void GuardaImagen(string path, string pathDestino,int pagina)
    {
      var dd = File.ReadAllBytes(path);
      byte[] imageData = Freeware.Pdf2Png.Convert(dd, pagina);


      // Especifica la ruta donde quieres guardar el archivo.
      string rutaArchivo = pathDestino;

      // Guarda el byte[] como un archivo .png.
      File.WriteAllBytes(rutaArchivo, imageData);
    }

    public static async Task ProcesaPdf(string folderPath, string file)
    {
    //  using (var pdfDocument = PdfDocument.Open(file))
      {
        // Seleccionar la primera página
       // var page = pdfDocument.GetPage(1);
        var dd = await File.ReadAllBytesAsync(file);
        var extractor = new ExtractorMulta(dd);
        var datos = await extractor.GetDatosAsync();

        var options1 = new JsonSerializerOptions
        {
          Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
          WriteIndented = true
        };
        var objetoComoJson = JsonSerializer.Serialize(datos, options1);
        string fileName = Path.GetFileNameWithoutExtension(file);

        // Define el path del nuevo archivo .txt
        string txtFilePath = Path.Combine(folderPath, fileName + "_json.txt");

        // Escribe "hola" dentro del archivo .txt
        await File.WriteAllTextAsync(txtFilePath, objetoComoJson);
      }
    }



    public static async Task ProcesarArchivosAsync(string folderPath, IEnumerable<string> files)
    {
      var tasks = files.Select(file => ProcesaPdf(folderPath, file)).ToList();
      await Task.WhenAll(tasks);
    }
    public static List<(string NombreArchivo, byte[] Contenido)> LeerPDFsDesdeZIP(string zipPath)
    {
      var pdfFiles = new List<(string NombreArchivo, byte[] Contenido)>();

      // Abrir el ZIP para lectura
      using (var zip = ZipFile.OpenRead(zipPath))
      {
        foreach (var entry in zip.Entries)
        {
          // Filtrar solo los archivos PDF
          if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
          {
            using (var memoryStream = new MemoryStream())
            {
              // Copiar el contenido del PDF en el MemoryStream
              using (var entryStream = entry.Open())
              {
                entryStream.CopyTo(memoryStream);
              }

              // Guardar el contenido del PDF como byte[] y su nombre
              pdfFiles.Add((entry.Name, memoryStream.ToArray()));
            }
          }
        }
      }

      return pdfFiles;
    }

    //static String tesseractData = "/tessdata";
    static async Task Main(string[] args)
    {
      var watch = new Stopwatch();
      watch.Start();
      Console.WriteLine(DateTime.Now);
    //  Console.OutputEncoding = Encoding.UTF8;
      string folderPath = @"D:\pdfsMultas\rectangulos";
    
     var files = Directory.GetFiles(folderPath).Where(x => x.EndsWith(".pdf"));
     // await ProcesarArchivosAsync(folderPath, files);
      foreach (var file in files)
      {
       await SacaRectangulos(file,1);
     
        //GuardaImagen(file, $"D:/pdfsMultas/imagenes/{Path.GetFileName(file)}.png",1);
      //await ProcesaPdf(folderPath, file);
      }
      watch.Stop();
      Console.WriteLine("Tarda: " + watch.ElapsedMilliseconds);
      Console.WriteLine(DateTime.Now);
      
    }

   

  }
}
