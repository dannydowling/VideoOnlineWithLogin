using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PreFlight_API.BLL
{
    public static class EnumHelpers
    //{
    //    public static IEnumerable<SelectListItem> GetItems(
    //        this Type enumType, int? selectedValue)
    //    {
    //        if (!typeof(Enum).IsAssignableFrom(enumType))
    //        {
    //            throw new ArgumentException("Type must be an enum");
    //        }

    //        var names = Enum.GetNames(enumType);
    //        var values = Enum.GetValues(enumType).Cast<int>();

    //        var items = names.Zip(values, (name, value) =>
    //                new SelectListItem
    //                {
    //                    Text = GetName(enumType, name),
    //                    Value = value.ToString(),
    //                    Selected = value == selectedValue
    //                }
    //            );
    //        return items;
    //    }
    { 
    public static string GetName<T>(Type type, string name) where T : Enum
        {
            var result = name;

            var attribute = type
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }
    }
}