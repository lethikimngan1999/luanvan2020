using quanlybenh.DataModels.Entities;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.DTO.HinhAnh;
using System;
using System.Collections.Generic;

namespace quanlybenh.Services.DTO.Ca
{
   public class CaDTO
    {
        public Guid MaCa { get; set; }

        public Guid MaBienThe { get; set; }

        public string TenCa { get; set; }

        public int? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string KichThuoc { get; set; }

        public DateTime? NgayDo { get; set; }

        public double? DonGia { get; set; }

        public string Tuoi { get; set; }

        public bool? TinhTrang { get; set; }


        public BienTheDTO BienThes { get; set; }

        public List<HinhAnhCaDTO> Listhacas { get; set; }
        public string Mahacas { get; set; }
    }
}
