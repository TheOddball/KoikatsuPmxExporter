using System;
using System.Text.RegularExpressions;

namespace PmxLib
{
	// Token: 0x02000029 RID: 41
	internal static class PmxTag
	{
		// Token: 0x06000248 RID: 584 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public static MatchCollection MatchsTag(string tag, string text, string groupName)
		{
			bool flag = groupName == null;
			if (flag)
			{
				groupName = "gp";
			}
			string text2 = "<" + tag + ">";
			string text3 = "</" + tag + ">";
			string pattern = string.Concat(new string[]
			{
				text2,
				"(?<",
				groupName,
				">.*?)",
				text3
			});
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.Matches(text);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00012D74 File Offset: 0x00010F74
		public static string[] GetTag(string tag, string text)
		{
			MatchCollection matchCollection = PmxTag.MatchsTag(tag, text, "gp");
			bool flag = matchCollection.Count <= 0;
			string[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] array = new string[matchCollection.Count];
				for (int i = 0; i < matchCollection.Count; i++)
				{
					Match match = matchCollection[i];
					string value = match.Groups["gp"].Value;
					array[i] = (string.IsNullOrEmpty(value) ? "" : value);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00012E14 File Offset: 0x00011014
		public static string SetTag(string tag, string text)
		{
			string text2 = tag.Trim();
			return string.Concat(new string[]
			{
				"<",
				text2,
				">",
				text,
				"</",
				text2,
				">"
			});
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00012E64 File Offset: 0x00011064
		public static bool ExistTag(string tag, string text)
		{
			string[] tag2 = PmxTag.GetTag(tag, text);
			return tag2 != null;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00012E84 File Offset: 0x00011084
		public static string RemoveTag(string tag, string text)
		{
			MatchCollection matchCollection = PmxTag.MatchsTag(tag, text, "gp");
			for (int i = 0; i < matchCollection.Count; i++)
			{
				text = text.Replace(matchCollection[i].Value, "");
			}
			return text;
		}

		// Token: 0x04000124 RID: 292
		public const string GROUPNAME = "gp";
	}
}
