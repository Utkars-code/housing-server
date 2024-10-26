using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using webApi_build_Real.ApplictionContext;
using webApi_build_Real.Dto;
using webApi_build_Real.Models;
using webApi_build_Real.Repository.implementation;

namespace webApi_build_Real.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _rep;
        private readonly IMapper _mapper;
        public CityController(IUnitOfWork repo, IMapper mapper)
        {

            _rep = repo;
            _mapper = mapper;
        }

        [HttpGet]
       // [AllowAnonymous]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var cites = await _rep.CityRepository.GetCitiesAsync();
            var CitiesDto = _mapper.Map<IEnumerable<CityDto>>(cites);
            //var CityDto = from c in cites
            //              select new CityDto()
            //              {
            //                  Id = c.Id,
            //                  Name = c.Name,
            //              };
            return Ok(CitiesDto);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            city.LAstUpdatedBy = 1;
            city.LastUpdateOn = DateTime.Now;
            //usinig with out automapper==> same work
            //var city = new City
            //{
            //    Name = cityDto.Name,
            //    LAstUpdatedBy = 1,
            //    LastUpdateOn = DateTime.Now,
            //};
            _rep.CityRepository.AddCity(city);
            await _rep.SaveAsync();
            return Ok(city);

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _rep.CityRepository.DeleteCity(id);
           await _rep.SaveAsync();
            return Ok("Secsucfull deleted");

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatedCity(int id, CityDto cityDto)
        {
            var cityFromDb = await _rep.CityRepository.FindCity(id);
            cityFromDb.LAstUpdatedBy = 1;
            cityFromDb.LastUpdateOn = DateTime.Now;
            _mapper.Map(cityDto, cityFromDb);
            await _rep.SaveAsync();
            return Ok(cityFromDb);
        }


        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdatedPatch(int id,JsonPatchDocument<City> cityTopatch)
        {
            var cityFromDb = await _rep.CityRepository.FindCity(id);
            cityFromDb.LAstUpdatedBy = 1;
            cityFromDb.LastUpdateOn = DateTime.Now;

            cityTopatch.ApplyTo(cityFromDb,ModelState);
            await _rep.SaveAsync();
            return Ok(cityFromDb);
        }
    }
}
