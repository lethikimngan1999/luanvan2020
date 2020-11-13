using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.Services.DTO.Benh;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.DTO.Ca;
using quanlybenh.Services.DTO.HinhAnh;
using quanlybenh.Services.DTO.Menu;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.AutoMapper
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<NhanVien, NhanVienDTO>();
            CreateMap<NhanVienDTO, NhanVien>();

            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<LoginDTO, User>();
            CreateMap<User, LoginDTO>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<Menu, MenuDTO>();
            CreateMap<MenuDTO, Menu>();

            CreateMap<RolePermission, RolePermissionDTO>();
            CreateMap<RolePermissionDTO, RolePermission>();

            CreateMap<UserRole, UserRoleDTO>();
            CreateMap<UserRoleDTO, UserRole>();


            CreateMap<ChungLoai, ChungLoaiDTO>();
            CreateMap<ChungLoaiDTO, ChungLoai>();


            CreateMap<ChatLuong, ChatLuongDTO>();
            CreateMap<ChatLuongDTO, ChatLuong>();


            CreateMap<Giong, GiongDTO>();
            CreateMap<GiongDTO, Giong>();


            CreateMap<BienThe, BienTheDTO>();
            CreateMap<BienTheDTO, BienThe>();


            CreateMap<HinhAnhBienThe, HinhAnhBienTheDTO>();
            CreateMap<HinhAnhBienTheDTO, HinhAnhBienThe>();

            CreateMap<Thuoc, ThuocDTO>();
            CreateMap<ThuocDTO, Thuoc>();


            CreateMap<Benh, BenhDTO>();
            CreateMap<BenhDTO, Benh>();

            CreateMap<ThuocDieuTri, ThuocDieuTriBenhDTO>();
            CreateMap<ThuocDieuTriBenhDTO, ThuocDieuTri>();

            CreateMap<LieuTrinh, LieuTrinhDTO>();
            CreateMap<LieuTrinhDTO, LieuTrinh>();

            CreateMap<TrieuChung, TrieuChungDTO>();
            CreateMap<TrieuChungDTO, TrieuChung>();

            CreateMap<Ca, CaDTO>();
            CreateMap<CaDTO, Ca>();

            CreateMap<TrieuChungBenh, TrieuChungBenhDTO>();
            CreateMap<TrieuChungBenhDTO, TrieuChungBenh>();

        }
    }

}
