using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultasExporter;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PdfController : ControllerBase
  {
    [HttpPost("upload")]
    public async Task<IActionResult> UploadPdf(IFormFile pdf, [FromForm] string key)
    {
      // Verifica si el archivo PDF fue proporcionado
    /*  if (pdf == null || pdf.Length == 0)
      {
        return BadRequest("No se proporcionó un archivo PDF.");
      }
    */
      // Verifica si la clave fue proporcionada
   /*   if (string.IsNullOrEmpty(key) || key!="wiber")
      {
        return Ok();
      }*/

      byte[] fileBytes;
      using (var memoryStream = new MemoryStream())
      {
        await pdf.CopyToAsync(memoryStream);
        fileBytes = memoryStream.ToArray();
      }

      var extractor = new ExtractorMulta(fileBytes);
       

      return Ok(new {info= extractor.GetDatos()});
    }

  }
}
