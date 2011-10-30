namespace Defize.Scythe
{
    using System;

    public class StringMapping
    {
        public StringMapping WithAliases(params string[] aliases)
        {
            return this;
        }

        public StringMapping Default(string defaultValue)
        {
            return this;
        }

        public StringMapping Validate(params Func<StringValidator, bool>[] validators)
        {
            return this;
        }
    }
}