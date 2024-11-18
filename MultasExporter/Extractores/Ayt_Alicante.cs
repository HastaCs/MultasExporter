using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{
  public class Ayt_Alicante :ExtractorBase, IExtractor
  {
   
    public string GetClaseVehiculo()
    {
      var clase = new Rect(1839, 953, 449, 52);
      return GetDato(clase);
    }
   

    public EmisorDTO GetEmisor()
    {
      return new EmisorDTO()
      {
        Id = Emisor.AytoAlicante,
        Nombre = "Ayuntamiento de Alicante"
      };
    }

    public string GetExpediente()
    {
      var expediente = new Rect(1896,198,395,59);
      return GetDato(expediente).Replace(" ","");
    }

    public DateTime GetFechaMulta()
    {
      var fechaMulta = new Rect(234, 1065, 485, 50);
      var horaMulta = new Rect(750, 1058, 480, 57);

     
      return Convert.ToDateTime(GetDato(fechaMulta) + " " + GetDato(horaMulta));
    }

    public decimal GetImporte()
    {
      var importe = new Rect(1268, 1176, 448, 54);
      var imp=Convert.ToDecimal(GetDato(importe).Replace("Eur",""));
      return imp;
    }

    public string GetLugarMulta()
    {
      var rect = new Rect(231,1272,1590,66);
      return GetDato(rect);
    }

    public string GetMarcaVehiculo()
    {
      var rectMarca = new Rect(746,951,495,53);
      return GetDato(rectMarca);
    }

    public string GetMatricula()
    {
      var rectMulta = new Rect(242, 954, 477, 47);
      return GetDato(rectMulta).Replace("-","");
    }

    public string GetModeloVehiculo()
    {
      var rectModelo = new Rect(1272,951,552,57);
      return GetDato(rectModelo);
    }

    public string GetMotivoMulta()
    {
      var rectMotivo = new Rect(223,1404,2059,200);
      return GetDato(rectMotivo);
    }

    public bool Match()
    {     
      var ayto = new Rect(111,246,662,54);
      return GetDato(ayto) == "EXCMO. AYUNTAMIENTO DE ALICANTE";
    }
   
    public void SetFichero(Pix imagenBytes)
    {
      base.SetFichero(imagenBytes);
    }
  }
}
