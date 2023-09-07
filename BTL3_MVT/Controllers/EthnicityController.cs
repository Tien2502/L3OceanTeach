using BTL3_MVT.Data.Static;
using BTL3_MVT.Models;
using BTL3_MVT.Service.IService;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BTL3_MVT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EthnicityController : ControllerBase
    {
        private readonly IEthnicityService _ethnicityService;
        public EthnicityController(IEthnicityService ethnicityService)
        {
            _ethnicityService = ethnicityService;
        }

        [HttpGet]
        public IActionResult GetEthnicitys(string? searchQuery, int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var ethnicitys = _ethnicityService.GetAllEthnicitys(searchQuery).ToPagedList(pageNumber, pageSize);
            if (ethnicitys.Count == 0) return StatusCode(200, "Danh sách dân tộc rỗng");

            return Ok(ethnicitys);
        }

        [HttpGet("{id}")]
        public IActionResult GetEthnicity(int id)
        {
            if (id == 0) return NotFound();
            var ethnicity = _ethnicityService.GetEthnicityById(id);
            if (ethnicity == null) return NotFound();

            return Ok(ethnicity);
        }

        [HttpPost]
        public IActionResult AddEthnicity(EthnicityDTO ethnicityDTO)
        {
            try
            {
                var ethnicityId = _ethnicityService.AddEthnicity(ethnicityDTO);
                return Ok(_ethnicityService.GetEthnicityById(ethnicityId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi thêm dân tộc: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEthnicity(int id, EthnicityDTO ethnicityDTO)
        {
            if (id == 0 || ethnicityDTO == null) return NotFound();
            var ethnicity = _ethnicityService.GetEthnicityById(id);
            if (ethnicity == null) return NotFound();

            try
            {
                ethnicity.EthnicityName = ethnicityDTO.EthnicityName;
                _ethnicityService.UpdateEthnicity(ethnicity);

                return Ok(ethnicity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi cập nhật dân tộc: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEthnicity(int id)
        {
            if (id == 0) return NotFound();

            try
            {
                _ethnicityService.DeleteEthnicity(id);
                return StatusCode(200, "Xoá dân tộc thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi xoá dân tộc: " + ex.Message);
            }
        }
    }
}
