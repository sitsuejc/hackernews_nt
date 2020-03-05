using System.Collections.Generic;

namespace CoreApi.Models.HackerNews
{
	public class Story : ItemBase
	{
		public string By { get; set; }

		public int Descendants { get; set; }

		public List<int> Kids { get; set; }

		public int Score { get; set; }

		public int Time { get; set; }

		public string Title { get; set; }

		// TODO: Change to enum
		public string Type { get; set; }

		public string Url { get; set; }
	}
}
