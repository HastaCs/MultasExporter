using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Fuengirola_Texto : ExtractorBaseTexto, IExtractorTexto
  {

    public int PaginaDatos() { return 2; }
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
      var rec_expediente = new PdfRectangle(93.165159, 752.572205, 163.998594, 774.552756);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(488.855381, 770.017087, 565.05, 784.56);
      var fecha = GetDato(rec_fecha);
      return Convert.ToDateTime(fecha);
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(471.408723, 724.311497, 562.131349, 739.314095);
      var importe = GetDato(rec_importe).Replace("€", "");
      return Convert.ToDecimal(importe);

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(101.539555, 709.657796, 287.172005, 726.404882);
      return GetDato(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(224.364033, 737.220709, 436.515405, 751.87441);
      return GetDato(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(92.118359, 737.220709, 159.462462, 753.072205);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(224.364033, 737.220709, 436.515405, 751.87441);
      return string.Join(" ",GetDato(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(109.565018, 698.841969, 565.05, 712.797875);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(93.514092, 770.365985, 274.610411, 784.56);
      var match = GetDato(rec_match);
      return match == "Ayuntamiento de Fuengirola";
    }
  }
}
