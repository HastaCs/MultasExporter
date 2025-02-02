using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    DipMalaga = 50,

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
    AytoSevilla = 46
  }
}
