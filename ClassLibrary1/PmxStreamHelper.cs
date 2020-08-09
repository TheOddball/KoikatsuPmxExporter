using System;
using System.IO;
using System.Text;

namespace PmxLib
{
	// Token: 0x02000028 RID: 40
	internal static class PmxStreamHelper
	{
		// Token: 0x0600022E RID: 558 RVA: 0x000124F8 File Offset: 0x000106F8
		public static void WriteString(Stream s, string str, int f)
		{
			if (f != 0)
			{
				if (f == 1)
				{
					PmxStreamHelper.WriteString_v2(s, str, Encoding.UTF8);
				}
			}
			else
			{
				PmxStreamHelper.WriteString_v2(s, str, Encoding.Unicode);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00012534 File Offset: 0x00010734
		public static string ReadString(Stream s, int f)
		{
			string result = "";
			if (f != 0)
			{
				if (f == 1)
				{
					result = PmxStreamHelper.ReadString_v2(s, Encoding.UTF8);
				}
			}
			else
			{
				result = PmxStreamHelper.ReadString_v2(s, Encoding.Unicode);
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00012578 File Offset: 0x00010778
		public static void WriteString(Stream s, string str, PmxElementFormat f)
		{
			bool flag = f == null;
			if (flag)
			{
				f = new PmxElementFormat(2.1f);
			}
			bool flag2 = f.Ver <= 1f;
			if (flag2)
			{
				PmxStreamHelper.WriteString_v1(s, str);
			}
			else
			{
				bool flag3 = f.Ver <= 2.1f;
				if (flag3)
				{
					bool flag4 = f.StringEnc == PmxElementFormat.StringEncType.UTF8;
					if (flag4)
					{
						PmxStreamHelper.WriteString_v2(s, str, Encoding.UTF8);
					}
					else
					{
						PmxStreamHelper.WriteString_v2(s, str, Encoding.Unicode);
					}
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00012604 File Offset: 0x00010804
		public static string ReadString(Stream s, PmxElementFormat f)
		{
			bool flag = f == null;
			if (flag)
			{
				f = new PmxElementFormat(2.1f);
			}
			string result = "";
			bool flag2 = f.Ver <= 1f;
			if (flag2)
			{
				result = PmxStreamHelper.ReadString_v1(s);
			}
			else
			{
				bool flag3 = f.Ver <= 2.1f;
				if (flag3)
				{
					bool flag4 = f.StringEnc == PmxElementFormat.StringEncType.UTF8;
					if (flag4)
					{
						result = PmxStreamHelper.ReadString_v2(s, Encoding.UTF8);
					}
					else
					{
						result = PmxStreamHelper.ReadString_v2(s, Encoding.Unicode);
					}
				}
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001269C File Offset: 0x0001089C
		public static void WriteString_v1(Stream s, string str)
		{
			byte[] array = BytesStringProc.StringToBuf_SJIS(str);
			byte[] bytes = BitConverter.GetBytes(array.Length);
			s.Write(bytes, 0, bytes.Length);
			bool flag = array.Length != 0;
			if (flag)
			{
				s.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000126E0 File Offset: 0x000108E0
		public static string ReadString_v1(Stream s)
		{
			string result = "";
			byte[] array = new byte[4];
			s.Read(array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			bool flag = num > 0;
			if (flag)
			{
				array = new byte[num];
				s.Read(array, 0, num);
				result = BytesStringProc.BufToString_SJIS(array);
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00012738 File Offset: 0x00010938
		public static void WriteString_v2(Stream s, string str, Encoding ec)
		{
			bool flag = ec == null;
			if (flag)
			{
				ec = Encoding.Unicode;
			}
			byte[] bytes = ec.GetBytes(str);
			byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
			s.Write(bytes2, 0, bytes2.Length);
			bool flag2 = bytes.Length != 0;
			if (flag2)
			{
				s.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001278C File Offset: 0x0001098C
		public static string ReadString_v2(Stream s, Encoding ec)
		{
			bool flag = ec == null;
			if (flag)
			{
				ec = Encoding.Unicode;
			}
			string result = "";
			byte[] array = new byte[4];
			s.Read(array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			bool flag2 = num > 0;
			if (flag2)
			{
				array = new byte[num];
				bool flag3 = s.Read(array, 0, num) > 0;
				if (flag3)
				{
					result = ec.GetString(array, 0, array.Length);
				}
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00012804 File Offset: 0x00010A04
		public static void WriteElement_Bool(Stream s, bool data)
		{
			PmxStreamHelper.WriteElement_Int32(s, data ? 1 : 0, 1, false);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00012818 File Offset: 0x00010A18
		public static bool ReadElement_Bool(Stream s)
		{
			return PmxStreamHelper.ReadElement_Int32(s, 1, false) != 0;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00012838 File Offset: 0x00010A38
		public static void WriteElement_Int32(Stream s, int data, int bufSize, bool signed)
		{
			byte[] array = null;
			switch (bufSize)
			{
			case 1:
				if (signed)
				{
					array = new byte[]
					{
						(byte)((sbyte)data)
					};
				}
				else
				{
					array = new byte[]
					{
						(byte)data
					};
				}
				break;
			case 2:
				if (signed)
				{
					array = BitConverter.GetBytes((short)data);
				}
				else
				{
					array = BitConverter.GetBytes((ushort)data);
				}
				break;
			case 4:
				array = BitConverter.GetBytes(data);
				break;
			}
			s.Write(array, 0, array.Length);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000128C0 File Offset: 0x00010AC0
		public static int ReadElement_Int32(Stream s, int bufSize, bool signed)
		{
			int result = 0;
			byte[] array = new byte[bufSize];
			s.Read(array, 0, bufSize);
			switch (bufSize)
			{
			case 1:
				if (signed)
				{
					result = (int)((sbyte)array[0]);
				}
				else
				{
					result = (int)array[0];
				}
				break;
			case 2:
				if (signed)
				{
					short num = BitConverter.ToInt16(array, 0);
					result = (int)num;
				}
				else
				{
					ushort num2 = BitConverter.ToUInt16(array, 0);
					result = (int)num2;
				}
				break;
			case 4:
				result = BitConverter.ToInt32(array, 0);
				break;
			}
			return result;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00012950 File Offset: 0x00010B50
		public static void WriteElement_UInt(Stream s, uint data)
		{
			byte[] bytes = BitConverter.GetBytes(data);
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00012974 File Offset: 0x00010B74
		public static uint ReadElement_UInt(Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, 4);
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000129A0 File Offset: 0x00010BA0
		public static void WriteElement_Float(Stream s, float data)
		{
			byte[] bytes = BitConverter.GetBytes(data);
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000129C4 File Offset: 0x00010BC4
		public static float ReadElement_Float(Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, 4);
			return BitConverter.ToSingle(array, 0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000129EE File Offset: 0x00010BEE
		public static void WriteElement_Vector2(Stream s, Vector2 data)
		{
			PmxStreamHelper.WriteElement_Float(s, data.X);
			PmxStreamHelper.WriteElement_Float(s, data.Y);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00012A10 File Offset: 0x00010C10
		public static Vector2 ReadElement_Vector2(Stream s)
		{
			return new Vector2
			{
				X = PmxStreamHelper.ReadElement_Float(s),
				Y = PmxStreamHelper.ReadElement_Float(s)
			};
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00012A47 File Offset: 0x00010C47
		public static void WriteElement_Vector3(Stream s, Vector3 data)
		{
			PmxStreamHelper.WriteElement_Float(s, data.X);
			PmxStreamHelper.WriteElement_Float(s, data.Y);
			PmxStreamHelper.WriteElement_Float(s, data.Z);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00012A74 File Offset: 0x00010C74
		public static Vector3 ReadElement_Vector3(Stream s)
		{
			return new Vector3
			{
				X = PmxStreamHelper.ReadElement_Float(s),
				Y = PmxStreamHelper.ReadElement_Float(s),
				Z = PmxStreamHelper.ReadElement_Float(s)
			};
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00012AB9 File Offset: 0x00010CB9
		public static void WriteElement_Vector4(Stream s, Vector4 data)
		{
			PmxStreamHelper.WriteElement_Float(s, data.X);
			PmxStreamHelper.WriteElement_Float(s, data.Y);
			PmxStreamHelper.WriteElement_Float(s, data.Z);
			PmxStreamHelper.WriteElement_Float(s, data.W);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00012AF4 File Offset: 0x00010CF4
		public static Vector4 ReadElement_Vector4(Stream s)
		{
			return new Vector4
			{
				X = PmxStreamHelper.ReadElement_Float(s),
				Y = PmxStreamHelper.ReadElement_Float(s),
				Z = PmxStreamHelper.ReadElement_Float(s),
				W = PmxStreamHelper.ReadElement_Float(s)
			};
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00012B47 File Offset: 0x00010D47
		public static void WriteElement_Quaternion(Stream s, Quaternion data)
		{
			PmxStreamHelper.WriteElement_Float(s, data.X);
			PmxStreamHelper.WriteElement_Float(s, data.Y);
			PmxStreamHelper.WriteElement_Float(s, data.Z);
			PmxStreamHelper.WriteElement_Float(s, data.W);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00012B84 File Offset: 0x00010D84
		public static Quaternion ReadElement_Quaternion(Stream s)
		{
			return new Quaternion
			{
				X = PmxStreamHelper.ReadElement_Float(s),
				Y = PmxStreamHelper.ReadElement_Float(s),
				Z = PmxStreamHelper.ReadElement_Float(s),
				W = PmxStreamHelper.ReadElement_Float(s)
			};
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public static void WriteElement_Matrix(Stream s, Matrix m)
		{
			foreach (float data in m.ToArray())
			{
				PmxStreamHelper.WriteElement_Float(s, data);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00012C10 File Offset: 0x00010E10
		public static Matrix ReadElement_Matrix(Stream s)
		{
			Matrix result;
			result.M11 = PmxStreamHelper.ReadElement_Float(s);
			result.M12 = PmxStreamHelper.ReadElement_Float(s);
			result.M13 = PmxStreamHelper.ReadElement_Float(s);
			result.M14 = PmxStreamHelper.ReadElement_Float(s);
			result.M21 = PmxStreamHelper.ReadElement_Float(s);
			result.M22 = PmxStreamHelper.ReadElement_Float(s);
			result.M23 = PmxStreamHelper.ReadElement_Float(s);
			result.M24 = PmxStreamHelper.ReadElement_Float(s);
			result.M31 = PmxStreamHelper.ReadElement_Float(s);
			result.M32 = PmxStreamHelper.ReadElement_Float(s);
			result.M33 = PmxStreamHelper.ReadElement_Float(s);
			result.M34 = PmxStreamHelper.ReadElement_Float(s);
			result.M41 = PmxStreamHelper.ReadElement_Float(s);
			result.M42 = PmxStreamHelper.ReadElement_Float(s);
			result.M43 = PmxStreamHelper.ReadElement_Float(s);
			result.M44 = PmxStreamHelper.ReadElement_Float(s);
			return result;
		}
	}
}
