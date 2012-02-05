namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Reflection;

    public class ScytheArgumentMapper<TConfiguration>
        where TConfiguration : new()
    {
        private readonly List<IMapping<TConfiguration>> _mappings;

        public ScytheArgumentMapper()
        {
            _mappings = new List<IMapping<TConfiguration>>();
        }

        internal IEnumerable<IMapping<TConfiguration>> Mappings
        {
            get { return _mappings; }
        }

        public StringMapping<TConfiguration> Map(Expression<Func<TConfiguration, string>> property)
        {
            var propertyInfo = GetProperty(property);

            var mapping = new StringMapping<TConfiguration>(propertyInfo);
            _mappings.Add(mapping);

            return mapping;
        }

        private PropertyInfo GetProperty<TProperty>(Expression<Func<TConfiguration, TProperty>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;

            return (PropertyInfo)memberExpression.Member;
        }
    }
}
