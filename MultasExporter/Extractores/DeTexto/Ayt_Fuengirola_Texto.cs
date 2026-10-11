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
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(488.855381, 770.017087, 565.05, 784.56);
            var fecha = GetDatos(rec_fecha);
      if(DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(471.408723, 724.311497, 562.131349, 739.314095);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if(decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(101.539555, 709.657796, 287.172005, 726.404882);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(224.364033, 737.220709, 436.515405, 751.87441);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(92.118359, 737.220709, 159.462462, 753.072205);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(224.364033, 737.220709, 436.515405, 751.87441);
      return string.Join(" ",GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(109.565018, 698.841969, 565.05, 712.797875);
      return GetDatos(rec_motivo);
    }
    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match = new PdfRectangle(93.514092, 770.365985, 274.610411, 784.56);
      var match = GetDatos(rec_match);
      var rec_match2=new PdfRectangle(32.2584, 739.13817, 93.774418, 750.013364);
      var match2 = GetDatos(rec_match2);
      return match == "Ayuntamiento de Fuengirola" && match2=="Matrícula";
    }
  }
}
