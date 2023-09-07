using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IDistrictRepository districtRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _mapper = mapper;
        }

        public int AddCity(CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
            _cityRepository.Add(city);
            return city.CityId;
        }

        public void DeleteCity(int id)
        {
            var city = _cityRepository.GetById(id);
            var districts = _districtRepository.GetAll(d => d.CityId == id);

            _districtRepository.RemoveRange(districts);
            _cityRepository.Delete(city);
        }

        public IEnumerable<CityDTO> GetAllCitys(string searchQuery)
        {
            var citys = _cityRepository.GetAll();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                citys = citys.Where(c => c.CityName.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase));
            }
            return _mapper.Map<List<CityDTO>>(citys);
        }

        public CityDTO GetCityById(int id)
        {
            var city = _cityRepository.GetById(id);
            return _mapper.Map<CityDTO>(city);
        }

        public int GetIdByName(string cityName) => _cityRepository.GetIdByName(cityName);
        
        public void UpdateCity(CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
            _cityRepository.Update(city);
        }
    }
}
