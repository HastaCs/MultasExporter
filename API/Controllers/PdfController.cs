using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultasExporter;
using MultasExporter.Modelos;

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
        return UnprocessableEntity(new { error =er.Message });

      }
    }

    [HttpPost("uploadzip")]
    public async Task<IActionResult> UploadPdfZip(IFormFile pdf, [FromForm] string key)
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

      byte[] ficheroZip;
      using (var memoryStream = new MemoryStream())
      {
        await pdf.CopyToAsync(memoryStream);
        ficheroZip = memoryStream.ToArray();
      }
      var multas = Utils.ExtractPdfsFromZip(ficheroZip);
      var resultado = new List<MultaDTO>();
      foreach (var multa in multas)
      {
        var res = new ExtractorMulta(multa.Contenido);
        resultado.Add(await res.GetDatosAsync());
      }



      return Ok(new { info = resultado });
    }

  }
}
