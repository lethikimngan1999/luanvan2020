using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class GiongService : BaseService, IGiongService
    {
        private IDataRepository<Giong> _giongRepository;
     
        private readonly IMapper _mapper;

        public GiongService(
               IDataRepository<Giong> giongRepository,
            
               IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _giongRepository = giongRepository;
            
            _mapper = mapper;
        }

        public List<GiongDTO> GetAll()
        {
            var _lstGiongs = _giongRepository.GetAll().OrderBy(x => x.TenGiong).ToList();
            var giongDtos = _mapper.Map<List<GiongDTO>>(_lstGiongs);

            
            return giongDtos;
        }
    }
}
