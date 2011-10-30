namespace Defize.Scythe
{
    public class ApplicationEntry<T>
    {
        public ApplicationEntryBinding<T, T2> Bind<T2>(ArgumentMapper<T2> mapper)
        {
            return new ApplicationEntryBinding<T, T2>(mapper);
        }

        public void Run(string[] arguments)
        {
            
        }
    }
}