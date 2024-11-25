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
  internal class Dip_Cadiz_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      var rec = new Rect(377, 1328, 585, 103);
      return GetDato(rec);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipCadiz.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec = new Rect(1833, 417, 422, 51);
      return GetDato(rec);
    }

    public DateTime GetFechaMulta()
    {
      var fechaR = new Rect(428, 479, 205, 63);
      var horaR = new Rect(806, 480, 145, 65);
      return Convert.ToDateTime(GetDato(fechaR) + " " + GetDato(horaR));
    }

    public decimal GetImporte()
    {
      var rec = new Rect(1869, 1140, 204, 53);
      return Convert.ToDecimal(GetDato(rec).Replace("E", ""));
    }

    public string GetLugarMulta()
    {
      var rec = new Rect(429, 636, 1862, 77);
      return GetDato(rec);
    }

    public string GetMarcaVehiculo()
    {
      var rec = new Rect(387, 1449, 596, 116);
      var marca = GetDato(rec);
      return marca.Split("-")[0];
    }

    public string GetMatricula()
    {
      var rec = new Rect(383, 1262, 363, 67);
      return GetDato(rec);
    }

    public string GetModeloVehiculo()
    {
      var rec = new Rect(387, 1449, 596, 116);
      var marca = GetDato(rec);
      return string.Join(" ", marca.Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec = new Rect(180, 753, 2109, 198);

      return GetDato(rec);
    }

    public bool Match()
    {
      var rec = new Rect(422, 560, 598, 64);
      return GetDato(rec) == "DIPUTACIÓN PROVINCIAL DE CÁDIZ";
    }

    public void SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
