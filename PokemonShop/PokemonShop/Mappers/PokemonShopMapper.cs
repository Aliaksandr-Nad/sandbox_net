using AutoMapper;
using PokemonShop.Data.Entities;
using PokemonShop.Models;

namespace PokemonShop.Mappers
{
    public class PokemonShopMapper : Profile
    {
        public PokemonShopMapper()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
        }
    }
}