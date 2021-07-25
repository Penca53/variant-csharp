using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

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

        private class VariantConverterInner<T1, T2> : JsonConverter<Variant<T1, T2>>
        {
            public VariantConverterInner(JsonSerializerOptions options) { }

            public override Variant<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                Variant<T1, T2> variant = new Variant<T1, T2>();

                reader.Read();
                VariantIndex index = (VariantIndex)JsonSerializer.Deserialize<int>(ref reader, options);

                reader.Read();
                switch (index)
                {
                    case VariantIndex.NONE:
                        reader.Read();
                        break;
                    case VariantIndex.T1:
                        T1 a = JsonSerializer.Deserialize<T1>(ref reader, options);
                        variant = a;
                        break;
                    case VariantIndex.T2:
                        variant = JsonSerializer.Deserialize<T2>(ref reader, options);
                        break;
                }

                reader.Read();

                return variant;
            }

            public override void Write(Utf8JsonWriter writer, Variant<T1, T2> variant, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                writer.WriteNumber(nameof(variant.Index), (int)variant.Index);

                writer.WritePropertyName("Item");
                switch (variant.Index)
                {
                    case VariantIndex.NONE:
                        writer.WriteNullValue();
                        break;
                    case VariantIndex.T1:
                        JsonSerializer.Serialize<T1>(writer, variant.GetT1(), options);
                        break;
                    case VariantIndex.T2:
                        JsonSerializer.Serialize<T2>(writer, variant.GetT2(), options);
                        break;
                }

                writer.WriteEndObject();
            }
        }
    }
}