using MultasExporter.Extractores;
using MultasExporter.Extractores.DeTexto;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;

using UglyToad.PdfPig;


namespace MultasExporter
{

  /// <summary>
  /// Clase principal del proyecto, que usa IMULTA para recuperar los datos
  /// </summary>
  public class ExtractorMulta
  {
    private IDatos _extractorMulta { get; set; }
    private List<IExtractorTexto> _extractoresText = new List<IExtractorTexto> { new Ayt_SantaEularia2(), new Dip_Malaga_Texto(), new DGT_Texto(), new DGTPuntos_Texto(), new Suma_Alicante_Texto(), new Ayt_Valencia_Texto(), new Ayt_Alicante_Texto(), new Ayt_Granada_Texto(), new Dip_Cadiz_Texto(), new Ayt_Madrid_Texto(), new Ayt_Fuengirola_Texto(), new DGT2_Texto(), new Ayt_Ibiza_Texto(), new Ayt_Cadiz_Texto(), new Ayt_Murcia_Texto(), new Ayt_Sevilla_Texto(), new Ayt_Sevilla2_Texto(), new Ayt_Cordoba_Texto(), new Ayt_Malaga_Texto(), new Atib_Texto(), new Atib2_Texto(), new PortsBalears_Texto(), new ServeiCatalaTransit_Texto(), new Ayt_Granada2_Texto(), new Ayt_Sevilla3_Texto(), new Ayt_Fuengirola2_Texto(), new Dip_Sevilla_Texto(), new Ayt_Cordoba2_Texto(), new Ayt_Almeria_Texto(), new Ayt_Cadiz2_Texto(),new Dip_Sevilla2_Texto(),new Ayt_Fuengirola3_Texto(),new Dip_Malaga2_Texto(),new Ayt_Fuengirola4_Texto(),new Ayt_Fuengirola5_Texto(),new Dip_Malaga3_Texto(),new Ayt_SantaEularia(),new PortsBalears2_Texto(),new Ayt_SanJosepSaTalaia(),new PortsBalears3_Texto(),new Ayt_Benalmadena(),new Ayt_Fuengirola6_Texto(),new Ayt_Granada3_Texto() };
    public ExtractorMulta(byte[] multaFichero)
    {

      var extractor = DeterminarExtractor(multaFichero);
      if (extractor == null)
        throw new Exception("No se ha podido detectar el formato");
      Console.WriteLine("Encontrado: " + extractor.GetType());
      _extractorMulta = extractor;
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
      return null;
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
