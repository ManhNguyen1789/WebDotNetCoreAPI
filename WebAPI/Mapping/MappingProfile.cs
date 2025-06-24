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
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); ;
            CreateMap<Products, UpdateProductDto > ();
            // Thêm các ánh xạ khác nếu cần
        }
    }
}
