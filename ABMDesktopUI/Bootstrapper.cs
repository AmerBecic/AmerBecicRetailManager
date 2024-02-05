using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ABMDesktopUI.Helpers;
using ABMDesktopUI.Library.Api;
using ABMDesktopUI.Library.Helpers;
using ABMDesktopUI.Library.Models;
using ABMDesktopUI.Models;
using ABMDesktopUI.ViewModels;
using AutoMapper;
using Caliburn.Micro;

namespace ABMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                    PasswordBoxHelper.BoundPasswordProperty,
                    "Password",
                    "PasswordChanged");

            // other bootstrapper stuff here
        }

        protected override void Configure()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartProductModel, CartProductDisplayModel>();
            });

            var mapper = configMapper.CreateMapper();

            _container.Instance<IMapper>(mapper);

            _container.Instance(_container)
                .PerRequest<ISaleApi, SaleApi>()
                .PerRequest<IProductApi, ProductApi>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAPIHelper, APIHelper>()
                .Singleton<IConfigHelper, ConfigHelper>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
