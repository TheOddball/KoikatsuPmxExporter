using System;

namespace PmxLib
{
	// Token: 0x0200003F RID: 63
	public class VmdIplPoint : ICloneable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00017B58 File Offset: 0x00015D58
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00017B70 File Offset: 0x00015D70
		public int X
		{
			get
			{
				return this.m_x;
			}
			set
			{
				this.m_x = this.RangeValue(value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00017B80 File Offset: 0x00015D80
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00017B98 File Offset: 0x00015D98
		public int Y
		{
			get
			{
				return this.m_y;
			}
			set
			{
				this.m_y = this.RangeValue(value);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
		public VmdIplPoint()
		{
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00017BA8 File Offset: 0x00015DA8
		public VmdIplPoint(VmdIplPoint ip)
		{
			this.X = ip.X;
			this.Y = ip.Y;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00017BCC File Offset: 0x00015DCC
		public VmdIplPoint(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00017BE6 File Offset: 0x00015DE6
		public VmdIplPoint(Point p)
		{
			this.Set(p);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public void Set(Point p)
		{
			this.X = p.X;
			this.Y = p.Y;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00017C18 File Offset: 0x00015E18
		protected int RangeValue(int v)
		{
			v = ((v < 0) ? 0 : v);
			v = ((v > 127) ? 127 : v);
			return v;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00017C44 File Offset: 0x00015E44
		public static implicit operator Point(VmdIplPoint p)
		{
			return new Point(p.X, p.Y);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00017C68 File Offset: 0x00015E68
		public static implicit operator VmdIplPoint(Point p)
		{
			return new VmdIplPoint(p);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00017C80 File Offset: 0x00015E80
		public object Clone()
		{
			return new VmdIplPoint(this);
		}

		// Token: 0x0400017B RID: 379
		protected int m_x;

		// Token: 0x0400017C RID: 380
		protected int m_y;
	}
}
