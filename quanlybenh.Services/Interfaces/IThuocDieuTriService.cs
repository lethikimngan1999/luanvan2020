using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IThuocDieuTriService
    {

        // get danh sach thuoc theo benh
        List<ThuocDieuTriBenhDTO> GetListByMaBenh(string mabenh);
        List<ThuocDieuTriBenhDTO> GetAll();

        bool Add(List<ThuocDieuTriBenhDTO> thuocdieutriDtos);

        bool AddDieuTriBenh(List<ThuocDieuTriBenhDTO> thuocdieutriDtos);

    }
}
