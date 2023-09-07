using BTL3_MVT.Data.Static;
using BTL3_MVT.Models;
using BTL3_MVT.Service.IService;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BTL3_MVT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WardController : ControllerBase
    {
        private readonly IWardService _wardService;

        public WardController(IWardService wardService)
        {
            _wardService = wardService;
        }

        [HttpGet]
        public IActionResult GetWards(int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var ward = _wardService.GetAllWards().ToPagedList(pageNumber, pageSize);
            if (ward.Count == 0) return Ok(new WardDTO());

            return Ok(ward);
        }

        [HttpGet("{id}")]
        public IActionResult GetWard(int id)
        {
            if (id == 0) return NotFound();
            var ward = _wardService.GetWardById(id);
            if (ward == null) return NotFound();

            return Ok(ward);
        }

        [HttpGet("ByDistrict/{districtId}")]
        public IActionResult GetWardsByDistrictId(string? searchQuery,int districtId, int? page)
        {
            if (districtId == 0) return NotFound();

            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var wardsByDistrict = _wardService.GetWardsByDistrictId(searchQuery, districtId).ToPagedList(pageNumber, pageSize);
            if (wardsByDistrict.Count() == 0) return Ok(new WardDTO());

            return Ok(wardsByDistrict);
        }

        [HttpPost]
        public IActionResult AddWard(WardDTO wardDTO)
        {
            try
            {
                var wardId = _wardService.AddWard(wardDTO);
                return Ok(_wardService.GetWardById(wardId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi thêm Ward: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateWard(int id, WardDTO wardDTO)
        {
            if (id == 0|| wardDTO == null) return NotFound();
            var existingWard = _wardService.GetWardById(id);
            if (existingWard == null) return NotFound();

            try
            {
                existingWard.WardName = wardDTO.WardName;
                existingWard.DistrictId = wardDTO.DistrictId;
                _wardService.UpdateWard(existingWard);
                return Ok(wardDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi cập nhật Ward: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWard(int id)
        {
            if (id == 0) return NotFound();

            try
            {
                _wardService.DeleteWard(id);
                return StatusCode(200, "Xoá Ward thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi xoá Ward: " + ex.Message);
            }
        }
    }
}
