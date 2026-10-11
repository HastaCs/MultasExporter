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
  internal class Ayt_Fuengirola6_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public int PaginaDatos()
    {
      return 2;
    }

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
      var rec_expediente = new PdfRectangle(229.934875, 764.638672, 302.703796, 781.138977);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha=new PdfRectangle(229.559753, 744.388306,304.954407, 759.38855);  var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe=new PdfRectangle(472.247986, 709.512634,508.632477, 723.012878);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar=new PdfRectangle(109.903625, 696.01239,273.82132, 708.387634);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca=new PdfRectangle(231.810364, 721.887878,381.099243, 742.138245);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula=new PdfRectangle(84.959625, 722.062012,147.132065, 740.626953);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca=new PdfRectangle(231.810364, 721.887878,381.099243, 742.138245);

      return string.Join(" ", GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo=new PdfRectangle(129.03363, 687.012207,464.746033, 697.51239);
      return GetDatos(rec_motivo);
    }

    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match1 = new PdfRectangle(471.872925, 759.38855, 552.518921, 784.513977);
      var rec_match2 = new PdfRectangle(277.947388, 697.137451, 358.218292, 708.762634);
      var rec_match3 = new PdfRectangle(39.385254, 673.511963, 128.658508, 684.387146);
      var rec_match4 = new PdfRectangle(165.042984, 724.887939, 230.685074, 738.013184);

      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      var match3 = GetDatos(rec_match3);
      var match4 = GetDatos(rec_match4);
      return match1 == "Ayuntamiento de Fuengirola" && match2 == "Precepto Infringido"
                                                    && match3 == "Motivo no Notificación" && match4 == "Marca y modelo";
    }
  }
}