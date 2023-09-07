using BTL3_MVT.Data.Entity;
using BTL3_MVT.Data.Static;
using BTL3_MVT.Models;
using BTL3_MVT.Models.ViewModel;
using BTL3_MVT.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BTL3_MVT.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IWorkService _workService;
        private readonly IEthnicityService _ethnicityService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;

        public PersonController(IPersonService personService,
                                IWorkService workService,
                                IEthnicityService ethnicityService,
                                ICityService cityService,
                                IDistrictService districtService,
                                IWardService wardService)
        {
            _personService = personService;
            _workService = workService;
            _ethnicityService = ethnicityService;
            _cityService = cityService;
            _districtService = districtService;
            _wardService = wardService;
        }

        public IActionResult Index(string search, int? page)
        {
            int pageNumber = page ?? Static.PageNumberDefault;
            int pageSize = Static.PageSizeDefault;

            var persons = _personService.GetAllPersons(search).ToPagedList(pageNumber, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_SearchResult", persons);
            }

            return View(persons);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(PersonDTO personDTO)
        {
            if (!ModelState.IsValid) return View(personDTO);
            try
            {
                TempData["success"] = "Thêm bệnh nhân thành công !";
                _personService.AddPerson(personDTO);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Có lỗi khi thêm bệnh nhân: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Details(int id) => View(_personService.GetPersonById(id));

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            var person = _personService.GetPersonDTOById(id);
            if (person == null) return NotFound();
            ViewBagPerson(person.EthnicityId, person.WorkId, person.CityId, person.DistrictId, person.WardId, person.WorkCityId, person.WorkDistrictId, person.WorkWardId);
            
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(PersonDTO personDTO)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _personService.UpdatePerson(personDTO);
                TempData["success"] = "Sửa thông tin bệnh nhân thành công !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Có lỗi khi sửa bệnh nhân: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult DeleteConfirmed(int id)
        {
            var person = _personService.GetPersonById(id);
            return View(person);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                TempData["success"] = "Xoá bệnh nhân thành công !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Có lỗi khi xoá bệnh nhân: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult ExportExcel(string[] selectedItems)
        {
            List<PersonVM> persons;
            if (selectedItems != null && selectedItems.Length > 0)
            {
                persons = _personService.GetSelectedPersons(selectedItems).ToList();
            }
            else
            {
                persons = _personService.GetAllPersons().ToList();
            }
            byte[] fileData = _personService.ExportExcel(persons);

            if (fileData != null)
            {
                string fileName = string.Format(Static.FileName, DateTime.Now.ToString("dd/MM/yyyy"), Guid.NewGuid().ToString());
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile excelFile) => _personService.ImportExcel(excelFile);

        public void ViewBagPerson(int ethnicityId, int workId, int cityId, int districtId, int wardId, int workCityId, int workDistrictId, int workWardId)
        {
            ViewBag.Work = _personService.CreateWorkDTO(workId);
            ViewBag.Ethnicity = _personService.CreateEthnicityDTO(ethnicityId);
            ViewBag.City = _personService.CreateCityDTO(cityId);
            ViewBag.District = _personService.CreateDistrictDTO(districtId);
            ViewBag.Ward = _personService.CreateWardDTO(wardId);
            ViewBag.WorkCity = _personService.CreateCityDTO(workCityId);
            ViewBag.WorkDistrict = _personService.CreateDistrictDTO(workDistrictId);
            ViewBag.WorkWard = _personService.CreateWardDTO(workWardId);
        }
    }
}
