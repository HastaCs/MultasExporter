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
{/// <summary>
/// Datos en segunda pagina
/// </summary>
  internal class Ayt_Sevilla3_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(106.10271, 764.50846, 180.337115, 774.631916);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(203.20731, 727.764061, 251.572149, 737.512575);
      var rec_hora = new PdfRectangle(199.083176, 720.640147, 233.950851, 730.763604);
      if (DateTime.TryParse(GetDatos(rec_fecha) + " " + GetDatos(rec_hora), out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(144.719597, 642.277092, 319.057972, 685.395519);
      var motivo = GetDatos(rec_importe);
      var palabraClave = "SANCIÓN PROPUESTA:";
      var indice = motivo.IndexOf(palabraClave);
      if (decimal.TryParse(motivo.Substring(indice + palabraClave.Length), out decimal importe))
        return importe;

      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(171.339005, 734.887975, 532.763075, 744.636489);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(179.962193, 706.767262, 529.763706, 716.890718);
      var marca=GetDatos(rec_marca);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(185.211091, 713.891176, 249.697543, 724.389575);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(179.962193, 706.767262, 529.763706, 716.890718);
      var marca = GetDatos(rec_marca);
      return string.Join(" ", GetDatos(rec_marca).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(142.47007, 665.148606, 538.386894, 695.144033);
      //String hasta que encuentre las palabras NORMATIVA APLICABLE
      var motivo = GetDatos(rec_motivo);
      var palabraClave = "NORMATIVA APLICABLE";
      var indice = motivo.IndexOf(palabraClave);
      return motivo.Substring(0, indice);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(62.236926, 771.632374, 183.711406, 784.38043);
      var rec_match2 = new PdfRectangle(64.486453, 621.280293, 142.844991, 641.527207);
      var match1=GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      return match1.Contains("NOTIFICACIÓN DENUNCIA DE TRÁFICO") && match2.Contains("Sevilla");
    }
  }
}
