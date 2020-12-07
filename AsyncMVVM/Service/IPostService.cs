using AsyncMVVM.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AsyncMVVM.Service
{
    public interface IPostService
    {
        Task<ObservableCollection<Post>> GetPostsAsync();
        Task<ObservableCollection<Post>> GetPostsHttpAsync();
        Task<ObservableCollection<Post>> GetPostsFileAsync();
        Task<ObservableCollection<Post>> GetPostsDbAsync();
    }
}
