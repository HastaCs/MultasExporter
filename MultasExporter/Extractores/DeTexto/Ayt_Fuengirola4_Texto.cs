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
  internal class Ayt_Fuengirola4_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(228.059383, 765.013631, 316.207335, 781.888931);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(228.809578, 744.76327, 308.330284, 760.138544);
      var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(472.623063, 708.012615, 516.50949, 724.137902);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;

      return 0;

      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(108.403227, 695.637395, 281.69835, 709.137635);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(228.434481, 719.637822, 381.849427, 744.388263);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(84.772074, 724.137902, 164.292779, 738.388156);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(228.434481, 719.637822, 381.849427, 744.388263);
      return string.Join(" ", GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(126.783012, 685.512214, 559.270624, 699.012455);
      return GetDatos(rec_motivo);
    }
    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match1 = new PdfRectangle(472.247965, 760.138544, 560.771015, 784.138972);
      var rec_match2 = new PdfRectangle(88.52305, 803.639319, 387.475892, 819.764606);
      var rec_match3= new PdfRectangle(41.484669, 659.413843, 117.456592, 673.911948);
      var match = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      var match3 = GetDatos(rec_match3);
      return match == "Ayuntamiento de Fuengirola" && match2 == "REQUERIMIENTO DE IDENTIFICACIÓN DE INFRACTOR"
              && match3 == "Velocidad";
    }
  }
}
