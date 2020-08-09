using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000030 RID: 48
	internal class SystemToon
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public static string GetToonName(int n)
		{
			bool flag = n < 0;
			string result;
			if (flag)
			{
				result = "toon0.bmp";
			}
			else
			{
				result = "toon" + (n + 1).ToString("00") + ".bmp";
			}
			return result;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00014D00 File Offset: 0x00012F00
		public static string[] GetToonNames()
		{
			string[] array = new string[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = SystemToon.GetToonName(i);
			}
			return array;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00014D38 File Offset: 0x00012F38
		private static void CreateNameTable()
		{
			int num = 10;
			SystemToon.m_nametable = new Dictionary<string, int>(num + 1);
			for (int i = -1; i < num; i++)
			{
				SystemToon.m_nametable.Add(SystemToon.GetToonName(i), i);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00014D7C File Offset: 0x00012F7C
		public static bool IsSystemToon(string name)
		{
			bool flag = SystemToon.m_nametable == null;
			if (flag)
			{
				SystemToon.CreateNameTable();
			}
			return !string.IsNullOrEmpty(name) && SystemToon.m_nametable.ContainsKey(name);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00014DB8 File Offset: 0x00012FB8
		public static int GetToonIndex(string name)
		{
			bool flag = SystemToon.m_nametable == null;
			if (flag)
			{
				SystemToon.CreateNameTable();
			}
			bool flag2 = !string.IsNullOrEmpty(name) && SystemToon.m_nametable.ContainsKey(name);
			int result;
			if (flag2)
			{
				result = SystemToon.m_nametable[name];
			}
			else
			{
				result = -2;
			}
			return result;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00014E10 File Offset: 0x00013010
		public SystemToon.ToonInfo GetToonInfo(List<PmxMaterial> list)
		{
			int count = list.Count;
			bool flag = count < 0;
			SystemToon.ToonInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SystemToon.ToonInfo toonInfo = new SystemToon.ToonInfo(count);
				bool[] array = new bool[10];
				List<string> list2 = new List<string>(count);
				Dictionary<string, int> dictionary = new Dictionary<string, int>(count);
				for (int i = 0; i < count; i++)
				{
					PmxMaterial pmxMaterial = list[i];
					bool flag2 = string.IsNullOrEmpty(pmxMaterial.Toon);
					if (flag2)
					{
						toonInfo.MaterialToon[i] = -1;
					}
					else
					{
						int toonIndex = SystemToon.GetToonIndex(pmxMaterial.Toon);
						toonInfo.MaterialToon[i] = toonIndex;
						bool flag3 = -1 <= toonIndex && toonIndex < 10;
						if (flag3)
						{
							bool flag4 = toonIndex >= 0;
							if (flag4)
							{
								array[toonIndex] = true;
							}
						}
						else
						{
							bool flag5 = !dictionary.ContainsKey(pmxMaterial.Toon);
							if (flag5)
							{
								list2.Add(pmxMaterial.Toon);
								dictionary.Add(pmxMaterial.Toon, 0);
							}
						}
					}
				}
				bool flag6 = list2.Count > 0;
				if (flag6)
				{
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>(list2.Count);
					int num = 0;
					for (int j = 0; j < list2.Count; j++)
					{
						for (int k = num; k < array.Length; k++)
						{
							bool flag7 = !array[k];
							if (flag7)
							{
								toonInfo.ToonNames[k] = list2[j];
								dictionary2.Add(list2[j], k);
								array[k] = true;
								num = k + 1;
								break;
							}
						}
					}
					for (int l = 0; l < count; l++)
					{
						bool flag8 = toonInfo.MaterialToon[l] < -1;
						if (flag8)
						{
							PmxMaterial pmxMaterial2 = list[l];
							bool flag9 = dictionary2.ContainsKey(pmxMaterial2.Toon);
							if (flag9)
							{
								toonInfo.MaterialToon[l] = dictionary2[pmxMaterial2.Toon];
							}
							else
							{
								toonInfo.MaterialToon[l] = -1;
								toonInfo.IsRejection = true;
							}
						}
					}
				}
				result = toonInfo;
			}
			return result;
		}

		// Token: 0x04000149 RID: 329
		public const int EnableToonCount = 10;

		// Token: 0x0400014A RID: 330
		private static Dictionary<string, int> m_nametable;

		// Token: 0x02000065 RID: 101
		public class ToonInfo
		{
			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001A153 File Offset: 0x00018353
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x0001A15B File Offset: 0x0001835B
			public string[] ToonNames { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001A164 File Offset: 0x00018364
			// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001A16C File Offset: 0x0001836C
			public int[] MaterialToon { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001A175 File Offset: 0x00018375
			// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001A17D File Offset: 0x0001837D
			public bool IsRejection { get; set; }

			// Token: 0x060003D6 RID: 982 RVA: 0x0001A186 File Offset: 0x00018386
			public ToonInfo(int count)
			{
				this.ToonNames = SystemToon.GetToonNames();
				this.MaterialToon = new int[count];
				this.IsRejection = false;
			}
		}
	}
}
