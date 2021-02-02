using CommonStore.Core.DomainObjects;
using System.Collections.Generic;

namespace CommonStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Product> Products { get; set; }

        // EF Relation
        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "Cagotegory's name cannot be empty");
            AssertionConcern.AssertArgumentEquals(Code, 0, "Code cannot be empty");
        }
    }
}