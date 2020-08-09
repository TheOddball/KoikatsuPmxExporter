using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000027 RID: 39
	public class PmxSoftBody : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0001165C File Offset: 0x0000F85C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.SoftBody;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00011670 File Offset: 0x0000F870
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00011678 File Offset: 0x0000F878
		public string Name { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00011681 File Offset: 0x0000F881
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00011689 File Offset: 0x0000F889
		public string NameE { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00011692 File Offset: 0x0000F892
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0001169A File Offset: 0x0000F89A
		public PmxMaterial RefMaterial { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000116A4 File Offset: 0x0000F8A4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public bool IsGenerateBendingLinks
		{
			get
			{
				return (this.Flags & PmxSoftBody.SoftBodyFlags.GenerateBendingLinks) > (PmxSoftBody.SoftBodyFlags)0;
			}
			set
			{
				if (value)
				{
					this.Flags |= PmxSoftBody.SoftBodyFlags.GenerateBendingLinks;
				}
				else
				{
					this.Flags &= ~PmxSoftBody.SoftBodyFlags.GenerateBendingLinks;
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000116FC File Offset: 0x0000F8FC
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0001171C File Offset: 0x0000F91C
		public bool IsGenerateClusters
		{
			get
			{
				return (this.Flags & PmxSoftBody.SoftBodyFlags.GenerateClusters) > (PmxSoftBody.SoftBodyFlags)0;
			}
			set
			{
				if (value)
				{
					this.Flags |= PmxSoftBody.SoftBodyFlags.GenerateClusters;
				}
				else
				{
					this.Flags &= ~PmxSoftBody.SoftBodyFlags.GenerateClusters;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00011754 File Offset: 0x0000F954
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00011774 File Offset: 0x0000F974
		public bool IsRandomizeConstraints
		{
			get
			{
				return (this.Flags & PmxSoftBody.SoftBodyFlags.RandomizeConstraints) > (PmxSoftBody.SoftBodyFlags)0;
			}
			set
			{
				if (value)
				{
					this.Flags |= PmxSoftBody.SoftBodyFlags.RandomizeConstraints;
				}
				else
				{
					this.Flags &= ~PmxSoftBody.SoftBodyFlags.RandomizeConstraints;
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000117AA File Offset: 0x0000F9AA
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000117B2 File Offset: 0x0000F9B2
		public List<PmxSoftBody.BodyAnchor> BodyAnchorList { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000117BB File Offset: 0x0000F9BB
		// (set) Token: 0x0600021C RID: 540 RVA: 0x000117C3 File Offset: 0x0000F9C3
		public List<PmxSoftBody.VertexPin> VertexPinList { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000117CC File Offset: 0x0000F9CC
		// (set) Token: 0x0600021E RID: 542 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public int[] VertexIndices { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000117E0 File Offset: 0x0000F9E0
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000117F8 File Offset: 0x0000F9F8
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

		// Token: 0x06000221 RID: 545 RVA: 0x00011804 File Offset: 0x0000FA04
		public void NormalizeBodyAnchorList()
		{
			bool flag = this.BodyAnchorList.Count > 0;
			if (flag)
			{
				List<int> list = new List<int>(this.BodyAnchorList.Count);
				Dictionary<string, int> dictionary = new Dictionary<string, int>(this.BodyAnchorList.Count);
				for (int i = 0; i < this.BodyAnchorList.Count; i++)
				{
					PmxSoftBody.BodyAnchor bodyAnchor = this.BodyAnchorList[i];
					string key = bodyAnchor.Body.ToString() + "_" + bodyAnchor.Vertex.ToString();
					bool flag2 = !dictionary.ContainsKey(key);
					if (flag2)
					{
						dictionary.Add(key, i);
					}
					else
					{
						list.Add(i);
					}
				}
				bool flag3 = list.Count > 0;
				if (flag3)
				{
					int[] array = CP.SortIndexForRemove(list.ToArray());
					for (int j = 0; j < array.Length; j++)
					{
						this.BodyAnchorList.RemoveAt(array[j]);
					}
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00011910 File Offset: 0x0000FB10
		public void SetVertexPinFromText(string text)
		{
			this.VertexPinList.Clear();
			string[] array = text.Split(new char[]
			{
				','
			});
			bool flag = array != null;
			if (flag)
			{
				this.VertexPinList.Capacity = array.Length;
				for (int i = 0; i < array.Length; i++)
				{
					int vertex;
					bool flag2 = !string.IsNullOrEmpty(array[i]) && int.TryParse(array[i].Trim(), out vertex);
					if (flag2)
					{
						this.VertexPinList.Add(new PmxSoftBody.VertexPin
						{
							Vertex = vertex
						});
					}
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public void SortVertexPinList()
		{
			bool flag = this.VertexPinList.Count > 0;
			if (flag)
			{
				List<int> list = new List<int>(this.VertexPinList.Count);
				for (int i = 0; i < this.VertexPinList.Count; i++)
				{
					list.Add(this.VertexPinList[i].Vertex);
				}
				list.Sort();
				for (int j = 0; j < this.VertexPinList.Count; j++)
				{
					PmxSoftBody.VertexPin vertexPin = this.VertexPinList[j];
					vertexPin.Vertex = list[j];
					vertexPin.NodeIndex = -1;
					vertexPin.RefVertex = null;
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public void NormalizeVertexPinList()
		{
			bool flag = this.VertexPinList.Count > 0;
			if (flag)
			{
				this.SortVertexPinList();
				bool[] array = new bool[this.VertexPinList.Count];
				array[0] = false;
				for (int i = 1; i < this.VertexPinList.Count; i++)
				{
					PmxSoftBody.VertexPin vertexPin = this.VertexPinList[i - 1];
					PmxSoftBody.VertexPin vertexPin2 = this.VertexPinList[i];
					bool flag2 = vertexPin.Vertex == vertexPin2.Vertex;
					if (flag2)
					{
						array[i] = true;
					}
				}
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (PmxSoftBody.BodyAnchor bodyAnchor in this.BodyAnchorList)
				{
					dictionary.Add(bodyAnchor.Vertex, 0);
				}
				for (int j = 0; j < this.VertexPinList.Count; j++)
				{
					int vertex = this.VertexPinList[j].Vertex;
					bool flag3 = dictionary.ContainsKey(vertex);
					if (flag3)
					{
						array[j] = true;
					}
				}
				for (int k = array.Length - 1; k > 0; k--)
				{
					bool flag4 = array[k];
					if (flag4)
					{
						this.VertexPinList.RemoveAt(k);
					}
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		public PmxSoftBody()
		{
			this.Name = "";
			this.NameE = "";
			this.Shape = PmxSoftBody.ShapeKind.TriMesh;
			this.Material = -1;
			this.Group = 0;
			this.PassGroup = new PmxBodyPassGroup();
			this.InitializeParameter();
			this.BodyAnchorList = new List<PmxSoftBody.BodyAnchor>();
			this.VertexPinList = new List<PmxSoftBody.VertexPin>();
			this.VertexIndices = new int[0];
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00011C59 File Offset: 0x0000FE59
		public PmxSoftBody(PmxSoftBody sbody, bool nonStr)
		{
			this.FromPmxSoftBody(sbody, nonStr);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public void InitializeParameter()
		{
			this.ClearGenerate();
			this.TotalMass = 1f;
			this.Margin = 0.05f;
			this.Config.Clear();
			this.MaterialConfig.Clear();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		public void ClearGenerate()
		{
			this.IsGenerateBendingLinks = true;
			this.IsGenerateClusters = false;
			this.IsRandomizeConstraints = true;
			this.BendingLinkDistance = 2;
			this.ClusterCount = 0;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public void FromPmxSoftBody(PmxSoftBody sbody, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = sbody.Name;
				this.NameE = sbody.NameE;
			}
			this.Shape = sbody.Shape;
			this.Material = sbody.Material;
			this.Group = sbody.Group;
			this.PassGroup = sbody.PassGroup.Clone();
			this.IsGenerateBendingLinks = sbody.IsGenerateBendingLinks;
			this.IsGenerateClusters = sbody.IsGenerateClusters;
			this.IsRandomizeConstraints = sbody.IsRandomizeConstraints;
			this.BendingLinkDistance = sbody.BendingLinkDistance;
			this.ClusterCount = sbody.ClusterCount;
			this.TotalMass = sbody.TotalMass;
			this.Margin = sbody.Margin;
			this.Config = sbody.Config;
			this.MaterialConfig = sbody.MaterialConfig;
			this.BodyAnchorList = CP.CloneList<PmxSoftBody.BodyAnchor>(sbody.BodyAnchorList);
			this.VertexPinList = CP.CloneList<PmxSoftBody.VertexPin>(sbody.VertexPinList);
			this.VertexIndices = CP.CloneArray_ValueType<int>(sbody.VertexIndices);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00011DDC File Offset: 0x0000FFDC
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Shape = (PmxSoftBody.ShapeKind)PmxStreamHelper.ReadElement_Int32(s, 1, true);
			this.Material = PmxStreamHelper.ReadElement_Int32(s, f.MaterialSize, true);
			this.Group = PmxStreamHelper.ReadElement_Int32(s, 1, true);
			ushort bits = (ushort)PmxStreamHelper.ReadElement_Int32(s, 2, false);
			this.PassGroup.FromFlagBits(bits);
			this.Flags = (PmxSoftBody.SoftBodyFlags)PmxStreamHelper.ReadElement_Int32(s, 1, true);
			this.BendingLinkDistance = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.ClusterCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.TotalMass = PmxStreamHelper.ReadElement_Float(s);
			this.Margin = PmxStreamHelper.ReadElement_Float(s);
			this.Config.AeroModel = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Config.VCF = PmxStreamHelper.ReadElement_Float(s);
			this.Config.DP = PmxStreamHelper.ReadElement_Float(s);
			this.Config.DG = PmxStreamHelper.ReadElement_Float(s);
			this.Config.LF = PmxStreamHelper.ReadElement_Float(s);
			this.Config.PR = PmxStreamHelper.ReadElement_Float(s);
			this.Config.VC = PmxStreamHelper.ReadElement_Float(s);
			this.Config.DF = PmxStreamHelper.ReadElement_Float(s);
			this.Config.MT = PmxStreamHelper.ReadElement_Float(s);
			this.Config.CHR = PmxStreamHelper.ReadElement_Float(s);
			this.Config.KHR = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SHR = PmxStreamHelper.ReadElement_Float(s);
			this.Config.AHR = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SRHR_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SKHR_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SSHR_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SR_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SK_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.SS_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
			this.Config.V_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Config.P_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Config.D_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.Config.C_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.MaterialConfig.LST = PmxStreamHelper.ReadElement_Float(s);
			this.MaterialConfig.AST = PmxStreamHelper.ReadElement_Float(s);
			this.MaterialConfig.VST = PmxStreamHelper.ReadElement_Float(s);
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.BodyAnchorList.Clear();
			this.BodyAnchorList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				int body = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
				int vertex = PmxStreamHelper.ReadElement_Int32(s, f.VertexSize, true);
				int num2 = PmxStreamHelper.ReadElement_Int32(s, 1, true);
				this.BodyAnchorList.Add(new PmxSoftBody.BodyAnchor
				{
					Body = body,
					Vertex = vertex,
					IsNear = (num2 != 0)
				});
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.VertexPinList.Clear();
			this.VertexPinList.Capacity = num;
			for (int j = 0; j < num; j++)
			{
				int vertex2 = PmxStreamHelper.ReadElement_Int32(s, f.VertexSize, true);
				this.VertexPinList.Add(new PmxSoftBody.VertexPin
				{
					Vertex = vertex2
				});
			}
			this.NormalizeBodyAnchorList();
			this.NormalizeVertexPinList();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0001214C File Offset: 0x0001034C
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			PmxStreamHelper.WriteElement_Int32(s, (int)this.Shape, 1, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Material, f.MaterialSize, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Group, 1, true);
			PmxStreamHelper.WriteElement_Int32(s, (int)this.PassGroup.ToFlagBits(), 2, false);
			PmxStreamHelper.WriteElement_Int32(s, (int)this.Flags, 1, false);
			PmxStreamHelper.WriteElement_Int32(s, this.BendingLinkDistance, 4, true);
			PmxStreamHelper.WriteElement_Int32(s, this.ClusterCount, 4, true);
			PmxStreamHelper.WriteElement_Float(s, this.TotalMass);
			PmxStreamHelper.WriteElement_Float(s, this.Margin);
			PmxStreamHelper.WriteElement_Int32(s, this.Config.AeroModel, 4, true);
			PmxStreamHelper.WriteElement_Float(s, this.Config.VCF);
			PmxStreamHelper.WriteElement_Float(s, this.Config.DP);
			PmxStreamHelper.WriteElement_Float(s, this.Config.DG);
			PmxStreamHelper.WriteElement_Float(s, this.Config.LF);
			PmxStreamHelper.WriteElement_Float(s, this.Config.PR);
			PmxStreamHelper.WriteElement_Float(s, this.Config.VC);
			PmxStreamHelper.WriteElement_Float(s, this.Config.DF);
			PmxStreamHelper.WriteElement_Float(s, this.Config.MT);
			PmxStreamHelper.WriteElement_Float(s, this.Config.CHR);
			PmxStreamHelper.WriteElement_Float(s, this.Config.KHR);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SHR);
			PmxStreamHelper.WriteElement_Float(s, this.Config.AHR);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SRHR_CL);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SKHR_CL);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SSHR_CL);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SR_SPLT_CL);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SK_SPLT_CL);
			PmxStreamHelper.WriteElement_Float(s, this.Config.SS_SPLT_CL);
			PmxStreamHelper.WriteElement_Int32(s, this.Config.V_IT, 4, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Config.P_IT, 4, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Config.D_IT, 4, true);
			PmxStreamHelper.WriteElement_Int32(s, this.Config.C_IT, 4, true);
			PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.LST);
			PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.AST);
			PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.VST);
			PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList.Count, 4, true);
			for (int i = 0; i < this.BodyAnchorList.Count; i++)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[i].Body, f.BodySize, true);
				PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[i].Vertex, f.VertexSize, false);
				PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[i].IsNear ? 1 : 0, 1, true);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.VertexPinList.Count, 4, true);
			for (int j = 0; j < this.VertexPinList.Count; j++)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.VertexPinList[j].Vertex, f.VertexSize, false);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000124C0 File Offset: 0x000106C0
		object ICloneable.Clone()
		{
			return new PmxSoftBody(this, false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000124DC File Offset: 0x000106DC
		public PmxSoftBody Clone()
		{
			return new PmxSoftBody(this, false);
		}

		// Token: 0x04000113 RID: 275
		public PmxSoftBody.ShapeKind Shape;

		// Token: 0x04000114 RID: 276
		public int Material;

		// Token: 0x04000115 RID: 277
		public int Group;

		// Token: 0x04000116 RID: 278
		public PmxBodyPassGroup PassGroup;

		// Token: 0x04000117 RID: 279
		public PmxSoftBody.SoftBodyFlags Flags;

		// Token: 0x04000118 RID: 280
		public int BendingLinkDistance;

		// Token: 0x04000119 RID: 281
		public int ClusterCount;

		// Token: 0x0400011A RID: 282
		public float TotalMass;

		// Token: 0x0400011B RID: 283
		public float Margin;

		// Token: 0x0400011C RID: 284
		public PmxSoftBody.SoftBodyConfig Config;

		// Token: 0x0400011D RID: 285
		public PmxSoftBody.SoftBodyMaterialConfig MaterialConfig;

		// Token: 0x0200005D RID: 93
		public enum ShapeKind
		{
			// Token: 0x04000216 RID: 534
			TriMesh,
			// Token: 0x04000217 RID: 535
			Rope
		}

		// Token: 0x0200005E RID: 94
		[Flags]
		public enum SoftBodyFlags
		{
			// Token: 0x04000219 RID: 537
			GenerateBendingLinks = 1,
			// Token: 0x0400021A RID: 538
			GenerateClusters = 2,
			// Token: 0x0400021B RID: 539
			RandomizeConstraints = 4
		}

		// Token: 0x0200005F RID: 95
		public struct SoftBodyConfig
		{
			// Token: 0x060003BC RID: 956 RVA: 0x00019EBC File Offset: 0x000180BC
			public void Clear()
			{
				this.AeroModel = 0;
				this.VCF = 1f;
				this.DP = 0f;
				this.DG = 0f;
				this.LF = 0f;
				this.PR = 0f;
				this.VC = 0f;
				this.DF = 0.2f;
				this.MT = 0f;
				this.CHR = 1f;
				this.KHR = 0.1f;
				this.SHR = 1f;
				this.AHR = 0.7f;
				this.SRHR_CL = 0.1f;
				this.SKHR_CL = 1f;
				this.SSHR_CL = 0.5f;
				this.SR_SPLT_CL = 0.5f;
				this.SK_SPLT_CL = 0.5f;
				this.SS_SPLT_CL = 0.5f;
				this.V_IT = 0;
				this.P_IT = 1;
				this.D_IT = 0;
				this.C_IT = 4;
			}

			// Token: 0x0400021C RID: 540
			public int AeroModel;

			// Token: 0x0400021D RID: 541
			public float VCF;

			// Token: 0x0400021E RID: 542
			public float DP;

			// Token: 0x0400021F RID: 543
			public float DG;

			// Token: 0x04000220 RID: 544
			public float LF;

			// Token: 0x04000221 RID: 545
			public float PR;

			// Token: 0x04000222 RID: 546
			public float VC;

			// Token: 0x04000223 RID: 547
			public float DF;

			// Token: 0x04000224 RID: 548
			public float MT;

			// Token: 0x04000225 RID: 549
			public float CHR;

			// Token: 0x04000226 RID: 550
			public float KHR;

			// Token: 0x04000227 RID: 551
			public float SHR;

			// Token: 0x04000228 RID: 552
			public float AHR;

			// Token: 0x04000229 RID: 553
			public float SRHR_CL;

			// Token: 0x0400022A RID: 554
			public float SKHR_CL;

			// Token: 0x0400022B RID: 555
			public float SSHR_CL;

			// Token: 0x0400022C RID: 556
			public float SR_SPLT_CL;

			// Token: 0x0400022D RID: 557
			public float SK_SPLT_CL;

			// Token: 0x0400022E RID: 558
			public float SS_SPLT_CL;

			// Token: 0x0400022F RID: 559
			public int V_IT;

			// Token: 0x04000230 RID: 560
			public int P_IT;

			// Token: 0x04000231 RID: 561
			public int D_IT;

			// Token: 0x04000232 RID: 562
			public int C_IT;
		}

		// Token: 0x02000060 RID: 96
		public struct SoftBodyMaterialConfig
		{
			// Token: 0x060003BD RID: 957 RVA: 0x00019FB3 File Offset: 0x000181B3
			public void Clear()
			{
				this.LST = 1f;
				this.AST = 1f;
				this.VST = 1f;
			}

			// Token: 0x04000233 RID: 563
			public float LST;

			// Token: 0x04000234 RID: 564
			public float AST;

			// Token: 0x04000235 RID: 565
			public float VST;
		}

		// Token: 0x02000061 RID: 97
		public class BodyAnchor : IPmxObjectKey, ICloneable
		{
			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00019FD7 File Offset: 0x000181D7
			// (set) Token: 0x060003BF RID: 959 RVA: 0x00019FDF File Offset: 0x000181DF
			public PmxBody RefBody { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x00019FE8 File Offset: 0x000181E8
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x00019FF0 File Offset: 0x000181F0
			public PmxVertex RefVertex { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x00019FFC File Offset: 0x000181FC
			public PmxObject ObjectKey
			{
				get
				{
					return PmxObject.SoftBodyAnchor;
				}
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x0001A010 File Offset: 0x00018210
			public BodyAnchor()
			{
				this.NodeIndex = -1;
				this.IsNear = false;
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x0001A028 File Offset: 0x00018228
			public BodyAnchor(PmxSoftBody.BodyAnchor ac)
			{
				this.Body = ac.Body;
				this.Vertex = ac.Vertex;
				this.NodeIndex = ac.NodeIndex;
				this.IsNear = ac.IsNear;
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x0001A064 File Offset: 0x00018264
			public object Clone()
			{
				return new PmxSoftBody.BodyAnchor(this);
			}

			// Token: 0x04000236 RID: 566
			public int Body;

			// Token: 0x04000237 RID: 567
			public int Vertex;

			// Token: 0x04000238 RID: 568
			public int NodeIndex;

			// Token: 0x04000239 RID: 569
			public bool IsNear;
		}

		// Token: 0x02000062 RID: 98
		public class VertexPin : IPmxObjectKey, ICloneable
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001A07C File Offset: 0x0001827C
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001A084 File Offset: 0x00018284
			public PmxVertex RefVertex { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001A090 File Offset: 0x00018290
			public PmxObject ObjectKey
			{
				get
				{
					return PmxObject.SoftBodyPinVertex;
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x0001A0A4 File Offset: 0x000182A4
			public VertexPin()
			{
				this.NodeIndex = -1;
			}

			// Token: 0x060003CA RID: 970 RVA: 0x0001A0B5 File Offset: 0x000182B5
			public VertexPin(PmxSoftBody.VertexPin pin)
			{
				this.Vertex = pin.Vertex;
				this.NodeIndex = pin.NodeIndex;
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0001A0D8 File Offset: 0x000182D8
			public object Clone()
			{
				return new PmxSoftBody.VertexPin(this);
			}

			// Token: 0x0400023C RID: 572
			public int Vertex;

			// Token: 0x0400023D RID: 573
			public int NodeIndex;
		}
	}
}
