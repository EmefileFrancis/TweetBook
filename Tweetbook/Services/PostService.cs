using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Data;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        public readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Post> GetPostByIdAsync(Guid Id)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync<Post>(x => x.Id == Id);
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync<Post>();
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            _dataContext.Posts.Add(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePostAsync(Post updatedPost)
        {
            _dataContext.Posts.Update(updatedPost);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid Id)
        {
            Post post = await _dataContext.Posts.SingleOrDefaultAsync<Post>(x => x.Id == Id);
            if (post == null)
                return false;
            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
