# MultasExporter

[![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/platform-linux%20%7C%20windows%20%7C%20docker-lightgrey)](#docker)
[![Demo](https://img.shields.io/badge/demo-online-brightgreen)](https://www.multasproapi.com/probar-api/)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](#crear-un-nuevo-extractor)

Lector de PDFs de **multas de tráfico (denuncias sancionadoras)** que extrae los datos y los
devuelve como **JSON**. Soporta documentos emitidos por distintos organismos (DGT, ayuntamientos,
diputaciones, agencias autonómicas, etc.).

A partir de un PDF obtienes un objeto homogéneo con el **vehículo**, la **fecha**, el
**expediente**, el **organismo emisor**, el **motivo**, el **lugar** y el **importe**, sin
preocuparte por el formato concreto de cada administración.

El proyecto ofrece tres formas de uso:

- Una **librería** (`MultasExporter`) con todo el motor de extracción.
- Una **API REST** (`API`) para subir PDFs o ZIPs y obtener el JSON por HTTP.
- Una **aplicación de consola** (`MultasConsola`) usada como herramienta de desarrollo.

## Demo

Prueba un PDF:

**👉 [https://www.multasproapi.com/probar-api/](https://www.multasproapi.com/probar-api/)**

Ejemplo de respuesta para un PDF de la DGT:

```json
{
  "info": {
    "vehiculo": {
      "matricula": "1234ABC",
      "marca": "SEAT",
      "clase": "TURISMO",
      "modelo": "LEON"
    },
    "fecha": "2024-01-15T10:30:00",
    "expediente": "0123456789",
    "emisor": {
      "nombre": "(DGT)Centro de tratamiento de denuncias automatizadas",
      "id": 4
    },
    "motivo": "Estacionar en zona prohibida",
    "lugar": "Calle Mayor, 1",
    "importe": 200.0
  }
}
```

## Crear un nuevo extractor

Un **extractor** es la clase que sabe leer una plantilla de multa concreta. Hay uno por organismo
o por tipo de documento, y todos viven en `MultasExporter/Extractores/DeTexto/`.

### Cómo funciona la extracción

El motor (`ExtractorMulta`) no "entiende" el PDF: trabaja por **coordenadas**. Cada extractor
define uno o varios rectángulos (`PdfRectangle`) sobre la página y lee el texto que cae dentro
de ellos.

1. Al abrir un PDF, se recorre cada extractor y se llama a su método `Match()`.
   `Match()` comprueba un texto fijo del documento (por ejemplo el nombre del organismo) para
   saber si esa plantilla es la correcta. Se usa el **primer** extractor que devuelve `true`.
2. Una vez elegido, se llaman sus métodos `Get*()` y cada uno devuelve un campo del `MultaDTO`.

Para encontrar las coordenadas de esos rectángulos, lo más cómodo es **marcarlos como
anotaciones** en el PDF y dejar que la consola genere el código.

### 1. Marca los rectángulos en el PDF

Con [PDF-XChange Editor](https://www.pdf-xchange.com/product/pdf-xchange-editor):

1. Abre un PDF de ejemplo del organismo.
2. Añade una anotación de tipo **Rectangle** (herramienta de comentarios) cubriendo cada campo que
   quieras extraer.
3. En las propiedades de cada anotación, escribe en **Asunto / Subject** el nombre del campo.
   Por convención usa estos nombres:

   | Subject de la anotación | Método que rellena   | Dato                             |
   |-------------------------|----------------------|----------------------------------|
   | `match`, `match2`, ...  | `Match()`            | textos fijos que identifican el PDF |
   | `expediente`            | `GetExpediente()`    | nº de expediente                 |
   | `fecha`                 | `GetFechaMulta()`    | fecha de la denuncia             |
   | `matricula`             | `GetMatricula()`     | matrícula del vehículo           |
   | `importe`               | `GetImporte()`       | importe en €                     |
   | `marca`                 | `GetMarcaVehiculo()` | marca del vehículo               |
   | `modelo`                | `GetModeloVehiculo()`| modelo del vehículo              |
   | `clase`                 | `GetClaseVehiculo()` | clase del vehículo               |
   | `motivo`                | `GetMotivoMulta()`   | hecho denunciado                 |
   | `lugar`                 | `GetLugarMulta()`    | lugar de la denuncia             |

4. Guarda el PDF.

> MultasExporter lee el **Subject** (`Subj`) de cada anotación como nombre del rectángulo.

**Consejos para marcar bien los rectángulos:**

- Un rectángulo debe contener **completamente** las palabras del dato; si una palabra queda fuera,
  se pierde (el motor solo lee las palabras totalmente incluidas).
- Marca el rectángulo con un poco de margen, pero sin invadir campos vecinos.
- La anotación `match` es la más importante: debe cubrir un texto que **siempre** aparezca y que
  sea exclusivo de esa plantilla.

> **Recomendado:** usa **3 o 4 anotaciones `match`** (`match`, `match2`, `match3`, `match4`)
> tomadas de **palabras y zonas distintas del PDF** (cabecera, pie, cuerpo...) y compruébalas
> **todas** en `Match()`. Combinar varios textos en distintas localizaciones reduce mucho los
> falsos positivos.

### 2. Genera las coordenadas con la consola

Ejecuta la consola apuntando a la carpeta con los PDFs anotados:

```bash
dotnet run --project MultasConsola -- /ruta/a/los/pdfs
```

Por cada anotación, la consola imprime el nombre y genera una línea con el `PdfRectangle`
correspondiente. El resultado se guarda en un fichero **`<nombre-del-pdf>_rectangulos.txt`** junto
al PDF.

Ejemplo de salida:

```csharp
var recmatch=new PdfRectangle(52, 734, 250, 750);
var recexpediente=new PdfRectangle(119.599874, 696.103341, 214.466545, 714.48107);
var recfecha=new PdfRectangle(454.404537, 691.922286, 536.914195, 713.730958);
var recmatricula=new PdfRectangle(101.228734, 647.721159, 240.849325, 659.72294);
var recimporte=new PdfRectangle(181.086957, 680.726058, 240.849325, 693.102896);
```

> Por defecto la consola lee solo la **página 1**. Si los datos están en otra página, cambia el
> `1` de la llamada `SacaRectangulos(file, 1)` en `MultasConsola/Program.cs`.

### 3. (Opcional) Añade el organismo al enum `Emisor`

Si el organismo aún no existe, añádelo con su nombre para mostrar en
`MultasExporter/Extractores/Emisor.cs`:

```csharp
[Display(Name = "Ayuntamiento de Ejemplo")]
AytoEjemplo = 999,
```

### 4. Crea la clase extractora

Crea un archivo nuevo en `MultasExporter/Extractores/DeTexto/` (por ejemplo
`Ayt_Ejemplo_Texto.cs`). Hereda de `ExtractorBaseTexto`, implementa `IExtractorTexto` y usa
`GetDatos(rect)` para leer el texto de cada rectángulo. Copia dentro de cada método el
`PdfRectangle` que generó la consola.

```csharp
using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System.Globalization;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  public class Ayt_Ejemplo_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    // Identifica la plantilla combinando varias palabras de zonas distintas del PDF.
    public bool Match()
    {
      var rec_match = new PdfRectangle(52, 734, 250, 750);    // cabecera
      var rec_match2 = new PdfRectangle(34, 437, 280, 473);   // párrafo intermedio
      var rec_match3 = new PdfRectangle(300, 700, 520, 720);  // pie de página
      return GetDatos(rec_match) == "AYUNTAMIENTO DE EJEMPLO"
          && GetDatos(rec_match2).ToLower().Contains("no lleva aparejada")
          && GetDatos(rec_match3).Contains("EXPEDIENTE");
    }

    // Organismo emisor (debe existir en el enum Emisor).
    public EmisorDTO GetEmisor() => Emisor.AytoEjemplo.ToEmisorDto();

    public string GetExpediente()
    {
      var rec = new PdfRectangle(119.599874, 696.103341, 214.466545, 714.48107);
      return GetDatos(rec);
    }

    public DateTime GetFechaMulta()
    {
      var rec = new PdfRectangle(454.404537, 691.922286, 536.914195, 713.730958);
      var texto = GetDatos(rec).Trim();
      if (DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                 DateTimeStyles.None, out var fecha))
        return fecha;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec = new PdfRectangle(181.086957, 680.726058, 240.849325, 693.102896);
      var importe = GetDatos(rec).Replace("€", "").Trim();
      if (decimal.TryParse(importe, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        return result;
      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetMatricula()
    {
      var rec = new PdfRectangle(101.228734, 647.721159, 240.849325, 659.72294);
      return GetDatos(rec);
    }

    public string GetMarcaVehiculo()
    {
      var rec = new PdfRectangle(100.853813, 625.592874, 238.224877, 639.094878);
      return GetDatos(rec);
    }

    public string GetModeloVehiculo() => "";
    public string GetClaseVehiculo() => "";

    public string GetMotivoMulta()
    {
      var rec = new PdfRectangle(35.902761, 369.918776, 537.249018, 412.19886);
      return GetDatos(rec);
    }

    public string GetLugarMulta()
    {
      var rec = new PdfRectangle(33.207075, 553.159855, 277.003505, 592.963029);
      return GetDatos(rec);
    }
  }
}
```

Si algún campo no existe en la plantilla, devuelve una cadena vacía o `""` en su método.

### 5. Registra el extractor

Añádelo a la lista `_extractoresText` de `MultasExporter/MultaExporter.cs`:

```csharp
private List<IExtractorTexto> _extractoresText = new List<IExtractorTexto>
{
  new Ayt_Ejemplo_Texto(),
  // ... resto de extractores
};
```

El orden importa: los extractores se prueban de arriba a abajo y se usa el primero cuyo `Match()`
devuelva `true`. Coloca los formatos más específicos antes que los genéricos.

### 6. Prueba el extractor

Compila y ejecuta la consola apuntando a un PDF de ese organismo:

```bash
dotnet build MultasExporter.sln
dotnet run --project MultasConsola -- /ruta/a/los/pdfs
```

La consola imprime `Encontrado: <tipo de extractor>` cuando detecta la plantilla. Si no coincide
con el tuyo, revisa los rectángulos `match` (que cada texto sea el esperado). También puedes subir
el PDF a la [demo](#demo) para ver el JSON resultante.

## Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior.
- Opcional: Docker para ejecutar la API en contenedor.

## Compilar

```bash
dotnet restore MultasExporter.sln
dotnet build MultasExporter.sln -c Release
```

## API REST

Ejecutar en desarrollo:

```bash
dotnet run --project API
```

La documentación Swagger está disponible en `http://localhost:5091/swagger` cuando
`ASPNETCORE_ENVIRONMENT=Development`.

### Endpoints

| Método | Ruta                     | Descripción                                                        |
|--------|--------------------------|--------------------------------------------------------------------|
| POST   | `/api/pdf/upload`        | Sube un PDF (`pdf`) y devuelve sus datos.                          |
| POST   | `/api/pdf/uploadfromweb` | Sube un PDF (`pdf`) con límite de 10 peticiones/día por IP.        |
| POST   | `/api/pdf/uploadzip`     | Sube un ZIP (`zip`) con varios PDFs; devuelve válidos y errores.   |
| GET    | `/api/pdf/ping`          | Comprobación de estado.                                            |

Ejemplo con `curl`:

```bash
curl -F "pdf=@multa.pdf" http://localhost:5091/api/pdf/upload
```

Respuesta de `upload`:

```json
{
  "info": {
    "vehiculo": {
      "matricula": "1234ABC",
      "marca": "SEAT",
      "clase": "TURISMO",
      "modelo": "LEON"
    },
    "fecha": "2024-01-15T10:30:00",
    "expediente": "0123456789",
    "emisor": {
      "nombre": "(DGT)Centro de tratamiento de denuncias automatizadas",
      "id": 4
    },
    "motivo": "Estacionar en zona prohibida",
    "lugar": "Calle Mayor, 1",
    "importe": 200.0
  }
}
```

Respuesta de `uploadzip`:

```json
{
  "info": [ { "nombreFichero": "multa1.pdf", "vehiculo": { }, "fecha": "..." } ],
  "errores": [ { "nombreFichero": "multa2.pdf", "error": "No se ha podido detectar el formato" } ]
}
```

Si no se detecta el formato del PDF, la API responde `400` con
`{ "error": "<mensaje>" }`.

## Aplicación de consola

`MultasConsola` es una herramienta de desarrollo: recorre los PDFs de una carpeta y extrae los
rectángulos de las anotaciones (útil para construir nuevos extractores) o serializa los datos a
JSON.

```bash
dotnet run --project MultasConsola -- /ruta/a/la/carpeta
```

Si no se indica carpeta, usa el directorio de trabajo actual.

## Docker

```bash
docker build -t multasexporter .
docker run -p 8080:8080 multasexporter
```

La API queda accesible en `http://localhost:8080`.

## Estructura del proyecto

```
MultasExporter/
├─ Extractores/
│  ├─ DeTexto/           Un extractor por organismo (coordenadas sobre texto del PDF)
│  └─ Emisor.cs          Enum de organismos emisores
├─ Interfaces/           IDatos, IExtractorTexto, IFromTexto
├─ Modelos/              MultaDTO, EmisorDTO, Vehiculo
├─ Utils/                Utilidades (DisplayAttribute, etc.)
└─ MultaExporter.cs      Orquestador: detección de extractor y ensamblado del DTO

API/
├─ Controllers/          Endpoints HTTP
├─ Modelos/              DTOs de la API (PdfExtraido, MultaErronea)
├─ Services/             RateLimitService (límite por IP en memoria)
└─ Utils/                Extracción de PDFs desde ZIP
```

## Limitaciones

- La extracción se basa en **coordenadas fijas** por plantilla; si un organismo cambia su
  formato hay que añadir o ajustar su extractor.
- No procesa PDFs escaneados (OCR deshabilitado).
- El límite de peticiones de `uploadfromweb` es **en memoria**: se reinicia con la aplicación y
  no se comparte entre instancias.
