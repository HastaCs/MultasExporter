using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
  internal class DGT2_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new Rect(792, 906, 318, 64);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Dgt.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_emisor = new Rect(1938, 105, 342, 59);
      return GetDato(rec_emisor);
    }

    public DateTime GetFechaMulta()
    {
      var rec_recha = new Rect(1338, 108, 279, 59);
      var rec_hora = new Rect(1694, 104, 219, 61);
      var fecha = GetDato(rec_recha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new Rect(1977, 293, 300, 51);
      var importe = GetDato(rec_importe).Replace("EUR", "").Replace(".", ",").Trim();
      return Convert.ToDecimal(importe);
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new Rect(618, 356, 1317, 97);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new Rect(789, 954, 318, 60);
      return GetDato(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new Rect(789,860,291,57);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new Rect(791, 1005, 318, 60);
      return GetDato(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new Rect(621,498,1653,251);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new Rect(780, 89, 414, 112);
      var match = GetDato(rec_match);
      return match== "Centro de Tratamiento de\nDenuncias Automatizadas";
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
