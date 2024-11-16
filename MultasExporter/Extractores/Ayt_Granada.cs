using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{
  internal class Ayt_Granada : ExtractorBase, IExtractor
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor() =>
      new EmisorDTO
      {
        Id = Emisor.AytoGranada,
        Nombre = "Ayuntamiento de Granada"
      };


    public string GetExpediente()
    {
      var rec = new Rect(1824, 110, 351, 37);
      return GetDato(rec);
    }

    public DateTime GetFechaMulta()
    {
      var recFecha = new Rect(325, 228, 324, 33);
      var recHora = new Rect(659, 228, 110, 30);
      return Convert.ToDateTime(GetDato(recFecha) + " " + GetDato(recHora));

    }

    public decimal GetImporte()
    {
      var rec = new Rect(1563, 224, 132, 42);
      var importe = Convert.ToDecimal(GetDato(rec));
      return importe;
    }

    public string GetLugarMulta()
    {
      var rec = new Rect(324, 455, 1048, 65);
      return GetDato(rec);
    }

    public string GetMarcaVehiculo()
    {
      var rec = new Rect(617, 788, 462, 43);
      return GetDato(rec);
    }

    public string GetMatricula()
    {
      var rec = new Rect(320, 789, 284, 31);
      return GetDato(rec);
    }

    public string GetModeloVehiculo()
    {
      //En estas multas vienen juntos la marca y el modelo
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec = new Rect(318, 337, 1215, 61);
      return GetDato(rec);
    }

    public bool Match()
    {
      var rec = new Rect(320, 71, 591, 39);
      var texto = GetDato(rec);

      return texto == "EXCMO. AYUNTAMIENTODE GRANADA";
    }

    void IExtractor.SetFichero(byte[] imagenBytes)
    {
      base.SetFichero(imagenBytes);
    }
  }
}
