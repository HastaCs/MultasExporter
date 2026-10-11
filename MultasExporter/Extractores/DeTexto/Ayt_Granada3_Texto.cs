using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
//Sin texto
namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Granada3_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoGranada.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(228.511536, 764.635376,317.222351, 788.129639);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha =  new PdfRectangle(230.011597, 753.651489,305.265381, 767.899658);
      var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(472.023804, 730.154602,513.525879, 743.402893);
      var importe = GetDatos(rec_importe).Replace(".", ",").Replace("€", "").Trim();
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(76.380323, 716.41264, 333.901135, 734.638402);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(229.011536, 743.902832,381.019226, 757.151062);
      var marca = GetDatos(rec_marcamodelo).Split(" ")[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(85.504303, 742.902954,166.508392, 755.401306);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(229.011536, 743.902832,381.019226, 757.151062);
      return string.Join(" ", GetDatos(rec_marcamodelo).Split(" ").Skip(1));

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(128.50647, 666.412964,555.278015, 695.40918);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1=new PdfRectangle(471.497803, 765.661499,557.864014, 786.757996);
      var rec_match2=new PdfRectangle(88.616821, 807.85437,198.895569, 824.450256);
      var rec_match3=new PdfRectangle(401.448303, 811.792358,560.677246, 824.168945);
      
      var match1=GetDatos(rec_match1).ToUpper().Contains("AYUNTAMIENTO DE GRANADA");
      var match2=GetDatos(rec_match2).ToUpper().Contains("NOTIFICACIÓN INCOACIÓN");
      var match3=GetDatos(rec_match3).ToUpper().Contains("EJEMPLAR PARA EL CONTRIBUYENTE");
        
      return match1 && match2 && match3 ;
      
    }
  }
}
