using MultasExporter.Extractores;
using MultasExporter.Extractores.DePix;
using MultasExporter.Extractores.DeTexto;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Tesseract;
using UglyToad.PdfPig;


namespace MultasExporter
{
 
  /// <summary>
  /// Clase principal del proyecto, que usa IMULTA para recuperar los datos
  /// </summary>
  public class ExtractorMulta
  {
    private IDatos _extractorMulta { get; set; }
    private List<IExtractorPix> _extractoresPdf = new List<IExtractorPix> { new DGT_Pix(), new DGTPuntos_Pix(), new Ayt_Alicante_Pix(), new Ayt_Valencia_Pix(), new Dip_Malaga_Pix(), new Dip_Cadiz_Pix(), new Suma_Alicante_Pix(), new Ayt_Granada_Pix(), new Ayt_Fuengirola_Pix(), new DGT2_Pix(), new Ayt_Ibiza_Pix(), new Ayt_Cadiz_Pix(), new Ayt_Murcia_Pix(),new Ayt_Cartagena_Pix() };
    private List<IExtractorTexto> _extractoresText = new List<IExtractorTexto> { new Dip_Malaga_Texto(), new DGT_Texto(), new DGTPuntos_Texto(), new Suma_Alicante_Texto(), new Ayt_Valencia_Texto(), new Ayt_Alicante_Texto(), new Ayt_Granada_Texto(), new Dip_Cadiz_Texto(), new Ayt_Madrid_Texto(), new Ayt_Fuengirola_Texto(), new DGT2_Texto(), new Ayt_Ibiza_Texto(), new Ayt_Cadiz_Texto(), new Ayt_Murcia_Texto(), new Ayt_Sevilla_Texto(), new Ayt_Sevilla2_Texto(),new Ayt_Cordoba_Texto(),new Ayt_Malaga_Texto() ,new Atib_Texto(), new Atib2_Texto(),new PortsBalears_Texto(),new ServeiCatalaTransit_Texto(),new Ayt_Granada2_Texto(),new Ayt_Sevilla3_Texto(),new Ayt_Fuengirola2_Texto(),new Dip_Sevilla_Texto(),new Ayt_Cordoba2_Texto(),new Ayt_Almeria_Texto(),new Ayt_Cadiz2_Texto()};
    public ExtractorMulta(byte[] multaFichero)
    {

      var extractor = DeterminarExtractor(multaFichero);
      if (extractor == null)
        throw new Exception("No se ha podido detectar el formato");
      Console.WriteLine("Encontrado: " + extractor.GetType());
      _extractorMulta = extractor;
    }
    /// <summary>
    /// Para la busqueda de los extractore PIX , que son mas lentos
    /// </summary>
    /// <param name="extractores"></param>
    /// <param name="pix"></param>
    /// <returns></returns>
    private async Task<IExtractorPix> EjecutarConLimite(IEnumerable<IExtractorPix> extractores, Pix pix)
    {
      int maxConcurrentTasks = 2; // Número máximo de tareas concurrentes
      var semaphore = new SemaphoreSlim(maxConcurrentTasks); // Controlador de concurrencia
      var cancellationTokenSource = new CancellationTokenSource();
      var cancellationToken = cancellationTokenSource.Token;

      var tasks = new List<Task<(IExtractorPix Extractor, bool Resultado)>>();

      var i = 0;
      foreach (var extractor in extractores)
      {


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
      //Miramos las dos primeras paginas. 
      var nPaginas = 0;
      using (var pdfDocument = PdfDocument.Open(multaFichero))
      {
        nPaginas = pdfDocument.NumberOfPages;
        for (int i = 1; i <= nPaginas && i <= 2; i++)
        {
          var page = pdfDocument.GetPage(i);
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

      }
      //Tamaño en bytes de multaFichero
      Console.WriteLine("No se ha encontrado ningun extractor de texto, buscando por OCR");
      Console.WriteLine("Tamaño del PDF: " + multaFichero.Length + " bytes");




      //Si no encuentra ninguno de texto, transforma en imagen y busca
      byte[] pngByte = Freeware.Pdf2Png.Convert(multaFichero, 1);

      //Tamaño en bytes de la imagen
      Console.WriteLine("Tamaño de la imagen: " + pngByte.Length + " bytes");

      var pix = Pix.LoadFromMemory(pngByte);

      //Si no encuentra datos en la pagina 1, busca en la pagina 2
      var pagina1 = EjecutarConLimite(_extractoresPdf.Where(x => x.PaginaDatos() == 1), pix).Result;
      if (pagina1 == null && nPaginas > 1)
      {
        pngByte = Freeware.Pdf2Png.Convert(multaFichero, 2);
        pix = Pix.LoadFromMemory(pngByte);
        return EjecutarConLimite(_extractoresPdf.Where(x => x.PaginaDatos() == 2), pix).Result;
      }
      else
        return pagina1;


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
      var expedienteTask = Task.Run(() => GetExpediente());
      tasks.Add(expedienteTask);
      // Crear tareas para cada método
      var matriculaTask = Task.Run(() => GetMatricula());
      tasks.Add(matriculaTask);

      var fechaMultaTask = Task.Run(() => GetFechaMulta());
      tasks.Add(fechaMultaTask);



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
