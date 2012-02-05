namespace Defize.Scythe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScytheApplication<TApplication>
    {
        private readonly List<ApplicationVerb<TApplication>> _verbs = new List<ApplicationVerb<TApplication>>();

        protected IEnumerable<ApplicationVerb<TApplication>> Verbs
        {
            get { return _verbs; }
        }

        public ApplicationVerb<TApplication, TConfiguration> Bind<TConfiguration>(ScytheArgumentMapper<TConfiguration> argumentMapper)
            where TConfiguration : new()
        {
            var verb = new ApplicationVerb<TApplication, TConfiguration>(argumentMapper);
            _verbs.Add(verb);

            return verb;
        }

        public void Run(TApplication application, string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
            {
                throw new NotImplementedException();
            }

            var verbName = arguments[0];
            
            var matchingVerbs = _verbs.Where(v => v.SatisfiesVerbName(verbName)).ToList();
            if (matchingVerbs.Count == 0)
            {
                throw new NotImplementedException();
            }

            var verbArguments = arguments.Skip(1).ToArray();
            var parsedArguments = RawArguments.Parse(verbArguments);

            var results = new List<VerbResult>();
            foreach (var verb in matchingVerbs)
            {
                var result = verb.Apply(application, parsedArguments);
                if (result.IsValid)
                {
                    result.Execute();
                    return;
                }

                results.Add(result);
            }

            throw new NotImplementedException();
        }
    }
}
