using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000044 RID: 68
	public class VmdSelfShadow : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00018784 File Offset: 0x00016984
		public int ByteCount
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00018798 File Offset: 0x00016998
		public VmdSelfShadow()
		{
			this.Mode = 0;
			this.Distance = 0.011f;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000187B4 File Offset: 0x000169B4
		public VmdSelfShadow(VmdSelfShadow shadow)
		{
			this.FrameIndex = shadow.FrameIndex;
			this.Mode = shadow.Mode;
			this.Distance = shadow.Distance;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000187E4 File Offset: 0x000169E4
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.Add((byte)this.Mode);
			list.AddRange(BitConverter.GetBytes(this.Distance));
			return list.ToArray();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00018834 File Offset: 0x00016A34
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
			int num = startIndex + 4;
			this.Mode = (int)bytes[num++];
			this.Distance = BitConverter.ToSingle(bytes, num);
			num += 4;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00018874 File Offset: 0x00016A74
		public object Clone()
		{
			return new VmdSelfShadow(this);
		}

		// Token: 0x0400018E RID: 398
		public int Mode;

		// Token: 0x0400018F RID: 399
		public float Distance;
	}
}
