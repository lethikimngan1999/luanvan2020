namespace quanlybenh.DataModels.Entities
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Table("User")]
    public class User : IdentityUser<Guid, IdentityUserLogin<Guid>, IdentityUserRole<Guid>, IdentityUserClaim<Guid>>, IBaseEntity
    {
        //public User()
        //{
        //    DonNhaps = new HashSet<DonNhap>();
        //    HoaDonXuats = new HashSet<HoaDonXuat>();
        //    UserRoles = new HashSet<UserRole>();
        //}

        public Guid MaNhanVien { get; set; }
        public Guid? MaKhachHang { get; set; }


        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public virtual ICollection<DonNhap> DonNhaps { get; set; }

      
        public virtual ICollection<HoaDonXuat> HoaDonXuats { get; set; }

        public virtual NhanVien NhanVien { get; set; }
        public virtual KhachHang KhachHang { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            return userIdentity;
        }

    }
}
