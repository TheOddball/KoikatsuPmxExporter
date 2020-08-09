using System;
using System.Collections.Generic;
using System.Text;

namespace PmxLib
{
	// Token: 0x02000005 RID: 5
	internal static class BytesStringProc
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000A6F4 File Offset: 0x000088F4
		public static void SetString(byte[] buf, string s, byte fix, byte fill)
		{
			bool flag = fill == 0;
			if (flag)
			{
				fill = 253;
			}
			s = s.Replace("\r\n", "\n");
			List<byte> list = new List<byte>(BytesStringProc.m_sjis.GetBytes(s));
			list.Add(fix);
			bool flag2 = list.Count > buf.Length;
			if (flag2)
			{
				list[buf.Length - 1] = fix;
			}
			for (int i = 0; i < buf.Length; i++)
			{
				buf[i] = fill;
			}
			byte[] array = list.ToArray();
			int length = Math.Min(buf.Length, array.Length);
			Array.Copy(array, buf, length);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000A798 File Offset: 0x00008998
		public static string GetString(byte[] buf, byte fix)
		{
			int num = buf.Length;
			int count = buf.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = buf[i] == fix;
				if (flag)
				{
					count = i;
					break;
				}
			}
			string @string = BytesStringProc.m_sjis.GetString(buf, 0, count);
			bool flag2 = @string == null;
			string result;
			if (flag2)
			{
				result = "";
			}
			else
			{
				result = @string.Replace("\n", "\r\n");
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000A818 File Offset: 0x00008A18
		public static byte[] StringToBuf_SJIS(string s)
		{
			bool flag = s.Length <= 0;
			byte[] result;
			if (flag)
			{
				result = new byte[0];
			}
			else
			{
				result = BytesStringProc.m_sjis.GetBytes(s);
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000A854 File Offset: 0x00008A54
		public static string BufToString_SJIS(byte[] buf)
		{
			bool flag = buf.Length == 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = BytesStringProc.m_sjis.GetString(buf, 0, buf.Length);
			}
			return result;
		}

		// Token: 0x04000018 RID: 24
		private static Encoding m_sjis = Encoding.GetEncoding("shift_jis");
	}
}
