using MultasExporter;
using System.ComponentModel;
using System.Diagnostics;
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
      string[] files = Directory.GetFiles(folderPath);

      var tasks=new List<Task>();

      // Recorre y muestra el path de cada archivo
      foreach (var file in files)
      {
        tasks.Add(ProcesaPdf(folderPath, file));

       
      }
      Task.WaitAll(tasks.ToArray());

    
    
      #region a
      /*
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      var dd = File.ReadAllBytes("pdf/DGT.pdf");
      byte[] pngByte = Freeware.Pdf2Png.Convert(dd, 1);


      // Crear una instancia del motor de OCR con el idioma "eng" (inglés)
      using (var ocrEngine = new TesseractEngine("", "spa", EngineMode.Default))
      {
        var image = Pix.LoadFromMemory(pngByte);
        var rect = new Rect(500, 544, 358, 62);
        // Procesar la imagen

        using (var page = ocrEngine.Process(image, rect))
        {
          // Obtener el texto extraído
          var identificador = page.GetText().Replace("\n", "");
          Console.WriteLine(page.GetText().Trim());
        }
        rect = new Rect(794, 762, 194, 42);
        using (var page = ocrEngine.Process(image, rect))
        {
          // Obtener el texto extraído
          var matricula = page.GetText().Replace("\n", "");
          Console.WriteLine(page.GetText().Trim());
        }
        rect = new Rect(162, 1788, 2079, 174);
        using (var page = ocrEngine.Process(image, rect))
        {
          // Obtener el texto extraído
          var motivo = page.GetText().Replace("\n", "");
          Console.WriteLine(page.GetText().Trim());
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " segundos");
      }*/
      #endregion
    }
  }
}
