namespace Defize.Scythe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScytheApplication<TApplication>
    {
        private readonly List<ApplicationVerb> _verbs = new List<ApplicationVerb>();

        protected IEnumerable<ApplicationVerb> Verbs
        {
            get { return _verbs; }
        }

        public ApplicationVerb<TApplication, TConfiguration> Bind<TConfiguration>(ScytheArgumentMapper<TConfiguration> argumentMapper)
        {
            var verb = new ApplicationVerb<TApplication, TConfiguration>(argumentMapper);
            _verbs.Add(verb);

            return verb;
        }

        public void Run(string[] arguments)
        {
            var verbName = arguments[0];
            var matches = from v in Verbs where v.SatisfiesVerb(verbName) select v;

            var verbArguments = arguments.Skip(1).ToArray();

            ArgumentMapperException failure = null;
            foreach (var match in matches)
            {
                try
                {
                    match.Run(verbArguments);
                    failure = null;
                    break;
                }
                catch (ArgumentMapperException ex)
                {
                    if (failure == null)
                    {
                        failure = ex;
                    }
                }
            }

            if (failure != null)
            {
                // show error
            }
        }
    }
}
