using System.Text.Json.Serialization;

namespace Penca53.Variant
{
    [JsonConverter(typeof(VariantConverter))]
    public struct Variant<T1, T2>
    {
        public static Variant<T1, T2> Empty
        {
            get => new Variant<T1, T2>();
        }

        public VariantType Type { get; private set; }
        private object _variant { get; set; }

        public Variant(T1 item) : this(item, VariantType.T1) { }
        public Variant(T2 item) : this(item, VariantType.T2) { }
        private Variant(object variant, VariantType type)
        {
            _variant = variant;
            Type = type;
        }

        public T1 GetT1()
            => Get<T1>();
        public T2 GetT2()
            => Get<T2>();

        public bool TryGetT1(out T1 t1)
        {
            t1 = default;

            if (Type == VariantType.T1)
            {
                t1 = Get<T1>();
                return true;
            }

            return false;
        }
        public bool TryGetT2(out T2 t2)
        {
            t2 = default;

            if (Type == VariantType.T2)
            {
                t2 = Get<T2>();
                return true;
            }

            return false;
        }

        public bool Is<T>()
            => _variant is T;

        public int GetIndex()
            => (int)Type - 1;

        public static implicit operator Variant<T1, T2>(T1 item)
            => new Variant<T1, T2>(item);
        public static implicit operator Variant<T1, T2>(T2 item)
            => new Variant<T1, T2>(item);

        public static explicit operator T1(Variant<T1, T2> item)
            => item.Get<T1>();
        public static explicit operator T2(Variant<T1, T2> item)
            => item.Get<T2>();

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Variant<T1, T2> v = (Variant<T1, T2>)obj;

                if (Type == VariantType.NONE && v.Type == VariantType.NONE)
                {
                    return true;
                }
                else
                {
                    return _variant.Equals(v._variant);
                }
            }
        }

        public override int GetHashCode()
            => _variant?.GetHashCode() ?? 0;
        public override string ToString()
            => _variant?.ToString() ?? "";

        private T Get<T>()
            => (T)_variant;
    }

    public enum VariantType
    {
        NONE,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8
    }
}

