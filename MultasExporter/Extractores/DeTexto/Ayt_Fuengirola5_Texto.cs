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
  internal class Ayt_Fuengirola5_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(229.184676, 766.888664, 315.45714, 782.263938);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(228.059383, 742.888237, 306.079698, 762.388584);
      var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(471.872868, 716.262762, 521.010662, 731.638036);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(110.653813, 703.137528, 278.322471, 720.012829);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(226.183895, 728.637983, 383.724915, 745.513283);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(84.772074, 729.012989, 163.542584, 743.63825);
      return GetDatos(rec_matricula);
    }
    
    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(226.183895, 728.637983, 383.724915, 745.513283);
      return string.Join(" ", GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(126.407915, 680.262121, 560.395917, 696.762415);
      return GetDatos(rec_motivo);
    }
    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match1 = new PdfRectangle(472.247965, 760.138544, 560.771015, 784.138972);
      var rec_match2 = new PdfRectangle(88.52305, 803.639319, 387.475892, 819.764606);
      var rec_match3 = new PdfRectangle(28.507423, 655.136673, 262.193271, 672.011974);
      var match = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      var match3 = GetDatos(rec_match3);
      return match == "Ayuntamiento de Fuengirola" && match2 == "REQUERIMIENTO DE IDENTIFICACIÓN DE INFRACTOR"
               && match3== "ESPACIO RESERVADO PARA LA VALIDACIÓN MECÁNICA";
    }
  }
}
