using System;
using Microsoft.AspNetCore.Mvc;
using PokemonShop.Models;
using PokemonShop.Services;

namespace PokemonShop.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet, Route("")]
        public IActionResult GetAllOrders()
        {
            return View("OrdersTable", _orderService.GetAll());
        }

        [HttpGet, Route("{guid:guid}")]
        public IActionResult GetOrderByGuid(Guid guid)
        {
            return View("OrderForm", _orderService.GetByGuid(guid));
        }

        [Route("add")]
        public IActionResult AddOrder()
        {
            try
            {
                return View("OrderForm", new OrderDto {Guid = Guid.NewGuid()});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToActionPermanent("GetAllOrders");
            }
        }

        [Route("save")]
        public IActionResult SaveOrder(OrderDto newOrder)
        {
            try
            {
                _orderService.ChangeEntity(newOrder);
                return RedirectToActionPermanent("GetAllOrders");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToActionPermanent("GetAllOrders");
            }
        }

        [Route("delete/{guid:guid}")]
        public IActionResult DeleteOrder(Guid guid)
        {
            try
            {
                _orderService.DeleteEntity(guid);
                return RedirectToActionPermanent("GetAllOrders");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToActionPermanent("GetAllOrders");
            }
        }
    }
}