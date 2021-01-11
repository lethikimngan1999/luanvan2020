using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface ITheoDoiThongTinService
    {
        List<TheoDoiThongTinDTO> GetAll();

       // ChungLoaiDTO GetById(string machungloai);

        bool Create(TheoDoiThongTinDTO thongtinDto);


        bool Update(TheoDoiThongTinDTO thongtinDto);

        bool Delete(string mathongtin);

        List<ThongKeDTO> Thongke(int? month, int? date);

    }
}
