using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Interfaces;
using CoreApi.Models.HackerNews;
using Newtonsoft.Json;
using RestSharp;

namespace CoreApi.Managers
{
	public class HackerNewsRequestManager : IHackerNewsRequestManager
	{
		private readonly string _apiRoot = "https://hacker-news.firebaseio.com/";
		private readonly string _apiVersion = "v0";
		private readonly string _printParam = "print=pretty";

		private string BaseUrl
		{
			get
			{
				return $"{_apiRoot}/{_apiVersion}";
			}
		}

		/// <summary>
		/// Returns a list of IDs that are the current Best Stories from Hacker News.
		/// If the request fails, null is returned.
		/// </summary>
		/// <returns></returns>
		public async Task<List<int>> GetBestStoryIds()
		{
			return await ExecuteGet<List<int>>($"{BaseUrl}/beststories.json?{_printParam}");
		}

		/// <summary>
		/// Returns the story details for the provided ID.
		/// </summary>
		/// <param name="storyId"></param>
		/// <returns></returns>
		public async Task<Story> GetStoryDetails(int storyId)
		{
			return await ExecuteGet<Story>($"{BaseUrl}/item/{storyId}.json?{_printParam}");
		}

		/// <summary>
		/// Returns the details for each story based on the Best Stories from Hacker News.
		/// </summary>
		/// <returns></returns>
		public async Task<List<Story>> GetBestStories()
		{
			// Need all the IDs first
			List<int> bestStoryIds = await GetBestStoryIds();
			if (bestStoryIds == null)
			{
				return null;
			}

			// Query to resolve details for each story. ToArray executes the query
			var queries = 
				from id in bestStoryIds select GetStoryDetails(id);

			// ToArray executes the query
			var executingQueries = queries.ToArray();

			// Execute the requests in parallel, waiting for all to finish before returning
			return (await Task.WhenAll(executingQueries)).ToList();
		}

		private async Task<T> ExecuteGet<T>(string url)
		{
			RestClient client = new RestClient(url);
			RestRequest request = new RestRequest(Method.GET);
			IRestResponse response = await client.ExecuteAsync(request);

			if (!response.IsSuccessful)
			{
				// TODO: Log the error
				Console.WriteLine(response.Content);
				return default;
			}
			return JsonConvert.DeserializeObject<T>(response.Content);
		}
	}
}
