using CoreApi.Interfaces;
using CoreApi.Models.HackerNews;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApi.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
	public class HackerNewsController : ControllerBase
	{
		private readonly IHackerNewsRequestManager _hackerNewsRequestManager;

		public HackerNewsController(IHackerNewsRequestManager hackerNewsRequestManager)
		{
			_hackerNewsRequestManager = hackerNewsRequestManager;
		}

		/// <summary>
		/// Return the IDs for the current Best Stories on Hacker News.
		/// </summary>
		/// <returns></returns>
		[HttpGet("BestStoryIds")]
		[ResponseCache(CacheProfileName = "Cache30Seconds")]
		public async Task<ActionResult<IEnumerable<int>>> GetBestStoryIds()
		{
			List<int> ids = await _hackerNewsRequestManager.GetBestStoryIds();
			if (ids == null)
			{
				return BadRequest();
			}
			return Ok(ids);
		}

		/// <summary>
		/// Return the details for the given story.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("StoryDetails/{id}")]
		[ResponseCache(CacheProfileName = "Cache30Seconds")]
		public async Task<ActionResult<Story>> GetStoryDetails(int id)
		{
			return Ok(await _hackerNewsRequestManager.GetStoryDetails(id));
		}

		/// <summary>
		/// Return a list of (fully-resolved) Best Stories on Hacker News.
		/// </summary>
		/// <returns></returns>
		[HttpGet("BestStories")]
		[ResponseCache(CacheProfileName = "Cache30Seconds")]
		public async Task<ActionResult<List<Story>>> GetBestStories()
		{
			List<Story> stories = await _hackerNewsRequestManager.GetBestStories();
			if (stories == null)
			{
				return BadRequest();
			}
			return Ok(stories);
		}

		[HttpGet("BestStories/{searchText}")]
		public async Task<ActionResult<List<Story>>> GetBestStories(string searchText)
		{
			List<Story> stories = await _hackerNewsRequestManager.GetBestStories(searchText);
			if (stories == null)
			{
				return BadRequest();
			}
			return Ok(stories);
		}

	}
}