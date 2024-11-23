using MultasExporter.Extractores;
using MultasExporter.Extractores.DePix;
using MultasExporter.Extractores.DeTexto;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;

using System.ComponentModel.DataAnnotations;

using Tesseract;
using UglyToad.PdfPig;


namespace MultasExporter
{
  public enum Emisor
  {

    [Display(Name = "(DGT)CENTRO DE TRATAMIENTO DE DENUNCIAS AUTOMATIZADAS")]
    Dgt = 1,

    [Display(Name = "Ayuntamiento de Alicante")]
    AytoAlicante,

    [Display(Name = "Ayuntamiento de Granada")]
    AytoGranada,

    [Display(Name = "Ayuntamiento de Valencia")]
    AytoValencia,

    [Display(Name = "Diputación provincial de Málaga")]
    DipMalaga,

    [Display(Name = "Diputación provincial de Cádiz")]
    DipCadiz,
    [Display(Name = "Suma Diputación de Alicante")]
    SumaAlicante
  }
  /// <summary>
  /// Clase principal del proyecto, que usa IMULTA para recuperar los datos
  /// </summary>
  public class ExtractorMulta
  {
    private IDatos _extractorMulta { get; set; }
    private List<IExtractorPdf> _extractoresPdf = new List<IExtractorPdf> { new DGT_Pix(), new Ayt_Alicante_Pix(), new Ayt_Valencia_Pix(), new Dip_Malaga_Pix(), new Dip_Cadiz_Pix(), new Suma_Alicante_Pix(), new Ayt_Granada_Pix() };
    private List<IExtractorTexto> _extractoresText = new List<IExtractorTexto> { new Dip_Malaga_Texto(), new DGT_Texto(), new Suma_Alicante_Texto(), new Ayt_Valencia_Texto(),new Ayt_Alicante_Texto() ,new Ayt_Granada_Texto(),new Dip_Cadiz_Texto(),};
    public ExtractorMulta(byte[] multaFichero)
    {

      var extractor = DeterminarExtractor(multaFichero);
      if (extractor == null)
        throw new Exception("No se ha encontrado ningun extractor");
      _extractorMulta = extractor;
    }
    /// <summary>
    /// Para la busqueda de los extractore PIX , que son mas lentos
    /// </summary>
    /// <param name="extractores"></param>
    /// <param name="pix"></param>
    /// <returns></returns>
    private async Task<IExtractorPdf> EjecutarConLimite(IEnumerable<IExtractorPdf> extractores, Pix pix)
    {
      int maxConcurrentTasks = 2; // Número máximo de tareas concurrentes
      var semaphore = new SemaphoreSlim(maxConcurrentTasks); // Controlador de concurrencia
      var cancellationTokenSource = new CancellationTokenSource();
      var cancellationToken = cancellationTokenSource.Token;

      var tasks = new List<Task<(IExtractorPdf Extractor, bool Resultado)>>();

      var i = 0;
      foreach (var extractor in extractores)
      {
        Console.WriteLine("__Extractor: " + i++);

        var task = Task.Run(async () =>
        {
          await semaphore.WaitAsync(); // Esperar espacio en el semáforo

          // Verificar si la tarea ha sido cancelada antes de procesar
          if (cancellationToken.IsCancellationRequested)
          {
            semaphore.Release();
            return (Extractor: null, Resultado: false); // Tarea no hace nada si ha sido cancelada
          }

          try
          {
            extractor.SetPix(pix);
            var resultado = extractor.Match();

            if (resultado)
            {
              // Si encontramos un extractor válido, cancelar el resto de las tareas
              cancellationTokenSource.Cancel();
            }

            return (Extractor: extractor, Resultado: resultado);
          }
          finally
          {
            semaphore.Release(); // Liberar semáforo
          }
        }, cancellationToken);

        tasks.Add(task);
      }

      try
      {
        // Procesar las tareas y detener cuando se encuentra un extractor válido
        while (tasks.Count > 0)
        {
          var completedTask = await Task.WhenAny(tasks); // Espera sin bloquear el hilo
          tasks.Remove(completedTask);

          var result = await completedTask;
          if (result.Resultado)
          {
            // Si encontramos un extractor válido, devolvemos inmediatamente
            Console.WriteLine("Extractor encontrado: " + result.Extractor.GetType());
            return result.Extractor;
          }
        }
      }
      catch (OperationCanceledException)
      {
        // Las tareas fueron canceladas
        Console.WriteLine("Tareas canceladas después de encontrar el extractor.");
      }

      return null; // Si no se encuentra ningún extractor válido
    }

    /// <summary>
    /// Método para determinar el extractor adecuado según el contenido del PDF
    /// </summary>
    private IDatos? DeterminarExtractor(byte[] multaFichero)
    {
      using (var pdfDocument = PdfDocument.Open(multaFichero))
      {
        var page = pdfDocument.GetPage(1);
        var words = page.GetWords();
        if (words.Any())
        {
          foreach (var extrator in _extractoresText)
          {
            extrator.SetPalabras(words);
            if (extrator.Match())
              return extrator;
          }
        }
      }
      //Si no encuentra ninguno de texto, transforma en imagen y busca
      byte[] pngByte = Freeware.Pdf2Png.Convert(multaFichero, 1);

      Console.WriteLine("convert fin");
      var pix = Pix.LoadFromMemory(pngByte);
      return EjecutarConLimite(_extractoresPdf, pix).Result;


    }


    private string GetMatricula() => _extractorMulta.GetMatricula().Trim().Replace("O", "0").Replace("I", "1");
    private DateTime GetFechaMulta() => _extractorMulta.GetFechaMulta();
    private string GetExpediente() => _extractorMulta.GetExpediente().Trim();
    private EmisorDTO GetEmisor() => _extractorMulta.GetEmisor();
    private string GetMotivoMulta() => _extractorMulta.GetMotivoMulta();
    private string GetLugarMulta() => _extractorMulta.GetLugarMulta();
    private string GetMarcaVehiculo() => _extractorMulta.GetMarcaVehiculo();
    private string GetClaseVehiculo() => _extractorMulta.GetClaseVehiculo();
    private string GetModeloVehiculo() => _extractorMulta.GetModeloVehiculo();
    private decimal GetImporte() => _extractorMulta.GetImporte();


    public async Task<MultaDTO> GetDatosAsync()
    {
      var tasks = new List<Task>();

      // Crear tareas para cada método
      var matriculaTask = Task.Run(() => GetMatricula());
      tasks.Add(matriculaTask);

      var fechaMultaTask = Task.Run(() => GetFechaMulta());
      tasks.Add(fechaMultaTask);

      var expedienteTask = Task.Run(() => GetExpediente());
      tasks.Add(expedienteTask);

      var emisorTask = Task.Run(() => GetEmisor());
      tasks.Add(emisorTask);

      var motivoMultaTask = Task.Run(() => GetMotivoMulta());
      tasks.Add(motivoMultaTask);

      var lugarMultaTask = Task.Run(() => GetLugarMulta());
      tasks.Add(lugarMultaTask);

      var marcaVehiculoTask = Task.Run(() => GetMarcaVehiculo());
      tasks.Add(marcaVehiculoTask);

      var claseVehiculoTask = Task.Run(() => GetClaseVehiculo());
      tasks.Add(claseVehiculoTask);

      var modeloVehiculoTask = Task.Run(() => GetModeloVehiculo());
      tasks.Add(modeloVehiculoTask);

      var importeTask = Task.Run(() => GetImporte());
      tasks.Add(importeTask);

      // Esperar a que todas las tareas terminen
      await Task.WhenAll(tasks);

      // Obtener los resultados
      var matricula = matriculaTask.Result;
      var fechaMulta = fechaMultaTask.Result;
      var expediente = expedienteTask.Result;
      var emisor = emisorTask.Result;
      var motivoMulta = motivoMultaTask.Result;
      var lugarMulta = lugarMultaTask.Result;
      var marcaVehiculo = marcaVehiculoTask.Result;
      var claseVehiculo = claseVehiculoTask.Result;
      var modeloVehiculo = modeloVehiculoTask.Result;
      var importe = importeTask.Result;




      return new MultaDTO
      {
        Vehiculo = new Vehiculo
        {
          Matricula = matricula,
          Marca = marcaVehiculo,
          Clase = claseVehiculo,
          Modelo = modeloVehiculo
        },
        Fecha = fechaMulta,
        Expediente = expediente,
        Emisor = emisor,
        Motivo = motivoMulta,
        Lugar = lugarMulta,
        Importe = importe
      };
    }
  }
}
