using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000032 RID: 50
	internal static class V2_BytesConvert
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000151CC File Offset: 0x000133CC
		public static int ByteCount
		{
			get
			{
				return V2_BytesConvert.UnitBytes;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000151E4 File Offset: 0x000133E4
		public static byte[] ToBytes(Vector2 v2)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(v2.x));
			list.AddRange(BitConverter.GetBytes(v2.y));
			return list.ToArray();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00015228 File Offset: 0x00013428
		public static Vector2 FromBytes(byte[] bytes, int startIndex)
		{
			int num = 4;
			float x = BitConverter.ToSingle(bytes, startIndex);
			float y = BitConverter.ToSingle(bytes, startIndex + num);
			return new Vector2(x, y);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00015258 File Offset: 0x00013458
		public static Vector2 FromStream(Stream s)
		{
			Vector2 zero = Vector2.zero;
			byte[] array = new byte[8];
			s.Read(array, 0, 8);
			int num = 0;
			zero.x = BitConverter.ToSingle(array, num);
			num += 4;
			zero.y = BitConverter.ToSingle(array, num);
			return zero;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000152A4 File Offset: 0x000134A4
		public static void ToStream(Stream s, Vector2 v)
		{
			s.Write(BitConverter.GetBytes(v.x), 0, 4);
			s.Write(BitConverter.GetBytes(v.y), 0, 4);
		}

		// Token: 0x0400014B RID: 331
		public static readonly int UnitBytes = 8;
	}
}
