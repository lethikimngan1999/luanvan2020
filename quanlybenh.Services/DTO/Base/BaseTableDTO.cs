using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Base
{
    public class BaseTableDTO
    {
        public string CreatedBy { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }
    }

    public interface IBaseDTO
    {
        string CreatedBy { set; get; }

        DateTime? CreatedDate { set; get; }

        string UpdatedBy { set; get; }

        DateTime? UpdatedDate { set; get; }
    }

}
