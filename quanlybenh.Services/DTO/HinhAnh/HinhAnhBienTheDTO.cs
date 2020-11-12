using Microsoft.AspNetCore.Http;
using quanlybenh.Services.DTO.BienThe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.HinhAnh
{
    public class HinhAnhBienTheDTO
    {
        public Guid MaHinhAnhBT { get; set; }

        public Guid MaBienThe { get; set; }
     
        public string DuongDan { get; set; }

        public bool? ChonAvt { get; set; }
        public DateTime? NgayTao { get; set; }

        public string TenHinhAnh { get; set; }
        public virtual BienTheDTO BienThe { get; set; }
    }
}
