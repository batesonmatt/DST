using DST.Models.Extensions;

namespace DST.Models.DataLayer.Query
{
    public abstract class BaseFilter : IFilter
    {
        public abstract string Id { get; }

        public abstract string Value { get; }

        public virtual bool EqualsSeo(string value)
        {
            return value.ToKebabCase().EqualsExact(Value);
        }

        public abstract bool IsDefault();

        public abstract void Reset();

        public override string ToString() => Value;
    }
}
