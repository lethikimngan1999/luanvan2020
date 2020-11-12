using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.DataModels.Entities
{
    public class BaseEntity
    {
        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }
    }

    public interface IBaseEntity
    {
        DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        string CreatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }

        [StringLength(128)]
        string UpdatedBy { get; set; }
    }

}
