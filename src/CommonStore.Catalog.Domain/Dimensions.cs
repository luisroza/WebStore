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
            Validation.ValidateLesserThan(height, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validation.ValidateLesserThan(width, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validation.ValidateLesserThan(depth, 1, "O campo Profundidade não pode ser menor ou igual a 0");

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