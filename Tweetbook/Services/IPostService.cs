using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(Guid Id);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post updatedPost);
        Task<bool> DeletePostAsync(Guid Id);
    }
}
