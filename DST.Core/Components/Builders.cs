namespace DST.Core.Components
{
    public class Builders
    {
        public static readonly IComponentsBuilder StandardComponents
            = ComponentsBuilderFactory.Create(RotationType.Full);

        public static readonly IComponentsBuilder ModifiedComponents
            = ComponentsBuilderFactory.Create(RotationType.Signed);
    }
}
