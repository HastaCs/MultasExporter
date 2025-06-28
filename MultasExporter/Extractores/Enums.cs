using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultasExporter
{
  public enum Emisor
  {

    [Display(Name = "(DGT)Centro de tratamiento de denuncias automatizadas")]
    Dgt = 4,

    [Display(Name = "Ayuntamiento de Alicante")]
    AytoAlicante = 16,

    [Display(Name = "Ayuntamiento de Granada")]
    AytoGranada = 14,

    [Display(Name = "Ayuntamiento de Valencia")]
    AytoValencia = 47,

    [Display(Name = "Diputación provincial de Málaga")]
    DipMalaga = 6,

    [Display(Name = "Diputación provincial de Cádiz")]
    DipCadiz = 37,

    [Display(Name = "Suma Diputación de Alicante")]
    SumaAlicante = 8,

    [Display(Name = "Ayuntamiento de Madrid")]
    AytoMadrid = 42,

    [Display(Name = "Ayuntamiento de Fuengirola")]
    AytoFuengirola = 21,

    [Display(Name = "Ayuntamiento de Ibiza")]
    AytoIbiza = 51,

    [Display(Name = "Ayuntamiento de Cádiz")]
    AytoCadiz = 65,

    [Display(Name = "Ayuntamiento de Murcia")]
    AytoMurcia = 56,

    [Display(Name = "Ayuntamiento de Sevilla")]
    AytoSevilla = 46,

    [Display(Name = "Ayuntamiento de Córdoba")]
    AytoCordoba = 104,

    [Display(Name = "Ayuntamiento de Málaga")]
    AytoMalaga = 50,

    [Display(Name = "Agencia tributaria Illes Balears")]
    Atib = 140,

    [Display(Name = "Ports Balears")]
    PortsBalears = 87,

    [Display(Name = "Servei catalá de Tránsit")]
    ServeiCatalaTransit = 11,

    [Display(Name = "Diputación de Sevilla")]
    DipSevilla = 124,
  }
}
