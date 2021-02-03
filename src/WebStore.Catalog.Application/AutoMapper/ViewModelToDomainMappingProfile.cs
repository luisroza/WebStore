using AutoMapper;
using WebStore.Catalog.Application.ViewModels;
using WebStore.Catalog.Domain;

namespace WebStore.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p =>
                    new Product(p.Name, p.Description, p.Active,
                        p.Price, p.CategoryId, p.CreateDate,
                        p.Image, new Dimensions(p.Height, p.Width, p.Depth)));

            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
        }
    }
}