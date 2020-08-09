using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200001C RID: 28
	public class PmxJoint : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000F708 File Offset: 0x0000D908
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Joint;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000F71C File Offset: 0x0000D91C
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000F724 File Offset: 0x0000D924
		public string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000F72D File Offset: 0x0000D92D
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000F735 File Offset: 0x0000D935
		public string NameE { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000F73E File Offset: 0x0000D93E
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000F746 File Offset: 0x0000D946
		public PmxBody RefBodyA { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000F74F File Offset: 0x0000D94F
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000F757 File Offset: 0x0000D957
		public PmxBody RefBodyB { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000F760 File Offset: 0x0000D960
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000F778 File Offset: 0x0000D978
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

		// Token: 0x06000182 RID: 386 RVA: 0x0000F783 File Offset: 0x0000D983
		public PmxJoint()
		{
			this.Name = "";
			this.NameE = "";
			this.BodyA = -1;
			this.BodyB = -1;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000F7B3 File Offset: 0x0000D9B3
		public PmxJoint(PmxJoint joint, bool nonStr)
		{
			this.FromPmxJoint(joint, nonStr);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public void FromPmxJoint(PmxJoint joint, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = joint.Name;
				this.NameE = joint.NameE;
			}
			this.Kind = joint.Kind;
			this.BodyA = joint.BodyA;
			this.BodyB = joint.BodyB;
			this.Position = joint.Position;
			this.Rotation = joint.Rotation;
			this.Limit_MoveLow = joint.Limit_MoveLow;
			this.Limit_MoveHigh = joint.Limit_MoveHigh;
			this.Limit_AngleLow = joint.Limit_AngleLow;
			this.Limit_AngleHigh = joint.Limit_AngleHigh;
			this.SpConst_Move = joint.SpConst_Move;
			this.SpConst_Rotate = joint.SpConst_Rotate;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000F880 File Offset: 0x0000DA80
		public void ClearLimit()
		{
			this.Limit_AngleLow = Vector3.zero;
			this.Limit_AngleHigh = Vector3.zero;
			bool flag = this.Kind == PmxJoint.JointKind.ConeTwist;
			if (flag)
			{
				this.Limit_MoveLow.y = 0f;
				this.Limit_MoveHigh.y = 0f;
			}
			else
			{
				this.Limit_MoveLow = Vector3.zero;
				this.Limit_MoveHigh = Vector3.zero;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public void ClearParameter()
		{
			switch (this.Kind)
			{
			case PmxJoint.JointKind.Sp6DOF:
			case PmxJoint.JointKind.G6DOF:
			case PmxJoint.JointKind.P2P:
			case PmxJoint.JointKind.Slider:
				this.SpConst_Move = Vector3.zero;
				this.SpConst_Rotate = Vector3.zero;
				break;
			case PmxJoint.JointKind.ConeTwist:
				this.Limit_MoveLow.x = 0f;
				this.Limit_MoveHigh.x = 1f;
				this.Limit_MoveLow.z = 0f;
				this.Limit_MoveHigh.z = 0f;
				this.SpConst_Move = new Vector3(1f, 0.3f, 1f);
				this.SpConst_Rotate = Vector3.zero;
				break;
			case PmxJoint.JointKind.Hinge:
				this.SpConst_Move = new Vector3(0.9f, 0.3f, 1f);
				this.SpConst_Rotate = Vector3.zero;
				break;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Kind = (PmxJoint.JointKind)s.ReadByte();
			this.BodyA = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
			this.BodyB = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
			this.Position = V3_BytesConvert.FromStream(s);
			this.Rotation = V3_BytesConvert.FromStream(s);
			this.Limit_MoveLow = V3_BytesConvert.FromStream(s);
			this.Limit_MoveHigh = V3_BytesConvert.FromStream(s);
			this.Limit_AngleLow = V3_BytesConvert.FromStream(s);
			this.Limit_AngleHigh = V3_BytesConvert.FromStream(s);
			this.SpConst_Move = V3_BytesConvert.FromStream(s);
			this.SpConst_Rotate = V3_BytesConvert.FromStream(s);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			bool flag = this.Kind != PmxJoint.JointKind.Sp6DOF && f.Ver < 2.1f;
			if (flag)
			{
				s.WriteByte(0);
			}
			else
			{
				s.WriteByte((byte)this.Kind);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.BodyA, f.BodySize, true);
			PmxStreamHelper.WriteElement_Int32(s, this.BodyB, f.BodySize, true);
			V3_BytesConvert.ToStream(s, this.Position);
			V3_BytesConvert.ToStream(s, this.Rotation);
			V3_BytesConvert.ToStream(s, this.Limit_MoveLow);
			V3_BytesConvert.ToStream(s, this.Limit_MoveHigh);
			V3_BytesConvert.ToStream(s, this.Limit_AngleLow);
			V3_BytesConvert.ToStream(s, this.Limit_AngleHigh);
			V3_BytesConvert.ToStream(s, this.SpConst_Move);
			V3_BytesConvert.ToStream(s, this.SpConst_Rotate);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000FB84 File Offset: 0x0000DD84
		object ICloneable.Clone()
		{
			return new PmxJoint(this, false);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public PmxJoint Clone()
		{
			return new PmxJoint(this, false);
		}

		// Token: 0x040000A7 RID: 167
		public PmxJoint.JointKind Kind;

		// Token: 0x040000A8 RID: 168
		public int BodyA;

		// Token: 0x040000A9 RID: 169
		public int BodyB;

		// Token: 0x040000AA RID: 170
		public Vector3 Position;

		// Token: 0x040000AB RID: 171
		public Vector3 Rotation;

		// Token: 0x040000AC RID: 172
		public Vector3 Limit_MoveLow;

		// Token: 0x040000AD RID: 173
		public Vector3 Limit_MoveHigh;

		// Token: 0x040000AE RID: 174
		public Vector3 Limit_AngleLow;

		// Token: 0x040000AF RID: 175
		public Vector3 Limit_AngleHigh;

		// Token: 0x040000B0 RID: 176
		public Vector3 SpConst_Move;

		// Token: 0x040000B1 RID: 177
		public Vector3 SpConst_Rotate;

		// Token: 0x02000053 RID: 83
		public enum JointKind
		{
			// Token: 0x040001D4 RID: 468
			Sp6DOF,
			// Token: 0x040001D5 RID: 469
			G6DOF,
			// Token: 0x040001D6 RID: 470
			P2P,
			// Token: 0x040001D7 RID: 471
			ConeTwist,
			// Token: 0x040001D8 RID: 472
			Slider,
			// Token: 0x040001D9 RID: 473
			Hinge
		}
	}
}
