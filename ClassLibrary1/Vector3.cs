using System;

namespace PmxLib
{
	// Token: 0x02000036 RID: 54
	public struct Vector3
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00015A34 File Offset: 0x00013C34
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00015A4C File Offset: 0x00013C4C
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00015A58 File Offset: 0x00013C58
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00015A70 File Offset: 0x00013C70
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00015A7C File Offset: 0x00013C7C
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00015A94 File Offset: 0x00013C94
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00015AC8 File Offset: 0x00013CC8
		public static Vector3 Zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00015AEE File Offset: 0x00013CEE
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00015B06 File Offset: 0x00013D06
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00015B22 File Offset: 0x00013D22
		public Vector3(Vector3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00015B4C File Offset: 0x00013D4C
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00015B88 File Offset: 0x00013D88
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00015BF0 File Offset: 0x00013DF0
		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00015C2C File Offset: 0x00013E2C
		public float Length()
		{
			double num = (double)this.Y;
			double num2 = (double)this.X;
			double num3 = (double)this.Z;
			double num4 = num2;
			double num5 = num4 * num4;
			double num6 = num;
			double num7 = num5 + num6 * num6;
			double num8 = num3;
			return (float)Math.Sqrt(num7 + num8 * num8);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00015C80 File Offset: 0x00013E80
		public void Normalize()
		{
			float num = this.Length();
			bool flag = num != 0f;
			if (flag)
			{
				float num2 = (float)(1.0 / (double)num);
				this.X = (float)((double)this.X * (double)num2);
				this.Y = (float)((double)this.Y * (double)num2);
				this.Z = (float)((double)this.Z * (double)num2);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00015CEC File Offset: 0x00013EEC
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00015D2C File Offset: 0x00013F2C
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00015D6C File Offset: 0x00013F6C
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00015D98 File Offset: 0x00013F98
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00015E28 File Offset: 0x00014028
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00015E50 File Offset: 0x00014050
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00015E78 File Offset: 0x00014078
		public override string ToString()
		{
			return string.Format("X:{0} Y:{1} Z:{2}", new object[]
			{
				this.X.ToString(),
				this.Y.ToString(),
				this.Z.ToString()
			});
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00015ED4 File Offset: 0x000140D4
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Vector3 vector = (Vector3)other;
				result = (this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z));
			}
			return result;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00015F44 File Offset: 0x00014144
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		// Token: 0x04000150 RID: 336
		public float x;

		// Token: 0x04000151 RID: 337
		public float y;

		// Token: 0x04000152 RID: 338
		public float z;
	}
}
