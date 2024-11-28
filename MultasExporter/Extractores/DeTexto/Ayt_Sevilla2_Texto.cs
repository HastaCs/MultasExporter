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
  internal class Ayt_Sevilla2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoSevilla.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(106.477631, 554.165527, 167.964691, 564.289001);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(203.25, 517.75, 251, 527);
      var rec_hora = new PdfRectangle(200.25, 510.25, 227, 518.75);

      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_motivoimporte = new PdfRectangle(140.75, 421.25, 533.75, 483.5);

      var motivo = GetDato(rec_motivoimporte);
      var palabraClave = "SANCIÓN PROPUESTA:";
      var indice = motivo.IndexOf(palabraClave);
      return Convert.ToDecimal(motivo.Substring(indice + palabraClave.Length));
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(170.964111, 524.170105, 530.51355, 533.54364);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(180, 495, 530.5, 505.75);
            return GetDato(rec_marca).Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(184.5, 502.5, 225, 512);
      return GetDato(rec_matricula);

    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(180, 495, 530.5, 505.75);
      return string.Join(" ", GetDato(rec_marca).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivoimporte = new PdfRectangle(140.75, 421.25, 533.75, 483.5);
            var motivo = GetDato(rec_motivoimporte);
      var palabraClave = "NORMATIVA APLICABLE";
      var termina = motivo.IndexOf(palabraClave);
      string resultado = motivo.Substring(0, termina).Replace("HECHO DENUNCIADO:", "");
      return resultado;
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(62.236938, 560.91449, 209.206055, 574.412415);
      var match = GetDato(rec_match);
      return match.Contains("NOTIFICACIÓN DENUNCIA DE TRÁFICO");
    }
  }
}
