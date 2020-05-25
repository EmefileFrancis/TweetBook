using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post Get(Guid Id);
        void Create(Post post);
        bool Update(Post updatedPost);
        bool Delete(Guid Id);
    }
}
