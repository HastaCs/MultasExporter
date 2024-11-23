using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  public class DGT_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(101.353813, 636.094433, 238.724877, 649.596437);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Dgt.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(119.599874, 696.103341, 214.466545, 714.48107);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(454.404537, 691.922286, 536.914195, 713.730958);
      var fecha = DateTime.ParseExact(GetDato(rec_fecha).Trim(), "dd/MM/yyyy - HH:mm'h'", CultureInfo.InvariantCulture);
      return fecha;
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(181.086957, 680.726058, 240.849325, 693.102896);
      var importe = GetDato(rec_importe).Replace("€", "");
      decimal result = decimal.Parse(importe, CultureInfo.InvariantCulture);
      return result;
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(33.207075, 553.159855, 277.003505, 592.963029);

      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(100.853813, 625.592874, 238.224877, 639.094878);
      return GetDato(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(101.228734, 647.721159, 240.849325, 659.72294);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(101.228735, 614.265619, 238.599799, 627.767623);
      return GetDato(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(35.902761, 369.918776, 537.249018, 412.19886);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(52, 734, 250, 750);
      var match = GetDato(rec_match);
      return match == "Centro de Tratamiento de Denuncias Automatizadas";

    }

  }
}
