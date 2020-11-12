using quanlybenh.Services.DTO.HinhAnh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.BienThe
{
    public class BienTheDTO
    {
        public Guid MaBienThe { get; set; }

        public Guid MaGiong { get; set; }

        public Guid MaChatLuong { get; set; }

        public Guid MaChungLoai { get; set; }

 
        public string TenBienThe { get; set; }

        public string MauSac { get; set; }

        public bool? TinhTrang { get; set; }

        public string MoTa { get; set; }

        public  ChatLuongDTO ChatLuongs { get; set; }

        public  GiongDTO Giongs { get; set; }

        public  ChungLoaiDTO ChungLoais { get; set; }

        public List<HinhAnhBienTheDTO> Listhas { get; set; }
        public string Mahas { get; set; }
    }
}
