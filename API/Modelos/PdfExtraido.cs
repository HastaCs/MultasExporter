namespace API.Modelos
{
  public class PdfExtraido
  {
    public string NombreFichero { get; set; } // Nombre del archivo
    public byte[] Contenido { get; set; } // Contenido en bytes
  }
}
