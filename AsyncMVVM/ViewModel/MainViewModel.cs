using AsyncAwaitBestPractices.MVVM;
using AsyncMVVM.Model;
using AsyncMVVM.Service;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPostService _postService;

        public MainViewModel(IPostService postService)
        {
            _postService = postService;
            RefreshCommand = new AsyncCommand(ExecuteRefreshCommand, _ => !IsListRefreshing);
        }

        public IAsyncCommand RefreshCommand { get; }

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }

        private bool _isListRefreshing;
        public bool IsListRefreshing
        {
            get => _isListRefreshing;
            set => Set(ref _isListRefreshing, value);
        }

        private async Task ExecuteRefreshCommand()
        {
            Debug.WriteLine("Refreshing list...");
            IsListRefreshing = true;

            try
            {
                // Use ConfigureAwait(true) to callback to the main thread

                // Get posts from code
                Posts = await _postService.GetPostsAsync().ConfigureAwait(true);

                // Get posts from Web API
                //Posts = await _postService.GetPostsHttpAsync().ConfigureAwait(true);

                // Get posts from File
                //Posts = await _postService.GetPostsFileAsync().ConfigureAwait(true);

                // Get posts from SQL Server
                //Posts = await _postService.GetPostsDbAsync().ConfigureAwait(true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                IsListRefreshing = false;
                Debug.WriteLine("Refreshing list complete!");
            }
        }
    }
}
