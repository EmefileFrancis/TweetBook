using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(postService.GetAll());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]Guid postId)
        {
            var post = postService.Get(postId);
            if (post == null)
                return NotFound();
            var postResponse = new PostResponse
            {
                Id = post.Id,
                Name = post.Name
            };
            return Ok(postResponse);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute]Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post {
                Id = postId,
                Name = request.Name
            };
            if (postService.Update(post))
                return Ok();
            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            if (Guid.Empty == postRequest.Id)
                postRequest.Id = Guid.NewGuid();

            var post = new Post { 
                Id = postRequest.Id,
                Name = postRequest.Name
            };

            postService.Create(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new CreatePostRequest { Id = post.Id };

            return Created(locationUri, response);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute]Guid postId)
        {
            if (postService.Delete(postId))
                return NoContent();
            return NotFound();
        }
    }
}
