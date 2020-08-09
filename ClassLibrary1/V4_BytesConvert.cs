using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
	// Token: 0x02000034 RID: 52
	internal static class V4_BytesConvert
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000154B0 File Offset: 0x000136B0
		public static int ByteCount
		{
			get
			{
				return V4_BytesConvert.UnitBytes;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000154C8 File Offset: 0x000136C8
		public static byte[] ToBytes(Vector4 v4)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(v4.x));
			list.AddRange(BitConverter.GetBytes(v4.y));
			list.AddRange(BitConverter.GetBytes(v4.z));
			list.AddRange(BitConverter.GetBytes(v4.w));
			return list.ToArray();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00015530 File Offset: 0x00013730
		public static Vector4 FromBytes(byte[] bytes, int startIndex)
		{
			int num = 4;
			float x = BitConverter.ToSingle(bytes, startIndex);
			float y = BitConverter.ToSingle(bytes, startIndex + num);
			float z = BitConverter.ToSingle(bytes, startIndex + num * 2);
			float w = BitConverter.ToSingle(bytes, startIndex + num * 3);
			return new Vector4(x, y, z, w);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001557C File Offset: 0x0001377C
		public static Vector4 FromStream(Stream s)
		{
			Vector4 zero = Vector4.zero;
			byte[] array = new byte[16];
			s.Read(array, 0, 16);
			int num = 0;
			zero.x = BitConverter.ToSingle(array, num);
			num += 4;
			zero.y = BitConverter.ToSingle(array, num);
			num += 4;
			zero.z = BitConverter.ToSingle(array, num);
			num += 4;
			zero.w = BitConverter.ToSingle(array, num);
			return zero;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000155F0 File Offset: 0x000137F0
		public static void ToStream(Stream s, Vector4 v)
		{
			s.Write(BitConverter.GetBytes(v.x), 0, 4);
			s.Write(BitConverter.GetBytes(v.y), 0, 4);
			s.Write(BitConverter.GetBytes(v.z), 0, 4);
			s.Write(BitConverter.GetBytes(v.w), 0, 4);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00015650 File Offset: 0x00013850
		public static Color Vector4ToColor(Vector4 v)
		{
			Color result = default(Color);
			result.a = v.w;
			result.r = v.x;
			result.g = v.y;
			result.b = v.z;
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000156A0 File Offset: 0x000138A0
		public static Vector4 ColorToVector4(Color color)
		{
			return new Vector4
			{
				w = color.a,
				x = color.r,
				y = color.g,
				z = color.b
			};
		}

		// Token: 0x0400014D RID: 333
		public static readonly int UnitBytes = 16;
	}
}
