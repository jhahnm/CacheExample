using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.repositories;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepository _repository;

        public CarsController(ICarsRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("make/{make}")]
        public async Task<ActionResult<List<Car>>> GetCarsByMake(string make)
        {
            return await _repository.GetCarsByMake(make);
        }
        
        [HttpGet("seller/{seller}")]
        public async Task<ActionResult<List<Car>>> GetCarsBySeller(string seller)
        {
            return await _repository.GetCarsBySeller(seller);
        }

        [HttpPost]
        public async Task<ActionResult> AddCar(Car car)
        {
            await _repository.CreateCar(car);
            return Ok();
        }
    }
}
