using System;
using System.IO;
using System.Text;

namespace PmxLib
{
	// Token: 0x02000019 RID: 25
	public class PmxHeader : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000F050 File Offset: 0x0000D250
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Header;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000F064 File Offset: 0x0000D264
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000F081 File Offset: 0x0000D281
		public float Ver
		{
			get
			{
				return this.ElementFormat.Ver;
			}
			set
			{
				this.ElementFormat.Ver = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000F091 File Offset: 0x0000D291
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000F099 File Offset: 0x0000D299
		public PmxElementFormat ElementFormat { get; private set; }

		// Token: 0x06000151 RID: 337 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public PmxHeader(float ver)
		{
			bool flag = ver == 0f;
			if (flag)
			{
				ver = 2.1f;
			}
			this.ElementFormat = new PmxElementFormat(ver);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000F0D9 File Offset: 0x0000D2D9
		public PmxHeader(PmxHeader h)
		{
			this.FromHeader(h);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000F0EB File Offset: 0x0000D2EB
		public void FromHeader(PmxHeader h)
		{
			this.ElementFormat = h.ElementFormat.Clone();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000F100 File Offset: 0x0000D300
		public void FromElementFormat(PmxElementFormat f)
		{
			this.ElementFormat = f;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000F10C File Offset: 0x0000D30C
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			string @string = Encoding.ASCII.GetString(array);
			bool flag = @string.Equals(PmxHeader.PmxKey_v1);
			if (flag)
			{
				this.Ver = 1f;
				array = new byte[4];
				s.Read(array, 0, array.Length);
			}
			else
			{
				bool flag2 = !@string.Equals(PmxHeader.PmxKey);
				if (flag2)
				{
					throw new Exception("ファイル形式が異なります.");
				}
				array = new byte[4];
				s.Read(array, 0, array.Length);
				this.Ver = BitConverter.ToSingle(array, 0);
			}
			bool flag3 = this.Ver > 2.1f;
			if (flag3)
			{
				throw new Exception("未対応のverです.");
			}
			this.ElementFormat = new PmxElementFormat(this.Ver);
			this.ElementFormat.FromStreamEx(s, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			bool flag = f == null;
			if (flag)
			{
				f = this.ElementFormat;
			}
			byte[] array = new byte[4];
			bool flag2 = f.Ver <= 1f;
			if (flag2)
			{
				array = Encoding.ASCII.GetBytes(PmxHeader.PmxKey_v1);
			}
			else
			{
				array = Encoding.ASCII.GetBytes(PmxHeader.PmxKey);
			}
			s.Write(array, 0, array.Length);
			array = BitConverter.GetBytes(this.Ver);
			s.Write(array, 0, array.Length);
			f.ToStreamEx(s, null);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000F27C File Offset: 0x0000D47C
		object ICloneable.Clone()
		{
			return new PmxHeader(this);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000F294 File Offset: 0x0000D494
		public PmxHeader Clone()
		{
			return new PmxHeader(this);
		}

		// Token: 0x04000098 RID: 152
		public const float LastVer = 2.1f;

		// Token: 0x04000099 RID: 153
		public static string PmxKey_v1 = "Pmx ";

		// Token: 0x0400009A RID: 154
		public static string PmxKey = "PMX ";
	}
}
