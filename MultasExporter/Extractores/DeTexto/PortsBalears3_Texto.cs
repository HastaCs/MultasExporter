using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto;

public class PortsBalears3_Texto : ExtractorBaseTexto, IExtractorTexto
{
  public bool Match()
  {
    var rec_match1 = new PdfRectangle(108.407593, 655.886536, 238.196625, 671.63678);
    var rec_match2 = new PdfRectangle(333.850372, 533.634399, 400.620453, 547.134583);
    var rec_match3 = new PdfRectangle(52.515785, 33.375587, 123.412102, 48.750854);
    var rec_match4 = new PdfRectangle(314.344513, 138.002426, 365.359833, 153.752701);
    var match1 = GetDatos(rec_match1);
    var match2 = GetDatos(rec_match2);
    var match3 = GetDatos(rec_match3);
    var match4 = GetDatos(rec_match4);

    return match1 == "Ref. Expedient/Expediente" && match2 == "Reduced amount" && match3 == "www.portsib.es" &&
           match4 == "Islas Baleares.";
  }

  public string GetMatricula()
  {
    var rec_matricula = new PdfRectangle(200.434814, 567.759949, 277.583435, 582.010193);
    var matricula = GetDatos(rec_matricula);
    return matricula;
  }

  public DateTime GetFechaMulta()
  {
    var rec_fechayhora = new PdfRectangle(103.046005, 533.77832, 235.758377, 548.410095);

    var fechaYHora = GetDatos(rec_fechayhora).Replace("Date/Fecha:","");

    if (DateTime.TryParse(fechaYHora, out DateTime fechaRes))
      return fechaRes;
    throw new Exception("No se ha podido recuperar la fecha");
  }

  public string GetExpediente()
  {
    var rec_expediente=new PdfRectangle(120.036072, 643.886292,228.818787, 657.386536);
    var expediente = GetDatos(rec_expediente);
    return expediente;
  }

  public EmisorDTO GetEmisor()
  {
    return Emisor.PortsBalears.ToEmisorDto();
  }

  public string GetMotivoMulta()
  {
    return "";
  }

  public string GetLugarMulta()
  {
    var rec_lugar=new PdfRectangle(156.422028, 551.259644,330.474335, 565.134888);
    var lugar = GetDatos(rec_lugar);
    return lugar;
  }

  public string GetMarcaVehiculo()
  {
    return "";
  }

  public string GetClaseVehiculo()
  {
    return "";
  }

  public string GetModeloVehiculo()
  {
    return "";
  }

  public decimal GetImporte()
  {
   
    var rec_importe=new PdfRectangle(356.16684, 567.009949,499.420258, 582.385193);
    var importe = GetDatos(rec_importe).Replace(".",",").Replace("€","").Replace("sanción:","");
    if (Decimal.TryParse(importe, out decimal importeRes))
      return importeRes;
    throw new Exception("No se ha podido recuperar el importe");
  }
}