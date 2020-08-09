using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200002C RID: 44
	public class PmxVertex : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001339C File Offset: 0x0001159C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Vertex;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000133B0 File Offset: 0x000115B0
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000133E0 File Offset: 0x000115E0
		public int NWeight
		{
			get
			{
				return (int)((this.Weight[0].Value + 0.005f) * 100f);
			}
			set
			{
				this.ClearWeightValue();
				this.Weight[0].Value = (float)value * 0.01f;
				this.Weight[1].Value = 1f - this.Weight[0].Value;
				this.UpdateDeformType();
				bool flag = this.Deform == PmxVertex.DeformType.BDEF4 || this.Deform == PmxVertex.DeformType.QDEF;
				if (flag)
				{
					this.Deform = PmxVertex.DeformType.BDEF2;
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00013460 File Offset: 0x00011660
		public PmxVertex()
		{
			this.UVAMorphIndex = new int[4];
			this.Weight = new PmxVertex.BoneWeight[4];
			this.UVA = new Vector4[4];
			this.VertexMorphIndex = -1;
			this.UVMorphIndex = -1;
			for (int i = 0; i < this.UVAMorphIndex.Length; i++)
			{
				this.UVAMorphIndex[i] = -1;
			}
			this.SDEFIndex = -1;
			this.QDEFIndex = -1;
			this.SoftBodyPosIndex = -1;
			this.SoftBodyNormalIndex = -1;
			this.ClearWeight();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013521 File Offset: 0x00011721
		public void ClearWeight()
		{
			this.ClearWeightBone();
			this.ClearWeightValue();
			this.Deform = PmxVertex.DeformType.BDEF1;
			this.SDEF = false;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013540 File Offset: 0x00011740
		public void ClearWeightBone()
		{
			this.Weight[0].Bone = 0;
			for (int i = 1; i < 4; i++)
			{
				this.Weight[i].Bone = -1;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013584 File Offset: 0x00011784
		public void ClearWeightValue()
		{
			this.Weight[0].Value = 1f;
			for (int i = 1; i < 4; i++)
			{
				this.Weight[i].Value = 0f;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000135D0 File Offset: 0x000117D0
		public void NormalizeWeight(bool bdef4Sum)
		{
			for (int i = 0; i < 4; i++)
			{
				bool flag = this.Weight[i].Bone < 0;
				if (flag)
				{
					this.Weight[i].Value = 0f;
				}
			}
			bool flag2 = this.Deform == PmxVertex.DeformType.SDEF;
			if (flag2)
			{
				this.NormalizeWeightOrder_SDEF();
			}
			else
			{
				this.NormalizeWeightOrder();
			}
			this.UpdateDeformType();
			bool flag3 = this.Deform == PmxVertex.DeformType.SDEF;
			if (flag3)
			{
				this.Weight[2].Bone = -1;
				this.Weight[2].Value = 0f;
				this.Weight[3].Bone = -1;
				this.Weight[3].Value = 0f;
			}
			this.NormalizeWeightSum(bdef4Sum);
			int num = 1;
			switch (this.Deform)
			{
			case PmxVertex.DeformType.BDEF2:
			case PmxVertex.DeformType.SDEF:
				num = 2;
				break;
			case PmxVertex.DeformType.BDEF4:
			case PmxVertex.DeformType.QDEF:
				num = 4;
				break;
			}
			for (int j = 0; j < num; j++)
			{
				bool flag4 = this.Weight[j].Bone < 0;
				if (flag4)
				{
					this.Weight[j].Bone = 0;
					this.Weight[j].Value = 0f;
				}
			}
			this.UpdateDeformType();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00013748 File Offset: 0x00011948
		public void NormalizeWeightOrder()
		{
			PmxVertex.BoneWeight[] array = PmxVertex.BoneWeight.Sort(this.Weight);
			for (int i = 0; i < array.Length; i++)
			{
				this.Weight[i] = array[i];
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0001378C File Offset: 0x0001198C
		public void NormalizeWeightOrder_BDEF2()
		{
			bool flag = this.Weight[0].Value < this.Weight[1].Value;
			if (flag)
			{
				CP.Swap<PmxVertex.BoneWeight>(ref this.Weight[0], ref this.Weight[1]);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000137E4 File Offset: 0x000119E4
		public void NormalizeWeightOrder_SDEF()
		{
			bool flag = this.Weight[0].Bone > this.Weight[1].Bone;
			if (flag)
			{
				CP.Swap<PmxVertex.BoneWeight>(ref this.Weight[0], ref this.Weight[1]);
				CP.Swap<Vector3>(ref this.R0, ref this.R1);
				CP.Swap<Vector3>(ref this.RW0, ref this.RW0);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00013860 File Offset: 0x00011A60
		public void NormalizeWeightSum(bool bdef4)
		{
			bool flag = bdef4 || (this.Deform != PmxVertex.DeformType.BDEF4 && this.Deform != PmxVertex.DeformType.QDEF);
			if (flag)
			{
				float num = 0f;
				for (int i = 0; i < 4; i++)
				{
					num += this.Weight[i].Value;
				}
				bool flag2 = num != 0f && num != 1f;
				if (flag2)
				{
					float num2 = 1f / num;
					for (int j = 0; j < 4; j++)
					{
						PmxVertex.BoneWeight[] weight = this.Weight;
						int num3 = j;
						weight[num3].Value = weight[num3].Value * num2;
					}
				}
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001392C File Offset: 0x00011B2C
		public void NormalizeWeightSum_BDEF2()
		{
			float num = this.Weight[0].Value + this.Weight[1].Value;
			bool flag = num != 1f;
			if (flag)
			{
				bool flag2 = num == 0f;
				if (flag2)
				{
					this.Weight[0].Value = 1f;
					this.Weight[1].Value = 0f;
				}
				else
				{
					float num2 = 1f / num;
					PmxVertex.BoneWeight[] weight = this.Weight;
					int num3 = 0;
					weight[num3].Value = weight[num3].Value * num2;
					PmxVertex.BoneWeight[] weight2 = this.Weight;
					int num4 = 1;
					weight2[num4].Value = weight2[num4].Value * num2;
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00013A0C File Offset: 0x00011C0C
		public PmxVertex.DeformType GetDeformType()
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				bool flag = this.Weight[i].Value != 0f;
				if (flag)
				{
					num++;
				}
			}
			bool flag2 = this.SDEF && num != 1;
			PmxVertex.DeformType result;
			if (flag2)
			{
				result = PmxVertex.DeformType.SDEF;
			}
			else
			{
				bool flag3 = this.Deform == PmxVertex.DeformType.QDEF && num != 1;
				if (flag3)
				{
					result = PmxVertex.DeformType.QDEF;
				}
				else
				{
					PmxVertex.DeformType deformType = PmxVertex.DeformType.BDEF1;
					switch (num)
					{
					case 0:
					case 1:
						deformType = PmxVertex.DeformType.BDEF1;
						break;
					case 2:
						deformType = PmxVertex.DeformType.BDEF2;
						break;
					case 3:
					case 4:
						deformType = PmxVertex.DeformType.BDEF4;
						break;
					}
					result = deformType;
				}
			}
			return result;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00013AD5 File Offset: 0x00011CD5
		public void UpdateDeformType()
		{
			this.Deform = this.GetDeformType();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public void SetSDEF_RV(Vector3 r0, Vector3 r1)
		{
			this.R0 = r0;
			this.R1 = r1;
			this.CalcSDEF_RW();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00013AFC File Offset: 0x00011CFC
		public void CalcSDEF_RW()
		{
			Vector3 b = this.Weight[0].Value * this.R0 + this.Weight[1].Value * this.R1;
			this.RW0 = this.R0 - b;
			this.RW1 = this.R1 - b;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00013B6C File Offset: 0x00011D6C
		public bool NormalizeSDEF_C0(List<PmxBone> boneList)
		{
			bool flag = this.Deform != PmxVertex.DeformType.SDEF;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				int bone = this.Weight[0].Bone;
				int bone2 = this.Weight[1].Bone;
				bool flag2 = !CP.InRange<PmxBone>(boneList, bone);
				if (flag2)
				{
					result = false;
				}
				else
				{
					PmxBone pmxBone = boneList[bone];
					bool flag3 = CP.InRange<PmxBone>(boneList, bone2);
					if (flag3)
					{
						PmxBone pmxBone2 = boneList[bone2];
						Vector3 position = pmxBone.Position;
						Vector3 position2 = pmxBone2.Position;
						Vector3 vector = position2 - position;
						Vector3 vector2 = vector;
						vector2.Normalize();
						Vector3 vector3 = this.Position;
						vector3 -= position;
						float d = Vector3.Dot(vector2, vector3);
						this.C0 = vector2 * d + position;
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00013C64 File Offset: 0x00011E64
		public bool IsSDEF_EnableBone(List<PmxBone> boneList)
		{
			int bone = this.Weight[0].Bone;
			int bone2 = this.Weight[1].Bone;
			bool flag = !CP.InRange<PmxBone>(boneList, bone);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PmxBone pmxBone = boneList[bone];
				bool flag2 = CP.InRange<PmxBone>(boneList, bone2);
				if (flag2)
				{
					PmxBone pmxBone2 = boneList[bone2];
					result = (pmxBone.Parent == bone2 || pmxBone2.Parent == bone);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00013CF2 File Offset: 0x00011EF2
		public PmxVertex(PmxVertex vertex) : this()
		{
			this.FromPmxVertex(vertex);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00013D04 File Offset: 0x00011F04
		public void FromPmxVertex(PmxVertex vertex)
		{
			this.Position = vertex.Position;
			this.Normal = vertex.Normal;
			this.UV = vertex.UV;
			for (int i = 0; i < 4; i++)
			{
				this.UVA[i] = vertex.UVA[i];
			}
			for (int j = 0; j < 4; j++)
			{
				this.Weight[j] = vertex.Weight[j];
				this.Weight[j].RefBone = null;
			}
			this.EdgeScale = vertex.EdgeScale;
			this.Deform = vertex.Deform;
			this.SDEF = vertex.SDEF;
			this.C0 = vertex.C0;
			this.R0 = vertex.R0;
			this.R1 = vertex.R1;
			this.RW0 = vertex.RW0;
			this.RW1 = vertex.RW1;
			this.VertexMorphIndex = vertex.VertexMorphIndex;
			this.UVMorphIndex = vertex.UVMorphIndex;
			for (int k = 0; k < this.UVAMorphIndex.Length; k++)
			{
				this.UVAMorphIndex[k] = vertex.UVAMorphIndex[k];
			}
			this.SDEFIndex = vertex.SDEFIndex;
			this.QDEFIndex = vertex.QDEFIndex;
			this.SoftBodyPosIndex = vertex.SoftBodyPosIndex;
			this.SoftBodyNormalIndex = vertex.SoftBodyNormalIndex;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00013E78 File Offset: 0x00012078
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Position = V3_BytesConvert.FromStream(s);
			this.Normal = V3_BytesConvert.FromStream(s);
			this.UV = V2_BytesConvert.FromStream(s);
			for (int i = 0; i < f.UVACount; i++)
			{
				Vector4 vector = V4_BytesConvert.FromStream(s);
				bool flag = 0 <= i && i < this.UVA.Length;
				if (flag)
				{
					this.UVA[i] = vector;
				}
			}
			this.Deform = (PmxVertex.DeformType)s.ReadByte();
			this.SDEF = false;
			switch (this.Deform)
			{
			case PmxVertex.DeformType.BDEF1:
				this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[0].Value = 1f;
				break;
			case PmxVertex.DeformType.BDEF2:
				this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
				this.Weight[1].Value = 1f - this.Weight[0].Value;
				break;
			case PmxVertex.DeformType.BDEF4:
			case PmxVertex.DeformType.QDEF:
				this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[2].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[3].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
				this.Weight[1].Value = PmxStreamHelper.ReadElement_Float(s);
				this.Weight[2].Value = PmxStreamHelper.ReadElement_Float(s);
				this.Weight[3].Value = PmxStreamHelper.ReadElement_Float(s);
				break;
			case PmxVertex.DeformType.SDEF:
				this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
				this.Weight[1].Value = 1f - this.Weight[0].Value;
				this.C0 = V3_BytesConvert.FromStream(s);
				this.R0 = V3_BytesConvert.FromStream(s);
				this.R1 = V3_BytesConvert.FromStream(s);
				this.CalcSDEF_RW();
				this.SDEF = true;
				break;
			}
			this.EdgeScale = PmxStreamHelper.ReadElement_Float(s);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001417C File Offset: 0x0001237C
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			V3_BytesConvert.ToStream(s, this.Position);
			V3_BytesConvert.ToStream(s, this.Normal);
			V2_BytesConvert.ToStream(s, this.UV);
			for (int i = 0; i < f.UVACount; i++)
			{
				V4_BytesConvert.ToStream(s, this.UVA[i]);
			}
			bool flag = this.Deform == PmxVertex.DeformType.QDEF && f.Ver < 2.1f;
			if (flag)
			{
				s.WriteByte(2);
			}
			else
			{
				s.WriteByte((byte)this.Deform);
			}
			switch (this.Deform)
			{
			case PmxVertex.DeformType.BDEF1:
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
				break;
			case PmxVertex.DeformType.BDEF2:
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
				break;
			case PmxVertex.DeformType.BDEF4:
			case PmxVertex.DeformType.QDEF:
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[2].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[3].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[1].Value);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[2].Value);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[3].Value);
				break;
			case PmxVertex.DeformType.SDEF:
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
				PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
				V3_BytesConvert.ToStream(s, this.C0);
				V3_BytesConvert.ToStream(s, this.R0);
				V3_BytesConvert.ToStream(s, this.R1);
				break;
			}
			PmxStreamHelper.WriteElement_Float(s, this.EdgeScale);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001442C File Offset: 0x0001262C
		object ICloneable.Clone()
		{
			return new PmxVertex(this);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00014444 File Offset: 0x00012644
		public PmxVertex Clone()
		{
			return new PmxVertex(this);
		}

		// Token: 0x0400012A RID: 298
		public const int MaxUVACount = 4;

		// Token: 0x0400012B RID: 299
		public const int MaxWeightBoneCount = 4;

		// Token: 0x0400012C RID: 300
		public Vector3 Position;

		// Token: 0x0400012D RID: 301
		public Vector3 Normal;

		// Token: 0x0400012E RID: 302
		public Vector2 UV;

		// Token: 0x0400012F RID: 303
		public Vector4[] UVA;

		// Token: 0x04000130 RID: 304
		public PmxVertex.BoneWeight[] Weight;

		// Token: 0x04000131 RID: 305
		public PmxVertex.DeformType Deform;

		// Token: 0x04000132 RID: 306
		public float EdgeScale = 1f;

		// Token: 0x04000133 RID: 307
		public int VertexMorphIndex = -1;

		// Token: 0x04000134 RID: 308
		public int UVMorphIndex = -1;

		// Token: 0x04000135 RID: 309
		public int[] UVAMorphIndex;

		// Token: 0x04000136 RID: 310
		public int SDEFIndex = -1;

		// Token: 0x04000137 RID: 311
		public int QDEFIndex = -1;

		// Token: 0x04000138 RID: 312
		public int SoftBodyPosIndex = -1;

		// Token: 0x04000139 RID: 313
		public int SoftBodyNormalIndex = -1;

		// Token: 0x0400013A RID: 314
		public bool SDEF;

		// Token: 0x0400013B RID: 315
		public Vector3 C0;

		// Token: 0x0400013C RID: 316
		public Vector3 R0;

		// Token: 0x0400013D RID: 317
		public Vector3 R1;

		// Token: 0x0400013E RID: 318
		public Vector3 RW0;

		// Token: 0x0400013F RID: 319
		public Vector3 RW1;

		// Token: 0x02000063 RID: 99
		public struct BoneWeight
		{
			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060003CC RID: 972 RVA: 0x0001A0F0 File Offset: 0x000182F0
			// (set) Token: 0x060003CD RID: 973 RVA: 0x0001A0F8 File Offset: 0x000182F8
			public PmxBone RefBone { get; set; }

			// Token: 0x060003CE RID: 974 RVA: 0x0001A104 File Offset: 0x00018304
			public static PmxVertex.BoneWeight[] Sort(PmxVertex.BoneWeight[] w)
			{
				List<PmxVertex.BoneWeight> list = new List<PmxVertex.BoneWeight>(w);
				PmxVertex.BoneWeight.SortList(list);
				return list.ToArray();
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0001A12A File Offset: 0x0001832A
			public static void SortList(List<PmxVertex.BoneWeight> list)
			{
				CP.SSort<PmxVertex.BoneWeight>(list, delegate(PmxVertex.BoneWeight l, PmxVertex.BoneWeight r)
				{
					float num = Math.Abs(r.Value) - Math.Abs(l.Value);
					bool flag = num < 0f;
					int result;
					if (flag)
					{
						result = -1;
					}
					else
					{
						bool flag2 = num <= 0f;
						if (flag2)
						{
							result = 0;
						}
						else
						{
							result = 1;
						}
					}
					return result;
				});
			}

			// Token: 0x0400023F RID: 575
			public int Bone;

			// Token: 0x04000240 RID: 576
			public float Value;
		}

		// Token: 0x02000064 RID: 100
		public enum DeformType
		{
			// Token: 0x04000243 RID: 579
			BDEF1,
			// Token: 0x04000244 RID: 580
			BDEF2,
			// Token: 0x04000245 RID: 581
			BDEF4,
			// Token: 0x04000246 RID: 582
			SDEF,
			// Token: 0x04000247 RID: 583
			QDEF
		}
	}
}
