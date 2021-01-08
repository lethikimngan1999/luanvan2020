
using quanlybenh.Services.DTO.NhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface INhanVienService
    {
        NhanVienDTO GetById(string MaNhanVien);

        bool Create(NhanVienDTO nhanVienDto);

        bool Update(NhanVienDTO nhanVienDto);

        bool Delete(string MaNhanVien);

        List<NhanVienDTO> GetAll();

        List<NhanVienDTO> GetEmployeeNotAccount();
        bool CheckExistsNhanVienByIdentityCardNumber(string cmnd);
        NhanVienDTO GetByCMND(string cmnd);
        //   bool CheckExistsEmployeePositionInEmployee(string nhanVienId);

    }
}
