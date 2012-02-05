namespace Defize.Scythe
{
    using System.Reflection;

    public class StringMapping<TConfiguration> : MappingBase<string, TConfiguration>
    {
        public StringMapping(PropertyInfo property)
            : base(property)
        { }

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

        protected override int ConvertValue(string valueString)
        {
            return int.Parse(valueString);
        }
    }
}