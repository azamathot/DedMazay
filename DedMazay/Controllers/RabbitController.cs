using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DedMazay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        // POST api/<RabbitController>
        [HttpPost]
        public IActionResult Post([FromBody] RabbitViewModel input)
        {
            if (input.Rabbits.Any(r => string.IsNullOrEmpty(r.Color)) || input.Count != input.Rabbits.Count)
                return BadRequest("Данные по зайцам неполные или количесво зайцев не совпадает с размером списка!");

            var result = new RabbitViewModel() {
                Rabbits = input.Rabbits.OrderByDescending(r => r.Weight).ThenBy(r => r.Color.ToLower() != "белый").Take(6).ToList(),
                Count = input.Count > 6 ? input.Count - 6 : 0
            };
            return Ok(result);
        }
    }
}
