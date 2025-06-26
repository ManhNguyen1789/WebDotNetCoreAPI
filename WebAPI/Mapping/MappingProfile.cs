using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.ProductDTO;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Automapper Products to ProductDto
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); ;

            // Automapper Products to UpdateProductDto
            CreateMap<Products, UpdateProductDto>();

            // Automapper UpdateProductDto to Products
            CreateMap<UpdateProductDto, Products>();
        }
    }
}
