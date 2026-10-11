using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto;

public class Ayt_SanJosepSaTalaia:ExtractorBaseTexto, IExtractorTexto
{
    public bool Match()
    {
        var rec_match1=new PdfRectangle(119.188446, 801.007202,319.877289, 814.671448);
        var rec_match2=new PdfRectangle(38.5, 749.5,141.5, 760.75);
        var rec_match3=new PdfRectangle(39, 703.75,79.25, 719.25);
        var rec_match4=new PdfRectangle(179.75, 650.75,231.5, 660.75);
        
        var match1=GetDatos(rec_match1);
        var match2=GetDatos(rec_match2);
        var match3=GetDatos(rec_match3);
        var match4=GetDatos(rec_match4);
        return match1 == "NOTIFICACIÓN ELECTRÓNICA DE DENUNCIA" &&
               match2 == "NÚMERO D'EXPEDIENT-BUTLLETÍ" &&
               match3 == "PRECEPTE INFRINGIT:" &&
               match4 == "REFERENCIA";
    }

    public string GetMatricula()
    {
        var rec_matricula=new PdfRectangle(167, 741.25,235.75, 750.75);
        var matricula=GetDatos(rec_matricula);
        return matricula;
    }

    public DateTime GetFechaMulta()
    {
        var rec_fechamulta=new PdfRectangle(427.75, 758.5,473.5, 768.5);
        var rec_horamulta=new PdfRectangle(513, 758.25,546, 770.25);
        var fecha=GetDatos(rec_fechamulta);
        var hora=GetDatos(rec_horamulta);
        if(DateTime.TryParse(fecha+" "+hora, out DateTime fechaMulta))
        {
            return fechaMulta;
        }
        throw new Exception("No se pudo parsear la fecha y hora de la multa");
    }

    public string GetExpediente()
    {
        var rec_expediente=new PdfRectangle(229.25, 749.75,325.5, 760.5);
        var expediente=GetDatos(rec_expediente);
        return expediente;
    }

    public EmisorDTO GetEmisor()
    {
        return Emisor.AytoSantJosepSaTalaia.ToEmisorDto();
    }

    public string GetMotivoMulta()
    {
        var rec_motivo=new PdfRectangle(95, 766.5,548.25, 782.75);
        var motivo=GetDatos(rec_motivo);
        return motivo;
    }

    public string GetLugarMulta()
    {
        var rec_lugar=new PdfRectangle(82, 758,342.5, 769);
        var lugar=GetDatos(rec_lugar);
        return lugar;
    }

    public string GetMarcaVehiculo()
    {
        var rec_marcamodelo=new PdfRectangle(331.5, 740.75,524, 750.5);
        var marcamodelo=GetDatos(rec_marcamodelo);
        var marca=marcamodelo.Split(' ')[0];
        return marca;
    }
    
    public string GetClaseVehiculo()
    {
        return "";
    }

    public string GetModeloVehiculo()
    {
        var rec_marcamodelo=new PdfRectangle(331.5, 740.75,524, 750.5);
        var marcamodelo=GetDatos(rec_marcamodelo);
        return marcamodelo.Split(' ').Last();
    }

    public decimal GetImporte()
    {
        var rec_importe=new PdfRectangle(461.25, 417.75,559.25, 452.75);
        var importe = GetDatos(rec_importe).Replace("€", "").Replace(".", ",").Trim();
        return Convert.ToDecimal((importe));
    }
}