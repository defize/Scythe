namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;

    public class ApplicationVerb<TApplication, TConfiguration> : ApplicationVerb
    {
        private readonly ScytheArgumentMapper<TConfiguration> _mapper;

        internal ApplicationVerb(ScytheArgumentMapper<TConfiguration> mapper)
        {
            _mapper = mapper;
        }

        public override void Run(string[] arguments)
        {
            _mapper.Apply(arguments);
        }

        public ApplicationVerb<TApplication, TConfiguration> To(Expression<Action<TApplication, TConfiguration>> action)
        {
            return this;
        }

        public ApplicationVerb<TApplication, TConfiguration> WithAliases(params string[] aliases)
        {
            return this;
        }
    }
}
