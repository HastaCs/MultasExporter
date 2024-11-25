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
  internal class Ayt_Fuengirola_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoFuengirola.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_exp = new Rect(395, 290, 288, 90);
      return GetDato(rec_exp).Replace("\n", "");
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new Rect(2054, 240, 300, 54);
      return Convert.ToDateTime(GetDato(rec_fecha));
    }

    public decimal GetImporte()
    {
      var rec_importe = new Rect(1962, 426, 357, 63);
      var importe = GetDato(rec_importe).Replace("E", "");
      return Convert.ToDecimal(importe);
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new Rect(422, 480, 771, 72);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new Rect(942, 377, 872, 55);
      return GetDato(rec_marca).Split("-")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new Rect(401, 375, 274, 59);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new Rect(942, 377, 872, 55);
      return string.Join(" ",GetDato(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new Rect(458, 545, 1827, 58);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new Rect(398, 244, 459, 50);
      var match = GetDato(rec_match);
      return match == "Ayuntamiento de Fuengirola";
    }

    public int PaginaDatos()
    {
      return 2;
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
