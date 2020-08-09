using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
	// Token: 0x02000033 RID: 51
	internal static class V3_BytesConvert
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000152D8 File Offset: 0x000134D8
		public static int ByteCount
		{
			get
			{
				return V3_BytesConvert.UnitBytes;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000152F0 File Offset: 0x000134F0
		public static byte[] ToBytes(Vector3 v3)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(v3.x));
			list.AddRange(BitConverter.GetBytes(v3.y));
			list.AddRange(BitConverter.GetBytes(v3.z));
			return list.ToArray();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00015344 File Offset: 0x00013544
		public static Vector3 FromBytes(byte[] bytes, int startIndex)
		{
			int num = 4;
			float x = BitConverter.ToSingle(bytes, startIndex);
			float y = BitConverter.ToSingle(bytes, startIndex + num);
			float z = BitConverter.ToSingle(bytes, startIndex + num * 2);
			return new Vector3(x, y, z);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015380 File Offset: 0x00013580
		public static Vector3 FromStream(Stream s)
		{
			Vector3 zero = Vector3.zero;
			byte[] array = new byte[12];
			s.Read(array, 0, 12);
			int num = 0;
			zero.x = BitConverter.ToSingle(array, num);
			num += 4;
			zero.y = BitConverter.ToSingle(array, num);
			num += 4;
			zero.z = BitConverter.ToSingle(array, num);
			return zero;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000153E0 File Offset: 0x000135E0
		public static void ToStream(Stream s, Vector3 v)
		{
			s.Write(BitConverter.GetBytes(v.x), 0, 4);
			s.Write(BitConverter.GetBytes(v.y), 0, 4);
			s.Write(BitConverter.GetBytes(v.z), 0, 4);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00015420 File Offset: 0x00013620
		public static Color Vector3ToColor(Vector3 v)
		{
			Color result = default(Color);
			result.r = v.x;
			result.g = v.y;
			result.b = v.z;
			return result;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00015464 File Offset: 0x00013664
		public static Vector3 ColorToVector3(Color color)
		{
			return new Vector3
			{
				x = color.r,
				y = color.g,
				z = color.b
			};
		}

		// Token: 0x0400014C RID: 332
		public static readonly int UnitBytes = 12;
	}
}
