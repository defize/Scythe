namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;

    public class ScytheArgumentMapper<TConfiguration>
    {
        public StringMapping Map(Expression<Func<TConfiguration, string>> property)
        {
            return new StringMapping();
        }
    }
}
