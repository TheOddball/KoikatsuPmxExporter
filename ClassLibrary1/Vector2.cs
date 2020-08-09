using System;

namespace PmxLib
{
	// Token: 0x02000035 RID: 53
	public struct Vector2
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000156F8 File Offset: 0x000138F8
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00015710 File Offset: 0x00013910
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001571C File Offset: 0x0001391C
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00015734 File Offset: 0x00013934
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00015740 File Offset: 0x00013940
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00015761 File Offset: 0x00013961
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00015772 File Offset: 0x00013972
		public Vector2(Vector2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00015790 File Offset: 0x00013990
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Vector2 vector = (Vector2)other;
				result = (this.x.Equals(vector.x) && this.y.Equals(vector.y));
			}
			return result;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000157EC File Offset: 0x000139EC
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00015820 File Offset: 0x00013A20
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00015854 File Offset: 0x00013A54
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001587C File Offset: 0x00013A7C
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000158A4 File Offset: 0x00013AA4
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000158CC File Offset: 0x00013ACC
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000158F4 File Offset: 0x00013AF4
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001591C File Offset: 0x00013B1C
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00015944 File Offset: 0x00013B44
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00015968 File Offset: 0x00013B68
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00015990 File Offset: 0x00013B90
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000159C0 File Offset: 0x00013BC0
		public override string ToString()
		{
			return string.Format("X:{0} Y:{1}", new object[]
			{
				this.X.ToString(),
				this.Y.ToString()
			});
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00015A08 File Offset: 0x00013C08
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
		}

		// Token: 0x0400014E RID: 334
		public float x;

		// Token: 0x0400014F RID: 335
		public float y;
	}
}
