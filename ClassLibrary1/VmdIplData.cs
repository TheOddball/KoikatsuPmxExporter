using System;

namespace PmxLib
{
	// Token: 0x0200003E RID: 62
	public class VmdIplData : ICloneable
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00017AE1 File Offset: 0x00015CE1
		public VmdIplData()
		{
			this.P1 = new VmdIplPoint(20, 20);
			this.P2 = new VmdIplPoint(107, 107);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00017B09 File Offset: 0x00015D09
		public VmdIplData(VmdIplData ip)
		{
			this.P1 = (VmdIplPoint)ip.P1.Clone();
			this.P2 = (VmdIplPoint)ip.P2.Clone();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00017B40 File Offset: 0x00015D40
		public object Clone()
		{
			return new VmdIplData(this);
		}

		// Token: 0x04000179 RID: 377
		public VmdIplPoint P1;

		// Token: 0x0400017A RID: 378
		public VmdIplPoint P2;
	}
}
