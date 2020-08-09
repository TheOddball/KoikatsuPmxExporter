using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200002D RID: 45
	internal class PmxVertexMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0001445C File Offset: 0x0001265C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.VertexMorph;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00014470 File Offset: 0x00012670
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00014488 File Offset: 0x00012688
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00014492 File Offset: 0x00012692
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0001449A File Offset: 0x0001269A
		public PmxVertex RefVertex { get; set; }

		// Token: 0x0600028A RID: 650 RVA: 0x000144A3 File Offset: 0x000126A3
		public PmxVertexMorph()
		{
			this.Index = -1;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000144B4 File Offset: 0x000126B4
		public PmxVertexMorph(int index, Vector3 offset) : this()
		{
			this.Index = index;
			this.Offset = offset;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000144CC File Offset: 0x000126CC
		public PmxVertexMorph(PmxVertexMorph sv) : this()
		{
			this.FromPmxVertexMorph(sv);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000144DE File Offset: 0x000126DE
		public void FromPmxVertexMorph(PmxVertexMorph sv)
		{
			this.Index = sv.Index;
			this.Offset = sv.Offset;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000144F9 File Offset: 0x000126F9
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.VertexSize, false);
			this.Offset = V3_BytesConvert.FromStream(s);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001451B File Offset: 0x0001271B
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.VertexSize, false);
			V3_BytesConvert.ToStream(s, this.Offset);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00014540 File Offset: 0x00012740
		object ICloneable.Clone()
		{
			return new PmxVertexMorph(this);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00014558 File Offset: 0x00012758
		public override PmxBaseMorph Clone()
		{
			return new PmxVertexMorph(this);
		}

		// Token: 0x04000140 RID: 320
		public int Index;

		// Token: 0x04000141 RID: 321
		public Vector3 Offset;
	}
}
