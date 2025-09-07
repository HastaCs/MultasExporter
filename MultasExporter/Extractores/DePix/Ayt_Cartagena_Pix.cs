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
  public class Ayt_Cartagena_Pix : ExtractorBasePix, IExtractorPix
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoCartagena.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec = new Rect(1241, 575, 547, 66);
      var dato = GetDato(rec).Replace(" ","");
      return dato;
    }

    public DateTime GetFechaMulta()
    {
      var recFecha = new Rect(1237, 1109, 337, 77);
      var recHora = new Rect(1642, 1118, 316, 65);
      var datoFecha = GetDato(recFecha).Replace(" ", "");
      var datoHora = GetDato(recHora).Replace(" ", "");
      if (DateTime.TryParse($"{datoFecha} {datoHora}", out DateTime fecha))
      {
        return fecha;
      }
      throw new Exception("No se ha podido recuperar la fecha");


    }

    public decimal GetImporte()
    {
      var rec = new Rect(1234, 1274, 366, 70);
      var dato = GetDato(rec).Replace("€", "");
      if (decimal.TryParse(dato, out decimal importe))
        return importe;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec = new Rect(96, 943, 1120, 75);
      var dato = GetDato(rec);
      return dato;
    }

    public string GetMarcaVehiculo()
    {
      var rec = new Rect(403, 785, 436, 75);
      var dato = GetDato(rec);
      return dato.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec = new Rect(98, 785, 293, 74);
      var dato = GetDato(rec);
      return dato;
    }

    public string GetModeloVehiculo()
    {
      var rec = new Rect(403, 785, 436, 75);
      var dato = GetDato(rec);
      var partes = dato.Split(" ");
      if (partes.Length > 1)
        return string.Join(" ", partes.Skip(1));
      else
        return "";

    }

    public string GetMotivoMulta()
    {
      var rec = new Rect(98, 1416, 2238, 216);
      var dato = GetDato(rec);
      return dato;
    }

    public bool Match()
    {
      var rec = new Rect(403, 126, 746, 94);
      var dato = GetDato(rec);
      return dato.Contains("AYUNTAMIENTO DE CARTAGENA");
    }

    void IFromPix.SetPix(Pix pixImagen)
    {
      SetPix(pixImagen);
    }
  }
}
