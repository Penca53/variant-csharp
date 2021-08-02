using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Penca53.JsonConverters;

namespace Penca53.Variant
{
    public class VariantConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Variant<,>))
            {
                return false;
            }

            return true;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            Type t1 = type.GetGenericArguments()[0];
            Type t2 = type.GetGenericArguments()[1];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(VariantConverterInner<,>).MakeGenericType(
                    new Type[] { t1, t2 }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class VariantConverterInner<T1, T2> : NonPublicConverter<Variant<T1, T2>>
        {
            public VariantConverterInner() : base() { }
            public VariantConverterInner(JsonSerializerOptions options) : base(options) { }
        }
    }
}