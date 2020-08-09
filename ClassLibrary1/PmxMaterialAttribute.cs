using System;

namespace PmxLib
{
	// Token: 0x0200001F RID: 31
	public class PmxMaterialAttribute : ICloneable
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00010455 File Offset: 0x0000E655
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0001045D File Offset: 0x0000E65D
		public string BumpMapTexture { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00010466 File Offset: 0x0000E666
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0001046E File Offset: 0x0000E66E
		public PmxMaterialAttribute.UVTarget BumpMapUV { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00010477 File Offset: 0x0000E677
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0001047F File Offset: 0x0000E67F
		public string NormalMapTexture { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00010488 File Offset: 0x0000E688
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00010490 File Offset: 0x0000E690
		public PmxMaterialAttribute.UVTarget NormalMapUV { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00010499 File Offset: 0x0000E699
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000104A1 File Offset: 0x0000E6A1
		public string CubeMapTexture { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000104AA File Offset: 0x0000E6AA
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000104B2 File Offset: 0x0000E6B2
		public PmxMaterialAttribute.UVTarget CubeMapUV { get; private set; }

		// Token: 0x060001BA RID: 442 RVA: 0x000104BB File Offset: 0x0000E6BB
		public PmxMaterialAttribute()
		{
			this.Clear();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000104CC File Offset: 0x0000E6CC
		public PmxMaterialAttribute(string text) : this()
		{
			this.SetFromText(text);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000104E0 File Offset: 0x0000E6E0
		public PmxMaterialAttribute(PmxMaterialAttribute att)
		{
			this.BumpMapTexture = att.BumpMapTexture;
			this.NormalMapTexture = att.NormalMapTexture;
			this.CubeMapTexture = att.CubeMapTexture;
			this.BumpMapUV = att.BumpMapUV;
			this.NormalMapUV = att.NormalMapUV;
			this.CubeMapUV = att.CubeMapUV;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00010543 File Offset: 0x0000E743
		public void Clear()
		{
			this.BumpMapTexture = null;
			this.NormalMapTexture = null;
			this.CubeMapTexture = null;
			this.BumpMapUV = PmxMaterialAttribute.UVTarget.UV;
			this.NormalMapUV = PmxMaterialAttribute.UVTarget.UV;
			this.CubeMapUV = PmxMaterialAttribute.UVTarget.UV;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00010578 File Offset: 0x0000E778
		private static PmxMaterialAttribute.UVTarget TextToUVTarget(string text)
		{
			PmxMaterialAttribute.UVTarget result = PmxMaterialAttribute.UVTarget.UV;
			string text2 = text.ToLower();
			string text3 = text2;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num <= 1220308614U)
			{
				if (num <= 677736828U)
				{
					if (num != 509666448U)
					{
						if (num == 677736828U)
						{
							if (text3 == "uva4xy")
							{
								result = PmxMaterialAttribute.UVTarget.UVA4xy;
							}
						}
					}
					else if (text3 == "uva4zw")
					{
						result = PmxMaterialAttribute.UVTarget.UVA4zw;
					}
				}
				else if (num != 1120040258U)
				{
					if (num == 1220308614U)
					{
						if (text3 == "uva2xy")
						{
							result = PmxMaterialAttribute.UVTarget.UVA2xy;
						}
					}
				}
				else if (text3 == "uva2zw")
				{
					result = PmxMaterialAttribute.UVTarget.UVA2zw;
				}
			}
			else if (num <= 3863463601U)
			{
				if (num != 3696084769U)
				{
					if (num == 3863463601U)
					{
						if (text3 == "uva3zw")
						{
							result = PmxMaterialAttribute.UVTarget.UVA3zw;
						}
					}
				}
				else if (text3 == "uva3xy")
				{
					result = PmxMaterialAttribute.UVTarget.UVA3xy;
				}
			}
			else if (num != 4246972211U)
			{
				if (num == 4280130091U)
				{
					if (text3 == "uva1xy")
					{
						result = PmxMaterialAttribute.UVTarget.UVA1xy;
					}
				}
			}
			else if (text3 == "uva1zw")
			{
				result = PmxMaterialAttribute.UVTarget.UVA1zw;
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public void SetFromText(string text)
		{
			this.Clear();
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				string[] tag = PmxTag.GetTag("BumpMap", text);
				bool flag2 = tag != null && tag.Length != 0;
				if (flag2)
				{
					this.BumpMapTexture = tag[0];
				}
				string[] tag2 = PmxTag.GetTag("BumpMapUV", text);
				bool flag3 = tag2 != null && tag2.Length != 0;
				if (flag3)
				{
					this.BumpMapUV = PmxMaterialAttribute.TextToUVTarget(tag2[0]);
				}
				string[] tag3 = PmxTag.GetTag("NormalMap", text);
				bool flag4 = tag3 != null && tag3.Length != 0;
				if (flag4)
				{
					this.NormalMapTexture = tag3[0];
				}
				string[] tag4 = PmxTag.GetTag("NormalMapUV", text);
				bool flag5 = tag4 != null && tag4.Length != 0;
				if (flag5)
				{
					this.NormalMapUV = PmxMaterialAttribute.TextToUVTarget(tag4[0]);
				}
				string[] tag5 = PmxTag.GetTag("CubeMap", text);
				bool flag6 = tag5 != null && tag5.Length != 0;
				if (flag6)
				{
					this.CubeMapTexture = tag5[0];
				}
				string[] tag6 = PmxTag.GetTag("CubeMapUV", text);
				bool flag7 = tag6 != null && tag6.Length != 0;
				if (flag7)
				{
					this.CubeMapUV = PmxMaterialAttribute.TextToUVTarget(tag6[0]);
				}
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000107F8 File Offset: 0x0000E9F8
		object ICloneable.Clone()
		{
			return new PmxMaterialAttribute(this);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00010810 File Offset: 0x0000EA10
		public PmxMaterialAttribute Clone()
		{
			return new PmxMaterialAttribute(this);
		}

		// Token: 0x040000CB RID: 203
		public const string TAG_BumpMap = "BumpMap";

		// Token: 0x040000CC RID: 204
		public const string TAG_NormalMap = "NormalMap";

		// Token: 0x040000CD RID: 205
		public const string TAG_CubeMap = "CubeMap";

		// Token: 0x040000CE RID: 206
		public const string TAG_BumpMapUV = "BumpMapUV";

		// Token: 0x040000CF RID: 207
		public const string TAG_NormalMapUV = "NormalMapUV";

		// Token: 0x040000D0 RID: 208
		public const string TAG_CubeMapUV = "CubeMapUV";

		// Token: 0x040000D1 RID: 209
		public const string UVTarget_UV = "uv";

		// Token: 0x040000D2 RID: 210
		public const string UVTarget_UVA1xy = "uva1xy";

		// Token: 0x040000D3 RID: 211
		public const string UVTarget_UVA1zw = "uva1zw";

		// Token: 0x040000D4 RID: 212
		public const string UVTarget_UVA2xy = "uva2xy";

		// Token: 0x040000D5 RID: 213
		public const string UVTarget_UVA2zw = "uva2zw";

		// Token: 0x040000D6 RID: 214
		public const string UVTarget_UVA3xy = "uva3xy";

		// Token: 0x040000D7 RID: 215
		public const string UVTarget_UVA3zw = "uva3zw";

		// Token: 0x040000D8 RID: 216
		public const string UVTarget_UVA4xy = "uva4xy";

		// Token: 0x040000D9 RID: 217
		public const string UVTarget_UVA4zw = "uva4zw";

		// Token: 0x02000057 RID: 87
		public enum UVTarget
		{
			// Token: 0x040001EE RID: 494
			UV,
			// Token: 0x040001EF RID: 495
			UVA1xy,
			// Token: 0x040001F0 RID: 496
			UVA1zw,
			// Token: 0x040001F1 RID: 497
			UVA2xy,
			// Token: 0x040001F2 RID: 498
			UVA2zw,
			// Token: 0x040001F3 RID: 499
			UVA3xy,
			// Token: 0x040001F4 RID: 500
			UVA3zw,
			// Token: 0x040001F5 RID: 501
			UVA4xy,
			// Token: 0x040001F6 RID: 502
			UVA4zw
		}
	}
}
