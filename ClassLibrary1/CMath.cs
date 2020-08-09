using System;

namespace PmxLib
{
	// Token: 0x02000007 RID: 7
	internal static class CMath
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000AA50 File Offset: 0x00008C50
		public static float CrossVector2(Vector2 p, Vector2 q)
		{
			return p.X * q.Y - p.Y * q.X;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000AA84 File Offset: 0x00008C84
		public static Vector2 NormalizeValue(Vector2 val)
		{
			bool flag = float.IsNaN(val.X);
			if (flag)
			{
				val.X = 0f;
			}
			bool flag2 = float.IsNaN(val.Y);
			if (flag2)
			{
				val.Y = 0f;
			}
			return val;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public static Vector3 NormalizeValue(Vector3 val)
		{
			bool flag = float.IsNaN(val.X);
			if (flag)
			{
				val.X = 0f;
			}
			bool flag2 = float.IsNaN(val.Y);
			if (flag2)
			{
				val.Y = 0f;
			}
			bool flag3 = float.IsNaN(val.Z);
			if (flag3)
			{
				val.Z = 0f;
			}
			return val;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000AB48 File Offset: 0x00008D48
		public static Vector4 NormalizeValue(Vector4 val)
		{
			bool flag = float.IsNaN(val.X);
			if (flag)
			{
				val.X = 0f;
			}
			bool flag2 = float.IsNaN(val.Y);
			if (flag2)
			{
				val.Y = 0f;
			}
			bool flag3 = float.IsNaN(val.Z);
			if (flag3)
			{
				val.Z = 0f;
			}
			bool flag4 = float.IsNaN(val.W);
			if (flag4)
			{
				val.W = 0f;
			}
			return val;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public static bool GetIntersectPoint(Vector2 p0, Vector2 p1, Vector2 q0, Vector2 q1, ref Vector2 rate, ref Vector2 pos)
		{
			Vector2 vector = p1 - p0;
			Vector2 q2 = q1 - q0;
			float num = CMath.CrossVector2(vector, q2);
			bool flag = num == 0f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Vector2 p2 = q0 - p0;
				float num2 = CMath.CrossVector2(p2, vector);
				float num3 = CMath.CrossVector2(p2, q2);
				float num4 = num2 / num;
				float num5 = num3 / num;
				bool flag2 = num5 + 1E-05f < 0f || num5 - 1E-05f > 1f || num4 + 1E-05f < 0f || num4 - 1E-05f > 1f;
				if (flag2)
				{
					result = false;
				}
				else
				{
					rate = new Vector2(num5, num4);
					pos = p0 + vector * num5;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public static Vector3 GetFaceNormal(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			Vector3 lhs = p1 - p0;
			Vector3 rhs = p2 - p0;
			Vector3 result = Vector3.Cross(lhs, rhs);
			result.Normalize();
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public static Vector3 GetFaceOrigin(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			return (p0 + p1 + p2) / 3f;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000AD20 File Offset: 0x00008F20
		public static Vector3 GetTriangleIntersect(Vector3 org, Vector3 dir, Vector3 v0, Vector3 v1, Vector3 v2)
		{
			Vector3 result = new Vector3(-1f, 0f, 0f);
			Vector3 vector = v1 - v0;
			Vector3 vector2 = v2 - v0;
			Vector3 rhs = Vector3.Cross(dir, vector2);
			float num = Vector3.Dot(vector, rhs);
			bool flag = num > 0.001f;
			float num2;
			Vector3 rhs2;
			float num3;
			if (flag)
			{
				Vector3 lhs = org - v0;
				num2 = Vector3.Dot(lhs, rhs);
				bool flag2 = num2 < 0f || num2 > num;
				if (flag2)
				{
					return result;
				}
				rhs2 = Vector3.Cross(lhs, vector);
				num3 = Vector3.Dot(dir, rhs2);
				bool flag3 = num3 < 0f || num2 + num3 > num;
				if (flag3)
				{
					return result;
				}
			}
			else
			{
				bool flag4 = num >= -0.001f;
				if (flag4)
				{
					return result;
				}
				Vector3 lhs2 = org - v0;
				num2 = Vector3.Dot(lhs2, rhs);
				bool flag5 = (double)num2 > 0.0 || num2 < num;
				if (flag5)
				{
					return result;
				}
				rhs2 = Vector3.Cross(lhs2, vector);
				num3 = Vector3.Dot(dir, rhs2);
				bool flag6 = (double)num3 > 0.0 || num2 + num3 < num;
				if (flag6)
				{
					return result;
				}
			}
			float num4 = 1f / num;
			float num5 = Vector3.Dot(vector2, rhs2);
			num5 *= num4;
			num2 *= num4;
			num3 *= num4;
			result.X = num5;
			result.Y = num2;
			result.Z = num3;
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public static Vector3 GetLineCrossPoint(Vector3 p, Vector3 from, Vector3 dir, out float d)
		{
			Vector3 rhs = p - from;
			d = Vector3.Dot(dir, rhs);
			return dir * d + from;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000AF14 File Offset: 0x00009114
		public static Vector3 GetLineCrossPoint(Vector3 p, Vector3 from, Vector3 dir)
		{
			float num;
			return CMath.GetLineCrossPoint(p, from, dir, out num);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000AF30 File Offset: 0x00009130
		public static Matrix CreateViewportMatrix(int width, int height)
		{
			Matrix identity = Matrix.Identity;
			float num = (float)width * 0.5f;
			float num2 = (float)height * 0.5f;
			identity.M11 = num;
			identity.M22 = -num2;
			identity.M41 = num;
			identity.M42 = num2;
			return identity;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000AF7C File Offset: 0x0000917C
		public static bool InArcPosition(Vector3 pos, float cx, float cy, float r2, out float d2)
		{
			float num = pos.X - cx;
			float num2 = pos.Y - cy;
			d2 = num * num + num2 * num2;
			return d2 <= r2;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public static bool InArcPosition(Vector3 pos, float cx, float cy, float r2)
		{
			float num;
			return CMath.InArcPosition(pos, cx, cy, r2, out num);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000AFD8 File Offset: 0x000091D8
		public static void NormalizeRotateXYZ(ref Vector3 v)
		{
			bool flag = v.X < -3.14159274f;
			if (flag)
			{
				v.X += 6.28318548f;
			}
			else
			{
				bool flag2 = v.X > 3.14159274f;
				if (flag2)
				{
					v.X -= 6.28318548f;
				}
			}
			bool flag3 = v.Y < -3.14159274f;
			if (flag3)
			{
				v.Y += 6.28318548f;
			}
			else
			{
				bool flag4 = v.Y > 3.14159274f;
				if (flag4)
				{
					v.Y -= 6.28318548f;
				}
			}
			bool flag5 = v.Z < -3.14159274f;
			if (flag5)
			{
				v.Z += 6.28318548f;
			}
			else
			{
				bool flag6 = v.Z > 3.14159274f;
				if (flag6)
				{
					v.Z -= 6.28318548f;
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000B0DC File Offset: 0x000092DC
		public static Vector3 MatrixToEuler_ZXY0(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			bool flag = m.M32 == 1f;
			if (flag)
			{
				zero.X = 1.57079637f;
				zero.Z = (float)Math.Atan2((double)m.M21, (double)m.M11);
			}
			else
			{
				bool flag2 = m.M32 == -1f;
				if (flag2)
				{
					zero.X = -1.57079637f;
					zero.Z = (float)Math.Atan2((double)m.M21, (double)m.M11);
				}
				else
				{
					zero.X = -(float)Math.Asin((double)m.M32);
					zero.Y = -(float)Math.Atan2(-(double)m.M31, (double)m.M33);
					zero.Z = -(float)Math.Atan2(-(double)m.M12, (double)m.M22);
				}
			}
			return zero;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000B1CC File Offset: 0x000093CC
		public static Vector3 MatrixToEuler_ZXY(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.X = -(float)Math.Asin((double)m.M32);
			bool flag = zero.X == 1.57079637f || zero.X == -1.57079637f;
			if (flag)
			{
				zero.Y = (float)Math.Atan2(-(double)m.M13, (double)m.M11);
			}
			else
			{
				zero.Y = (float)Math.Atan2((double)m.M31, (double)m.M33);
				zero.Z = (float)Math.Asin((double)(m.M12 / (float)Math.Cos((double)zero.X)));
				bool flag2 = m.M22 < 0f;
				if (flag2)
				{
					zero.Z = 3.14159274f - zero.Z;
				}
			}
			return zero;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000B2AC File Offset: 0x000094AC
		public static Vector3 MatrixToEuler_XYZ(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.Y = -(float)Math.Asin((double)m.M13);
			bool flag = zero.Y == 1.57079637f || zero.Y == -1.57079637f;
			if (flag)
			{
				zero.Z = (float)Math.Atan2(-(double)m.M21, (double)m.M22);
			}
			else
			{
				zero.Z = (float)Math.Atan2((double)m.M12, (double)m.M11);
				zero.X = (float)Math.Asin((double)(m.M23 / (float)Math.Cos((double)zero.Y)));
				bool flag2 = m.M33 < 0f;
				if (flag2)
				{
					zero.X = 3.14159274f - zero.X;
				}
			}
			return zero;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000B38C File Offset: 0x0000958C
		public static Vector3 MatrixToEuler_YZX(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.Z = -(float)Math.Asin((double)m.M21);
			bool flag = zero.Z == 1.57079637f || zero.Z == -1.57079637f;
			if (flag)
			{
				zero.X = (float)Math.Atan2(-(double)m.M32, (double)m.M33);
			}
			else
			{
				zero.X = (float)Math.Atan2((double)m.M23, (double)m.M22);
				zero.Y = (float)Math.Asin((double)(m.M31 / (float)Math.Cos((double)zero.Z)));
				bool flag2 = m.M11 < 0f;
				if (flag2)
				{
					zero.Y = 3.14159274f - zero.Y;
				}
			}
			return zero;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000B46C File Offset: 0x0000966C
		public static Vector3 MatrixToEuler_ZXY_Lim2(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.X = -(float)Math.Asin((double)m.M32);
			bool flag = zero.X == 1.57079637f || zero.X == -1.57079637f;
			if (flag)
			{
				zero.Y = (float)Math.Atan2(-(double)m.M13, (double)m.M11);
			}
			else
			{
				bool flag2 = 1.53588974f < zero.X;
				if (flag2)
				{
					zero.X = 1.53588974f;
				}
				else
				{
					bool flag3 = zero.X < -1.53588974f;
					if (flag3)
					{
						zero.X = -1.53588974f;
					}
				}
				zero.Y = (float)Math.Atan2((double)m.M31, (double)m.M33);
				zero.Z = (float)Math.Asin((double)(m.M12 / (float)Math.Cos((double)zero.X)));
				bool flag4 = m.M22 < 0f;
				if (flag4)
				{
					zero.Z = 3.14159274f - zero.Z;
				}
			}
			return zero;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000B598 File Offset: 0x00009798
		public static Vector3 MatrixToEuler_XYZ_Lim2(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.Y = -(float)Math.Asin((double)m.M13);
			bool flag = zero.Y == 1.57079637f || zero.Y == -1.57079637f;
			if (flag)
			{
				zero.Z = (float)Math.Atan2(-(double)m.M21, (double)m.M22);
			}
			else
			{
				bool flag2 = 1.53588974f < zero.Y;
				if (flag2)
				{
					zero.Y = 1.53588974f;
				}
				else
				{
					bool flag3 = zero.Y < -1.53588974f;
					if (flag3)
					{
						zero.Y = -1.53588974f;
					}
				}
				zero.Z = (float)Math.Atan2((double)m.M12, (double)m.M11);
				zero.X = (float)Math.Asin((double)(m.M23 / (float)Math.Cos((double)zero.Y)));
				bool flag4 = m.M33 < 0f;
				if (flag4)
				{
					zero.X = 3.14159274f - zero.X;
				}
			}
			return zero;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public static Vector3 MatrixToEuler_YZX_Lim2(ref Matrix m)
		{
			Vector3 zero = Vector3.Zero;
			zero.Z = -(float)Math.Asin((double)m.M21);
			bool flag = zero.Z == 1.57079637f || zero.Z == -1.57079637f;
			if (flag)
			{
				zero.X = (float)Math.Atan2(-(double)m.M32, (double)m.M33);
			}
			else
			{
				bool flag2 = 1.53588974f < zero.Z;
				if (flag2)
				{
					zero.Z = 1.53588974f;
				}
				else
				{
					bool flag3 = zero.Z < -1.53588974f;
					if (flag3)
					{
						zero.Z = -1.53588974f;
					}
				}
				zero.X = (float)Math.Atan2((double)m.M23, (double)m.M22);
				zero.Y = (float)Math.Asin((double)(m.M31 / (float)Math.Cos((double)zero.Z)));
				bool flag4 = m.M11 < 0f;
				if (flag4)
				{
					zero.Y = 3.14159274f - zero.Y;
				}
			}
			return zero;
		}
	}
}
