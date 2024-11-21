using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MultasExporter.Extractores.DePix
{
    public class Ayt_Valencia_Pix : ExtractorBasePix, IExtractorPdf
    {
        public string GetClaseVehiculo()
        {
            return "";
        }

        public EmisorDTO GetEmisor()
        {
            return Emisor.AytoValencia.ToEmisorDto();
        }

        public string GetExpediente()
        {
            var rec = new Rect(1484, 280, 668, 66);
            return GetDato(rec);
        }

        public DateTime GetFechaMulta()
        {
            var fechaR = new Rect(488, 484, 244, 66);
            var horaR = new Rect(736, 480, 196, 70);
            return Convert.ToDateTime(GetDato(fechaR) + " " + GetDato(horaR));
        }

        public decimal GetImporte()
        {
            var rec = new Rect(1416, 848, 328, 62);
            var importe = GetDato(rec).Replace("E", "").Replace(".", ",");
            return Convert.ToDecimal(importe);
        }

        public string GetLugarMulta()
        {
            var rec = new Rect(124, 1118, 1142, 112);
            return GetDato(rec);
        }

        public string GetMarcaVehiculo()
        {
            var rec = new Rect(318, 378, 848, 68);
            return GetDato(rec).Split(" ")[0];
        }

        public string GetMatricula()
        {
            var rec = new Rect(120, 384, 194, 64);
            return GetDato(rec);
        }

        public string GetModeloVehiculo()
        {
            var rec = new Rect(318, 378, 848, 68);

            return string.Join(" ", GetDato(rec).Split(" ").Skip(1));

        }

        public string GetMotivoMulta()
        {
            var rec = new Rect(128, 588, 1132, 496);
            return GetDato(rec);
        }

        public bool Match()
        {
            Console.WriteLine("match valencia");
            var rect = new Rect(294, 60, 354, 146);
            var valencia = GetDato(rect);
            Console.WriteLine("fin match ");
            return valencia == "AJUNTAMENT\nDE VALENCIA";
        }

        public void SetPix(Pix pixImagen)
        {
            base.SetPix(pixImagen);
        }
    }
}
