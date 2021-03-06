﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await postService.GetAllPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post =  await postService.GetPostByIdAsync(postId);
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
        public async Task<IActionResult> Update([FromRoute]Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post {
                Id = postId,
                Name = request.Name
            };
            if (await postService.UpdatePostAsync(post))
                return Ok();
            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            
            var post = new Post { 
                Name = postRequest.Name
            };

            await postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { 
                Id = post.Id,
                Name = post.Name
            };

            return Created(locationUri, response);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId)
        {
            if (await postService.DeletePostAsync(postId))
                return NoContent();
            return NotFound();
        }
    }
}
