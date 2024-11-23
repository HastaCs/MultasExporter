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

namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Granada_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(434.470143, 802.443252, 522.943823, 817.188866);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(75.213381, 777.14846, 156.314255, 790.146761);
      var rec_hora = new PdfRectangle(157.654765, 777.14846, 185.805482, 789.646826);
      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(373.476924, 776.898492, 409.000447, 791.71917);
      return Convert.ToDecimal(GetDato(rec_importe));
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(75.213381, 715.310082, 331.921105, 734.077227);
      return GetDato(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(146.260428, 640.886419, 260.203803, 656.261693);
      var marca = GetDato(rec_marcamodelo).Split(" ")[0];
      return marca;

    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(75.213381, 642.252271, 145.590172, 655.699183);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(146.260428, 640.886419, 260.203803, 656.261693);
      var marca = GetDato(rec_marcamodelo).Split(" ");
      return string.Join(" ", marca.Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(75.213381, 744.131054, 370.795904, 764.908964);
      return GetDato(rec_motivo);

    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(75.883637, 812.582599, 221.329005, 825.902182);
      var match = GetDato(rec_match);
      return match == "EXCMO. AYUNTAMIENTO DE GRANADA";
    }

    public void SetPalabras(IEnumerable<Word> palabras)
    {
      base.Set(palabras);
    }
  }
}
