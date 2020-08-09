using System;

namespace PmxLib
{
	// Token: 0x02000037 RID: 55
	public struct Vector4
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00015F80 File Offset: 0x00014180
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00015F98 File Offset: 0x00014198
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00015FA4 File Offset: 0x000141A4
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00015FBC File Offset: 0x000141BC
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00015FC8 File Offset: 0x000141C8
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00015FE0 File Offset: 0x000141E0
		public float Z
		{
			get
			{
				return this.z;
			}
			set
			{
				this.z = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00015FEC File Offset: 0x000141EC
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00016004 File Offset: 0x00014204
		public float W
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00016010 File Offset: 0x00014210
		public static Vector4 zero
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001603B File Offset: 0x0001423B
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001605B File Offset: 0x0001425B
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001607E File Offset: 0x0001427E
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000160A8 File Offset: 0x000142A8
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000160F4 File Offset: 0x000142F4
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00016110 File Offset: 0x00014310
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001615C File Offset: 0x0001435C
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000161A8 File Offset: 0x000143A8
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000161DC File Offset: 0x000143DC
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00016214 File Offset: 0x00014414
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001624C File Offset: 0x0001444C
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00016284 File Offset: 0x00014484
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000162AC File Offset: 0x000144AC
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000162D4 File Offset: 0x000144D4
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00016304 File Offset: 0x00014504
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00016330 File Offset: 0x00014530
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00016360 File Offset: 0x00014560
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00016384 File Offset: 0x00014584
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector4);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Vector4 vector = (Vector4)other;
				result = (this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w));
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00016404 File Offset: 0x00014604
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x04000153 RID: 339
		public float x;

		// Token: 0x04000154 RID: 340
		public float y;

		// Token: 0x04000155 RID: 341
		public float z;

		// Token: 0x04000156 RID: 342
		public float w;
	}
}
