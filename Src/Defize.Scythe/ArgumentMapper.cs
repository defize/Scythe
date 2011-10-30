namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;

    public class ArgumentMapper<T>
    {
        public StringMapping Map(Expression<Func<T, string>> property)
        {
            return new StringMapping();
        }
    }
}