using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000020 RID: 32
	public class PmxMaterialMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00010828 File Offset: 0x0000EA28
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.MaterialMorph;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0001083C File Offset: 0x0000EA3C
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00010854 File Offset: 0x0000EA54
		public override int BaseIndex
		{
			get
			{
				return this.Index;
			}
			set
			{
				this.Index = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0001085E File Offset: 0x0000EA5E
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00010866 File Offset: 0x0000EA66
		public PmxMaterial RefMaterial { get; set; }

		// Token: 0x060001C7 RID: 455 RVA: 0x0001086F File Offset: 0x0000EA6F
		public void ClearData()
		{
			this.Data.Clear(this.Op);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00010884 File Offset: 0x0000EA84
		public PmxMaterialMorph()
		{
			this.Op = PmxMaterialMorph.OpType.Mul;
			this.ClearData();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001089C File Offset: 0x0000EA9C
		public PmxMaterialMorph(int index, PmxMaterialMorph.MorphData d) : this()
		{
			this.Index = index;
			this.Data = d;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public PmxMaterialMorph(PmxMaterialMorph sv) : this()
		{
			this.FromPmxMaterialMorph(sv);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000108C6 File Offset: 0x0000EAC6
		public void FromPmxMaterialMorph(PmxMaterialMorph sv)
		{
			this.Index = sv.Index;
			this.Op = sv.Op;
			this.Data = sv.Data;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.MaterialSize, true);
			this.Op = (PmxMaterialMorph.OpType)s.ReadByte();
			this.Data.Diffuse = V4_BytesConvert.FromStream(s);
			this.Data.Specular = V4_BytesConvert.FromStream(s);
			this.Data.Ambient = V3_BytesConvert.FromStream(s);
			this.Data.EdgeColor = V4_BytesConvert.FromStream(s);
			this.Data.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
			this.Data.Tex = V4_BytesConvert.FromStream(s);
			this.Data.Sphere = V4_BytesConvert.FromStream(s);
			this.Data.Toon = V4_BytesConvert.FromStream(s);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000109A8 File Offset: 0x0000EBA8
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.MaterialSize, true);
			s.WriteByte((byte)this.Op);
			V4_BytesConvert.ToStream(s, this.Data.Diffuse);
			V4_BytesConvert.ToStream(s, this.Data.Specular);
			V3_BytesConvert.ToStream(s, this.Data.Ambient);
			V4_BytesConvert.ToStream(s, this.Data.EdgeColor);
			PmxStreamHelper.WriteElement_Float(s, this.Data.EdgeSize);
			V4_BytesConvert.ToStream(s, this.Data.Tex);
			V4_BytesConvert.ToStream(s, this.Data.Sphere);
			V4_BytesConvert.ToStream(s, this.Data.Toon);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00010A68 File Offset: 0x0000EC68
		object ICloneable.Clone()
		{
			return new PmxMaterialMorph(this);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00010A80 File Offset: 0x0000EC80
		public override PmxBaseMorph Clone()
		{
			return new PmxMaterialMorph(this);
		}

		// Token: 0x040000E0 RID: 224
		public int Index;

		// Token: 0x040000E1 RID: 225
		public static PmxMaterial RefAllMaterial = new PmxMaterial();

		// Token: 0x040000E2 RID: 226
		public PmxMaterialMorph.OpType Op;

		// Token: 0x040000E3 RID: 227
		public PmxMaterialMorph.MorphData Data;

		// Token: 0x02000058 RID: 88
		public enum OpType
		{
			// Token: 0x040001F8 RID: 504
			Mul,
			// Token: 0x040001F9 RID: 505
			Add
		}

		// Token: 0x02000059 RID: 89
		public struct MorphData
		{
			// Token: 0x060003A6 RID: 934 RVA: 0x00019818 File Offset: 0x00017A18
			public void Set(float v)
			{
				this.Diffuse = new Vector4(v, v, v, v);
				this.Specular = new Vector4(v, v, v, v);
				this.Ambient = new Vector3(v, v, v);
				this.EdgeSize = v;
				this.EdgeColor = new Vector4(v, v, v, v);
				this.Tex = new Vector4(v, v, v, v);
				this.Sphere = new Vector4(v, v, v, v);
				this.Toon = new Vector4(v, v, v, v);
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x00019898 File Offset: 0x00017A98
			public void Clear(PmxMaterialMorph.OpType op)
			{
				if (op != PmxMaterialMorph.OpType.Mul)
				{
					if (op == PmxMaterialMorph.OpType.Add)
					{
						this.Diffuse = Vector4.zero;
						this.Specular = Vector4.zero;
						this.Ambient = Vector3.zero;
						this.EdgeSize = 0f;
						this.EdgeColor = Vector4.zero;
						this.Tex = Vector4.zero;
						this.Sphere = Vector4.zero;
						this.Toon = Vector4.zero;
					}
				}
				else
				{
					this.Set(1f);
					this.Diffuse = new Vector4(1f, 1f, 1f, 1f);
					this.Specular = new Vector4(1f, 1f, 1f, 1f);
					this.Ambient = new Vector3(1f, 1f, 1f);
					this.EdgeSize = 1f;
					this.EdgeColor = new Vector4(1f, 1f, 1f, 1f);
					this.Tex = new Vector4(1f, 1f, 1f, 1f);
					this.Sphere = new Vector4(1f, 1f, 1f, 1f);
					this.Toon = new Vector4(1f, 1f, 1f, 1f);
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00019A04 File Offset: 0x00017C04
			private Vector4 mul_v4(Vector4 v0, Vector4 v1)
			{
				return new Vector4(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z, v0.w * v1.w);
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x00019A50 File Offset: 0x00017C50
			private Vector3 mul_v3(Vector3 v0, Vector3 v1)
			{
				return new Vector3(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z);
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00019A90 File Offset: 0x00017C90
			public void Mul(PmxMaterialMorph.MorphData d)
			{
				this.Diffuse = this.mul_v4(this.Diffuse, d.Diffuse);
				this.Specular = this.mul_v4(this.Specular, d.Specular);
				this.Ambient = this.mul_v3(this.Ambient, d.Ambient);
				this.EdgeSize *= d.EdgeSize;
				this.EdgeColor = this.mul_v4(this.EdgeColor, d.EdgeColor);
				this.Tex = this.mul_v4(this.Tex, d.Tex);
				this.Sphere = this.mul_v4(this.Sphere, d.Sphere);
				this.Toon = this.mul_v4(this.Toon, d.Toon);
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00019B5C File Offset: 0x00017D5C
			public void Mul(float v)
			{
				this.Diffuse *= v;
				this.Specular *= v;
				this.Ambient *= v;
				this.EdgeSize *= v;
				this.EdgeColor *= v;
				this.Tex *= v;
				this.Sphere *= v;
				this.Toon *= v;
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00019BF8 File Offset: 0x00017DF8
			public void Add(PmxMaterialMorph.MorphData d)
			{
				this.Diffuse += d.Diffuse;
				this.Specular += d.Specular;
				this.Ambient += d.Ambient;
				this.EdgeSize += d.EdgeSize;
				this.EdgeColor += d.EdgeColor;
				this.Tex += d.Tex;
				this.Sphere += d.Sphere;
				this.Toon += d.Toon;
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00019CBC File Offset: 0x00017EBC
			public static PmxMaterialMorph.MorphData Inter(PmxMaterialMorph.MorphData a, PmxMaterialMorph.MorphData b, float val)
			{
				bool flag = val == 0f;
				PmxMaterialMorph.MorphData result;
				if (flag)
				{
					result = a;
				}
				else
				{
					bool flag2 = val == 1f;
					if (flag2)
					{
						result = b;
					}
					else
					{
						PmxMaterialMorph.MorphData morphData = a;
						morphData.Mul(1f - val);
						PmxMaterialMorph.MorphData d = b;
						d.Mul(val);
						morphData.Add(d);
						result = morphData;
					}
				}
				return result;
			}

			// Token: 0x040001FA RID: 506
			public Vector4 Diffuse;

			// Token: 0x040001FB RID: 507
			public Vector4 Specular;

			// Token: 0x040001FC RID: 508
			public Vector3 Ambient;

			// Token: 0x040001FD RID: 509
			public float EdgeSize;

			// Token: 0x040001FE RID: 510
			public Vector4 EdgeColor;

			// Token: 0x040001FF RID: 511
			public Vector4 Tex;

			// Token: 0x04000200 RID: 512
			public Vector4 Sphere;

			// Token: 0x04000201 RID: 513
			public Vector4 Toon;
		}
	}
}
