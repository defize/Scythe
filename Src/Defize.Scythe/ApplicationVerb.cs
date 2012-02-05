namespace Defize.Scythe
{
    using System;
    using System.Collections.Generic;

    public abstract class ApplicationVerb<TApplication>
    {
        private readonly HashSet<string> _aliases;

        protected ApplicationVerb()
        {
            _aliases = new HashSet<string>();
        }

        internal abstract VerbResult Apply(TApplication application, RawArguments arguments);

        public bool SatisfiesVerbName(string verbName)
        {
            return _aliases.Contains(verbName.ToUpperInvariant());
        }

        protected void AddAlias(string alias)
        {
            _aliases.Add(alias.ToUpperInvariant());
        }
    }
}
