using CoreApi.Interfaces;
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
		public async Task<ActionResult<IEnumerable<int>>> GetBestStoryIds()
		{
			List<int> ids = await _hackerNewsRequestManager.GetBestStoryIds();
			if (ids == null)
			{
				return BadRequest();
			}
			return Ok(ids);
		}
	}
}