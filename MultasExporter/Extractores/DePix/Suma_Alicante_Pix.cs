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
    internal class Suma_Alicante_Pix : ExtractorBasePix, IExtractorPdf
    {
        public string GetClaseVehiculo()
        {
            var rec = new Rect(513, 1409, 1244, 51);
            var datos = GetDato(rec);
            return string.Join(" ", datos.Split(" ").Skip(1));
        }

        public EmisorDTO GetEmisor()
        {
            return Emisor.SumaAlicante.ToEmisorDto();
        }

        public string GetExpediente()
        {
            var rec = new Rect(1775, 1277, 382, 52);
            return GetDato(rec);
        }

        public DateTime GetFechaMulta()
        {
            var fechaMulta = new Rect(1370, 1322, 283, 54);
            var horaMulta = new Rect(1775, 1325, 232, 51);

            return Convert.ToDateTime(GetDato(fechaMulta) + " " + GetDato(horaMulta));
        }

        public decimal GetImporte()
        {
            var rec = new Rect(846, 2046, 339, 54);
            return Convert.ToDecimal(GetDato(rec));
        }

        public string GetLugarMulta()
        {
            var rec = new Rect(446, 1364, 1594, 48);
            return GetDato(rec);
        }

        public string GetMarcaVehiculo()
        {
            var rec = new Rect(524, 1445, 663, 54);
            return GetDato(rec).Split(" ")[0];
        }

        //TODO probar esto con mas
        public string GetMatricula()
        {

            var rec = new Rect(293, 1403, 618, 63);
            var matricula = GetDato(rec);
            matricula = matricula.Replace("VEHICULO:", "").Trim();

            matricula = matricula.Split(" ")[0];
            return matricula;
            // return numero + letras;
        }

        public string GetModeloVehiculo()
        {
            var rec = new Rect(524, 1445, 1637, 54);
            return string.Join(" ", GetDato(rec).Split(" ").Skip(1));
        }
        //TODO revisar esto un poco
        public string GetMotivoMulta()
        {
            var rec = new Rect(305, 1583, 1741, 124);
            return GetDato(rec).Replace("HECHO DENUNCIADO:", "");
        }

        public bool Match()
        {
            var rec = new Rect(1114, 2696, 295, 94);
            var match = GetDato(rec);
            return match.Contains("Oficinas de Suma");
        }

        public void SetPix(Pix pixImagen)
        {
            throw new NotImplementedException();
        }


    }
}
