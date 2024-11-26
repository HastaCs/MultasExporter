using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
  internal class Ayt_Ibiza_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoIbiza.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new Rect(1745,107,507,54);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new Rect(611, 207, 238, 66);
      var rec_hora = new Rect(861, 215, 254, 60);
      var fecha = GetDato(rec_fecha).Replace(".", "/");
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha+" "+hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new Rect(1880,1950,342,42);
      var importe = GetDato(rec_importe);
      return Convert.ToDecimal(importe);
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new Rect(1160, 1358, 789, 70);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new Rect(174, 1670, 429, 66);
      return GetDato(rec_marca).Split("-")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new Rect(188,1568,391,61);
      return GetDato(rec_matricula).Replace("-","").Trim();
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new Rect(174, 1670, 429, 66);
      return string.Join(" ", GetDato(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new Rect(617, 1565, 1668, 181);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new Rect(321, 1289, 281, 139);
      var match = GetDato(rec_match);
      return match== "Ajuntament\nd'Eivissa";
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      base.SetPix(pixImagen);
    }
    public int PaginaDatos() { return 2; }
  }
}
