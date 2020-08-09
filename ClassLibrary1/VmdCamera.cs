using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x0200003A RID: 58
	public class VmdCamera : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00016E1C File Offset: 0x0001501C
		public int ByteCount
		{
			get
			{
				return 32 + this.IPL.ByteCount + 1 + 4;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00016E40 File Offset: 0x00015040
		public VmdCamera()
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00016E58 File Offset: 0x00015058
		public VmdCamera(VmdCamera camera)
		{
			this.FrameIndex = camera.FrameIndex;
			this.Distance = camera.Distance;
			this.Position = camera.Position;
			this.Rotate = camera.Rotate;
			this.IPL = (VmdCameraIPL)camera.IPL.Clone();
			this.Angle = camera.Angle;
			this.Pers = camera.Pers;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00016ED8 File Offset: 0x000150D8
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.AddRange(BitConverter.GetBytes(this.Distance));
			list.AddRange(BitConverter.GetBytes(this.Position.x));
			list.AddRange(BitConverter.GetBytes(this.Position.y));
			list.AddRange(BitConverter.GetBytes(this.Position.z));
			list.AddRange(BitConverter.GetBytes(this.Rotate.x));
			list.AddRange(BitConverter.GetBytes(this.Rotate.y));
			list.AddRange(BitConverter.GetBytes(this.Rotate.z));
			list.AddRange(this.IPL.ToBytes());
			list.AddRange(BitConverter.GetBytes((int)this.Angle));
			list.Add(this.Pers);
			return list.ToArray();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00016FD8 File Offset: 0x000151D8
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
			int num = startIndex + 4;
			this.Distance = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Position.x = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Position.y = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Position.z = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.x = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.y = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.z = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.IPL.FromBytes(bytes, num);
			num += this.IPL.ByteCount;
			this.Angle = (float)BitConverter.ToInt32(bytes, num);
			num += 4;
			this.Pers = bytes[num];
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000170C4 File Offset: 0x000152C4
		public object Clone()
		{
			return new VmdCamera(this);
		}

		// Token: 0x04000168 RID: 360
		public float Distance;

		// Token: 0x04000169 RID: 361
		public Vector3 Position;

		// Token: 0x0400016A RID: 362
		public Vector3 Rotate;

		// Token: 0x0400016B RID: 363
		public VmdCameraIPL IPL = new VmdCameraIPL();

		// Token: 0x0400016C RID: 364
		public float Angle;

		// Token: 0x0400016D RID: 365
		public byte Pers;
	}
}
