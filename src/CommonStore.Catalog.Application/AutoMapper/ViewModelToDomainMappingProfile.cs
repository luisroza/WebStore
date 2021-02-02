using AutoMapper;
using CommonStore.Catalog.Application.ViewModels;
using CommonStore.Catalog.Domain;

namespace CommonStore.Catalog.Application.AutoMapper
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