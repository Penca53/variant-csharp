using System.Text.Json.Serialization;

namespace Penca53.Variant
{
    public interface IVariantHolder
    {
        bool Is<T>();
    }

    public class VariantHolder<T> : IVariantHolder
    {
        public T Item { get; }

        public VariantHolder(T item)
            => Item = item;

        public bool Is<U>() => typeof(U) == typeof(T);
    }

    [JsonConverter(typeof(VariantConverter))]
    public class Variant<T1, T2>
    {
        public VariantIndex Index { get; private set; }
        private IVariantHolder _variant { get; set; }

        public Variant()
            => Index = VariantIndex.NONE;
        public Variant(T1 item) : this(new VariantHolder<T1>(item), VariantIndex.T1) { }
        public Variant(T2 item) : this(new VariantHolder<T2>(item), VariantIndex.T2) { }
        private Variant(IVariantHolder variant, VariantIndex index)
        {
            _variant = variant;
            Index = index;
        }

        public T1 GetT1()
            => Get<T1>();
        public T2 GetT2()
            => Get<T2>();
        public bool Is<T>()
            => _variant.Is<T>();

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
            if (obj == null || !(obj is Variant<T1, T2>))
            {
                return false;
            }

            Variant<T1, T2> other = (Variant<T1, T2>)obj;

            if (Index != other.Index)
            {
                return false;
            }

            switch (Index)
            {
                case VariantIndex.NONE:
                    return true;
                case VariantIndex.T1:
                    return Get<T1>().Equals(other.Get<T1>());
                case VariantIndex.T2:
                    return Get<T2>().Equals(other.Get<T2>());
                default:
                    return false;
            }
        }


        public override int GetHashCode()
        {
            switch (Index)
            {
                case VariantIndex.NONE:
                    return 0;
                case VariantIndex.T1:
                    return Get<T1>().GetHashCode();
                case VariantIndex.T2:
                    return Get<T2>().GetHashCode();
                default:
                    return 0;
            }
        }

        private T Get<T>() => ((VariantHolder<T>)_variant).Item;
    }

    public enum VariantIndex
    {
        NONE = -1,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
    }
}

