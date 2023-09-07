using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Service
{
    public class WardService : IWardService
    {
        private readonly IWardRepository _wardRepository;
        private readonly IMapper _mapper;
        public WardService(IWardRepository wardRepository, IMapper mapper)
        {
            _wardRepository = wardRepository;
            _mapper = mapper;
        }

        public int AddWard(WardDTO wardDTO)
        {
            var ward = _mapper.Map<Ward>(wardDTO);
            _wardRepository.Add(ward);
            return ward.WardId;
        }

        public void DeleteWard(int id)
        {
            var ward = _wardRepository.GetById(id);
            _wardRepository.Delete(ward);
        }

        public IEnumerable<WardDTO> GetAllWards()
        {
            var wards = _wardRepository.GetAll();
            return _mapper.Map<List<WardDTO>>(wards);
        }

        public WardDTO GetWardById(int id)
        {
            var ward = _wardRepository.GetById(id);
            return _mapper.Map<WardDTO>(ward);
        }

        public IEnumerable<WardDTO> GetWardsByDistrictId(string searchQuery, int id)
        {
            var wards = _wardRepository.GetAll(w => w.DistrictId == id);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                wards = wards.Where(w => w.WardName.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase));
            }
            return _mapper.Map<List<WardDTO>>(wards);
        }

        public void UpdateWard(WardDTO wardDTO)
        {
            var ward = _mapper.Map<Ward>(wardDTO);
            _wardRepository.Update(ward);
        }
    }
}
