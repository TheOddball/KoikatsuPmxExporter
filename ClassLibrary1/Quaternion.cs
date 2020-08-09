using System;

namespace PmxLib
{
	// Token: 0x0200002F RID: 47
	public struct Quaternion
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00014588 File Offset: 0x00012788
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000145A0 File Offset: 0x000127A0
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000145AC File Offset: 0x000127AC
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000145C4 File Offset: 0x000127C4
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000145D0 File Offset: 0x000127D0
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000145E8 File Offset: 0x000127E8
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000145F4 File Offset: 0x000127F4
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0001460C File Offset: 0x0001280C
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00014618 File Offset: 0x00012818
		public static Quaternion identity
		{
			get
			{
				return new Quaternion
				{
					X = 0f,
					Y = 0f,
					Z = 0f,
					W = 1f
				};
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00014668 File Offset: 0x00012868
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion
				{
					X = 0f,
					Y = 0f,
					Z = 0f,
					W = 1f
				};
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000146B7 File Offset: 0x000128B7
		public Quaternion(Vector3 value, float w)
		{
			this.x = value.X;
			this.y = value.Y;
			this.z = value.Z;
			this.w = w;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000146E8 File Offset: 0x000128E8
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014708 File Offset: 0x00012908
		public static Quaternion operator *(float scale, Quaternion quaternion)
		{
			return new Quaternion
			{
				X = (float)((double)quaternion.X * (double)scale),
				Y = (float)((double)quaternion.Y * (double)scale),
				Z = (float)((double)quaternion.Z * (double)scale),
				W = (float)((double)quaternion.W * (double)scale)
			};
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00014774 File Offset: 0x00012974
		public static Quaternion operator *(Quaternion quaternion, float scale)
		{
			return new Quaternion
			{
				X = (float)((double)quaternion.X * (double)scale),
				Y = (float)((double)quaternion.Y * (double)scale),
				Z = (float)((double)quaternion.Z * (double)scale),
				W = (float)((double)quaternion.W * (double)scale)
			};
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000147E0 File Offset: 0x000129E0
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			Quaternion result = default(Quaternion);
			float num = left.X;
			float num2 = left.Y;
			float num3 = left.Z;
			float num4 = left.W;
			float num5 = right.X;
			float num6 = right.Y;
			float num7 = right.Z;
			float num8 = right.W;
			result.X = (float)((double)num5 * (double)num4 + (double)num8 * (double)num + (double)num6 * (double)num3 - (double)num7 * (double)num2);
			result.Y = (float)((double)num6 * (double)num4 + (double)num8 * (double)num2 + (double)num7 * (double)num - (double)num5 * (double)num3);
			result.Z = (float)((double)num7 * (double)num4 + (double)num8 * (double)num3 + (double)num5 * (double)num2 - (double)num6 * (double)num);
			result.W = (float)((double)num8 * (double)num4 - ((double)num6 * (double)num2 + (double)num5 * (double)num + (double)num7 * (double)num3));
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000148D8 File Offset: 0x00012AD8
		public static Quaternion operator /(Quaternion left, float right)
		{
			return new Quaternion
			{
				X = (float)((double)left.X / (double)right),
				Y = (float)((double)left.Y / (double)right),
				Z = (float)((double)left.Z / (double)right),
				W = (float)((double)left.W / (double)right)
			};
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014944 File Offset: 0x00012B44
		public static Quaternion operator +(Quaternion left, Quaternion right)
		{
			return new Quaternion
			{
				X = (float)((double)left.X + (double)right.X),
				Y = (float)((double)left.Y + (double)right.Y),
				Z = (float)((double)left.Z + (double)right.Z),
				W = (float)((double)left.W + (double)right.W)
			};
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000149C8 File Offset: 0x00012BC8
		public static Quaternion operator -(Quaternion quaternion)
		{
			return new Quaternion
			{
				X = -quaternion.X,
				Y = -quaternion.Y,
				Z = -quaternion.Z,
				W = -quaternion.W
			};
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014A24 File Offset: 0x00012C24
		public static Quaternion operator -(Quaternion left, Quaternion right)
		{
			return new Quaternion
			{
				X = (float)((double)left.X - (double)right.X),
				Y = (float)((double)left.Y - (double)right.Y),
				Z = (float)((double)left.Z - (double)right.Z),
				W = (float)((double)left.W - (double)right.W)
			};
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public static bool operator ==(Quaternion left, Quaternion right)
		{
			return Quaternion.Equals(ref left, ref right);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public static bool operator !=(Quaternion left, Quaternion right)
		{
			return ((!Quaternion.Equals(ref left, ref right)) ? 1 : 0) != 0;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014AE8 File Offset: 0x00012CE8
		public override string ToString()
		{
			return string.Format("X:{0} Y:{1} Z:{2} W:{3}", new object[]
			{
				this.X.ToString(),
				this.Y.ToString(),
				this.Z.ToString(),
				this.W.ToString()
			});
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00014B54 File Offset: 0x00012D54
		public override int GetHashCode()
		{
			float num = this.X;
			float num2 = this.Y;
			float num3 = this.Z;
			float num4 = this.W;
			int num5 = num3.GetHashCode() + num4.GetHashCode() + num2.GetHashCode();
			return num.GetHashCode() + num5;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public static bool Equals(ref Quaternion value1, ref Quaternion value2)
		{
			bool flag = (double)value1.X == (double)value2.X && (double)value1.Y == (double)value2.Y && (double)value1.Z == (double)value2.Z && (double)value1.W == (double)value2.W;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return (byte)num > 0;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00014C10 File Offset: 0x00012E10
		public bool Equals(Quaternion other)
		{
			bool flag = (double)this.X == (double)other.X && (double)this.Y == (double)other.Y && (double)this.Z == (double)other.Z && (double)this.W == (double)other.W;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return (byte)num > 0;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00014C7C File Offset: 0x00012E7C
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && this.Equals((Quaternion)obj);
		}

		// Token: 0x04000145 RID: 325
		public float x;

		// Token: 0x04000146 RID: 326
		public float y;

		// Token: 0x04000147 RID: 327
		public float z;

		// Token: 0x04000148 RID: 328
		public float w;
	}
}
