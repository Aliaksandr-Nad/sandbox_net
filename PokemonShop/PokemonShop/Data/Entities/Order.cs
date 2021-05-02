using System;

namespace PokemonShop.Data.Entities
{
    public class Order
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }
}