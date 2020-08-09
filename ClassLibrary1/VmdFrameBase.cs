using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x0200003D RID: 61
	public abstract class VmdFrameBase : IComparer<VmdFrameBase>
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00017AA8 File Offset: 0x00015CA8
		public static int Compare(VmdFrameBase x, VmdFrameBase y)
		{
			return x.FrameIndex - y.FrameIndex;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00017AC8 File Offset: 0x00015CC8
		int IComparer<VmdFrameBase>.Compare(VmdFrameBase x, VmdFrameBase y)
		{
			return VmdFrameBase.Compare(x, y);
		}

		// Token: 0x04000178 RID: 376
		public int FrameIndex;
	}
}
