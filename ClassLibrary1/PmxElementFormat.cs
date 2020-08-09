using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000016 RID: 22
	public class PmxElementFormat : IPmxStreamIO, ICloneable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000EA10 File Offset: 0x0000CC10
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public float Ver { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000EA21 File Offset: 0x0000CC21
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000EA29 File Offset: 0x0000CC29
		public PmxElementFormat.StringEncType StringEnc { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000EA32 File Offset: 0x0000CC32
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000EA3A File Offset: 0x0000CC3A
		public int UVACount { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000EA43 File Offset: 0x0000CC43
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		public int VertexSize { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000EA54 File Offset: 0x0000CC54
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public int TexSize { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000EA65 File Offset: 0x0000CC65
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000EA6D File Offset: 0x0000CC6D
		public int MaterialSize { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000EA76 File Offset: 0x0000CC76
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000EA7E File Offset: 0x0000CC7E
		public int BoneSize { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000EA87 File Offset: 0x0000CC87
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		public int MorphSize { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000EA98 File Offset: 0x0000CC98
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public int BodySize { get; set; }

		// Token: 0x0600012A RID: 298 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public PmxElementFormat(float ver)
		{
			bool flag = ver == 0f;
			if (flag)
			{
				ver = 2.1f;
			}
			this.Ver = ver;
			this.StringEnc = PmxElementFormat.StringEncType.UTF16;
			this.UVACount = 0;
			this.VertexSize = 2;
			this.TexSize = 1;
			this.MaterialSize = 1;
			this.BoneSize = 2;
			this.MorphSize = 2;
			this.BodySize = 4;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public PmxElementFormat(PmxElementFormat f)
		{
			this.FromElementFormat(f);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public void FromElementFormat(PmxElementFormat f)
		{
			this.Ver = f.Ver;
			this.StringEnc = f.StringEnc;
			this.UVACount = f.UVACount;
			this.VertexSize = f.VertexSize;
			this.TexSize = f.TexSize;
			this.MaterialSize = f.MaterialSize;
			this.BoneSize = f.BoneSize;
			this.MorphSize = f.MorphSize;
			this.BodySize = f.BodySize;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public static int GetUnsignedBufSize(int count)
		{
			bool flag = count < 256;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = count < 65536;
				if (flag2)
				{
					result = 2;
				}
				else
				{
					result = 4;
				}
			}
			return result;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public static int GetSignedBufSize(int count)
		{
			bool flag = count < 128;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = count < 32768;
				if (flag2)
				{
					result = 2;
				}
				else
				{
					result = 4;
				}
			}
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			int num = PmxStreamHelper.ReadElement_Int32(s, 1, true);
			byte[] array = new byte[num];
			s.Read(array, 0, array.Length);
			int num2 = 0;
			bool flag = this.Ver <= 1f;
			if (flag)
			{
				this.VertexSize = (int)array[num2++];
				this.BoneSize = (int)array[num2++];
				this.MorphSize = (int)array[num2++];
				this.MaterialSize = (int)array[num2++];
				this.BodySize = (int)array[num2++];
			}
			else
			{
				this.StringEnc = (PmxElementFormat.StringEncType)array[num2++];
				this.UVACount = (int)array[num2++];
				this.VertexSize = (int)array[num2++];
				this.TexSize = (int)array[num2++];
				this.MaterialSize = (int)array[num2++];
				this.BoneSize = (int)array[num2++];
				this.MorphSize = (int)array[num2++];
				this.BodySize = (int)array[num2++];
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			byte[] array = new byte[8];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			int num = 0;
			bool flag = this.Ver <= 1f;
			if (flag)
			{
				array[num++] = (byte)this.VertexSize;
				array[num++] = (byte)this.BoneSize;
				array[num++] = (byte)this.MorphSize;
				array[num++] = (byte)this.MaterialSize;
				array[num++] = (byte)this.BodySize;
			}
			else
			{
				array[num++] = (byte)this.StringEnc;
				array[num++] = (byte)this.UVACount;
				array[num++] = (byte)this.VertexSize;
				array[num++] = (byte)this.TexSize;
				array[num++] = (byte)this.MaterialSize;
				array[num++] = (byte)this.BoneSize;
				array[num++] = (byte)this.MorphSize;
				array[num++] = (byte)this.BodySize;
			}
			PmxStreamHelper.WriteElement_Int32(s, array.Length, 1, true);
			s.Write(array, 0, array.Length);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000EE4C File Offset: 0x0000D04C
		object ICloneable.Clone()
		{
			return new PmxElementFormat(this);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000EE64 File Offset: 0x0000D064
		public PmxElementFormat Clone()
		{
			return new PmxElementFormat(this);
		}

		// Token: 0x04000087 RID: 135
		private const int SizeBufLength = 8;

		// Token: 0x04000088 RID: 136
		public const int MaxUVACount = 4;

		// Token: 0x02000051 RID: 81
		public enum StringEncType
		{
			// Token: 0x040001CA RID: 458
			UTF16,
			// Token: 0x040001CB RID: 459
			UTF8
		}
	}
}
