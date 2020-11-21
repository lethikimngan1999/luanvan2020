using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
   public class TrieuChungBenhDTO
    {
        public Guid MaBenh { get; set; }
        public Guid MaTrieuChung { get; set; }

        public BenhDTO Benh { get; set; }

        public TrieuChungDTO TrieuChung { get; set; }
        public IEnumerable<string> MaTrieuChungs { get; set; }
    }
}
