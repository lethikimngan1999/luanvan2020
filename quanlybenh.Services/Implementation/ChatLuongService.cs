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
    public class ChatLuongService : BaseService, IChatLuongService
    {
        private IDataRepository<ChatLuong> _chatluongRepository;

        private readonly IMapper _mapper;

        public ChatLuongService(
               IDataRepository<ChatLuong> chatluongRepository,

               IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _chatluongRepository = chatluongRepository;

            _mapper = mapper;
        }

        public List<ChatLuongDTO> GetAll()
        {
            var _lstChatLuongs = _chatluongRepository.GetAll().OrderBy(x => x.TenChatLuong).ToList();
            var ChatLuongDtos = _mapper.Map<List<ChatLuongDTO>>(_lstChatLuongs);


            return ChatLuongDtos;
        }
    }
}
