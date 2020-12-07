using AsyncMVVM.Model;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Web.Http;

namespace AsyncMVVM.Service
{
    public class PostService : IPostService
    {
        public async Task<ObservableCollection<Post>> GetPostsAsync()
        {
            // This delay simulates a long (or potentially long) running operation.
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);

            //throw new Exception("The caller in the view model will catch this");

            var posts = new ObservableCollection<Post>();
            for (int i = 1; i <= 20; i++)
            {
                var post = new Post()
                {
                    Title = "Post " + i,
                    Content = "The quick brown fox jumps over the lazy dog.",
                    PostDate = DateTime.Now
                };
                posts.Add(post);
            }
            return posts;
        }

        public async Task<ObservableCollection<Post>> GetPostsHttpAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);

            using (var client = new HttpClient())
            {
                string json = await client.GetStringAsync(new Uri("http://localhost:3000/posts"));
                var posts = JsonConvert.DeserializeObject<ObservableCollection<Post>>(json);
                return posts;
            }
        }

        public async Task<ObservableCollection<Post>> GetPostsFileAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);

            var installedLocation = Package.Current.InstalledLocation; //AsyncMVVM\bin\x86\Debug\AppX
            var file = await installedLocation.GetFileAsync("posts.json");
            string json = await FileIO.ReadTextAsync(file);
            var posts = JsonConvert.DeserializeObject<ObservableCollection<Post>>(json);
            return posts;
        }

        public async Task<ObservableCollection<Post>> GetPostsDbAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);

            string connectionString = "Data Source=SQL-TEST;Initial Catalog=TEST_DB;Integrated Security=SSPI;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                string commandString = "sp_TestGetPosts";
                using (var command = new SqlCommand(commandString, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        var posts = new ObservableCollection<Post>();
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var post = new Post
                            {
                                Title = (string)reader["Title"],
                                Content = (string)reader["Content"],
                                PostDate = (DateTime)reader["PostDate"],
                                // Use reader.GetString(int) to optimize large data sets.
                            };
                            posts.Add(post);
                        }
                        return posts;
                    }
                }
            }
        }
    }
}
