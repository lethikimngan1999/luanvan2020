using AutoMapper;
using Newtonsoft.Json;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.AutoMapper;
using Swashbuckle.Application;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using quanlybenh.DataModels.Entities;
using System.Linq;
using quanlybenh.Services.Interfaces;
using quanlybenh.Services.Implementation;
using Microsoft.AspNet.Identity;
using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;

namespace quanlybenh
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // allow cors
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            var services = new ServiceCollection();
            services.AddCors();

            services.AddSingleton(_ => new AppDbContext());
            //  services.AddTransient<DbInitializer>();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // add config swagger rourte
            config.Routes.MapHttpRoute(
             name: "Swagger",
             routeTemplate: "",
             defaults: null,
             constraints: null,
             handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

            config.Formatters.Add(new BrowserJsonFormatter());

            // Add Repository
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));

            // add UserManager
            services.AddTransient<IUserStore<User, Guid>, UserStore>();
            services.AddTransient(typeof(ApplicationUserManager), typeof(ApplicationUserManager));

            // add RoleManager
            services.AddTransient<IRoleStore<Role, Guid>, RoleStore>();
            services.AddTransient(typeof(ApplicationRoleManager), typeof(ApplicationRoleManager));

            //Add Service
            services.AddTransient<INhanVienService, NhanVienService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBienTheService, BienTheService>();
            services.AddTransient<IHinhAnhBienTheService, HinhAnhBienTheService>();
            services.AddTransient<IThuocService, ThuocService>();
            services.AddTransient<IBenhService, BenhService>();
            services.AddTransient<IThuocDieuTriService, ThuocDieuTriService>();
            services.AddTransient<ILieuTrinhService, LieuTrinhService>();
            services.AddTransient<ITrieuChungService, TrieuChungService>();
            services.AddTransient<ITrieuChungBenhService, TrieuChungBenhService>();
            services.AddTransient<ICaService, CaService>();
            services.AddTransient<IChungLoaiService, ChungLoaiService>();
            services.AddTransient<IChatLuongService, ChatLuongService>();
            services.AddTransient<IGiongService, GiongService>();
            services.AddTransient<IKhachHangService, KhachHangService>();
            services.AddTransient<ITheoDoiThongTinService, TheoDoiThongTinService>();

            var configMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<DtoMappingProfile>();
            });
            configMapper.CompileMappings();

            IMapper mapper = configMapper.CreateMapper();
            services.AddSingleton(mapper);


            // Add all controllers as services
            services.AddControllersAsServices(typeof(WebApiConfig).Assembly.GetExportedTypes().Where(t => t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            var resolver = new MyDependencyResolver(services.BuildServiceProvider());
            config.DependencyResolver = resolver;


        }
    }

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }

    public class MyDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider _serviceProvider;

        public MyDependencyResolver(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return this._serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._serviceProvider.GetServices(serviceType);
        }

        public void AddService()
        {
        }
    }


    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddControllersAsServices(this IServiceCollection services, IEnumerable<Type> serviceTypes)
        {
            foreach (var type in serviceTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }


}
