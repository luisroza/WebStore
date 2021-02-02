using System.Collections.Generic;
using CommonStore.Core.DomainObjects;

namespace CommonStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Product> Products { get; set; }
        
        // EF issue
        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validar();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validar()
        {
            Validation.ValidateEmpty(Name, "O campo Nome da categoria não pode estar vazio");
            Validation.ValidateEqual(Code, 0, "O campo Code não pode ser 0");
        }
    }
}