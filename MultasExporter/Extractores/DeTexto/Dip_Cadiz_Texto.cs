using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class Dip_Cadiz_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(90, 496, 237.700064, 523);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipCadiz.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(430, 729, 555, 741);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(99.354128, 709, 155, 729);
      var rec_hora = new PdfRectangle(190, 709, 230, 729);
      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(445, 554, 500, 569);
      return Convert.ToDecimal(GetDato(rec_importe).Replace("€",""));
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(100, 669, 555.5, 689);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(90, 464, 240.5, 496);
      var marca = GetDato(rec_marca);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(90, 523, 240.5, 538);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(90, 464, 240.5, 496);
      var marca = GetDato(rec_marca);
      return string.Join(" ", marca.Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var recmotivo = new PdfRectangle(40, 584, 555.5, 661.707591);
      return GetDato(recmotivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(74.609326, 739.390103, 220.828608, 754.762818);
      var match = GetDato(rec_match);
      return match == "NOTIFICACIÓN DE DENUNCIA";
    }

    public void SetPalabras(IEnumerable<Word> palabras)
    {
      base.Set(palabras);
    }
  }
}
