namespace DST.Core.Components
{
    public class ComponentsBuilderFactory
    {
        // Creates a new IComponentsBuilder object given the specified RotationType value.
        public static IComponentsBuilder Create(RotationType rotationType)
        {
            return rotationType switch
            {
                RotationType.Full => new StandardComponentsBuilder(),
                RotationType.Signed => new ModifiedComponentsBuilder(),
                _ => throw new NotSupportedException($"{nameof(RotationType)} value '{rotationType}' is not supported.")
            };
        }
    }
}
