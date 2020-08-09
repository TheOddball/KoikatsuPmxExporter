using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x0200003B RID: 59
	public class VmdCameraIPL : IBytesConvert, ICloneable
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000170DC File Offset: 0x000152DC
		public int ByteCount
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000170F0 File Offset: 0x000152F0
		public VmdCameraIPL()
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00017148 File Offset: 0x00015348
		public VmdCameraIPL(VmdCameraIPL ipl)
		{
			this.MoveX = (VmdIplData)ipl.MoveX.Clone();
			this.MoveY = (VmdIplData)ipl.MoveY.Clone();
			this.MoveZ = (VmdIplData)ipl.MoveZ.Clone();
			this.Rotate = (VmdIplData)ipl.Rotate.Clone();
			this.Distance = (VmdIplData)ipl.Distance.Clone();
			this.Angle = (VmdIplData)ipl.Angle.Clone();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00017224 File Offset: 0x00015424
		public byte[] ToBytes()
		{
			return new List<byte>
			{
				(byte)this.MoveX.P1.X,
				(byte)this.MoveX.P2.X,
				(byte)this.MoveX.P1.Y,
				(byte)this.MoveX.P2.Y,
				(byte)this.MoveY.P1.X,
				(byte)this.MoveY.P2.X,
				(byte)this.MoveY.P1.Y,
				(byte)this.MoveY.P2.Y,
				(byte)this.MoveZ.P1.X,
				(byte)this.MoveZ.P2.X,
				(byte)this.MoveZ.P1.Y,
				(byte)this.MoveZ.P2.Y,
				(byte)this.Rotate.P1.X,
				(byte)this.Rotate.P2.X,
				(byte)this.Rotate.P1.Y,
				(byte)this.Rotate.P2.Y,
				(byte)this.Distance.P1.X,
				(byte)this.Distance.P2.X,
				(byte)this.Distance.P1.Y,
				(byte)this.Distance.P2.Y,
				(byte)this.Angle.P1.X,
				(byte)this.Angle.P2.X,
				(byte)this.Angle.P1.Y,
				(byte)this.Angle.P2.Y
			}.ToArray();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00017480 File Offset: 0x00015680
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.MoveX.P1.X = (int)bytes[startIndex];
			int num = startIndex + 1;
			this.MoveX.P2.X = (int)bytes[num];
			num++;
			this.MoveX.P1.Y = (int)bytes[num];
			num++;
			this.MoveX.P2.Y = (int)bytes[num];
			num++;
			this.MoveY.P1.X = (int)bytes[num];
			num++;
			this.MoveY.P2.X = (int)bytes[num];
			num++;
			this.MoveY.P1.Y = (int)bytes[num];
			num++;
			this.MoveY.P2.Y = (int)bytes[num];
			num++;
			this.MoveZ.P1.X = (int)bytes[num];
			num++;
			this.MoveZ.P2.X = (int)bytes[num];
			num++;
			this.MoveZ.P1.Y = (int)bytes[num];
			num++;
			this.MoveZ.P2.Y = (int)bytes[num];
			num++;
			this.Rotate.P1.X = (int)bytes[num];
			num++;
			this.Rotate.P2.X = (int)bytes[num];
			num++;
			this.Rotate.P1.Y = (int)bytes[num];
			num++;
			this.Rotate.P2.Y = (int)bytes[num];
			num++;
			this.Distance.P1.X = (int)bytes[num];
			num++;
			this.Distance.P2.X = (int)bytes[num];
			num++;
			this.Distance.P1.Y = (int)bytes[num];
			num++;
			this.Distance.P2.Y = (int)bytes[num];
			num++;
			this.Angle.P1.X = (int)bytes[num];
			num++;
			this.Angle.P2.X = (int)bytes[num];
			num++;
			this.Angle.P1.Y = (int)bytes[num];
			num++;
			this.Angle.P2.Y = (int)bytes[num];
			num++;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000176D0 File Offset: 0x000158D0
		public object Clone()
		{
			return new VmdCameraIPL(this);
		}

		// Token: 0x0400016E RID: 366
		public VmdIplData MoveX = new VmdIplData();

		// Token: 0x0400016F RID: 367
		public VmdIplData MoveY = new VmdIplData();

		// Token: 0x04000170 RID: 368
		public VmdIplData MoveZ = new VmdIplData();

		// Token: 0x04000171 RID: 369
		public VmdIplData Rotate = new VmdIplData();

		// Token: 0x04000172 RID: 370
		public VmdIplData Distance = new VmdIplData();

		// Token: 0x04000173 RID: 371
		public VmdIplData Angle = new VmdIplData();
	}
}
