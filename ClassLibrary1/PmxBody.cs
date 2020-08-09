using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000012 RID: 18
	public class PmxBody : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Body;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000DB80 File Offset: 0x0000BD80
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000DB88 File Offset: 0x0000BD88
		public string Name { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000DB91 File Offset: 0x0000BD91
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000DB99 File Offset: 0x0000BD99
		public string NameE { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000DBA2 File Offset: 0x0000BDA2
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000DBAA File Offset: 0x0000BDAA
		public PmxBone RefBone { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000DBB3 File Offset: 0x0000BDB3
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000DBBB File Offset: 0x0000BDBB
		public PmxBody.BoxKind BoxType { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public PmxBody.ModeType Mode { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
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

		// Token: 0x060000D9 RID: 217 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public PmxBody()
		{
			this.Name = "";
			this.NameE = "";
			this.PassGroup = new PmxBodyPassGroup();
			this.InitializeParameter();
			this.BoxSize = new Vector3(2f, 2f, 2f);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000DC55 File Offset: 0x0000BE55
		public PmxBody(PmxBody body, bool nonStr)
		{
			this.FromPmxBody(body, nonStr);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public void FromPmxBody(PmxBody body, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = body.Name;
				this.NameE = body.NameE;
			}
			this.Bone = body.Bone;
			this.Group = body.Group;
			this.PassGroup = body.PassGroup.Clone();
			this.BoxType = body.BoxType;
			this.BoxSize = body.BoxSize;
			this.Position = body.Position;
			this.Rotation = body.Rotation;
			this.Mass = body.Mass;
			this.PositionDamping = body.PositionDamping;
			this.RotationDamping = body.RotationDamping;
			this.Restitution = body.Restitution;
			this.Friction = body.Friction;
			this.Mode = body.Mode;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		public void InitializeParameter()
		{
			this.Mass = 1f;
			this.PositionDamping = 0.5f;
			this.RotationDamping = 0.5f;
			this.Restitution = 0f;
			this.Friction = 0.5f;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
			this.Group = PmxStreamHelper.ReadElement_Int32(s, 1, true);
			ushort bits = (ushort)PmxStreamHelper.ReadElement_Int32(s, 2, false);
			this.PassGroup.FromFlagBits(bits);
			this.BoxType = (PmxBody.BoxKind)s.ReadByte();
			this.BoxSize = V3_BytesConvert.FromStream(s);
			this.Position = V3_BytesConvert.FromStream(s);
			this.Rotation = V3_BytesConvert.FromStream(s);
			this.Mass = PmxStreamHelper.ReadElement_Float(s);
			Vector4 vector = V4_BytesConvert.FromStream(s);
			this.PositionDamping = vector.x;
			this.RotationDamping = vector.y;
			this.Restitution = vector.z;
			this.Friction = vector.w;
			this.Mode = (PmxBody.ModeType)s.ReadByte();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			PmxStreamHelper.WriteElement_Int32(s, this.Bone, f.BoneSize, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Group, 1, true);
			PmxStreamHelper.WriteElement_Int32(s, (int)this.PassGroup.ToFlagBits(), 2, false);
			s.WriteByte((byte)this.BoxType);
			V3_BytesConvert.ToStream(s, this.BoxSize);
			V3_BytesConvert.ToStream(s, this.Position);
			V3_BytesConvert.ToStream(s, this.Rotation);
			PmxStreamHelper.WriteElement_Float(s, this.Mass);
			V4_BytesConvert.ToStream(s, new Vector4(this.PositionDamping, this.RotationDamping, this.Restitution, this.Friction));
			s.WriteByte((byte)this.Mode);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000DF34 File Offset: 0x0000C134
		object ICloneable.Clone()
		{
			return new PmxBody(this, false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000DF50 File Offset: 0x0000C150
		public PmxBody Clone()
		{
			return new PmxBody(this, false);
		}

		// Token: 0x0400005C RID: 92
		public int Bone;

		// Token: 0x0400005D RID: 93
		public static string NullBoneName = "-";

		// Token: 0x0400005E RID: 94
		public int Group;

		// Token: 0x0400005F RID: 95
		public PmxBodyPassGroup PassGroup;

		// Token: 0x04000060 RID: 96
		public Vector3 BoxSize;

		// Token: 0x04000061 RID: 97
		public Vector3 Position;

		// Token: 0x04000062 RID: 98
		public Vector3 Rotation;

		// Token: 0x04000063 RID: 99
		public float Mass;

		// Token: 0x04000064 RID: 100
		public float PositionDamping;

		// Token: 0x04000065 RID: 101
		public float RotationDamping;

		// Token: 0x04000066 RID: 102
		public float Restitution;

		// Token: 0x04000067 RID: 103
		public float Friction;

		// Token: 0x0200004D RID: 77
		public enum BoxKind
		{
			// Token: 0x040001AE RID: 430
			Sphere,
			// Token: 0x040001AF RID: 431
			Box,
			// Token: 0x040001B0 RID: 432
			Capsule
		}

		// Token: 0x0200004E RID: 78
		public enum ModeType
		{
			// Token: 0x040001B2 RID: 434
			Static,
			// Token: 0x040001B3 RID: 435
			Dynamic,
			// Token: 0x040001B4 RID: 436
			DynamicWithBone
		}
	}
}
