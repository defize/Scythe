namespace Defize.Scythe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class MappingBase<TMappingType, TConfiguration> : IMapping<TConfiguration>
    {
        private readonly ISet<string> _aliases;
        private readonly PropertyInfo _property;

        private bool _hasDefault;
        private TMappingType _defaultValue;
        private int? _position;

        internal MappingBase(PropertyInfo property)
        {
            _aliases = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            _property = property;

            _aliases.Add(property.Name);
        }

        internal PropertyInfo Property
        {
            get { return _property; }
        }

        public MappingResult Apply(TConfiguration configuration, RawArguments arguments)
        {
            string valueString = null;
            var hasValue = false;

            if (_position.HasValue)
            {
                string testValue;
                if (arguments.TryGetPositionalArgument(_position.Value, out testValue))
                {
                    valueString = testValue;
                    hasValue = true;
                }
            }
            else
            {
                var values = new List<string>();
                foreach (var alias in _aliases)
                {
                    string testValue;
                    if (arguments.TryGetNamedArgument(alias, out testValue))
                    {
                        values.Add(testValue);
                    }
                }

                if (values.Count == 1)
                {
                    valueString = values[0];
                    hasValue = true;
                }
            }

            if (!hasValue && !_hasDefault)
            {
                return new MappingResult
                           {
                               IsValid = false,
                               ErrorMessage = string.Format("Parameter '{0}' is required.", _aliases.First())
                           };
            }

            var value = hasValue ? ConvertValue(valueString) : _defaultValue;

            _property.SetValue(configuration, value, null);

            return new MappingResult { IsValid = true };
        }

        public MappingBase<TMappingType, TConfiguration> WithAliases(params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                _aliases.Add(alias);
            }

            return this;
        }

        public MappingBase<TMappingType, TConfiguration> AtPosition(int position)
        {
            _position = position;

            return this;
        }

        public MappingBase<TMappingType, TConfiguration> Default(TMappingType defaultValue)
        {
            _hasDefault = true;
            _defaultValue = defaultValue;

            return this;
        }

        public MappingBase<TMappingType, TConfiguration> Validate(params Func<StringValidator, bool>[] validators)
        {
            return this;
        }

        protected abstract TMappingType ConvertValue(string valueString);
    }
}