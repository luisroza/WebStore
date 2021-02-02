using System;
using CommonStore.Core.DomainObjects;

namespace CommonStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Category Category { get; private set; }

        protected Product() { }
        public Product(string name, string description, bool active, decimal price, Guid categoryId, DateTime createDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            CreateDate = createDate;
            Image = image;
            Dimensions = dimensions;

            Validate();
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            Validation.ValidateEmpty(description, "O campo Descricao do produto não pode estar vazio");
            Description = description;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasStock(quantity)) throw new DomainException("Estoque insuficiente");
            StockQuantity -= quantity;
        }

        public void ReplenishStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Validate()
        {
            Validation.ValidateEmpty(Name, "O campo Nome do produto não pode estar vazio");
            Validation.ValidateEmpty(Description, "O campo Descricao do produto não pode estar vazio");
            Validation.ValidateEqual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validation.ValidateLesserThan(Price, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validation.ValidateEmpty(Image, "O campo Imagem do produto não pode estar vazio");
        }
    }
}