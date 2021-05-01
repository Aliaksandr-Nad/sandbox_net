using System;
using System.Collections.Generic;
using PokemonShop.DataBase;
using PokemonShop.Models;

namespace PokemonShop.Services
{
    public class OrderService
    {
        private static List<OrderDto> _orderList;

        public OrderService()
        {
            _orderList = StaticOrder.OrderList;
        }
        
        public List<OrderDto> GetAll()
        {
            return _orderList;
        }

        public OrderDto GetByGuid(Guid guid)
        {
            return _orderList.Find(x => x.Guid.Equals(guid));
        }

        public void AddEntity(OrderDto newEntity)
        {
            _orderList.Add(newEntity);
        }

        public void ChangeEntity(OrderDto newEntity)
        {
            foreach (var userDto in _orderList)
            {
                if (userDto.Guid == newEntity.Guid)
                {
                    _orderList.Remove(userDto);
                    break;
                }
            }
            _orderList.Add(newEntity);
        }

        public void DeleteEntity(Guid guid)
        {
            _orderList.RemoveAt(FindIndex(guid));
        }

        private int FindIndex(Guid guid)
        {
            return _orderList.FindIndex(x => x.Guid.Equals(guid));
        }
    }
}