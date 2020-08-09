using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000043 RID: 67
	public class VmdMotionIPL : IBytesConvert, ICloneable
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00018358 File Offset: 0x00016558
		public int ByteCount
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001836C File Offset: 0x0001656C
		public VmdMotionIPL()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000183A4 File Offset: 0x000165A4
		public VmdMotionIPL(VmdMotionIPL ipl)
		{
			this.MoveX = (VmdIplData)ipl.MoveX.Clone();
			this.MoveY = (VmdIplData)ipl.MoveY.Clone();
			this.MoveZ = (VmdIplData)ipl.MoveZ.Clone();
			this.Rotate = (VmdIplData)ipl.Rotate.Clone();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00018440 File Offset: 0x00016640
		public byte[] ToBytes()
		{
			return new List<byte>
			{
				(byte)this.MoveX.P1.X,
				(byte)this.MoveX.P1.Y,
				(byte)this.MoveX.P2.X,
				(byte)this.MoveX.P2.Y,
				(byte)this.MoveY.P1.X,
				(byte)this.MoveY.P1.Y,
				(byte)this.MoveY.P2.X,
				(byte)this.MoveY.P2.Y,
				(byte)this.MoveZ.P1.X,
				(byte)this.MoveZ.P1.Y,
				(byte)this.MoveZ.P2.X,
				(byte)this.MoveZ.P2.Y,
				(byte)this.Rotate.P1.X,
				(byte)this.Rotate.P1.Y,
				(byte)this.Rotate.P2.X,
				(byte)this.Rotate.P2.Y
			}.ToArray();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000185DC File Offset: 0x000167DC
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.MoveX.P1.X = (int)bytes[startIndex];
			int num = startIndex + 1;
			this.MoveX.P1.Y = (int)bytes[num];
			num++;
			this.MoveX.P2.X = (int)bytes[num];
			num++;
			this.MoveX.P2.Y = (int)bytes[num];
			num++;
			this.MoveY.P1.X = (int)bytes[num];
			num++;
			this.MoveY.P1.Y = (int)bytes[num];
			num++;
			this.MoveY.P2.X = (int)bytes[num];
			num++;
			this.MoveY.P2.Y = (int)bytes[num];
			num++;
			this.MoveZ.P1.X = (int)bytes[num];
			num++;
			this.MoveZ.P1.Y = (int)bytes[num];
			num++;
			this.MoveZ.P2.X = (int)bytes[num];
			num++;
			this.MoveZ.P2.Y = (int)bytes[num];
			num++;
			this.Rotate.P1.X = (int)bytes[num];
			num++;
			this.Rotate.P1.Y = (int)bytes[num];
			num++;
			this.Rotate.P2.X = (int)bytes[num];
			num++;
			this.Rotate.P2.Y = (int)bytes[num];
			num++;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001876C File Offset: 0x0001696C
		public object Clone()
		{
			return new VmdMotionIPL(this);
		}

		// Token: 0x0400018A RID: 394
		public VmdIplData MoveX = new VmdIplData();

		// Token: 0x0400018B RID: 395
		public VmdIplData MoveY = new VmdIplData();

		// Token: 0x0400018C RID: 396
		public VmdIplData MoveZ = new VmdIplData();

		// Token: 0x0400018D RID: 397
		public VmdIplData Rotate = new VmdIplData();
	}
}
