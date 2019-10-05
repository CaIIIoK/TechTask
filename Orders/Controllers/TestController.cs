using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILibService _libService;

        public OrdersController(ILibService libService) {
            _libService = libService;
        }

        // GET api/values
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            return _libService.GetOrderById(id);
        }

        // GET api/values
        [HttpPut("{id}")]
        public ActionResult<bool> Cancel(int id)
        {
            return _libService.CancelOrder(id);
        }

        // GET api/values
        [HttpGet("all")]
        public ActionResult<List<Order>> GetAllOrders(int id)
        {
            return _libService.GetAllOrders();
        }

        // POST api/values
        [HttpPost("create")]
        public ActionResult<bool> AddOder(Order order)
        {
            return _libService.AddOrder(order);
        }


    }
}
