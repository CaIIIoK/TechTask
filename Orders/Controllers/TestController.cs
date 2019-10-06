﻿using System.Collections.Generic;
using CustomLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestProject.Filters;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILibService _libService;

        public OrdersController(ILibService libService)
        {
            _libService = libService;
        }

        [HttpGet("{id}")]
        [ExceptionFilter]
        public ActionResult<Order> Get(int id)
        {
            return _libService.GetOrderById(id);
        }

        [HttpPut("{id}")]
        [ExceptionFilter]
        public ActionResult<bool> Cancel(int id)
        {
            return _libService.CancelOrder(id);
        }

        [HttpPut("{orderId}/tests/{testId}")]
        [ExceptionFilter]
        public ActionResult<bool> CancelTest(int orderId, int testId)
        {
            return _libService.CancelTest(orderId, testId);
        }

        [HttpGet("all")]
        [ExceptionFilter]
        public ActionResult<List<Order>> GetAllOrders(int id)
        {
            return _libService.GetAllOrders();
        }

        [HttpPost("create")]
        [ExceptionFilter]
        public ActionResult<bool> AddOder(Order order)
        {
            return _libService.AddOrder(order);
        }
    }
}
