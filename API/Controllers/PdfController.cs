using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultasApi.Modelos;
using MultasExporter;
using MultasExporter.Modelos;
using System.Text.Unicode;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PdfController : ControllerBase
  {
    [HttpPost("upload")]
    public async Task<IActionResult> UploadPdf(IFormFile pdf, [FromForm] string key)
    {
      try
      {

       
        // Verifica si el archivo PDF fue proporcionado
        if (pdf == null || pdf.Length == 0)
        {
          return BadRequest("No se proporcionó un archivo PDF.");
        }

        // Verifica si la clave fue proporcionada
        if (string.IsNullOrEmpty(key) || key != "wiber")
        {
          return Ok();
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
          await pdf.CopyToAsync(memoryStream);
          fileBytes = memoryStream.ToArray();
        }
       
        var extractor = new ExtractorMulta(fileBytes);
       
        return Ok(new { info = await extractor.GetDatosAsync() });
      }
      catch (Exception er)
      {
        return UnprocessableEntity(new { error = er.Message });

      }
    }

    [HttpPost("uploadzip")]
    public async Task<IActionResult> UploadPdfZip(IFormFile zip, [FromForm] string key)
    {
      // Verifica si el archivo PDF fue proporcionado
      if (zip == null || zip.Length == 0)
      {
        return BadRequest("No se proporcionó un archivo ZIP.");
      }

      // Verifica si la clave fue proporcionada
      if (string.IsNullOrEmpty(key) || key != "wiber")
      {
        return Ok();
      }

      byte[] ficheroZip;
      using (var memoryStream = new MemoryStream())
      {
        await zip.CopyToAsync(memoryStream);
        ficheroZip = memoryStream.ToArray();
      }
      var multasPdf = Utils.ExtractPdfsFromZip(ficheroZip);
      var multasBien = new List<MultaDTOZip>();
      var multasMal = new List<MultaErronea>();

      foreach (var multa in multasPdf)
      {
        try
        {
          var res = new ExtractorMulta(multa.Contenido);
          var datosMulta = await res.GetDatosAsync();

          multasBien.Add(new MultaDTOZip(multa.NombreFichero,datosMulta));
        }
        catch (Exception er)
        {
          multasMal.Add(new MultaErronea(multa.NombreFichero, er.Message));
        }
      }

      return Ok(new { info = multasBien, errores = multasMal });
    }


    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
      return Ok();
    }

  }
}
