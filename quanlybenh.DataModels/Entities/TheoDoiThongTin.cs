using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.DataModels.Entities
{
    [Table("TheoDoiThongTin")]
    public partial class TheoDoiThongTin
    {
        [Key]
        public Guid MaThongTin { get; set; }

        public Guid MaKhachHang { get; set; }
        public string TenThongTin { get; set; }
        public string TenBenh { get; set; }
        public string TenThuoc { get; set; }

        public DateTime? ThoiGianDanhThuoc { get; set; }
        public string TrieuChung { get; set; }
        public string KetQua { get; set; }
        public string GhiChu { get; set; }
        
        public virtual KhachHang KhachHang { get; set; }
    }
}
