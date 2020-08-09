using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000014 RID: 20
	public class PmxBone : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Bone;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000E203 File Offset: 0x0000C403
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000E20B File Offset: 0x0000C40B
		public string Name { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000E214 File Offset: 0x0000C414
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000E21C File Offset: 0x0000C41C
		public string NameE { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000E225 File Offset: 0x0000C425
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000E22D File Offset: 0x0000C42D
		public PmxBone RefParent { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000E236 File Offset: 0x0000C436
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000E23E File Offset: 0x0000C43E
		public PmxBone RefTo_Bone { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000E247 File Offset: 0x0000C447
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000E24F File Offset: 0x0000C44F
		public PmxBone RefAppendParent { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000E258 File Offset: 0x0000C458
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000E260 File Offset: 0x0000C460
		public PmxIK IK { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000E269 File Offset: 0x0000C469
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000E271 File Offset: 0x0000C471
		public PmxBone.IKKindType IKKind { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000E27C File Offset: 0x0000C47C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000E294 File Offset: 0x0000C494
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

		// Token: 0x060000FE RID: 254 RVA: 0x0000E29F File Offset: 0x0000C49F
		public void ClearFlags()
		{
			this.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public bool GetFlag(PmxBone.BoneFlags f)
		{
			return (f & this.Flags) == f;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		public void SetFlag(PmxBone.BoneFlags f, bool val)
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

		// Token: 0x06000101 RID: 257 RVA: 0x0000E304 File Offset: 0x0000C504
		public void ClearLocal()
		{
			this.LocalX = new Vector3(1f, 0f, 0f);
			this.LocalY = new Vector3(0f, 1f, 0f);
			this.LocalZ = new Vector3(0f, 0f, 1f);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000E360 File Offset: 0x0000C560
		public void NormalizeLocal()
		{
			this.LocalZ.Normalize();
			this.LocalX.Normalize();
			this.LocalY = Vector3.Cross(this.LocalZ, this.LocalX);
			this.LocalZ = Vector3.Cross(this.LocalX, this.LocalY);
			this.LocalY.Normalize();
			this.LocalZ.Normalize();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		public PmxBone()
		{
			this.Name = "";
			this.NameE = "";
			this.ClearFlags();
			this.Parent = -1;
			this.To_Bone = -1;
			this.To_Offset = Vector3.zero;
			this.AppendParent = -1;
			this.AppendRatio = 1f;
			this.Level = 0;
			this.ClearLocal();
			this.IK = new PmxIK();
			this.IKKind = PmxBone.IKKindType.None;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000E44D File Offset: 0x0000C64D
		public PmxBone(PmxBone bone, bool nonStr)
		{
			this.FromPmxBone(bone, nonStr);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000E460 File Offset: 0x0000C660
		public void FromPmxBone(PmxBone bone, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = bone.Name;
				this.NameE = bone.NameE;
			}
			this.Flags = bone.Flags;
			this.Parent = bone.Parent;
			this.To_Bone = bone.To_Bone;
			this.To_Offset = bone.To_Offset;
			this.Position = bone.Position;
			this.Level = bone.Level;
			this.AppendParent = bone.AppendParent;
			this.AppendRatio = bone.AppendRatio;
			this.Axis = bone.Axis;
			this.LocalX = bone.LocalX;
			this.LocalY = bone.LocalY;
			this.LocalZ = bone.LocalZ;
			this.ExtKey = bone.ExtKey;
			this.IK = bone.IK.Clone();
			this.IKKind = bone.IKKind;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000E550 File Offset: 0x0000C750
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Position = V3_BytesConvert.FromStream(s);
			this.Parent = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
			this.Level = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Flags = (PmxBone.BoneFlags)PmxStreamHelper.ReadElement_Int32(s, 2, false);
			bool flag = this.GetFlag(PmxBone.BoneFlags.ToBone);
			if (flag)
			{
				this.To_Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
			}
			else
			{
				this.To_Offset = V3_BytesConvert.FromStream(s);
			}
			bool flag2 = this.GetFlag(PmxBone.BoneFlags.AppendRotation) || this.GetFlag(PmxBone.BoneFlags.AppendTranslation);
			if (flag2)
			{
				this.AppendParent = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.AppendRatio = PmxStreamHelper.ReadElement_Float(s);
			}
			bool flag3 = this.GetFlag(PmxBone.BoneFlags.FixAxis);
			if (flag3)
			{
				this.Axis = V3_BytesConvert.FromStream(s);
			}
			bool flag4 = this.GetFlag(PmxBone.BoneFlags.LocalFrame);
			if (flag4)
			{
				this.LocalX = V3_BytesConvert.FromStream(s);
				this.LocalZ = V3_BytesConvert.FromStream(s);
				this.NormalizeLocal();
			}
			bool flag5 = this.GetFlag(PmxBone.BoneFlags.ExtParent);
			if (flag5)
			{
				this.ExtKey = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			}
			bool flag6 = this.GetFlag(PmxBone.BoneFlags.IK);
			if (flag6)
			{
				this.IK.FromStreamEx(s, f);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			V3_BytesConvert.ToStream(s, this.Position);
			PmxStreamHelper.WriteElement_Int32(s, this.Parent, f.BoneSize, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Level, 4, true);
			PmxStreamHelper.WriteElement_Int32(s, (int)this.Flags, 2, false);
			bool flag = this.GetFlag(PmxBone.BoneFlags.ToBone);
			if (flag)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.To_Bone, f.BoneSize, true);
			}
			else
			{
				V3_BytesConvert.ToStream(s, this.To_Offset);
			}
			bool flag2 = this.GetFlag(PmxBone.BoneFlags.AppendRotation) || this.GetFlag(PmxBone.BoneFlags.AppendTranslation);
			if (flag2)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.AppendParent, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Float(s, this.AppendRatio);
			}
			bool flag3 = this.GetFlag(PmxBone.BoneFlags.FixAxis);
			if (flag3)
			{
				V3_BytesConvert.ToStream(s, this.Axis);
			}
			bool flag4 = this.GetFlag(PmxBone.BoneFlags.LocalFrame);
			if (flag4)
			{
				this.NormalizeLocal();
				V3_BytesConvert.ToStream(s, this.LocalX);
				V3_BytesConvert.ToStream(s, this.LocalZ);
			}
			bool flag5 = this.GetFlag(PmxBone.BoneFlags.ExtParent);
			if (flag5)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.ExtKey, 4, true);
			}
			bool flag6 = this.GetFlag(PmxBone.BoneFlags.IK);
			if (flag6)
			{
				this.IK.ToStreamEx(s, f);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000E824 File Offset: 0x0000CA24
		object ICloneable.Clone()
		{
			return new PmxBone(this, false);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000E840 File Offset: 0x0000CA40
		public PmxBone Clone()
		{
			return new PmxBone(this, false);
		}

		// Token: 0x0400006F RID: 111
		public PmxBone.BoneFlags Flags;

		// Token: 0x04000070 RID: 112
		public int Parent;

		// Token: 0x04000071 RID: 113
		public int To_Bone;

		// Token: 0x04000072 RID: 114
		public Vector3 To_Offset;

		// Token: 0x04000073 RID: 115
		public Vector3 Position;

		// Token: 0x04000074 RID: 116
		public int Level;

		// Token: 0x04000075 RID: 117
		public int AppendParent;

		// Token: 0x04000076 RID: 118
		public float AppendRatio;

		// Token: 0x04000077 RID: 119
		public Vector3 Axis;

		// Token: 0x04000078 RID: 120
		public Vector3 LocalX;

		// Token: 0x04000079 RID: 121
		public Vector3 LocalY;

		// Token: 0x0400007A RID: 122
		public Vector3 LocalZ;

		// Token: 0x0400007B RID: 123
		public int ExtKey;

		// Token: 0x0200004F RID: 79
		[Flags]
		public enum BoneFlags
		{
			// Token: 0x040001B6 RID: 438
			None = 0,
			// Token: 0x040001B7 RID: 439
			ToBone = 1,
			// Token: 0x040001B8 RID: 440
			Rotation = 2,
			// Token: 0x040001B9 RID: 441
			Translation = 4,
			// Token: 0x040001BA RID: 442
			Visible = 8,
			// Token: 0x040001BB RID: 443
			Enable = 16,
			// Token: 0x040001BC RID: 444
			IK = 32,
			// Token: 0x040001BD RID: 445
			AppendLocal = 128,
			// Token: 0x040001BE RID: 446
			AppendRotation = 256,
			// Token: 0x040001BF RID: 447
			AppendTranslation = 512,
			// Token: 0x040001C0 RID: 448
			FixAxis = 1024,
			// Token: 0x040001C1 RID: 449
			LocalFrame = 2048,
			// Token: 0x040001C2 RID: 450
			AfterPhysics = 4096,
			// Token: 0x040001C3 RID: 451
			ExtParent = 8192
		}

		// Token: 0x02000050 RID: 80
		public enum IKKindType
		{
			// Token: 0x040001C5 RID: 453
			None,
			// Token: 0x040001C6 RID: 454
			IK,
			// Token: 0x040001C7 RID: 455
			Target,
			// Token: 0x040001C8 RID: 456
			Link
		}
	}
}
