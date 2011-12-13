namespace Defize.Scythe
{
    public abstract class ApplicationVerb
    {
        public abstract void Run(string[] arguments);

        public bool SatisfiesVerb(string verb)
        {
            return false;
        }
    }
}
