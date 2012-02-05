namespace Defize.Scythe
{
    internal interface IMapping<in TConfiguration>
    {
        MappingResult Apply(TConfiguration configuration, RawArguments arguments);
    }
}