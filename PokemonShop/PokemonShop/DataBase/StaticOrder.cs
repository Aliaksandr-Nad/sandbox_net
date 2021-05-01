using System;
using System.Collections.Generic;
using PokemonShop.Models;

namespace PokemonShop.DataBase
{
    public static class StaticOrder
    {
        public static List<OrderDto> OrderList = new()
        {
            new() {Guid = Guid.NewGuid(), Name = "Chuck" , Email = "Smirnov@example.com", Telephone = "+11(11)11111"},
            new() {Guid = Guid.NewGuid(), Name = "Tom", Email = "Sovkov@example.com", Telephone = "+22(22)22222"},
            new() {Guid = Guid.NewGuid(), Name = "Pol", Email = "Obrukov@example.com", Telephone = "+33(33)33333"},
            new() {Guid = Guid.NewGuid(), Name = "Vera", Email = "Trump@example.com", Telephone = "+44(44)44444"},
        };
    }
}