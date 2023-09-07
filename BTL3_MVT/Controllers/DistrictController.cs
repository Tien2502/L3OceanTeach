using BTL3_MVT.Data.Static;
using BTL3_MVT.Models;
using BTL3_MVT.Service.IService;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BTL3_MVT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public IActionResult GetDistricts(int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var district = _districtService.GetAllDistricts().ToPagedList(pageNumber, pageSize);
            if (district.Count == 0) return Ok(new DistrictDTO());

            return Ok(district);
        }

        [HttpGet("{id}")]
        public IActionResult GetDistrict(int id)
        {
            if (id == 0) return NotFound();
            var district = _districtService.GetDistrictById(id);
            if (district == null) return NotFound();

            return Ok(district);
        }

        [HttpGet("ByCity/{cityId}")]
        public IActionResult GetDistrictsByCityId(string? searchQuery, int cityId, int? page)
        {
            if (cityId == 0) return NotFound();

            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var districtByCity = _districtService.GetDistrictsByCityId(searchQuery, cityId).ToPagedList(pageNumber, pageSize);
            if (districtByCity.Count() == 0) return Ok(new DistrictDTO());

            return Ok(districtByCity);
        }

        [HttpPost]
        public IActionResult AddDistrict(DistrictDTO district)
        {
            try
            {
                var districtId = _districtService.AddDistrict(district);
                return Ok(_districtService.GetDistrictById(districtId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi thêm Quận, Huyện: " + ex.Message);
            }

        }

        [HttpPut]
        public IActionResult UpdateDistrict(int id, DistrictDTO district)
        {
            if (id == 0) return NotFound();
            var existingDistrict = _districtService.GetDistrictById(id);
            if (district == null) return NotFound();

            try
            {
                existingDistrict.DistrictName = district.DistrictName;
                _districtService.UpdateDistrict(existingDistrict);
                return Ok(district);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi cập nhật Quận, Huyện: " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDistrict(int id)
        {
            if (id == 0) return NotFound();

            try
            {
                _districtService.DeleteDistrict(id);
                return StatusCode(200, "Xoá Quận, Huyện thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi xoá Quận, Huyện: " + ex.Message);
            }
        }
    }
}
