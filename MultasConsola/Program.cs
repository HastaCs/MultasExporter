using MultasExporter;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Tesseract;

namespace MultasConsola
{
  public class Program
  {
    public static async Task<byte[]> PruebaAsync(string path)
    {
      Console.WriteLine("empiezo");
      var dd = await File.ReadAllBytesAsync("pdf/DGT.pdf");
      byte[] pngByte = Freeware.Pdf2Png.Convert(dd, 1);
      Console.WriteLine("He terminado");
      return pngByte;
    }


    public static void GuardaImagen(string path,string pathDestino)
    {
      var dd =  File.ReadAllBytes(path);
      byte[] imageData = Freeware.Pdf2Png.Convert(dd, 1);


      // Especifica la ruta donde quieres guardar el archivo.
      string rutaArchivo = pathDestino;

      // Guarda el byte[] como un archivo .png.
      File.WriteAllBytes(rutaArchivo, imageData);
    }

    public static async Task ProcesaPdf(string folderPath,string file)
    {
      var dd = await File.ReadAllBytesAsync(file);
      var extractor = new ExtractorMulta(dd);
      var datos = extractor.GetDatos();

      var options1 = new JsonSerializerOptions
      {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
        WriteIndented = true
      };
      var objetoComoJson = JsonSerializer.Serialize(datos, options1);
      string fileName = Path.GetFileNameWithoutExtension(file);

      // Define el path del nuevo archivo .txt
      string txtFilePath = Path.Combine(folderPath, fileName + ".txt");

      // Escribe "hola" dentro del archivo .txt
      await File.WriteAllTextAsync(txtFilePath, objetoComoJson);
    }




    //static String tesseractData = "/tessdata";
    static async Task Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;
      string folderPath = @"D:\pdfsMultas";
      var files = Directory.GetFiles(folderPath).Where(x=>x.EndsWith(".pdf")).ToList();
      foreach (var file in files)
      {
       // GuardaImagen(file, $"D:/pdfsMultas/imagenes/{Path.GetFileName(file)}.png");
        await ProcesaPdf(folderPath, file);
      }
    
    }
  }
}
