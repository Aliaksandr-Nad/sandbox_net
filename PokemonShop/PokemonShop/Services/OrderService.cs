using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonShop.Data;
using PokemonShop.Data.Entities;
using PokemonShop.Models;

namespace PokemonShop.Services
{
    public class OrderService
    {
        private readonly PokemonShopDbContext _pokemonShopDbContext;
        private readonly IMapper _mapper;
        private static List<OrderDto> _orderList;

        public OrderService(PokemonShopDbContext pokemonShopDbContext, IMapper mapper)
        {
            _pokemonShopDbContext = pokemonShopDbContext;
            _mapper = mapper;
            _orderList = StaticOrder.OrderList;
        }

        public List<OrderDto> GetAll()
        {
            return _pokemonShopDbContext.Orders.AsNoTracking()
                .Select(x => _mapper.Map<OrderDto>(x)).ToList();
        }

        public OrderDto GetByGuid(Guid guid)
        {
            return _mapper.Map<OrderDto>(_pokemonShopDbContext.Orders.AsNoTracking()
                .FirstOrDefault(x => x.Guid.Equals(guid)));
        }

        public async Task SaveEntity(OrderDto newEntity)
        {
            var order = _mapper.Map<Order>(newEntity);

            var isExist = _pokemonShopDbContext.Orders.AsNoTracking()
                .FirstOrDefault(x => x.Guid.Equals(order.Guid));
            if (isExist == null)
            {
                await CreateEntity(order);
            }
            else
            {
                await UpdateEntity(order);
            }
        }

        public async Task DeleteEntity(Guid guid)
        {
            var order = _mapper.Map<Order>(GetByGuid(guid));
            _pokemonShopDbContext.Orders.Remove(order);
            await _pokemonShopDbContext.SaveChangesAsync();
        }

        private async Task CreateEntity(Order entity)
        {
            _pokemonShopDbContext.Orders.Add(entity);
            await _pokemonShopDbContext.SaveChangesAsync();
        }

        private async Task UpdateEntity(Order entity)
        {
            _pokemonShopDbContext.Orders.Update(entity);
            await _pokemonShopDbContext.SaveChangesAsync();
        }
    }
}