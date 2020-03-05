using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApi.Interfaces;
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
			// Generate and execute the API request
			IRestResponse response = await ExecuteGet($"{BaseUrl}/beststories.json?{_printParam}");
			if (!response.IsSuccessful)
			{
				// TODO: Log the error
				Console.WriteLine(response.Content);
				return null;
			}

			// The API returns an array of numbers, so deserialize to that
			return JsonConvert.DeserializeObject<List<int>>(response.Content);
		}

		private async Task<IRestResponse> ExecuteGet(string url)
		{
			RestClient client = new RestClient(url);
			RestRequest request = new RestRequest(Method.GET);
			return await client.ExecuteAsync(request);
		}
	}
}
