using System;

namespace PmxLib
{
	// Token: 0x0200001D RID: 29
	internal static class PmxLibClass
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public static void Unlock(string key)
		{
			PmxLibClass.m_lock = true;
			bool flag = key == PmxLibClass.RString(-167698971, "UnlockPmxLibClass");
			if (flag)
			{
				PmxLibClass.m_lock = false;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		public static bool IsLocked()
		{
			return PmxLibClass.m_lock;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public static string RString(int s, string str)
		{
			Random random = new Random(s);
			char[] array = str.ToCharArray();
			int i = array.Length;
			while (i > 1)
			{
				i--;
				int num = random.Next(i + 1);
				char c = array[num];
				array[num] = array[i];
				array[i] = c;
			}
			return new string(array);
		}

		// Token: 0x040000B6 RID: 182
		private static bool m_lock = false;
	}
}
