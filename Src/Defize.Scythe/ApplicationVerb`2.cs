namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;

    public class ApplicationVerb<TApplication, TConfiguration> : ApplicationVerb<TApplication>
        where TConfiguration : new()
    {
        private readonly ScytheArgumentMapper<TConfiguration> _mapper;

        private Expression<Action<TApplication, TConfiguration>> _actionExpression;

        internal ApplicationVerb(ScytheArgumentMapper<TConfiguration> mapper)
        {
            _mapper = mapper;
        }

        internal override VerbResult Apply(TApplication application, RawArguments arguments)
        {
            TConfiguration configuration;

            if (!TryApplyMappings(arguments, out configuration))
            {
                return new VerbResult { IsValid = false };
            }

            var compiledAction = _actionExpression.Compile();
            Action executeAction = () => compiledAction(application, configuration);

            return new VerbResult { IsValid = true, Execute = executeAction };
        }

        public ApplicationVerb<TApplication, TConfiguration> To(Expression<Action<TApplication, TConfiguration>> actionExpression)
        {
            _actionExpression = actionExpression;

            return this;
        }

        public ApplicationVerb<TApplication, TConfiguration> WithAliases(params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                AddAlias(alias);
            }

            return this;
        }

        private bool TryApplyMappings(RawArguments arguments, out TConfiguration configuration)
        {
            configuration = new TConfiguration();

            foreach (var mapping in _mapper.Mappings)
            {
                var result = mapping.Apply(configuration, arguments);
                if (!result.IsValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
