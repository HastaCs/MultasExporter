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
  internal class Ayt_Sevilla_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(107, 713.25, 192, 723.75);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(203.25, 677.5, 255.5, 687.5);
      var rec_hora = new PdfRectangle(200.25, 669.75, 223.25, 679.75);
      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      if (DateTime.TryParse(fecha + " " + hora, out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_motivoimporte = new PdfRectangle(141.75, 576.5, 536.5, 643);
      var motivo = GetDato(rec_motivoimporte);
      var palabraClave = "SANCIÓN PROPUESTA:";
      var indice = motivo.IndexOf(palabraClave);
      if (decimal.TryParse(motivo.Substring(indice + palabraClave.Length), out decimal importe))
        return importe;

      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(170.75, 682.25, 530.75, 693.25);
      return GetDato(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(180.5, 655.5, 294, 665.5);
      return GetDato(rec_marca).Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(184, 662.5, 228.5, 673);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(180.5, 655.5, 294, 665.5);
      return string.Join(" ", GetDato(rec_marca).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivoyimporte = new PdfRectangle(141.75, 576.5, 536.5, 643);
      var motivo = GetDato(rec_motivoyimporte);
      var palabraClave = "NORMATIVA APLICABLE";
      var termina = motivo.IndexOf(palabraClave);
      string resultado = motivo.Substring(0, termina).Replace("HECHO DENUNCIADO:", "");
      return resultado;

    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(62.25, 721.5, 186, 733.25);
      var match = GetDato(rec_match);
      return match.Contains("NOTIFICACIÓN DENUNCIA DE TRÁFICO");
    }
    public int PaginaDatos() { return 2; }
  }
}
