using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultasExporter.Modelos
{
  public class EmisorDTO
  {
    public string Nombre { get; set; }
    public Emisor Id { get; set; }
  }
  public class Vehiculo
  {
    public string Matricula { get; set; }
    public string Marca { get; set; }
    public string Clase { get; set; }
    public string Modelo { get; set; }
  }
  public class MultaDTO
  {
  public required Vehiculo Vehiculo { get; set; }
    public required DateTime FechaMulta { get; set; }
    public string ExpedienteMulta { get; set; }
    public required EmisorDTO Emisor { get; set; }
    public string MotivoMulta { get; set; }
    public string LugarMulta { get; set; }
    public required decimal ImportMulta { get; set; }

  }
}
