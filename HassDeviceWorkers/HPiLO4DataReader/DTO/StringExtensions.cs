using System;
using System.ComponentModel.DataAnnotations;

namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public static class StringExtensions
    {
        public static int? ParseToIntWithNA(this string fromString) => int.TryParse(fromString, out var result) ? result : null;

        public static T ParseToEnumWithDefault<T>(this string fromString) where T : struct, Enum
        {
            // trivial parse
            var parsed = Enum.TryParse(fromString, out T enumVal);

            if (parsed)
                return enumVal;

            // parse by Display attribute
            var names = Enum.GetValues<T>();
            foreach (var name in names)
            {
                var fieldInfo = name.GetType().GetField(name.ToString());
                var descriptionAttributes = fieldInfo!.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes!.Length != 0 && descriptionAttributes[0].Description == fromString)
                    return name;
            }

            // nothing found --> return default...
            return default;
        }
    }
}
