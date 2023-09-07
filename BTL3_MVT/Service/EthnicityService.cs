using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Service
{
    public class EthnicityService : IEthnicityService
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        private readonly IMapper _mapper;

        public EthnicityService(IEthnicityRepository ethnicityRepository, IMapper mapper)
        {
            _ethnicityRepository = ethnicityRepository;
            _mapper = mapper;
        }

        public int AddEthnicity(EthnicityDTO ethnicityDTO)
        {
            var ethnicity = _mapper.Map<Ethnicity>(ethnicityDTO);
            _ethnicityRepository.Add(ethnicity);
            return ethnicity.EthnicityId;
        }

        public void DeleteEthnicity(int id)
        {
            var ethnicity = _ethnicityRepository.GetById(id);
            _ethnicityRepository.Delete(ethnicity);
        }

        public IEnumerable<EthnicityDTO> GetAllEthnicitys(string searchQuery)
        {
            var ethnicitys = _ethnicityRepository.GetAll();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                ethnicitys = ethnicitys.Where(e => e.EthnicityName.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase));
            }
            return _mapper.Map<List<EthnicityDTO>>(ethnicitys);
        }

        public EthnicityDTO GetEthnicityById(int id)
        {
            var ethnicity = _ethnicityRepository.GetById(id);
            return _mapper.Map<EthnicityDTO>(ethnicity);
        }

        public void UpdateEthnicity(EthnicityDTO ethnicityDTO)
        {
            var ethnicity = _mapper.Map<Ethnicity>(ethnicityDTO);
            _ethnicityRepository.Update(ethnicity);
        }
    }
}
