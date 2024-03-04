
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rainfall.SharedLibrary
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Code
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("value is not valid")]
        InvalidValue = 47350,

        /// <summary>
        /// 
        /// </summary>
        [Description("field is required")]
        RequiredField = 48215,

        [Description("invalid route parameters")]
        InvalidRouteParameters = 40405,

        [Description("record not found")]
        RecordNotFound = 40410,
    }

    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static string ToInt32String<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            return Convert.ToInt32(enumValue).ToString();
        }

        public static string ToSnakeCase<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            // TODO: extension method
            // https://gist.github.com/vkobel/d7302c0076c64c95ef4b
            return string.Concat(enumValue.ToString().Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static string GetEnumNameByValue<TEnum>(int value) where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return Enum.GetName(typeof(TEnum), value);
            }
            else
            {
                return null;
            }
        }
    }

    //public class IdInRouteNotValidException : BaseException
    //{
    //    public IdInRouteNotValidException(string message)
    //        : base(message)
    //    {
    //        var fieldName = GetFieldName(message);

    //        Errors.Add(new Error(Code.InvalidRouteParameters));
    //    }

    //    private string GetFieldName(string message)
    //    {
    //        var x1 = message.IndexOf("'");
    //        var x2 = message.LastIndexOf("'") + 1;

    //        var fieldName = message.Substring(x1, x2);

    //        return fieldName;
    //    }
    //}
}
