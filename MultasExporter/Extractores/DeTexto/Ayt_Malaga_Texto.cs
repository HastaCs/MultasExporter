using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{//TODO: Revisar mejor el match, este pdf no me ha dejado leer texto, solo los rellenados de la multa
  internal class Ayt_Malaga_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoMalaga.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(253.316391, 673.652199, 413.084405, 691.17697);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(256.403517, 695.409139, 360.864209, 712.206695);
      var rec_hora = new PdfRectangle(361.580243, 695.73341, 408.879984, 711.155209);
      var fecha = GetDatos(rec_fecha) + " " + GetDatos(rec_hora);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(305.871659, 497.353002, 411.682932, 517.331241);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(26.989844, 695.409139, 252.405021, 711.906984);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(139.947339, 717.9062, 250.405774, 731.904371);
      return GetDatos(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(30.488528, 718.406135, 137.448279, 733.404175);
      var matricula = GetDatos(rec_matricula);
      string matriculaLimpia = matricula.Replace(" ", "");
      Match match = Regex.Match(matriculaLimpia, @"^(\d+)([A-Z]+)$", RegexOptions.IgnoreCase);
      if (match.Success)
      {
        string numeros = match.Groups[1].Value.TrimStart('0'); // Números sin ceros a la izquierda
        string letras = match.Groups[2].Value.ToUpper();      // Letras en mayúsculas

        // Ajuste al formato actual: 4 números y 3 letras
        numeros = numeros.PadLeft(4, '0');                    // Asegura 4 dígitos
        letras = letras.PadRight(3, 'X').Substring(0, 3);    // Asegura 3 letras
        matriculaLimpia = numeros + letras;

      }
      return matriculaLimpia;
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(256.403517, 718.165117, 405.34747, 731.133448);
      return GetDatos(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(26.978371, 523.289663, 534.662258, 668.745263);
      return GetDatos(rec_motivo);

    }

    public bool Match()
    {
      var rec_mach = new PdfRectangle(75.679586, 637.901666, 162.570962, 663.487831);

      var match = GetDatos(rec_mach);
      return match.Contains("NOTIFICACIÓN DENUNCIA");
    }
  }
}
