using quanlybenh.Services.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Menu
{
  public class MenuDTO : BaseTableDTO
    {
        public Guid MenuId { get; set; }

        public string MenuName { set; get; }

        public string Status { get; set; }

        public string Description { get; set; }

    }
}
