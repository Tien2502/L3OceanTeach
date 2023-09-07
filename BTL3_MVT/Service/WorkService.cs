using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using BTL3_MVT.Service.IService;

namespace BTL3_MVT.Service
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;

        public WorkService(IWorkRepository workRepository, IMapper mapper)
        {
            _workRepository = workRepository;
            _mapper = mapper;
        }

        public int AddWork(WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);
            _workRepository.Add(work);
            return work.WorkId;
        }

        public void DeleteWork(int id)
        {
            var work = _workRepository.GetById(id);
            _workRepository.Delete(work);
        }

        public IEnumerable<WorkDTO> GetAllWorks(string searchQuery)
        {
            var works = _workRepository.GetAll();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                works = works.Where(w => w.WorkName.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase));
            }
            return _mapper.Map<List<WorkDTO>>(works);
        }

        public WorkDTO GetWorkById(int id)
        {
            var work = _workRepository.GetById(id);
            return _mapper.Map<WorkDTO>(work);
        }

        public void UpdateWork(WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);
            _workRepository.Update(work);
        }
    }
}
