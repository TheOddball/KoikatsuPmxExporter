using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200001A RID: 26
	public class PmxIK : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.IK;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public PmxBone RefTarget { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000F2E9 File Offset: 0x0000D4E9
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000F2F1 File Offset: 0x0000D4F1
		public List<PmxIK.IKLink> LinkList { get; private set; }

		// Token: 0x0600015F RID: 351 RVA: 0x0000F2FA File Offset: 0x0000D4FA
		public PmxIK()
		{
			this.Target = -1;
			this.LoopCount = 0;
			this.Angle = 1f;
			this.LinkList = new List<PmxIK.IKLink>();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000F329 File Offset: 0x0000D529
		public PmxIK(PmxIK ik)
		{
			this.FromPmxIK(ik);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000F33C File Offset: 0x0000D53C
		public void FromPmxIK(PmxIK ik)
		{
			this.Target = ik.Target;
			this.LoopCount = ik.LoopCount;
			this.Angle = ik.Angle;
			this.LinkList = new List<PmxIK.IKLink>();
			for (int i = 0; i < ik.LinkList.Count; i++)
			{
				this.LinkList.Add(ik.LinkList[i].Clone());
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Target = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
			this.LoopCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Angle = PmxStreamHelper.ReadElement_Float(s);
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.LinkList.Clear();
			this.LinkList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				PmxIK.IKLink iklink = new PmxIK.IKLink();
				iklink.FromStreamEx(s, f);
				this.LinkList.Add(iklink);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000F440 File Offset: 0x0000D640
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Target, f.BoneSize, true);
			PmxStreamHelper.WriteElement_Int32(s, this.LoopCount, 4, true);
			PmxStreamHelper.WriteElement_Float(s, this.Angle);
			PmxStreamHelper.WriteElement_Int32(s, this.LinkList.Count, 4, true);
			for (int i = 0; i < this.LinkList.Count; i++)
			{
				this.LinkList[i].ToStreamEx(s, f);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		object ICloneable.Clone()
		{
			return new PmxIK(this);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public PmxIK Clone()
		{
			return new PmxIK(this);
		}

		// Token: 0x0400009C RID: 156
		public int Target;

		// Token: 0x0400009D RID: 157
		public int LoopCount;

		// Token: 0x0400009E RID: 158
		public float Angle;

		// Token: 0x02000052 RID: 82
		public class IKLink : IPmxObjectKey, IPmxStreamIO, ICloneable
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600039A RID: 922 RVA: 0x00019370 File Offset: 0x00017570
			PmxObject IPmxObjectKey.ObjectKey
			{
				get
				{
					return PmxObject.IKLink;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600039B RID: 923 RVA: 0x00019384 File Offset: 0x00017584
			// (set) Token: 0x0600039C RID: 924 RVA: 0x0001938C File Offset: 0x0001758C
			public PmxBone RefBone { get; set; }

			// Token: 0x0600039D RID: 925 RVA: 0x00019395 File Offset: 0x00017595
			public IKLink()
			{
				this.Bone = -1;
				this.IsLimit = false;
			}

			// Token: 0x0600039E RID: 926 RVA: 0x000193AD File Offset: 0x000175AD
			public IKLink(PmxIK.IKLink link)
			{
				this.FromIKLink(link);
			}

			// Token: 0x0600039F RID: 927 RVA: 0x000193BF File Offset: 0x000175BF
			public void FromIKLink(PmxIK.IKLink link)
			{
				this.Bone = link.Bone;
				this.IsLimit = link.IsLimit;
				this.Low = link.Low;
				this.High = link.High;
				this.Euler = link.Euler;
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x00019400 File Offset: 0x00017600
			public void NormalizeAngle()
			{
				Vector3 low = default(Vector3);
				this.Low.x = Math.Min(this.Low.x, this.High.x);
				Vector3 high = default(Vector3);
				this.High.x = Math.Max(this.Low.x, this.High.x);
				this.Low.y = Math.Min(this.Low.y, this.High.y);
				this.High.y = Math.Max(this.Low.y, this.High.y);
				this.Low.z = Math.Min(this.Low.z, this.High.z);
				this.High.z = Math.Max(this.Low.z, this.High.z);
				this.Low = low;
				this.High = high;
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00019510 File Offset: 0x00017710
			public void NormalizeEulerAxis()
			{
				bool flag = -1.57079637f < this.Low.x && this.High.x < 1.57079637f;
				if (flag)
				{
					this.Euler = PmxIK.IKLink.EulerType.ZXY;
				}
				else
				{
					bool flag2 = -1.57079637f < this.Low.y && this.High.y < 1.57079637f;
					if (flag2)
					{
						this.Euler = PmxIK.IKLink.EulerType.XYZ;
					}
					else
					{
						this.Euler = PmxIK.IKLink.EulerType.YZX;
					}
				}
				this.FixAxis = PmxIK.IKLink.FixAxisType.None;
				bool flag3 = this.Low.x == 0f && this.High.x == 0f && this.Low.y == 0f && this.High.y == 0f && this.Low.z == 0f && this.High.z == 0f;
				if (flag3)
				{
					this.FixAxis = PmxIK.IKLink.FixAxisType.Fix;
				}
				else
				{
					bool flag4 = this.Low.y == 0f && this.High.y == 0f && this.Low.z == 0f && this.High.z == 0f;
					if (flag4)
					{
						this.FixAxis = PmxIK.IKLink.FixAxisType.X;
					}
					else
					{
						bool flag5 = this.Low.x == 0f && this.High.x == 0f && this.Low.z == 0f && this.High.z == 0f;
						if (flag5)
						{
							this.FixAxis = PmxIK.IKLink.FixAxisType.Y;
						}
						else
						{
							bool flag6 = this.Low.x == 0f && this.High.x == 0f && this.Low.y == 0f && this.High.y == 0f;
							if (flag6)
							{
								this.FixAxis = PmxIK.IKLink.FixAxisType.Z;
							}
						}
					}
				}
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00019738 File Offset: 0x00017938
			public void FromStreamEx(Stream s, PmxElementFormat f)
			{
				this.Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.IsLimit = (s.ReadByte() != 0);
				bool isLimit = this.IsLimit;
				if (isLimit)
				{
					this.Low = V3_BytesConvert.FromStream(s);
					this.High = V3_BytesConvert.FromStream(s);
				}
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x0001978C File Offset: 0x0001798C
			public void ToStreamEx(Stream s, PmxElementFormat f)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.Bone, f.BoneSize, true);
				s.WriteByte(this.IsLimit ? 1 : 0);
				bool isLimit = this.IsLimit;
				if (isLimit)
				{
					V3_BytesConvert.ToStream(s, this.Low);
					V3_BytesConvert.ToStream(s, this.High);
				}
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x000197E8 File Offset: 0x000179E8
			object ICloneable.Clone()
			{
				return new PmxIK.IKLink(this);
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x00019800 File Offset: 0x00017A00
			public PmxIK.IKLink Clone()
			{
				return new PmxIK.IKLink(this);
			}

			// Token: 0x040001CC RID: 460
			public int Bone;

			// Token: 0x040001CD RID: 461
			public bool IsLimit;

			// Token: 0x040001CE RID: 462
			public Vector3 Low;

			// Token: 0x040001CF RID: 463
			public Vector3 High;

			// Token: 0x040001D0 RID: 464
			public PmxIK.IKLink.EulerType Euler;

			// Token: 0x040001D1 RID: 465
			public PmxIK.IKLink.FixAxisType FixAxis;

			// Token: 0x0200006B RID: 107
			public enum EulerType
			{
				// Token: 0x0400025E RID: 606
				ZXY,
				// Token: 0x0400025F RID: 607
				XYZ,
				// Token: 0x04000260 RID: 608
				YZX
			}

			// Token: 0x0200006C RID: 108
			public enum FixAxisType
			{
				// Token: 0x04000262 RID: 610
				None,
				// Token: 0x04000263 RID: 611
				Fix,
				// Token: 0x04000264 RID: 612
				X,
				// Token: 0x04000265 RID: 613
				Y,
				// Token: 0x04000266 RID: 614
				Z
			}
		}
	}
}
