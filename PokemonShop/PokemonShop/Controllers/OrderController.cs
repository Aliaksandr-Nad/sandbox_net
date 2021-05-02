using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokemonShop.Models;
using PokemonShop.Services;

namespace PokemonShop.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly EmailService _emailService;

        public OrderController(OrderService orderService, EmailService emailService)
        {
            _orderService = orderService;
            _emailService = emailService;
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
        public async Task<IActionResult> SaveOrder(OrderDto newOrder)
        {
            try
            {
                await _orderService.SaveEntity(newOrder);
                await _emailService.SendEmailAsync(newOrder.Name, newOrder.Email);
                return RedirectToActionPermanent("GetAllOrders");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToActionPermanent("GetAllOrders");
            }
        }

        [Route("delete/{guid:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid guid)
        {
            try
            {
                await _orderService.DeleteEntity(guid);
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