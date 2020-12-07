using AsyncMVVM.Service;
using GalaSoft.MvvmLight.Ioc;

namespace AsyncMVVM.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IPostService, PostService>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
