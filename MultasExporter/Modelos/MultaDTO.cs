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
  public record MultaDTO
  {
  public required Vehiculo Vehiculo { get; set; }
    public required DateTime Fecha { get; set; }
    public string Expediente { get; set; }
    public required EmisorDTO Emisor { get; set; }
    public string Motivo { get; set; }
    public string Lugar { get; set; }
    public required decimal Importe { get; set; }

  }
}
