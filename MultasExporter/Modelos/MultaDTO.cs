using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultasExporter.Modelos
{
  public record EmisorDTO
  {
    public string Nombre { get; set; }
    public Emisor Id { get; set; }
  }
  public record Vehiculo
  {
    public string Matricula { get; set; }
    public string Marca { get; set; }
    public string Clase { get; set; }
    public string Modelo { get; set; }
  }
  public class MultaDTO
  {
    //Constructor que recibe una MultaDTO y rellena los datos
    public MultaDTO(MultaDTO multa)
    {
      Vehiculo = multa.Vehiculo;
      Fecha = multa.Fecha;
      Expediente = multa.Expediente;
      Emisor = multa.Emisor;
      Motivo = multa.Motivo;
      Lugar = multa.Lugar;
      Importe = multa.Importe;
    }
    public MultaDTO()
    {
    }
    public Vehiculo Vehiculo { get; set; }
    public DateTime Fecha { get; set; }
    public string Expediente { get; set; }
    public EmisorDTO Emisor { get; set; }
    public string Motivo { get; set; }
    public string Lugar { get; set; }
    public decimal Importe { get; set; }

  }
  public class MultaDTOZip : MultaDTO
  {
    // Constructor que recibe nombre fichero y una MultaDTO y rellena los datos
    public MultaDTOZip(string nombreFichero, MultaDTO multa) : base(multa)
    {
      NombreFichero = nombreFichero;
    }

    public string NombreFichero { get; set; }
  }
}
