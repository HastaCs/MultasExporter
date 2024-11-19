using MultasExporter.Extractores;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tesseract;
using UglyToad.PdfPig;

namespace MultasExporter
{
  public enum Emisor
  {

    [Display(Name = "(DGT)CENTRO DE TRATAMIENTO DE DENUNCIAS AUTOMATIZADAS")]
    Dgt = 1,

    [Display(Name = "Ayuntamiento de Alicante")]
    AytoAlicante,

    [Display(Name = "Ayuntamiento de Granada")]
    AytoGranada,

    [Display(Name = "Ayuntamiento de Valencia")]
    AytoValencia,

    [Display(Name = "Diputación provincial de Málaga")]
    DipMalaga,

    [Display(Name = "Diputación provincial de Cádiz")]
    DipCadiz,
    [Display(Name = "Suma Diputación de Alicante")]
    SumaAlicante
  }
  /// <summary>
  /// Clase principal del proyecto, que usa IMULTA para recuperar los datos
  /// </summary>
  public class ExtractorMulta
  {
    private IDatos _extractorMulta { get; set; }
    private List<IExtractorPdf> _extractoresPdf = new List<IExtractorPdf> { new Ayt_Granada(), new DGT(), new Ayt_Alicante(), new Ayt_Valencia(), new Dip_Malaga(), new Dip_Cadiz(), new Suma_Alicante() };

    public ExtractorMulta(byte[] multaFichero)
    {
      /* Console.WriteLine("empiezo PIG----" + DateTime.Now);
      string texto = "";
      using (var pdfDocument = PdfDocument.Open(multaFichero))
       {
       
         foreach (var page in pdfDocument.GetPages().Take(1))
         {
           texto += page.Text + Environment.NewLine;
         }

         Console.WriteLine(texto);
       }
       Console.WriteLine("terino string" + DateTime.Now);
      var extractor = DeterminarExtractorFromString(texto);
      if (extractor == null)*/

      var extractor = DeterminarExtractor(multaFichero);
      if (extractor == null)
        throw new Exception("No se ha encontrado ningun extractor");
      _extractorMulta = extractor;
    }


    /// <summary>
    /// Método para determinar el extractor adecuado según el contenido del PDF
    /// </summary>
    private IDatos? DeterminarExtractor(byte[] multaFichero)
    {
      Console.WriteLine("convert");
      byte[] pngByte = Freeware.Pdf2Png.Convert(multaFichero, 1);


      Console.WriteLine("convert fin");
      var pix = Pix.LoadFromMemory(pngByte);


      // using (var engine = new TesseractEngine("", "spa", EngineMode.Default))
      {
        foreach (var extractor in _extractoresPdf)
        {
          //TODO Intenar hacer la busqueda del extractor con Tasks y con un WhenAny
          //TODO cada Iextractor debe tener el rect que tengo que inspeccionar, y el texto que tiene que haber
          /* var rectMulta = new Rect(794, 762, 194, 42);
           using (var page = engine.Process(pix, rectMulta))
           {
             Console.WriteLine("chek extractor");
             var ee = page.GetText();
           }*/

          Console.WriteLine("set extractor " + extractor.GetType());
          extractor.SetPix(pix);
          //  Console.WriteLine("fin fichero "+DateTime.Now);
          var encontrado = extractor.Match();
          //  Console.WriteLine("fin fichero " + DateTime.Now);
          if (encontrado)
          {
            return extractor;
          }
        }

      }
      /*  var extractorEE = _extractores.Last();
        extractorEE.SetFichero(pngByte);
        Console.WriteLine("extractores "+_extractores.Count());
        return extractorEE;*/
      return null;
    }

    private IDatos? DeterminarExtractorFromString(string multaFichero)
    {
      return null;
      /* foreach (var extractor in _extractoresPdf)
       {
         if(extractor.mat)
       }*/
    }
    private string GetMatricula() => _extractorMulta.GetMatricula().Trim().Replace("O", "0").Replace("I", "1");
    private DateTime GetFechaMulta() => _extractorMulta.GetFechaMulta();
    private string GetExpediente() => _extractorMulta.GetExpediente().Trim();
    private EmisorDTO GetEmisor() => _extractorMulta.GetEmisor();
    private string GetMotivoMulta() => _extractorMulta.GetMotivoMulta();
    private string GetLugarMulta() => _extractorMulta.GetLugarMulta();
    private string GetMarcaVehiculo() => _extractorMulta.GetMarcaVehiculo();
    private string GetClaseVehiculo() => _extractorMulta.GetClaseVehiculo();
    private string GetModeloVehiculo() => _extractorMulta.GetModeloVehiculo();
    private decimal GetImporte() => _extractorMulta.GetImporte();
    public MultaDTO GetDatos()
    {
      return new MultaDTO
      {
        Vehiculo = new Vehiculo
        {
          Matricula = GetMatricula(),
          Marca = GetMarcaVehiculo(),
          Clase = GetClaseVehiculo(),
          Modelo = GetModeloVehiculo()
        },
        FechaMulta = GetFechaMulta(),
        ExpedienteMulta = GetExpediente(),
        Emisor = GetEmisor(),
        MotivoMulta = GetMotivoMulta(),
        LugarMulta = GetLugarMulta(),
        ImportMulta = GetImporte()
      };
    }
  }
}
