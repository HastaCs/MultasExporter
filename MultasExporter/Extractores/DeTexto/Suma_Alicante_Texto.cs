using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
    internal class Suma_Alicante_Texto : ExtractorBaseTexto, IExtractorTexto
    {
        public string GetClaseVehiculo()
        {
            var rec_clase = new PdfRectangle(176.571613, 489.970914, 444.615567, 503.47356);
            return GetDatos(rec_clase);
        }

        public EmisorDTO GetEmisor()
        {
            return Emisor.SumaAlicante.ToEmisorDto();
        }

        public string GetExpediente()
        {
            var rec_expediente = new PdfRectangle(425.496348, 522.852455, 521.092444, 536.730174);
            return GetDatos(rec_expediente);
        }

        public DateTime GetFechaMulta()
        {
            var fechaMulta = new PdfRectangle(328.400706, 512.72547, 397.004963, 525.477969);
            var horaMulta = new PdfRectangle(424.371688, 514.225764, 476.480932, 526.228116);
            if (DateTime.TryParse(GetDatos(fechaMulta) + " " + GetDatos(horaMulta), out DateTime fechaHora))
                return fechaHora;
            throw new Exception("No se pudo recuperar la fecha");
        }

        public decimal GetImporte()
        {
            var rec_importe = new PdfRectangle(200.939245, 338.691368, 284.913854, 351.068793);
            var importeTexto = GetDatos(rec_importe);
            if(importeTexto=="")
            {
                rec_importe=new PdfRectangle(215.559824, 327.814237, 304.782847, 342.442103);
                importeTexto = GetDatos(rec_importe);
            }
            if (decimal.TryParse(importeTexto, out decimal importe))
                return importe;
            throw new Exception("No se pudo recuperar el importe");
        }

        public string GetLugarMulta()
        {
            var rec_lugar = new PdfRectangle(108.342242, 501.848339, 522.217104, 514.975911);
            return GetDatos(rec_lugar);
        }

        public string GetMarcaVehiculo()
        {
            var rec_marca = new PdfRectangle(124.087482, 483.469738, 520.342671, 494.721943);
            return GetDatos(rec_marca).Split(" ")[0];
        }

        public string GetMatricula()
        {
            var rec_matricula = new PdfRectangle(122.962822, 491.721355, 177.321386, 504.09878);
            return GetDatos(rec_matricula);
        }

        public string GetModeloVehiculo()
        {
            var rec_marca = new PdfRectangle(124.087482, 483.469738, 520.342671, 494.721943);
            return string.Join(" ", GetDatos(rec_marca).Split(" ").Skip(1));
        }

        public string GetMotivoMulta()
        {
            var rec_motivo = new PdfRectangle(71.228464, 381.074673, 518.468237, 464.340989);
            return GetDatos(rec_motivo);
        }

        public bool Match()
        {
            var rec_match = new PdfRectangle(319.02854, 803.782504, 391.006776, 818.035297);
            var match = GetDatos(rec_match);
            //A veces lee 'modelo', y otras 'modelo notden'
            var rec_match2= new PdfRectangle(65.605164, 690.135234, 141.332267, 703.63788);
            var match2=GetDatos(rec_match2);
            
            var rec_match3=new PdfRectangle(68.366135, 563.516907,118.726387, 573.926331);
            var match3=GetDatos(rec_match3);
            return match.Contains("Modelo") && match2=="Oficina remitente" && match3=="Referencia \\";

        }


    }
}
