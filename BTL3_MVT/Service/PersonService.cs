using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Models.ViewModel;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Unidecode.NET;
using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IEthnicityRepository _ethnicityRepository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository personRepository,
                             ICityRepository cityRepository,
                             IDistrictRepository districtRepository,
                             IWardRepository wardRepository,
                             IWorkRepository workRepository,
                             IEthnicityRepository ethnicityRepository,
                             IMapper mapper)
        {
            _personRepository = personRepository;
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _workRepository = workRepository;
            _ethnicityRepository = ethnicityRepository;
            _mapper = mapper;
        }

        public void AddPerson(PersonDTO PersonDTO)
        {
            var person = _mapper.Map<Person>(PersonDTO);
            _personRepository.Add(person);
        }

        public void DeletePerson(int id)
        {
            var person = _personRepository.GetById(id);
            _personRepository.Delete(person);
        }

        public byte[]? ExportExcel(List<PersonVM> persons)
        {
            try
            {
                if (persons != null && persons.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        string[] propertyNames = { "FullName", "BirthDay", "Age", "Gender", "Ethnicity", "Work", "PhoneNumber", "IdentityCard", "Address", "WorkAddress" };
                        var worksheet = ToConvert(persons, propertyNames);
                        wb.Worksheets.Add(worksheet);

                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return stream.ToArray();
                        }
                    }
                }
            }
            catch
            {
                // Handle exception
            }
            return null;
        }

        public IEnumerable<PersonVM> GetAllPersons(string searchQuery)
        {
            var persons = Search(searchQuery);
            return persons;
        }

        public IEnumerable<PersonVM> GetAllPersons()
        {
            var includeProperties = new Expression<Func<Person, object>>[]
            {
                p => p.Work,
                p => p.Ethnicity,
                p => p.City,
                p => p.District,
                p => p.Ward,
                p => p.WorkCity,
                p => p.WorkDistrict,
                p => p.WorkWard
            };
            var persons = _personRepository.GetAll(p => true, includeProperties);
            return _mapper.Map<List<PersonVM>>(persons);
        }

        public PersonVM GetPersonById(int id)
        {
            var person = _personRepository.GetById(id);
            return _mapper.Map<PersonVM>(person);
        }

        public void UpdatePerson(PersonDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            _personRepository.Update(person);
        }

        public DataTable ToConvert<T>(List<T> items, string[] propertyNames)
        {
            DataTable dt = new DataTable(typeof(T).Name);

            foreach (string propertyName in propertyNames)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    dt.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                }
            }

            foreach (T item in items)
            {
                var values = new object[propertyNames.Length];

                for (int i = 0; i < propertyNames.Length; i++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(propertyNames[i], BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo != null)
                    {
                        values[i] = propertyInfo.GetValue(item);
                    }
                }

                dt.Rows.Add(values);
            }
            return dt;
        }

        public IEnumerable<PersonVM> GetSelectedPersons(string[] selectedItems)
        {
            var selectedPersons = GetAllPersons()
                .Where(p => selectedItems.Contains(p.PersonId.ToString()));

            return selectedPersons;
        }

        public IEnumerable<PersonVM> Search(string search)
        {
            var persons = GetAllPersons();
            if (!string.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out int searchNumber))
                {
                    persons = persons.Where(p => p.Age == searchNumber);
                }
                else if (string.Equals(search, "nam", StringComparison.InvariantCultureIgnoreCase) || string.Equals(search, "nữ", StringComparison.InvariantCultureIgnoreCase))
                {
                    persons = persons.Where(p => string.Equals(p.Gender, search, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    var nameSearchResults = persons.Where(p => RemoveDiacritics(p.FullName).IndexOf(RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase) >= 0)
                                                   .OrderBy(p => p.FullName)
                                                   .ThenBy(p => p.Age)
                                                   .ThenBy(p => p.Gender);

                    if (!nameSearchResults.Any())
                    {
                        persons = persons.Where(p => RemoveDiacritics(p.City).IndexOf(RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase) >= 0
                            || RemoveDiacritics(p.District).IndexOf(RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase) >= 0
                            || RemoveDiacritics(p.Ward).IndexOf(RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase) >= 0)
                            .OrderBy(p => p.FullName)
                            .ThenBy(p => p.Age)
                            .ThenBy(p => p.Gender);
                    }
                    else
                    {
                        persons = nameSearchResults;
                    }
                }
            }
            return persons;
        }

        private string RemoveDiacritics(string text)
        {
            return Unidecoder.Unidecode(text);
        }

        public List<PersonDTO> GetPersonsFromExcel(IFormFile excelFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                excelFile.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet != null)
                    {
                        List<PersonDTO> persons = new List<PersonDTO>();
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            string? fullName = worksheet.Cells[row, 1].Value?.ToString();
                            DateTime birthDay = worksheet.Cells[row, 2].GetValue<DateTime>();
                            string? gender = worksheet.Cells[row, 3].Value?.ToString();
                            string? ethnicity = worksheet.Cells[row, 4].Value?.ToString();
                            string? work = worksheet.Cells[row, 5].Value?.ToString();
                            string? phoneNumber = worksheet.Cells[row, 6].Value?.ToString();
                            string? identityCard = worksheet.Cells[row, 7].Value?.ToString();
                            string? city = worksheet.Cells[row, 8].Value?.ToString();
                            string? district = worksheet.Cells[row, 9].Value?.ToString();
                            string? ward = worksheet.Cells[row, 10].Value?.ToString();
                            string? specificAddress = worksheet.Cells[row, 11].Value?.ToString();
                            string? workCity = worksheet.Cells[row, 12].Value?.ToString();
                            string? workDistrict = worksheet.Cells[row, 13].Value?.ToString();
                            string? workWard = worksheet.Cells[row, 14].Value?.ToString();
                            string? workSpecificAddress = worksheet.Cells[row, 15].Value?.ToString();

                            PersonDTO person = MapPersonFromExcel(fullName, birthDay, gender, work, ethnicity, phoneNumber, identityCard, city, district, ward, 
                                specificAddress, workCity, workDistrict, workWard, workSpecificAddress);
                            persons.Add(person);
                        }
                        return persons;
                    }
                }
            }
            return new List<PersonDTO>();
        }

        public PersonDTO MapPersonFromExcel(string fullName, DateTime birthDay, string gender, string work, string ethnicity, string phoneNumber, string identityCard, string city, string district, 
              string ward, string specificAddress, string workCity, string workDistrict, string workWard, string workSpecificAddress)
        {
            return new PersonDTO
            {
                FullName = fullName,
                BirthDay = birthDay,
                Gender = gender,
                WorkId = _workRepository.GetIdByName(work),
                EthnicityId = _ethnicityRepository.GetIdByName(ethnicity),
                IdentityCard = identityCard,
                PhoneNumber = phoneNumber,
                CityId = _cityRepository.GetIdByName(city),
                DistrictId = _districtRepository.GetIdByName(district),
                WardId = _wardRepository.GetIdByName(ward),
                SpecificAddress = specificAddress,
                WorkCityId = _cityRepository.GetIdByName(workCity),
                WorkDistrictId = _districtRepository.GetIdByName(workDistrict),
                WorkWardId = _wardRepository.GetIdByName(workWard),
                WorkSpecificAddress = workSpecificAddress
            };
        }

        public List<string> CheckValidatePersonDTO(List<PersonDTO> personDTOs)
        {
            List<string> errorMessages = new List<string>();

            for (int row = 0; row < personDTOs.Count; row++)
            {
                PersonDTO person = personDTOs[row];
                var validationContext = new ValidationContext(person, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(person, validationContext, validationResults, validateAllProperties: true);

                if (!isValid)
                {
                    var errors = validationResults.SelectMany(vr => vr.MemberNames.Select(memberName => $"{memberName}=> {vr.ErrorMessage}"));
                    string errorMessage = $"\nLỗi dòng {row + 1}: {string.Join("\n                   ", errors)}";
                    errorMessages.Add(errorMessage);
                }
            }
            return errorMessages;
        }

        public IActionResult ImportExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length <= 0)
            {
                return new JsonResult(new { success = false, message = "Vui lòng chọn tệp tin Excel" });
            }

            try
            {
                List<PersonDTO> importData = GetPersonsFromExcel(excelFile);
                if (CheckValidatePersonDTO(importData).Any())
                {
                    return new JsonResult(new { success = false, message = CheckValidatePersonDTO(importData) });
                }

                foreach (var person in importData)
                {
                    AddPerson(person);
                }

                return new JsonResult(new { success = true, message = "Import thành công" });
            }
            catch (FormatException ex)
            {
                return new JsonResult(new { success = false, message = "Nhập định dạng dữ liệu không hợp lệ: " + ex.Message});
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Import thất bại: " + ex.Message });
            }
        }

        public PersonDTO GetPersonDTOById(int id)
        {
            var person = _personRepository.GetById(id);
            return _mapper.Map<PersonDTO>(person);
        }

        public WorkDTO CreateWorkDTO(int workId)
        {
            var work = _workRepository.GetById(workId);
            return new WorkDTO
            {
                WorkId = workId,
                WorkName = work.WorkName
            };
        }

        public EthnicityDTO CreateEthnicityDTO(int ethnicityId)
        {
            var ethnicity = _ethnicityRepository.GetById(ethnicityId);
            return new EthnicityDTO
            {
                EthnicityId = ethnicityId,
                EthnicityName = ethnicity.EthnicityName
            };
        }

        public CityDTO CreateCityDTO(int cityId)
        {
            var city = _cityRepository.GetById(cityId);
            return new CityDTO
            {
                CityId = cityId,
                CityName = city.CityName
            };
        }

        public DistrictDTO CreateDistrictDTO(int districtId)
        {
            var district = _districtRepository.GetById(districtId);
            return new DistrictDTO
            {
                DistrictId = districtId,
                DistrictName = district.DistrictName
            };
        }

        public WardDTO CreateWardDTO(int wardId)
        {
            var ward = _wardRepository.GetById(wardId);
            return new WardDTO
            {
                WardId = wardId,
                WardName = ward.WardName
            };
        }
    }
}
