
using CoreApi.Models.HackerNews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApi.Interfaces
{
	public interface IHackerNewsRequestManager
	{
		Task<List<int>> GetBestStoryIds();
		Task<Story> GetStoryDetails(int storyId);
	}
}
