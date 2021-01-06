namespace quanlybenh.DataModels.Entities
{
    using System.Data.Entity;
    using quanlybenh.Utilities.Configurations;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base(AppSettings.ConnectString)
          
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new DbInitializer(new ApplicationUserManager(new UserStore(this))));
        }
        public static AppDbContext Init()
        {
            return new AppDbContext();
        }

        public virtual DbSet<Benh> Benhs { get; set; }
        public virtual DbSet<BienThe> BienThes { get; set; }
        public virtual DbSet<Ca> Cas { get; set; }
        public virtual DbSet<ChatLuong> ChatLuongs { get; set; }
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; }
        public virtual DbSet<ChungLoai> ChungLoais { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<DonNhap> DonNhaps { get; set; }
        public virtual DbSet<DonNhapChiTiet> DonNhapChiTiets { get; set; }
        public virtual DbSet<Giong> Giongs { get; set; }
        public virtual DbSet<HinhAnhBienThe> HinhAnhBienThes { get; set; }
        public virtual DbSet<HinhAnhCa> HinhAnhCas { get; set; }
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public virtual DbSet<HoaDonXuat> HoaDonXuats { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<TheoDoiThongTin> TheoDoiThongTins { get; set; }
        public virtual DbSet<LieuTrinh> LieuTrinhs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NoiCungCap> NoiCungCaps { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Thuoc> Thuocs { get; set; }
        public virtual DbSet<ThuocDieuTri> ThuocDieuTris { get; set; }
        public virtual DbSet<TrieuChungBenh> TrieuChungBenhs { get; set; }
        public virtual DbSet<TrieuChung> TrieuChungs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Role>().HasKey(x => new { x.Id });
            modelBuilder.Entity<UserRole>().ToTable("UserRoles").HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<Menu>().HasKey(p => p.MenuId).HasIndex(p => p.MenuName).IsUnique();
            modelBuilder.Entity<RolePermission>().HasKey(c => new { c.RoleId, c.MenuId });


            modelBuilder.Entity<Benh>()
                .HasMany(e => e.ThuocDieuTris)
                .WithRequired(e => e.Benh)
                .WillCascadeOnDelete(false);

          

            modelBuilder.Entity<BienThe>()
                .HasMany(e => e.Cas)
                .WithRequired(e => e.BienThe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BienThe>()
                .HasMany(e => e.HinhAnhBienThes)
                .WithRequired(e => e.BienThe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ca>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.Ca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ca>()
                .HasMany(e => e.DonNhapChiTiets)
                .WithRequired(e => e.Ca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ca>()
                .HasMany(e => e.HinhAnhCas)
                .WithRequired(e => e.Ca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ca>()
                .HasMany(e => e.HoaDonChiTiets)
                .WithRequired(e => e.Ca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatLuong>()
                .HasMany(e => e.BienThes)
                .WithRequired(e => e.ChatLuong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChungLoai>()
                .HasMany(e => e.BienThes)
                .WithRequired(e => e.ChungLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DatHang>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.DatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonNhap>()
                .HasMany(e => e.DonNhapChiTiets)
                .WithRequired(e => e.DonNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Giong>()
                .HasMany(e => e.BienThes)
                .WithRequired(e => e.Giong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDonXuat>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonXuat>()
                .HasMany(e => e.HoaDonChiTiets)
                .WithRequired(e => e.HoaDonXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DatHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDonXuats)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
              .HasMany(e => e.TheoDoiThongTins)
              .WithRequired(e => e.KhachHang)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NoiCungCap>()
                .HasMany(e => e.DonNhaps)
                .WithRequired(e => e.NoiCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RolePermission>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.ThuocDieuTris)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.LieuTrinhs)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.DonNhaps)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.HoaDonXuats)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
