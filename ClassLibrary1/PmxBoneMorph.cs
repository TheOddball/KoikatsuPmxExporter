using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000015 RID: 21
	internal class PmxBoneMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000E85C File Offset: 0x0000CA5C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.BoneMorph;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000E870 File Offset: 0x0000CA70
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000E888 File Offset: 0x0000CA88
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000E892 File Offset: 0x0000CA92
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000E89A File Offset: 0x0000CA9A
		public PmxBone RefBone { get; set; }

		// Token: 0x0600010F RID: 271 RVA: 0x0000E8A3 File Offset: 0x0000CAA3
		public PmxBoneMorph()
		{
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000E8AD File Offset: 0x0000CAAD
		public PmxBoneMorph(int index, Vector3 t, Quaternion r) : this()
		{
			this.Index = index;
			this.Translation = t;
			this.Rotaion = r;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public PmxBoneMorph(PmxBoneMorph sv) : this()
		{
			this.FromPmxBoneMorph(sv);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000E8DE File Offset: 0x0000CADE
		public void FromPmxBoneMorph(PmxBoneMorph sv)
		{
			this.Index = sv.Index;
			this.Translation = sv.Translation;
			this.Rotaion = sv.Rotaion;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000E905 File Offset: 0x0000CB05
		public void Clear()
		{
			this.Translation = Vector3.zero;
			this.Rotaion = Quaternion.identity;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000E920 File Offset: 0x0000CB20
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.BoneSize, true);
			this.Translation = V3_BytesConvert.FromStream(s);
			Vector4 vector = V4_BytesConvert.FromStream(s);
			this.Rotaion = new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000E978 File Offset: 0x0000CB78
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.BoneSize, true);
			V3_BytesConvert.ToStream(s, this.Translation);
			V4_BytesConvert.ToStream(s, new Vector4(this.Rotaion.x, this.Rotaion.y, this.Rotaion.z, this.Rotaion.w));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		object ICloneable.Clone()
		{
			return new PmxBoneMorph(this);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		public override PmxBaseMorph Clone()
		{
			return new PmxBoneMorph(this);
		}

		// Token: 0x04000083 RID: 131
		public int Index;

		// Token: 0x04000084 RID: 132
		public Vector3 Translation;

		// Token: 0x04000085 RID: 133
		public Quaternion Rotaion;
	}
}
