using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter
{
  public interface IDatos
  {
    /// <summary>
    /// El pdf que he subido corresponde a esta implementacion
    /// Para que a través del pdf subido sea capaz de detectar a que implementacio corresponde
    /// </summary>
    /// <returns></returns>
    /// TODO Por performance tal vez seria mejor pasarle una Image directamente.
    public bool Match();
  
    /// <summary>
    /// Recupera la matricula f
    /// </summary>
    /// <returns></returns>
    public string GetMatricula();
    public DateTime GetFechaMulta();
    /// <summary>
    /// El identificador de la multa
    /// </summary>
    /// <returns></returns>
    public string GetExpediente();
    public EmisorDTO GetEmisor();
    public string GetMotivoMulta();
    public string GetLugarMulta();
    public string GetMarcaVehiculo();
    public string GetClaseVehiculo();
    public string GetModeloVehiculo();
    public decimal GetImporte();
   

  }

}
