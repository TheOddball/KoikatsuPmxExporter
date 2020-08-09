using System;
using System.Collections.Generic;
using UnityEngine;

namespace PmxLib
{
	// Token: 0x02000040 RID: 64
	public class VmdLight : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00017C98 File Offset: 0x00015E98
		public int ByteCount
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00017CAC File Offset: 0x00015EAC
		public VmdLight()
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00017CB6 File Offset: 0x00015EB6
		public VmdLight(VmdLight light) : this()
		{
			this.FrameIndex = light.FrameIndex;
			this.Color = light.Color;
			this.Direction = light.Direction;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00017CE4 File Offset: 0x00015EE4
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.AddRange(BitConverter.GetBytes(this.Color.r));
			list.AddRange(BitConverter.GetBytes(this.Color.g));
			list.AddRange(BitConverter.GetBytes(this.Color.b));
			list.AddRange(BitConverter.GetBytes(this.Direction.x));
			list.AddRange(BitConverter.GetBytes(this.Direction.y));
			list.AddRange(BitConverter.GetBytes(this.Direction.z));
			return list.ToArray();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00017DA0 File Offset: 0x00015FA0
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
			int num = startIndex + 4;
			this.Color.r = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Color.g = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Color.b = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Direction.x = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Direction.y = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Direction.z = BitConverter.ToSingle(bytes, num);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00017E40 File Offset: 0x00016040
		public object Clone()
		{
			return new VmdLight(this);
		}

		// Token: 0x0400017D RID: 381
		public Color Color;

		// Token: 0x0400017E RID: 382
		public Vector3 Direction;
	}
}
