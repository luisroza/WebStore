using WebStore.Core.DomainObjects;

namespace WebStore.Catalog.Domain
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

       private void Validate()
        {
            AssertionConcern.AssertArgumentLesserThan(Height, 1, "Height must be equal or greater than zero");
            AssertionConcern.AssertArgumentLesserThan(Width, 1, "Width must be equal or greater than zero");
            AssertionConcern.AssertArgumentLesserThan(Depth, 1, "Depth must be equal or greater than zero");
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