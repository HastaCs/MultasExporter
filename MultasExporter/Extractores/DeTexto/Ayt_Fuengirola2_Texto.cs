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
  internal class Ayt_Fuengirola2_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(88.643814, 760.224568, 164.323909, 771.790916);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(489.818388, 770.73943, 565.498482, 783.707761);
      var fecha = GetDatos(rec_fecha);
      if(DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(472.299847, 733.23642, 566.549594, 747.606732);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if(decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(101.957905, 718.866108, 288.004803, 734.638402);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(226.689912, 746.204751, 436.562025, 760.925558);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(84.789736, 748.658219, 152.06093, 759.874072);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(226.689912, 746.204751, 436.562025, 760.925558);
      return string.Join(" ",GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(109.666063, 708.701741, 526.957693, 720.26809);
      return GetDatos(rec_motivo);
    }
    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match = new PdfRectangle(93.899377, 771.089926, 256.471431, 784.408752);
      var match = GetDatos(rec_match);
      var rec_match2= new PdfRectangle(31.883744, 746.905742, 99.505309, 759.874072);
      var match2 = GetDatos(rec_match2);
      return match == "Ayuntamiento de Fuengirola" && match2=="Matrícula";
    }
  }
}
