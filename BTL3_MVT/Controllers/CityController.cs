using Microsoft.AspNetCore.Mvc;
using BTL3_MVT.Models;
using BTL3_MVT.Data.Static;
using X.PagedList;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetCitys(string? searchQuery, int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var citys = _cityService.GetAllCitys(searchQuery).ToPagedList(pageNumber, pageSize);
            if (citys.Count == 0) return Ok(new CityDTO());

            return Ok(citys);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            if (id == 0) return NotFound();
            var city = _cityService.GetCityById(id);
            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public IActionResult AddCity(CityDTO cityDTO)
        {
            try
            {
                var cityId = _cityService.AddCity(cityDTO);
                return Ok(_cityService.GetCityById(cityId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi thêm thành phố: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, CityDTO cityDTO)
        {
            if (id == 0 || cityDTO == null) return NotFound();
            var city = _cityService.GetCityById(id);
            if (city == null) return NotFound();

            try
            {
                city.CityName = cityDTO.CityName;
                _cityService.UpdateCity(city);
                return Ok(city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi cập nhật thành phố: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            if (id == 0) return NotFound();

            try
            {
                _cityService.DeleteCity(id);
                return StatusCode(200, "Xoá thành phố thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi xoá thành phố: " + ex.Message);
            }
        }
    }
}
