using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
	// Token: 0x0200001E RID: 30
	public class PmxMaterial : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Material;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000FC7F File Offset: 0x0000DE7F
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000FC87 File Offset: 0x0000DE87
		public string Name { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000FC90 File Offset: 0x0000DE90
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000FC98 File Offset: 0x0000DE98
		public string NameE { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000FCA1 File Offset: 0x0000DEA1
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000FCA9 File Offset: 0x0000DEA9
		public string Tex { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000FCBA File Offset: 0x0000DEBA
		public string Sphere { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000FCC3 File Offset: 0x0000DEC3
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000FCCB File Offset: 0x0000DECB
		public string Toon { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		public string Memo { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000FCED File Offset: 0x0000DEED
		public PmxMaterialAttribute Attribute { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public string NXName
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000FD1B File Offset: 0x0000DF1B
		public void ClearFlags()
		{
			this.Flags = PmxMaterial.MaterialFlags.None;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public bool GetFlag(PmxMaterial.MaterialFlags f)
		{
			return (f & this.Flags) == f;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000FD48 File Offset: 0x0000DF48
		public void SetFlag(PmxMaterial.MaterialFlags f, bool val)
		{
			if (val)
			{
				this.Flags |= f;
			}
			else
			{
				this.Flags &= ~f;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000FD7E File Offset: 0x0000DF7E
		public void ClearOffset()
		{
			this.OffsetMul.Clear(PmxMaterialMorph.OpType.Mul);
			this.OffsetAdd.Clear(PmxMaterialMorph.OpType.Add);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000FD9B File Offset: 0x0000DF9B
		public void UpdateAttributeFromMemo()
		{
			this.Attribute.SetFromText(this.Memo);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public PmxMaterial()
		{
			this.Name = "";
			this.NameE = "";
			this.Diffuse = new Color(1f, 1f, 1f);
			this.Specular = new Color(0f, 0f, 0f);
			this.Power = 0f;
			this.Ambient = new Color(1f, 1f, 1f);
			this.ClearFlags();
			this.EdgeColor = new Color(0f, 0f, 0f);
			this.EdgeSize = 1f;
			this.Tex = "";
			this.Sphere = "";
			this.SphereMode = PmxMaterial.SphereModeType.Mul;
			this.Toon = "";
			this.Memo = "";
			this.OffsetMul = default(PmxMaterialMorph.MorphData);
			this.OffsetAdd = default(PmxMaterialMorph.MorphData);
			this.ClearOffset();
			this.ExDraw = PmxMaterial.ExDrawMode.F3;
			this.Attribute = new PmxMaterialAttribute();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000FECB File Offset: 0x0000E0CB
		public PmxMaterial(PmxMaterial m, bool nonStr) : this()
		{
			this.FromPmxMaterial(m, nonStr);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		public void FromPmxMaterial(PmxMaterial m, bool nonStr)
		{
			this.Diffuse = m.Diffuse;
			this.Specular = m.Specular;
			this.Power = m.Power;
			this.Ambient = m.Ambient;
			this.Flags = m.Flags;
			this.EdgeColor = m.EdgeColor;
			this.EdgeSize = m.EdgeSize;
			this.SphereMode = m.SphereMode;
			this.FaceCount = m.FaceCount;
			this.ExDraw = m.ExDraw;
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = m.Name;
				this.NameE = m.NameE;
				this.Tex = m.Tex;
				this.Sphere = m.Sphere;
				this.Toon = m.Toon;
				this.Memo = m.Memo;
			}
			this.Attribute = m.Attribute;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Diffuse = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.Specular = V3_BytesConvert.Vector3ToColor(V3_BytesConvert.FromStream(s));
			this.Power = PmxStreamHelper.ReadElement_Float(s);
			this.Ambient = V3_BytesConvert.Vector3ToColor(V3_BytesConvert.FromStream(s));
			this.Flags = (PmxMaterial.MaterialFlags)s.ReadByte();
			this.EdgeColor = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
			this.Tex = PmxStreamHelper.ReadString(s, f);
			this.Sphere = PmxStreamHelper.ReadString(s, f);
			this.SphereMode = (PmxMaterial.SphereModeType)s.ReadByte();
			this.Toon = PmxStreamHelper.ReadString(s, f);
			this.Memo = PmxStreamHelper.ReadString(s, f);
			this.FaceCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000100B0 File Offset: 0x0000E2B0
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.Diffuse));
			V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Specular));
			PmxStreamHelper.WriteElement_Float(s, this.Power);
			V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Ambient));
			s.WriteByte((byte)this.Flags);
			V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.EdgeColor));
			PmxStreamHelper.WriteElement_Float(s, this.EdgeSize);
			PmxStreamHelper.WriteString(s, this.Tex, f);
			PmxStreamHelper.WriteString(s, this.Sphere, f);
			s.WriteByte((byte)this.SphereMode);
			PmxStreamHelper.WriteString(s, this.Toon, f);
			PmxStreamHelper.WriteString(s, this.Memo, f);
			PmxStreamHelper.WriteElement_Int32(s, this.FaceCount, 4, true);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public void FromStreamEx_TexTable(Stream s, PmxTextureTable tx, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Diffuse = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.Specular = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.Power = PmxStreamHelper.ReadElement_Float(s);
			this.Ambient = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.Flags = (PmxMaterial.MaterialFlags)s.ReadByte();
			this.EdgeColor = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
			this.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
			this.Tex = tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
			this.Sphere = tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
			this.SphereMode = (PmxMaterial.SphereModeType)s.ReadByte();
			bool flag = s.ReadByte() == 0;
			if (flag)
			{
				this.Toon = tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
			}
			else
			{
				int n = s.ReadByte();
				this.Toon = SystemToon.GetToonName(n);
			}
			this.Memo = PmxStreamHelper.ReadString(s, f);
			this.UpdateAttributeFromMemo();
			this.FaceCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public void ToStreamEx_TexTable(Stream s, PmxTextureTable tx, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.Diffuse));
			V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Specular));
			PmxStreamHelper.WriteElement_Float(s, this.Power);
			V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Ambient));
			s.WriteByte((byte)this.Flags);
			V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.EdgeColor));
			PmxStreamHelper.WriteElement_Float(s, this.EdgeSize);
			PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Tex), f.TexSize, true);
			PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Sphere), f.TexSize, true);
			s.WriteByte((byte)this.SphereMode);
			int toonIndex = SystemToon.GetToonIndex(this.Toon);
			bool flag = toonIndex < 0;
			if (flag)
			{
				s.WriteByte(0);
				PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Toon), f.TexSize, true);
			}
			else
			{
				s.WriteByte(1);
				s.WriteByte((byte)toonIndex);
			}
			PmxStreamHelper.WriteString(s, this.Memo, f);
			PmxStreamHelper.WriteElement_Int32(s, this.FaceCount, 4, true);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00010420 File Offset: 0x0000E620
		object ICloneable.Clone()
		{
			return new PmxMaterial(this, false);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0001043C File Offset: 0x0000E63C
		public PmxMaterial Clone()
		{
			return new PmxMaterial(this, false);
		}

		// Token: 0x040000B7 RID: 183
		public Color Diffuse;

		// Token: 0x040000B8 RID: 184
		public Color Specular;

		// Token: 0x040000B9 RID: 185
		public float Power;

		// Token: 0x040000BA RID: 186
		public Color Ambient;

		// Token: 0x040000BB RID: 187
		public PmxMaterial.ExDrawMode ExDraw;

		// Token: 0x040000BC RID: 188
		public PmxMaterial.MaterialFlags Flags;

		// Token: 0x040000BD RID: 189
		public Color EdgeColor;

		// Token: 0x040000BE RID: 190
		public float EdgeSize;

		// Token: 0x040000BF RID: 191
		public int FaceCount;

		// Token: 0x040000C0 RID: 192
		public PmxMaterial.SphereModeType SphereMode;

		// Token: 0x040000C1 RID: 193
		public PmxMaterialMorph.MorphData OffsetMul;

		// Token: 0x040000C2 RID: 194
		public PmxMaterialMorph.MorphData OffsetAdd;

		// Token: 0x040000C3 RID: 195
		public List<PmxFace> FaceList;

		// Token: 0x02000054 RID: 84
		[Flags]
		public enum MaterialFlags
		{
			// Token: 0x040001DB RID: 475
			None = 0,
			// Token: 0x040001DC RID: 476
			DrawBoth = 1,
			// Token: 0x040001DD RID: 477
			Shadow = 2,
			// Token: 0x040001DE RID: 478
			SelfShadowMap = 4,
			// Token: 0x040001DF RID: 479
			SelfShadow = 8,
			// Token: 0x040001E0 RID: 480
			Edge = 16,
			// Token: 0x040001E1 RID: 481
			VertexColor = 32,
			// Token: 0x040001E2 RID: 482
			PointDraw = 64,
			// Token: 0x040001E3 RID: 483
			LineDraw = 128
		}

		// Token: 0x02000055 RID: 85
		public enum ExDrawMode
		{
			// Token: 0x040001E5 RID: 485
			F1,
			// Token: 0x040001E6 RID: 486
			F2,
			// Token: 0x040001E7 RID: 487
			F3
		}

		// Token: 0x02000056 RID: 86
		public enum SphereModeType
		{
			// Token: 0x040001E9 RID: 489
			None,
			// Token: 0x040001EA RID: 490
			Mul,
			// Token: 0x040001EB RID: 491
			Add,
			// Token: 0x040001EC RID: 492
			SubTex
		}
	}
}
