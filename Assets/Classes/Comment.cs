using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Comment
	{
		public string username;
		public string comment;
		public string comment_timestamp;
		public string explorer_avatar;

		public static Comment GetComment(string jsonString)
		{
			return JsonUtility.FromJson<Comment>(jsonString);
		}
	}
}

