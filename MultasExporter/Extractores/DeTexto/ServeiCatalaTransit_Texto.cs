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
  internal class ServeiCatalaTransit_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.ServeiCatalaTransit.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(420.795339, 677.157153, 522.052502, 690.475979);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechahora = new PdfRectangle(366.838235, 365.216229, 496.825804, 384.142981);
      var fecha = GetDatos(rec_fechahora).Replace("-", "/");
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }


    public decimal GetImporte()
    {//No sale en el pdf
      return 0;
    }

    public string GetLugarMulta()
    {
      var rec_lugar1 = new PdfRectangle(86.191219, 365.216229, 173.083179, 399.56478);
      var rec_lugar2 = new PdfRectangle(173.43355, 364.865733, 316.034468, 399.915275);
      var lugar1 = GetDatos(rec_lugar1);
      var lugar2 = GetDatos(rec_lugar2);
      return lugar1 + " " + lugar2;

    }

    public string GetMarcaVehiculo()
    {
      return "";
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(185.696528, 335.774613, 257.172172, 352.247898);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(83.388252, 301.426062, 295.712961, 334.372632);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(74.979353, 710.804713, 240.354374, 761.276054);
      var match = GetDatos(rec_match);
      return match.Contains("//transit.gencat.cat");
    }
  }
}
