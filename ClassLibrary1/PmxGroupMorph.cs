using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000018 RID: 24
	internal class PmxGroupMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000EF38 File Offset: 0x0000D138
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.GroupMorph;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000EF4C File Offset: 0x0000D14C
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000EF64 File Offset: 0x0000D164
		public override int BaseIndex
		{
			get
			{
				return this.Index;
			}
			set
			{
				this.Index = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000EF6E File Offset: 0x0000D16E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000EF76 File Offset: 0x0000D176
		public PmxMorph RefMorph { get; set; }

		// Token: 0x06000144 RID: 324 RVA: 0x0000EF7F File Offset: 0x0000D17F
		public PmxGroupMorph()
		{
			this.Ratio = 1f;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000EF94 File Offset: 0x0000D194
		public PmxGroupMorph(int index, float r) : this()
		{
			this.Index = index;
			this.Ratio = r;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public PmxGroupMorph(PmxGroupMorph sv) : this()
		{
			this.FromPmxGroupMorph(sv);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		public void FromPmxGroupMorph(PmxGroupMorph sv)
		{
			this.Index = sv.Index;
			this.Ratio = sv.Ratio;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.MorphSize, true);
			this.Ratio = PmxStreamHelper.ReadElement_Float(s);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000EFFB File Offset: 0x0000D1FB
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.MorphSize, true);
			PmxStreamHelper.WriteElement_Float(s, this.Ratio);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000F020 File Offset: 0x0000D220
		object ICloneable.Clone()
		{
			return new PmxGroupMorph(this);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000F038 File Offset: 0x0000D238
		public override PmxBaseMorph Clone()
		{
			return new PmxGroupMorph(this);
		}

		// Token: 0x04000095 RID: 149
		public int Index;

		// Token: 0x04000096 RID: 150
		public float Ratio;
	}
}
