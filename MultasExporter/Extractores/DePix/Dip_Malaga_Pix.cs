using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
  internal class Dip_Malaga_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      var rec = new Rect(1101, 789, 416, 54);
      return GetDato(rec);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipMalaga.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec = new Rect(1697, 105, 645, 69);
      return GetDato(rec).Replace(" ", "");
    }

    public DateTime GetFechaMulta()
    {
      var fechaR = new Rect(1749, 522, 293, 66);
      var horaR = new Rect(2049, 515, 305, 72);
      return Convert.ToDateTime(GetDato(fechaR) + " " + GetDato(horaR));
    }

    public decimal GetImporte()
    {
      var rec = new Rect(99, 900, 473, 59);
      return Convert.ToDecimal(GetDato(rec).Split(" ")[0]);
    }

    public string GetLugarMulta()
    {
      var rec = new Rect(98, 510, 1630, 83);
      return GetDato(rec);
    }

    public string GetMarcaVehiculo()
    {
      var rec = new Rect(1526, 785, 568, 61);
      return GetDato(rec).Split(" ")[0];
    }

    public string GetMatricula()
    {
      //TODO Revisar este extractor, la matricula no va fina
      var rec = new Rect(860, 791, 78, 49);
      var numero = GetDato(rec);
      rec = new Rect(937, 795, 67, 45);
      var letras = GetDato(rec);
      return numero + letras;
    }

    public string GetModeloVehiculo()
    {
      var rec = new Rect(1526, 785, 568, 61);

      return string.Join(" ", GetDato(rec).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec = new Rect(99, 387, 2258, 77);
      return GetDato(rec);

    }

    public bool Match()
    {
     
        var rec = new Rect(1656, 40, 722, 75);
        var match = GetDato(rec);
        return match == "DIPUTACIÓN PROVINCIAL DE MÁLAGA";
    
    }

    public void SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
