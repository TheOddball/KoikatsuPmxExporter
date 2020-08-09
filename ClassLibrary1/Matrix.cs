using System;

namespace PmxLib
{
	// Token: 0x0200000F RID: 15
	public struct Matrix
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000C204 File Offset: 0x0000A404
		public static Matrix Identity
		{
			get
			{
				return new Matrix
				{
					M11 = 1f,
					M22 = 1f,
					M33 = 1f,
					M44 = 1f
				};
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000C250 File Offset: 0x0000A450
		public float[] ToArray()
		{
			return new float[]
			{
				this.M11,
				this.M12,
				this.M13,
				this.M14,
				this.M21,
				this.M22,
				this.M23,
				this.M24,
				this.M31,
				this.M32,
				this.M33,
				this.M34,
				this.M41,
				this.M42,
				this.M43,
				this.M44
			};
		}

		// Token: 0x04000039 RID: 57
		public float M11;

		// Token: 0x0400003A RID: 58
		public float M12;

		// Token: 0x0400003B RID: 59
		public float M13;

		// Token: 0x0400003C RID: 60
		public float M14;

		// Token: 0x0400003D RID: 61
		public float M21;

		// Token: 0x0400003E RID: 62
		public float M22;

		// Token: 0x0400003F RID: 63
		public float M23;

		// Token: 0x04000040 RID: 64
		public float M24;

		// Token: 0x04000041 RID: 65
		public float M31;

		// Token: 0x04000042 RID: 66
		public float M32;

		// Token: 0x04000043 RID: 67
		public float M33;

		// Token: 0x04000044 RID: 68
		public float M34;

		// Token: 0x04000045 RID: 69
		public float M41;

		// Token: 0x04000046 RID: 70
		public float M42;

		// Token: 0x04000047 RID: 71
		public float M43;

		// Token: 0x04000048 RID: 72
		public float M44;
	}
}
