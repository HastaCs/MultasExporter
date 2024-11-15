using MultasExporter.Extractores;
using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultasExporter
{
  public enum Emisor
  {
    Dgt = 1, AytoAlicante
  }
  /// <summary>
  /// Clase principal del proyecto, que usa IMULTA para recuperar los datos
  /// </summary>
  public class ExtractorMulta
  {
    private IExtractor _extractorMulta { get; set; }
    private List<IExtractor> _extractores = new List<IExtractor> { new DGT(), new Ayt_Alicante() };

    public ExtractorMulta(byte[] multaFichero)
    {
      var extractor= DeterminarExtractor(multaFichero);
      if (extractor == null)
        throw new Exception("No se ha encontrado ningun extractor");
      _extractorMulta = extractor;
    }


    /// <summary>
    /// Método para determinar el extractor adecuado según el contenido del PDF
    /// </summary>
    private IExtractor? DeterminarExtractor(byte[] multaFichero)
    {
      byte[] pngByte = Freeware.Pdf2Png.Convert(multaFichero, 1);
      foreach (var extractor in _extractores)
      {
        extractor.SetFichero(pngByte);
        if (extractor.Match())
        {
          return extractor;
        }

      }
      return null;
    }


    private string GetMatricula() => _extractorMulta.GetMatricula().Trim();
    private DateTime GetFechaMulta() => _extractorMulta.GetFechaMulta();
    private string GetExpediente() => _extractorMulta.GetExpediente().Trim();
    private EmisorDTO GetEmisor() => _extractorMulta.GetEmisor();
    private string GetMotivoMulta() => _extractorMulta.GetMotivoMulta();
    private string GetLugarMulta() => _extractorMulta.GetLugarMulta();
    private string GetMarcaVehiculo() => _extractorMulta.GetMarcaVehiculo();
    private string GetClaseVehiculo() => _extractorMulta.GetClaseVehiculo();
    private string GetModeloVehiculo() => _extractorMulta.GetModeloVehiculo();
    private decimal GetImporte() => _extractorMulta.GetImporte();
    public MultaDTO GetDatos()
    {
      return new MultaDTO
      {
        Vehiculo = new Vehiculo
        {
          Matricula = GetMatricula(),
          Marca = GetMarcaVehiculo(),
          Clase = GetClaseVehiculo(),
          Modelo = GetModeloVehiculo()
        },
        FechaMulta = GetFechaMulta(),
        ExpedienteMulta = GetExpediente(),
        Emisor = GetEmisor(),
        MotivoMulta = GetMotivoMulta(),
        LugarMulta = GetLugarMulta(),
        ImportMulta = GetImporte()
      };
    }
  }
}
