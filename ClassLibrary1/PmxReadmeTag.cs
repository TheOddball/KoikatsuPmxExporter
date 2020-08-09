using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000025 RID: 37
	internal static class PmxReadmeTag
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00011524 File Offset: 0x0000F724
		public static string[] GetReadme(Pmx pmx, string root)
		{
			bool flag = root == null;
			if (flag)
			{
				root = "";
			}
			string[] tag = PmxTag.GetTag("readme", pmx.ModelInfo.CommentE);
			bool flag2 = tag != null && !string.IsNullOrEmpty(root);
			if (flag2)
			{
				for (int i = 0; i < tag.Length; i++)
				{
					bool flag3 = !Path.IsPathRooted(tag[i]);
					if (flag3)
					{
						tag[i] = root + "\\" + tag[i];
					}
				}
			}
			return tag;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public static void SetReadme(Pmx pmx, string path)
		{
			PmxModelInfo modelInfo = pmx.ModelInfo;
			PmxModelInfo pmxModelInfo = modelInfo;
			PmxModelInfo pmxModelInfo2 = pmxModelInfo;
			pmxModelInfo2.CommentE += Environment.NewLine;
			PmxTag.SetTag("readme", pmx.ModelInfo.CommentE);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public static bool ExistReadme(Pmx pmx)
		{
			return PmxTag.ExistTag("readme", pmx.ModelInfo.CommentE);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001161B File Offset: 0x0000F81B
		public static void RemoveReadme(Pmx pmx)
		{
			pmx.ModelInfo.CommentE = PmxTag.RemoveTag("readme", pmx.ModelInfo.CommentE);
			pmx.ModelInfo.CommentE = pmx.ModelInfo.CommentE.Trim();
		}

		// Token: 0x0400010E RID: 270
		public const string TAG_README = "readme";
	}
}
