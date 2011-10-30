namespace Defize.Scythe
{
    using System;

    public class StringValidator
    {
        public bool Length(uint min = 0U, uint max = 0U)
        {
            return true;
        }

        public bool Matches(string match)
        {
            return false;
        }

        public bool Custom(Func<string, bool> custom)
        {
            return false;
        }
    }
}