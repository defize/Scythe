namespace Defize.Scythe
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ApplicationEntryBinding<T, T2>
    {
        internal ApplicationEntryBinding(ArgumentMapper<T2> mapper)
        {

        }

        public ApplicationEntryBindingAliases To(Expression<Func<T, Action<T2>>> binding)
        {
            Console.WriteLine(binding.Body);
            DetermineMethod(binding);
            return new ApplicationEntryBindingAliases();
        }

        private void DetermineMethod(Expression<Func<T, Action<T2>>> binding)
        {
            var ue = (UnaryExpression)binding.Body;

            var mce = (MethodCallExpression)ue.Operand;

            var argument = (ConstantExpression)mce.Arguments[2];
            var methodInfo = (MethodInfo)argument.Value;

            if (methodInfo.DeclaringType != typeof(T) || methodInfo.GetParameters().Length != 1 || methodInfo.GetParameters()[0].ParameterType != typeof(T2))
            {
                throw new Exception();
            }
            Console.WriteLine(methodInfo.Name);
        }
    }
}
