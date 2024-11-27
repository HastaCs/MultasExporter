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
  internal class Ayt_Murcia_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new Rect(444, 1661, 375, 48);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoMurcia.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new Rect(98,1662,323,43);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new Rect(1461,1662,290,45);
      var fecha = GetDato(rec_fecha);
      var rec_hora = new Rect(1763,1666,165,42);
      var hora = GetDato(rec_hora).Replace(".", ":");
      return Convert.ToDateTime(fecha+" "+hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new Rect(2122, 1665, 240, 46);
      return Convert.ToDecimal(GetDato( rec_importe));
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new Rect(1216, 1742, 1147, 57);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      return "";
    }

    public string GetMatricula()
    {
      var rec_matricula = new Rect(822, 1664, 272, 46);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new Rect(400, 1904, 2038, 160);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new Rect(248, 188, 646, 64);
      var match = GetDato(rec_match);
      return match.Contains("AYUNTAMIENTO DE MURCIA");
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
