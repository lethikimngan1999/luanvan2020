using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
   public class ThuocDieuTriBenhDTO
    {
        public Guid MaThuoc { get; set; }
        public Guid MaBenh { get; set; }

        public string LieuDung { get; set; }

        public string MoTa { get; set; }

        public  BenhDTO Benh { get; set; }

        public  ThuocDTO Thuoc { get; set; }
    }
}
