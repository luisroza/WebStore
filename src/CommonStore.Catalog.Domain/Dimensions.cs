using CommonStore.Core.DomainObjects;

namespace CommonStore.Catalog.Domain
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.AssertArgumentLesserThan(height, 1, "Height must be equal or greater than zero");
            AssertionConcern.AssertArgumentLesserThan(width, 1, "Width must be equal or greater than zero");
            AssertionConcern.AssertArgumentLesserThan(depth, 1, "Depth must be equal or greater than zero");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string DescriptionFormated()
        {
            return $"HxWxD: {Height} x {Width} x {Depth}";
        }

        public override string ToString()
        {
            return DescriptionFormated();
        }
    }
}