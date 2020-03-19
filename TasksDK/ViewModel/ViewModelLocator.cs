using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TasksDK.Interfaces;
using TasksDK.Model.DataProviders;

namespace TasksDK.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IEmployeeProvider, InMemoryEmployeeProvider>();
            SimpleIoc.Default.Register<ITaskProvider, InMemoryTaskProvider>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}