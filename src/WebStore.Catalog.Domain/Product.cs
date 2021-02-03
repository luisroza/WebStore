using System;
using WebStore.Core.DomainObjects;

namespace WebStore.Catalog.Domain
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
            AssertionConcern.AssertArgumentNotEmpty(description, "Description cannot not be empty");
            Description = description;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasStock(quantity)) throw new DomainException("There are no items enough in stock");
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
            AssertionConcern.AssertArgumentNotEmpty(Name, "Name cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Description, "Description cannot be empty");
            AssertionConcern.AssertArgumentEquals(CategoryId, Guid.Empty, "Product's CategoriyId cannot be empty");
            AssertionConcern.AssertArgumentLesserThan(Price, 1, "Product's price cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Image, "Image cannot be empty");
        }
    }
}