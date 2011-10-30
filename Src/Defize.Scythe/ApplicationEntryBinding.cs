namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;

    public class ApplicationEntryBinding<T, T2>
    {
        internal ApplicationEntryBinding(ArgumentMapper<T2> mapper)
        {

        }

        public ApplicationEntryBindingAliases To(Expression<Func<T, Action<T2>>> binding)
        {
            Console.WriteLine(binding.Body);
            return new ApplicationEntryBindingAliases();
        }
    }
}
