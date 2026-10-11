using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultasApi.Modelos;
using MultasExporter;
using MultasExporter.Modelos;
using System.Text.Unicode;
using API.Services;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PdfController : ControllerBase
  {
    private readonly RateLimitService _rateLimitService;

    public PdfController(RateLimitService rateLimitService)
    {
      _rateLimitService = rateLimitService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadPdf(IFormFile pdf)
    {
      try
      {
        // Verifica si el archivo PDF fue proporcionado
        if (pdf == null || pdf.Length == 0)
        {
          return BadRequest("No se proporcionó un archivo PDF.");
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
        Console.WriteLine(er.ToString());
        return BadRequest(new { error = er.Message });

      }
    }

    [HttpPost("uploadfromweb")]
    public async Task<IActionResult> UploadPdfFromWeb(IFormFile pdf)
    {
      try
      {
        // Obtener la IP del cliente
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        if (string.IsNullOrEmpty(ipAddress))
        {
          return BadRequest("No se pudo identificar la dirección IP.");
        }

        // Verificar el límite de peticiones
        if (!_rateLimitService.IsAllowed(ipAddress))
        {
          return StatusCode(429, new { error = "Has excedido el límite de 10 peticiones por día. Intenta de nuevo mañana." });
        }

        // Verifica si el archivo PDF fue proporcionado
        if (pdf == null || pdf.Length == 0)
        {
          return BadRequest("No se proporcionó un archivo PDF.");
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
          await pdf.CopyToAsync(memoryStream);
          fileBytes = memoryStream.ToArray();
        }

        var extractor = new ExtractorMulta(fileBytes);

        return Ok(new { info = await extractor.GetDatosAsync(), remainingRequests = _rateLimitService.GetRemainingRequests(ipAddress) });
      }
      catch (Exception er)
      {
        Console.WriteLine(er.ToString());
        return BadRequest(new { error = er.Message });
      }
    }

    [HttpPost("uploadzip")]
    public async Task<IActionResult> UploadPdfZip(IFormFile zip)
    {
      // Verifica si el archivo PDF fue proporcionado
      if (zip == null || zip.Length == 0)
      {
        return BadRequest("No se proporcionó un archivo ZIP.");
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

          multasBien.Add(new MultaDTOZip(multa.NombreFichero, datosMulta));
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
