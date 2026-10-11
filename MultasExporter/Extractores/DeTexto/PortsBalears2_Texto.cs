using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto;

public class PortsBalears2_Texto: ExtractorBaseTexto, IExtractorTexto
{
    public bool Match()
    {
        var  rec_match=new PdfRectangle(102.634705, 124.022064,165.902649, 133.865082);
        var match=GetDatos(rec_match);
        return match.Contains("www.portsdebalears.com");
    }

    public string GetMatricula()
    {
        var rec_matricula=new PdfRectangle(152.378052, 638.049805,233.404465, 648.932495);
        var matricula=GetDatos(rec_matricula);
        return matricula;
    }

    public DateTime GetFechaMulta()
    {
        var rec_fechamulta=new PdfRectangle(253.157166, 664.248901,286.615845, 674.3255);
        var rec_horamulta=new PdfRectangle(291.78064, 665.200623,314.845581, 674.94989);
        var fecha=GetDatos(rec_fechamulta);
        var hora=GetDatos(rec_horamulta);
        
        if(DateTime.TryParse(fecha+" "+hora, out DateTime fechaRes))
            return fechaRes;
        throw new Exception("No se ha podido recuperar la fecha");
    }

    public string GetExpediente()
    {
        var rec_expediente=new PdfRectangle(101.75, 689.75,188.25, 702.5);
        var expediente=GetDatos(rec_expediente);
        return expediente;
    }

    public EmisorDTO GetEmisor()
    {
        return Emisor.PortsBalears.ToEmisorDto();
    }

    public string GetMotivoMulta()
    {
        var rec_motivo=new PdfRectangle(248.197876, 568.209351,532.388184, 580.961243);
        var motivo=GetDatos(rec_motivo);
        return motivo;
        
    }

    public string GetLugarMulta()
    {
        var rec_lugar=new PdfRectangle(102.75, 663.5,250.75, 676);
        var lugar=GetDatos(rec_lugar);
        return lugar;
    }

    public string GetMarcaVehiculo()
    {
        var rec_marca=new PdfRectangle(153.25, 628.5,323.75, 640.5);
        var marca=GetDatos(rec_marca);
        return marca;
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
        var rec_importe=new PdfRectangle(381.75, 530.5,442.5, 552.5);
        var importe=GetDatos(rec_importe).Replace(".",",").Replace("€","");
        if(Decimal.TryParse(importe, out decimal importeRes))
            return importeRes;
        throw new Exception("No se ha podido recuperar el importe");
    }
}