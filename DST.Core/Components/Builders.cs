namespace DST.Core.Components
{
    public class Builders
    {
        public static IComponentsBuilder StandardComponents
            => ComponentsBuilderFactory.Create(RotationType.Full);

        public static IComponentsBuilder ModifiedComponents
            => ComponentsBuilderFactory.Create(RotationType.Signed);
    }
}
