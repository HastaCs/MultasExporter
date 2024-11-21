using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
    public class DGT_pix : ExtractorBasePix, IExtractorPdf
    {


        public bool Match()
        {
            var ayto = new Rect(225, 383, 774, 57);
            Console.WriteLine("match DGT Fin_");
            return GetDato(ayto).ToUpper() == "CENTRO DE TRATAMIENTO DE DENUNCIAS AUTOMATIZADAS";
        }


        public string GetMatricula()
        {
            var rectMulta = new Rect(794, 762, 194, 42);
            return GetDato(rectMulta);
        }



        public string GetExpediente()
        {
            var recExpediente = new Rect(500, 544, 358, 62);
            return GetDato(recExpediente);
        }
        public DateTime GetFechaMulta()
        {
            var recFechaMulta = new Rect(1894, 534, 346, 88);
            var fecha = DateTime.ParseExact(GetDato(recFechaMulta), "dd/MM/yyyy - HH:mm'h'", CultureInfo.InvariantCulture);
            return fecha;
        }

        public EmisorDTO GetEmisor() => Emisor.Dgt.ToEmisorDto();

        public string GetMotivoMulta()
        {
            return GetDato(new Rect(156, 1790, 2084, 176));
        }

        public string GetLugarMulta()
        {
            var lugar = GetDato(new Rect(146, 1036, 992, 166));
            return lugar;
        }

        public string GetMarcaVehiculo()
        {
            var marca = GetDato(new Rect(338, 846, 650, 48));
            return marca;
        }

        public string GetClaseVehiculo()
        {
            var clase = GetDato(new Rect(320, 804, 672, 48));
            return clase;
        }

        public string GetModeloVehiculo()
        {
            var modelo = GetDato(new Rect(362, 886, 628, 58));
            return modelo;
        }

        public decimal GetImporte()
        {
            var importe = GetDato(new Rect(750, 621, 218, 50));
            decimal result = decimal.Parse(importe, CultureInfo.InvariantCulture);


            return result;
        }


        public void SetPix(Pix pixImagen)
        {
            Console.WriteLine("setpix dgt");
            base.SetPix(pixImagen);
        }
    }
}
