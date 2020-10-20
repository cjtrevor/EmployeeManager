using Caliburn.Micro;
using EmployeeManager.Binaries.Services;
using EmployeeManager.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IRestApiService, RestApiService>();

            _container.RegisterPerRequest(typeof(IShellViewModel), "", typeof(ShellViewModel));
            _container.RegisterPerRequest(typeof(IEmployeeService), "", typeof(EmployeeService));
            _container.RegisterPerRequest(typeof(IDataValidator), "", typeof(DataValidator));
            _container.RegisterPerRequest(typeof(IConfigurationService), "", typeof(ConfigurationService));
            _container.RegisterPerRequest(typeof(ISerializeService), "", typeof(SerializeService));

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
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
