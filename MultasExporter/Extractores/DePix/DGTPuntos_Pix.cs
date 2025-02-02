using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
  internal class DGTPuntos_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      return GetDato(new Rect(320, 696, 682, 78));
    }

    public EmisorDTO GetEmisor() => Emisor.Dgt.ToEmisorDto();

    public string GetExpediente()
    {
      return GetDato(new Rect(492, 520, 398, 86));
    }

    public DateTime GetFechaMulta()
    {
      var fechaString = GetDato(new Rect(1890, 546, 346, 64));
      var fecha = DateTime.ParseExact(fechaString, "dd/MM/yyyy - HH:mm'h'", CultureInfo.InvariantCulture);
      return fecha;
    }

    public decimal GetImporte()
    {
      return 0;
    }

    public string GetLugarMulta()
    {
      return GetDato(new Rect(148, 1040, 988, 160));
    }

    public string GetMarcaVehiculo()
    {
      return GetDato(new Rect(334, 776, 654, 62));
    }

    public string GetMatricula()
    {
      return GetDato(new Rect(428, 624, 560, 70));
    }

    public string GetModeloVehiculo()
    {
      return GetDato(new Rect(385, 858, 608, 56));
    }

    public string GetMotivoMulta()
    {
      return GetDato(new Rect(154, 1786, 2088, 178));
    }

    public bool Match()
    {
      var centroDenuncias = GetDato(new Rect(200, 374, 836, 76));
      var puntos = GetDato(new Rect(156, 1552, 702, 68));
      return centroDenuncias == "Centro de Tratamiento de Denuncias Automatizadas" && puntos.Contains("Esta infracción lleva aparejada la pérdida");

    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
  }
}
