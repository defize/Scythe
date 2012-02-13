namespace Defize.Scythe
{
    using System;
    using System.Reflection;

    public class StringMapping<TConfiguration> : MappingBase<string, TConfiguration>
    {
        public StringMapping(PropertyInfo property)
            : base(property)
        { }

        public StringMapping<TConfiguration> WithAliases(params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                AddAlias(alias);
            }

            return this;
        }

        public StringMapping<TConfiguration> AtPosition(int position)
        {
            SetPosition(position);

            return this;
        }

        public StringMapping<TConfiguration> Default(string defaultValue)
        {
            SetDefault(defaultValue);

            return this;
        }

        public StringMapping<TConfiguration> Validate(params Action<StringValidator>[] validators)
        {
            return this;
        }

        protected override string ConvertValue(string valueString)
        {
            return valueString;
        }
    }

    public class IntegerMapping<TConfiguration> : MappingBase<int, TConfiguration>
    {
        public IntegerMapping(PropertyInfo property)
            : base(property)
        { }

        public IntegerMapping<TConfiguration> WithAliases(params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                AddAlias(alias);
            }

            return this;
        }

        public IntegerMapping<TConfiguration> AtPosition(int position)
        {
            SetPosition(position);

            return this;
        }

        public IntegerMapping<TConfiguration> Default(int defaultValue)
        {
            SetDefault(defaultValue);

            return this;
        }

        public IntegerMapping<TConfiguration> Validate(params Action<NumberValidator<int>>[] validators)
        {
            return this;
        }

        protected override int ConvertValue(string valueString)
        {
            return int.Parse(valueString);
        }
    }
}