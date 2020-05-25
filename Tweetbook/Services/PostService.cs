using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        public readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();
            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post {
                    Id = Guid.NewGuid(),
                    Name = $"Post Name {i}"
                });
            }
        }

        public Post Get(Guid Id)
        {
            return _posts.SingleOrDefault(x => x.Id == Id);
        }

        public List<Post> GetAll()
        {
            return _posts;
        }

        public void Create(Post post)
        {
            _posts.Add(post);
        }

        public bool Update(Post updatedPost)
        {
            var exist = Get(updatedPost.Id) != null;
            if (!exist)
                return false;
            var index = _posts.FindIndex(x => x.Id == updatedPost.Id);
            _posts[index] = updatedPost;
            return true;
        }

        public bool Delete(Guid Id)
        {
            var post = Get(Id);
            if (post == null)
                return false;
            _posts.Remove(post);
            return true;
        }
    }
}
