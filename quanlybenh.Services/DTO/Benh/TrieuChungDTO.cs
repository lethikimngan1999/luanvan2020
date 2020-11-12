using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
    public class TrieuChungDTO
    {
        public Guid MaTrieuChung { get; set; }

        public Guid MaBenh { get; set; }

    
        public string TenTrieuChung { get; set; }

        public string MoTaTrieuChung { get; set; }

        public BenhDTO Benh { get; set; }
    }
}
