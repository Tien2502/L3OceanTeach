using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BTL3_MVT.Service.IService
{
    public interface IPersonService
    {
        IEnumerable<PersonVM> GetAllPersons(string? searchQuery);
        IEnumerable<PersonVM> GetAllPersons();
        PersonVM GetPersonById(int id);
        PersonDTO GetPersonDTOById(int id);
        void AddPerson(PersonDTO personDTO);
        void UpdatePerson(PersonDTO personDTO);
        void DeletePerson(int id);
        byte[] ExportExcel(List<PersonVM> persons);
        IActionResult ImportExcel(IFormFile excelFile);
        IEnumerable<PersonVM> GetSelectedPersons(string[] selectedItems);
        WorkDTO CreateWorkDTO(int workId);
        EthnicityDTO CreateEthnicityDTO(int ethnicityId);
        CityDTO CreateCityDTO(int cityId);
        DistrictDTO CreateDistrictDTO(int districtId);
        WardDTO CreateWardDTO(int wardId);
    }
}
