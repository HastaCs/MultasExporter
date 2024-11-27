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
  internal class Ayt_Cadiz_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoCadiz.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_exp = new Rect(498, 1626, 344, 54);
      return GetDato(rec_exp);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new Rect(1920, 1688, 274, 50);
      return Convert.ToDateTime(GetDato(rec_fecha));
    }

    public decimal GetImporte()
    {
      var rec_importe = new Rect(2126, 1220, 148, 48);
      var importe = GetDato(rec_importe).Replace("€", "").Trim();
      return Convert.ToDecimal(importe);
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new Rect(254, 1732, 944, 50);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new Rect(1312, 1686, 446, 46);
      return GetDato(rec_marca).Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new Rect(986, 1688, 231, 48);
      return GetDato(rec_matricula).Replace(" ", "");
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new Rect(1312, 1686, 446, 46);
      return string.Join(" ", GetDato(rec_marca).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new Rect(156, 1839, 1256, 170);
      return GetDato(rec_motivo).Replace("Hecho denunciado:", "");
    }

    public bool Match()
    {
      var rec_match = new Rect(378,140,744,56);
      var match = GetDato(rec_match);
      return match == "EXCMO. AYUNTAMIENTO DE CÁDIZ";
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
