using BTL3_MVT.Data.Static;
using BTL3_MVT.Models;
using BTL3_MVT.Service.IService;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BTL3_MVT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _workService;
        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpGet]
        public IActionResult GetWorks(string? searchQuery, int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var works = _workService.GetAllWorks(searchQuery).ToPagedList(pageNumber, pageSize);
            if (works.Count == 0) return StatusCode(200, "Danh sách nghề nghiệp rỗng");

            return Ok(works);
        }

        [HttpGet("{id}")]
        public IActionResult GetWork(int id)
        {
            if (id == 0) return NotFound();
            var work = _workService.GetWorkById(id);
            if (work == null) return NotFound();

            return Ok(work);
        }

        [HttpPost]
        public IActionResult AddWork(WorkDTO workDTO)
        {
            try
            {
                var workId = _workService.AddWork(workDTO);
                return Ok(_workService.GetWorkById(workId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi thêm nghề nghiệp: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWork(int id, WorkDTO workDTO)
        {
            if(id == 0) return NotFound();
            var work = _workService.GetWorkById(id);
            if (work == null) return NotFound();

            try
            {
                work.WorkName = workDTO.WorkName;
                _workService.UpdateWork(work);
                return Ok(work);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi khi cập nhật nghề nghiệp: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWork(int id)
        {
            if (id == 0) return NotFound();

            try
            {
                _workService.DeleteWork(id);
                return StatusCode(200, "Xoá nghề nghiệp thành công");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Có lỗi khi xoá nghề nghiệp: " + ex.Message);

            }
        }
    }
}
