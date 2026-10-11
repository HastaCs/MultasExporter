using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultasExporter.Utils
{
  internal static class Utils
  {
    public static string GetDisplayName(this Enum enumValue)
    {
      var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
      if (memberInfo.Length > 0)
      {
        var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
        if (attrs.Length > 0)
        {
          return ((DisplayAttribute)attrs[0]).Name;
        }
      }
      return enumValue.ToString(); // Si no hay DisplayAttribute, devuelve el nombre del enum
    }
    public static EmisorDTO ToEmisorDto(this Emisor enumValue)
    {
      return new EmisorDTO
      {
        Id = enumValue,
        Nombre = enumValue.GetDisplayName()
      };
    }
  }
}
