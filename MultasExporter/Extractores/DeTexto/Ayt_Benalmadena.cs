using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto;

public class Ayt_Benalmadena:ExtractorBaseTexto, IExtractorTexto
{
  public bool Match()
  {
    var rec_match1=new PdfRectangle(90.402496, 794.336243,199.260834, 808.414612);
    var rec_match2=new PdfRectangle(278.714935, 696.100098,355.97934, 709.865662);
    var rec_match3=new PdfRectangle(106.355865, 558.757202,180.804962, 570.332764);
    var rec_match4=new PdfRectangle(144.518829, 657.306152,196.132706, 667.943237);
    var rec_match5=new PdfRectangle(471.872925, 746.263306,554.019287, 772.138794);

    var match1=GetDatos(rec_match1);
    var match2=GetDatos(rec_match2);
    var match3=GetDatos(rec_match3);
    var match4=GetDatos(rec_match4);
    var match5=GetDatos(rec_match5);
    return match1=="NOTIFICACIÓN INCOACIÓN"  && match2=="Precepto Infringido" && match4=="www.dgt.es" && match3=="PERÍODO DE PAGO" && match5.Contains("Benalmadena");
  }
  
  public string GetMatricula()
  {
    var rec_matricula=new PdfRectangle(85.522278, 725.262939,138.03595, 737.638184);
    var matricula=GetDatos(rec_matricula);
    return matricula;
  }

  public DateTime GetFechaMulta()
  {
    var rec_fechayhora=new PdfRectangle(228.059387, 736.888123,304.204224, 750.013367);
    var fechayhora=GetDatos(rec_fechayhora);
    if(DateTime.TryParse(fechayhora, out DateTime fechaRes))
      return fechaRes;
    throw new Exception("No se ha podido recuperar la fecha");
  }

  public string GetExpediente()
  {
    var rec_expediente=new PdfRectangle(227.684265, 751.888428,295.576965, 768.388733);
    var expediente=GetDatos(rec_expediente);
    return expediente;
  }

  public EmisorDTO GetEmisor()
  {
    return Emisor.AytoBenalmadena.ToEmisorDto();
  }

  public string GetMotivoMulta()
  {
    var rec_motivo=new PdfRectangle(129.033569, 680.637146,559.645752, 695.63739);
    var motivo=GetDatos(rec_motivo);
    return motivo;
  }

  public string GetLugarMulta()
  {
    var rec_lugar=new PdfRectangle(110.278717, 692.637329,253.941132, 714.762756);
    var lugar=GetDatos(rec_lugar);
    return lugar;
  }

  public string GetMarcaVehiculo()
  {
    var rec_marcamodelo=new PdfRectangle(227.684326, 724.512939,319.20813, 739.138184);
    var marcaModelo=GetDatos(rec_marcamodelo).Split("-");
    return marcaModelo[0];
    
  }

  public string GetClaseVehiculo()
  {
    return "";
  }

  public string GetModeloVehiculo()
  {
    var rec_marcamodelo=new PdfRectangle(227.684326, 724.512939,319.20813, 739.138184);
    var marcaModelo=GetDatos(rec_marcamodelo).Split("-");
    return marcaModelo.Last();
  }

  public decimal GetImporte()
  {
    var rec_importe=new PdfRectangle(469.997375, 714.387695,522.886108, 727.137878);
    var importe=GetDatos(rec_importe).Replace(".",",").Replace("€","");
    if(Decimal.TryParse(importe, out decimal importeRes))
      return importeRes;
    throw new Exception("No se ha podido recuperar el importe");
  }
}