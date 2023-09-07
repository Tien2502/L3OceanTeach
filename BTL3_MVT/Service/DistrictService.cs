using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Service
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IMapper _mapper;
        public DistrictService(IDistrictRepository districtRepository, IWardRepository wardRepository, IMapper mapper)
        {
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _mapper = mapper;
        }
        public int AddDistrict(DistrictDTO districtDTO)
        {
            var district = _mapper.Map<District>(districtDTO);
            _districtRepository.Add(district);
            return district.DistrictId;
        }

        public void DeleteDistrict(int id)
        {
            var districts = _districtRepository.GetById(id);
            var wards = _wardRepository.GetAll(w => w.DistrictId == id);

            _wardRepository.RemoveRange(wards);
            _districtRepository.Delete(districts);
        }

        public IEnumerable<DistrictDTO> GetAllDistricts()
        {
            var districts = _districtRepository.GetAll();
            return _mapper.Map<List<DistrictDTO>>(districts);
        }

        public DistrictDTO GetDistrictById(int id)
        {
            var district = _districtRepository.GetById(id);
            return _mapper.Map<DistrictDTO>(district);
        }

        public IEnumerable<DistrictDTO> GetDistrictsByCityId(string searchQuery, int id)
        {
            var districts = _districtRepository.GetAll(d => d.CityId == id);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                districts = districts.Where(d => d.DistrictName.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase));
            }
            return _mapper.Map<List<DistrictDTO>>(districts);
        }

        public void UpdateDistrict(DistrictDTO districtDTO)
        {
            var district = _mapper.Map<District>(districtDTO);
            _districtRepository.Update(district);
        }
    }
}
