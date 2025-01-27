using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultasApi.Modelos;
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

        return Ok("llego");
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
      var multasPdf = Utils.ExtractPdfsFromZip(ficheroZip);
      var multasBien = new List<MultaDTO>();
      var multasMal = new List<MultaErronea>();

      foreach (var multa in multasPdf)
      {
        var res = new ExtractorMulta(multa.Contenido);
        try
        {

          multasBien.Add(await res.GetDatosAsync());
        }
        catch (Exception er)
        {
          multasMal.Add(new MultaErronea(multa.NombreFichero, er.Message));
        }
      }

      return Ok(new { info = multasBien, errores = multasMal });
    }

  }
}
