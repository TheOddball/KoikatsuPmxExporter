using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200002B RID: 43
	internal class PmxUVMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00013288 File Offset: 0x00011488
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.UVMorph;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0001329C File Offset: 0x0001149C
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000132B4 File Offset: 0x000114B4
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000132BE File Offset: 0x000114BE
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000132C6 File Offset: 0x000114C6
		public PmxVertex RefVertex { get; set; }

		// Token: 0x06000264 RID: 612 RVA: 0x000132CF File Offset: 0x000114CF
		public PmxUVMorph()
		{
			this.Index = -1;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000132E0 File Offset: 0x000114E0
		public PmxUVMorph(int index, Vector4 offset) : this()
		{
			this.Index = index;
			this.Offset = offset;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000132F8 File Offset: 0x000114F8
		public PmxUVMorph(PmxUVMorph sv) : this()
		{
			this.FromPmxUVMorph(sv);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001330A File Offset: 0x0001150A
		public void FromPmxUVMorph(PmxUVMorph sv)
		{
			this.Index = sv.Index;
			this.Offset = sv.Offset;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00013325 File Offset: 0x00011525
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.VertexSize, false);
			this.Offset = V4_BytesConvert.FromStream(s);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00013347 File Offset: 0x00011547
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.VertexSize, false);
			V4_BytesConvert.ToStream(s, this.Offset);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001336C File Offset: 0x0001156C
		object ICloneable.Clone()
		{
			return new PmxUVMorph(this);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00013384 File Offset: 0x00011584
		public override PmxBaseMorph Clone()
		{
			return new PmxUVMorph(this);
		}

		// Token: 0x04000127 RID: 295
		public int Index;

		// Token: 0x04000128 RID: 296
		public Vector4 Offset;
	}
}
