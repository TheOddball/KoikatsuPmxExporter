using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000010 RID: 16
	public class Pmx : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000C300 File Offset: 0x0000A500
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Pmx;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000C313 File Offset: 0x0000A513
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000C31B File Offset: 0x0000A51B
		public PmxHeader Header { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000C32C File Offset: 0x0000A52C
		public PmxModelInfo ModelInfo { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000C335 File Offset: 0x0000A535
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000C33D File Offset: 0x0000A53D
		public List<PmxVertex> VertexList { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000C346 File Offset: 0x0000A546
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000C34E File Offset: 0x0000A54E
		public List<int> FaceList { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000C357 File Offset: 0x0000A557
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000C35F File Offset: 0x0000A55F
		public List<PmxMaterial> MaterialList { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000C368 File Offset: 0x0000A568
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000C370 File Offset: 0x0000A570
		public List<PmxBone> BoneList { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000C379 File Offset: 0x0000A579
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000C381 File Offset: 0x0000A581
		public List<PmxMorph> MorphList { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000C38A File Offset: 0x0000A58A
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000C392 File Offset: 0x0000A592
		public List<PmxNode> NodeList { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public List<PmxBody> BodyList { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		public List<PmxJoint> JointList { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000C3BD File Offset: 0x0000A5BD
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public List<PmxSoftBody> SoftBodyList { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public PmxNode RootNode { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000C3DF File Offset: 0x0000A5DF
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		public PmxNode ExpNode { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public string FilePath { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000C401 File Offset: 0x0000A601
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000C408 File Offset: 0x0000A608
		public static PmxSaveVersion SaveVersion { get; set; } = PmxSaveVersion.AutoSelect;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000C410 File Offset: 0x0000A610
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000C417 File Offset: 0x0000A617
		public static bool AutoSelect_UVACount { get; set; } = true;

		// Token: 0x060000A6 RID: 166 RVA: 0x0000C420 File Offset: 0x0000A620
		public Pmx()
		{
			bool flag = !PmxLibClass.IsLocked();
			if (flag)
			{
				this.Header = new PmxHeader(2.1f);
				this.ModelInfo = new PmxModelInfo();
				this.VertexList = new List<PmxVertex>();
				this.FaceList = new List<int>();
				this.MaterialList = new List<PmxMaterial>();
				this.BoneList = new List<PmxBone>();
				this.MorphList = new List<PmxMorph>();
				this.NodeList = new List<PmxNode>();
				this.BodyList = new List<PmxBody>();
				this.JointList = new List<PmxJoint>();
				this.SoftBodyList = new List<PmxSoftBody>();
				this.RootNode = new PmxNode();
				this.ExpNode = new PmxNode();
				this.InitializeSystemNode();
				this.FilePath = "";
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000C50B File Offset: 0x0000A70B
		public Pmx(Pmx pmx) : this()
		{
			this.FromPmx(pmx);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000C51D File Offset: 0x0000A71D
		public Pmx(string path) : this()
		{
			this.FromFile(path);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000C530 File Offset: 0x0000A730
		public virtual void Clear()
		{
			this.Header.ElementFormat.Ver = 2.1f;
			this.Header.ElementFormat.UVACount = 0;
			this.ModelInfo.Clear();
			this.VertexList.Clear();
			this.FaceList.Clear();
			this.MaterialList.Clear();
			this.BoneList.Clear();
			this.MorphList.Clear();
			this.BodyList.Clear();
			this.JointList.Clear();
			this.SoftBodyList.Clear();
			this.InitializeSystemNode();
			this.FilePath = "";
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
		public void Initialize()
		{
			this.Clear();
			this.InitializeBone();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public void InitializeBone()
		{
			this.BoneList.Clear();
			PmxBone pmxBone = new PmxBone();
			pmxBone.Name = "センター";
			pmxBone.NameE = "center";
			pmxBone.Parent = -1;
			pmxBone.SetFlag(PmxBone.BoneFlags.Translation, true);
			this.BoneList.Add(pmxBone);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000C650 File Offset: 0x0000A850
		public void InitializeSystemNode()
		{
			this.RootNode.Name = "Root";
			this.RootNode.NameE = "Root";
			this.RootNode.SystemNode = true;
			this.RootNode.ElementList.Clear();
			this.RootNode.ElementList.Add(new PmxNode.NodeElement
			{
				ElementType = PmxNode.ElementType.Bone,
				Index = 0
			});
			this.ExpNode.Name = "表情";
			this.ExpNode.NameE = "Exp";
			this.ExpNode.SystemNode = true;
			this.ExpNode.ElementList.Clear();
			this.NodeList.Clear();
			this.NodeList.Add(this.RootNode);
			this.NodeList.Add(this.ExpNode);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000C730 File Offset: 0x0000A930
		public void UpdateSystemNode()
		{
			for (int i = 0; i < this.NodeList.Count; i++)
			{
				bool systemNode = this.NodeList[i].SystemNode;
				if (systemNode)
				{
					bool flag = this.NodeList[i].Name == "Root";
					if (flag)
					{
						this.RootNode = this.NodeList[i];
					}
					else
					{
						bool flag2 = this.NodeList[i].Name == "表情";
						if (flag2)
						{
							this.ExpNode = this.NodeList[i];
						}
					}
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public bool FromFile(string path)
		{
			bool result = false;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				try
				{
					this.FromStreamEx(fileStream, null);
					result = true;
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
			}
			this.FilePath = path;
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000C854 File Offset: 0x0000AA54
		public bool ToFile(string path)
		{
			bool result = false;
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				try
				{
					this.NormalizeVersion();
					bool autoSelect_UVACount = Pmx.AutoSelect_UVACount;
					if (autoSelect_UVACount)
					{
					}
					this.ToStreamEx(fileStream, null);
					result = true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					throw new Exception("保存中にエラーが発生しました." + ex);
				}
			}
			this.FilePath = path;
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public void FromPmx(Pmx pmx)
		{
			this.Clear();
			this.FilePath = pmx.FilePath;
			this.Header = pmx.Header.Clone();
			this.ModelInfo = pmx.ModelInfo.Clone();
			int count = pmx.VertexList.Count;
			this.VertexList.Capacity = count;
			for (int i = 0; i < count; i++)
			{
				this.VertexList.Add(pmx.VertexList[i].Clone());
			}
			count = pmx.FaceList.Count;
			this.FaceList.Capacity = count;
			for (int j = 0; j < count; j++)
			{
				this.FaceList.Add(pmx.FaceList[j]);
			}
			count = pmx.MaterialList.Count;
			this.MaterialList.Capacity = count;
			for (int k = 0; k < count; k++)
			{
				this.MaterialList.Add(pmx.MaterialList[k].Clone());
			}
			count = pmx.BoneList.Count;
			this.BoneList.Capacity = count;
			for (int l = 0; l < count; l++)
			{
				this.BoneList.Add(pmx.BoneList[l].Clone());
			}
			count = pmx.MorphList.Count;
			this.MorphList.Capacity = count;
			for (int m = 0; m < count; m++)
			{
				this.MorphList.Add(pmx.MorphList[m].Clone());
			}
			count = pmx.NodeList.Count;
			this.NodeList.Clear();
			this.NodeList.Capacity = count;
			for (int n = 0; n < count; n++)
			{
				this.NodeList.Add(pmx.NodeList[n].Clone());
				bool systemNode = this.NodeList[n].SystemNode;
				if (systemNode)
				{
					bool flag = this.NodeList[n].Name == "Root";
					if (flag)
					{
						this.RootNode = this.NodeList[n];
					}
					else
					{
						bool flag2 = this.NodeList[n].Name == "表情";
						if (flag2)
						{
							this.ExpNode = this.NodeList[n];
						}
					}
				}
			}
			count = pmx.BodyList.Count;
			this.BodyList.Capacity = count;
			for (int num = 0; num < count; num++)
			{
				this.BodyList.Add(pmx.BodyList[num].Clone());
			}
			count = pmx.JointList.Count;
			this.JointList.Capacity = count;
			for (int num2 = 0; num2 < count; num2++)
			{
				this.JointList.Add(pmx.JointList[num2].Clone());
			}
			count = pmx.SoftBodyList.Count;
			this.SoftBodyList.Capacity = count;
			for (int num3 = 0; num3 < count; num3++)
			{
				this.SoftBodyList.Add(pmx.SoftBodyList[num3].Clone());
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public virtual void FromStreamEx(Stream s, PmxElementFormat f)
		{
			PmxHeader pmxHeader = new PmxHeader(2.1f);
			pmxHeader.FromStreamEx(s, null);
			this.Header.FromHeader(pmxHeader);
			this.ModelInfo.FromStreamEx(s, pmxHeader.ElementFormat);
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.VertexList.Clear();
			this.VertexList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				PmxVertex pmxVertex = new PmxVertex();
				pmxVertex.FromStreamEx(s, pmxHeader.ElementFormat);
				this.VertexList.Add(pmxVertex);
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.FaceList.Clear();
			this.FaceList.Capacity = num;
			for (int j = 0; j < num; j++)
			{
				int item = PmxStreamHelper.ReadElement_Int32(s, pmxHeader.ElementFormat.VertexSize, false);
				this.FaceList.Add(item);
			}
			PmxTextureTable pmxTextureTable = new PmxTextureTable();
			pmxTextureTable.FromStreamEx(s, pmxHeader.ElementFormat);
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.MaterialList.Clear();
			this.MaterialList.Capacity = num;
			for (int k = 0; k < num; k++)
			{
				PmxMaterial pmxMaterial = new PmxMaterial();
				pmxMaterial.FromStreamEx_TexTable(s, pmxTextureTable, pmxHeader.ElementFormat);
				this.MaterialList.Add(pmxMaterial);
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.BoneList.Clear();
			this.BoneList.Capacity = num;
			for (int l = 0; l < num; l++)
			{
				PmxBone pmxBone = new PmxBone();
				pmxBone.FromStreamEx(s, pmxHeader.ElementFormat);
				this.BoneList.Add(pmxBone);
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.MorphList.Clear();
			this.MorphList.Capacity = num;
			for (int m = 0; m < num; m++)
			{
				PmxMorph pmxMorph = new PmxMorph();
				pmxMorph.FromStreamEx(s, pmxHeader.ElementFormat);
				this.MorphList.Add(pmxMorph);
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.NodeList.Clear();
			this.NodeList.Capacity = num;
			for (int n = 0; n < num; n++)
			{
				PmxNode pmxNode = new PmxNode();
				pmxNode.FromStreamEx(s, pmxHeader.ElementFormat);
				this.NodeList.Add(pmxNode);
				bool systemNode = this.NodeList[n].SystemNode;
				if (systemNode)
				{
					bool flag = this.NodeList[n].Name == "Root";
					if (flag)
					{
						this.RootNode = this.NodeList[n];
					}
					else
					{
						bool flag2 = this.NodeList[n].Name == "表情";
						if (flag2)
						{
							this.ExpNode = this.NodeList[n];
						}
					}
				}
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.BodyList.Clear();
			this.BodyList.Capacity = num;
			for (int num2 = 0; num2 < num; num2++)
			{
				PmxBody pmxBody = new PmxBody();
				pmxBody.FromStreamEx(s, pmxHeader.ElementFormat);
				this.BodyList.Add(pmxBody);
			}
			num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.JointList.Clear();
			this.JointList.Capacity = num;
			for (int num3 = 0; num3 < num; num3++)
			{
				PmxJoint pmxJoint = new PmxJoint();
				pmxJoint.FromStreamEx(s, pmxHeader.ElementFormat);
				this.JointList.Add(pmxJoint);
			}
			bool flag3 = pmxHeader.Ver >= 2.1f;
			if (flag3)
			{
				num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
				this.SoftBodyList.Clear();
				this.SoftBodyList.Capacity = num;
				for (int num4 = 0; num4 < num; num4++)
				{
					PmxSoftBody pmxSoftBody = new PmxSoftBody();
					pmxSoftBody.FromStreamEx(s, pmxHeader.ElementFormat);
					this.SoftBodyList.Add(pmxSoftBody);
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public void UpdateElementFormatSize(PmxElementFormat f, PmxTextureTable tx)
		{
			bool flag = f == null;
			if (flag)
			{
				f = this.Header.ElementFormat;
			}
			f.VertexSize = PmxElementFormat.GetUnsignedBufSize(this.VertexList.Count);
			f.MaterialSize = PmxElementFormat.GetSignedBufSize(this.MaterialList.Count);
			f.BoneSize = PmxElementFormat.GetSignedBufSize(this.BoneList.Count);
			f.MorphSize = PmxElementFormat.GetSignedBufSize(this.MorphList.Count);
			f.BodySize = PmxElementFormat.GetSignedBufSize(this.BodyList.Count);
			bool flag2 = tx == null;
			if (flag2)
			{
				tx = new PmxTextureTable(this.MaterialList);
			}
			f.TexSize = PmxElementFormat.GetSignedBufSize(tx.Count);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000D190 File Offset: 0x0000B390
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxHeader header = this.Header;
			PmxTextureTable pmxTextureTable = new PmxTextureTable(this.MaterialList);
			this.UpdateElementFormatSize(header.ElementFormat, pmxTextureTable);
			header.ToStreamEx(s, null);
			this.ModelInfo.ToStreamEx(s, header.ElementFormat);
			PmxStreamHelper.WriteElement_Int32(s, this.VertexList.Count, 4, true);
			for (int i = 0; i < this.VertexList.Count; i++)
			{
				this.VertexList[i].ToStreamEx(s, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.FaceList.Count, 4, true);
			for (int j = 0; j < this.FaceList.Count; j++)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.FaceList[j], header.ElementFormat.VertexSize, false);
			}
			pmxTextureTable.ToStreamEx(s, header.ElementFormat);
			PmxStreamHelper.WriteElement_Int32(s, this.MaterialList.Count, 4, true);
			for (int k = 0; k < this.MaterialList.Count; k++)
			{
				this.MaterialList[k].ToStreamEx_TexTable(s, pmxTextureTable, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.BoneList.Count, 4, true);
			for (int l = 0; l < this.BoneList.Count; l++)
			{
				this.BoneList[l].ToStreamEx(s, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.MorphList.Count, 4, true);
			for (int m = 0; m < this.MorphList.Count; m++)
			{
				this.MorphList[m].ToStreamEx(s, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.NodeList.Count, 4, true);
			for (int n = 0; n < this.NodeList.Count; n++)
			{
				this.NodeList[n].ToStreamEx(s, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.BodyList.Count, 4, true);
			for (int num = 0; num < this.BodyList.Count; num++)
			{
				this.BodyList[num].ToStreamEx(s, header.ElementFormat);
			}
			PmxStreamHelper.WriteElement_Int32(s, this.JointList.Count, 4, true);
			for (int num2 = 0; num2 < this.JointList.Count; num2++)
			{
				this.JointList[num2].ToStreamEx(s, header.ElementFormat);
			}
			bool flag = header.Ver >= 2.1f;
			if (flag)
			{
				PmxStreamHelper.WriteElement_Int32(s, this.SoftBodyList.Count, 4, true);
				for (int num3 = 0; num3 < this.SoftBodyList.Count; num3++)
				{
					this.SoftBodyList[num3].ToStreamEx(s, header.ElementFormat);
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public void ClearMaterialNames()
		{
			for (int i = 0; i < this.MaterialList.Count; i++)
			{
				this.MaterialList[i].Name = "材質" + (i + 1).ToString();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000D528 File Offset: 0x0000B728
		public static void UpdateBoneIKKind(List<PmxBone> boneList)
		{
			for (int i = 0; i < boneList.Count; i++)
			{
				PmxBone pmxBone = boneList[i];
				pmxBone.IKKind = PmxBone.IKKindType.None;
			}
			for (int j = 0; j < boneList.Count; j++)
			{
				PmxBone pmxBone2 = boneList[j];
				bool flag = pmxBone2.GetFlag(PmxBone.BoneFlags.IK);
				if (flag)
				{
					pmxBone2.IKKind = PmxBone.IKKindType.IK;
					int target = pmxBone2.IK.Target;
					bool flag2 = CP.InRange<PmxBone>(boneList, target);
					if (flag2)
					{
						boneList[target].IKKind = PmxBone.IKKindType.Target;
					}
					for (int k = 0; k < pmxBone2.IK.LinkList.Count; k++)
					{
						int bone = pmxBone2.IK.LinkList[k].Bone;
						bool flag3 = CP.InRange<PmxBone>(boneList, bone);
						if (flag3)
						{
							boneList[bone].IKKind = PmxBone.IKKindType.Link;
						}
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000D630 File Offset: 0x0000B830
		public void UpdateBoneIKKind()
		{
			Pmx.UpdateBoneIKKind(this.BoneList);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000D640 File Offset: 0x0000B840
		public void NormalizeVertex_SDEF_C0()
		{
			for (int i = 0; i < this.VertexList.Count; i++)
			{
				this.VertexList[i].NormalizeSDEF_C0(this.BoneList);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000D684 File Offset: 0x0000B884
		public float RequireVersion(out bool isQDEF, out bool isExMorph, out bool isExJoint, out bool isSoftBody)
		{
			Func<bool> func = delegate()
			{
				bool result2 = false;
				for (int i = 0; i < this.VertexList.Count; i++)
				{
					PmxVertex pmxVertex = this.VertexList[i];
					bool flag2 = pmxVertex.Deform == PmxVertex.DeformType.QDEF;
					if (flag2)
					{
						result2 = true;
						break;
					}
				}
				return result2;
			};
			Func<bool> func2 = delegate()
			{
				bool result2 = false;
				for (int i = 0; i < this.MorphList.Count; i++)
				{
					PmxMorph pmxMorph = this.MorphList[i];
					bool flag2 = pmxMorph.IsFlip || pmxMorph.IsImpulse;
					if (flag2)
					{
						result2 = true;
						break;
					}
				}
				return result2;
			};
			Func<bool> func3 = delegate()
			{
				bool result2 = false;
				for (int i = 0; i < this.JointList.Count; i++)
				{
					PmxJoint pmxJoint = this.JointList[i];
					bool flag2 = pmxJoint.Kind > PmxJoint.JointKind.Sp6DOF;
					if (flag2)
					{
						result2 = true;
						break;
					}
				}
				return result2;
			};
			Func<bool> func4 = () => this.SoftBodyList.Count > 0;
			isQDEF = func();
			isExMorph = func2();
			isExJoint = func3();
			isSoftBody = func4();
			float result = 2f;
			bool flag = isQDEF | isExMorph | isExJoint | isSoftBody;
			if (flag)
			{
				result = 2.1f;
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000D714 File Offset: 0x0000B914
		private void NormalizeVersion()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			float ver = 2f;
			switch (Pmx.SaveVersion)
			{
			case PmxSaveVersion.AutoSelect:
				this.Header.Ver = ver;
				break;
			case PmxSaveVersion.PMX2_0:
			{
				string text = "";
				bool flag5 = flag;
				if (flag5)
				{
					text = text + "頂点ウェイト : QDEF -> BDEF4" + Environment.NewLine;
				}
				bool flag6 = flag2;
				if (flag6)
				{
					text = text + "モーフ : インパルス->削除／フリップ->グループ" + Environment.NewLine;
				}
				bool flag7 = flag3;
				if (flag7)
				{
					text = text + "Joint : 拡張Joint -> 基本Joint(ﾊﾞﾈ付6DOF)" + Environment.NewLine;
				}
				bool flag8 = flag4;
				if (flag8)
				{
					text = text + "SoftBody : 削除" + Environment.NewLine;
				}
				this.Header.Ver = 2f;
				bool flag9 = text.Length > 0;
				if (flag9)
				{
					text = "PMX2.0での保存では 以下の項目が書き換えられますが よろしいですか?" + Environment.NewLine + Environment.NewLine + text;
				}
				break;
			}
			case PmxSaveVersion.PMX2_1:
				this.Header.Ver = 2.1f;
				break;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000D830 File Offset: 0x0000BA30
		public void NormalizeUVACount()
		{
			bool flag = this.VertexList.Count <= 0;
			if (flag)
			{
				this.Header.ElementFormat.UVACount = 0;
			}
			else
			{
				Func<Vector4, bool> func = (Vector4 v) => Math.Abs(v.x) > 1E-12f || Math.Abs(v.y) > 1E-12f || Math.Abs(v.z) > 1E-12f || Math.Abs(v.w) > 1E-12f;
				int num = 0;
				foreach (PmxVertex pmxVertex in this.VertexList)
				{
					for (int i = 0; i < pmxVertex.UVA.Length; i++)
					{
						bool flag2 = func(pmxVertex.UVA[i]);
						if (flag2)
						{
							int num2 = i + 1;
							bool flag3 = num < num2;
							if (flag3)
							{
								num = num2;
							}
						}
					}
				}
				this.Header.ElementFormat.UVACount = num;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000D938 File Offset: 0x0000BB38
		object ICloneable.Clone()
		{
			return new Pmx(this);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000D950 File Offset: 0x0000BB50
		public virtual Pmx Clone()
		{
			return new Pmx(this);
		}

		// Token: 0x04000049 RID: 73
		public const string RootNodeName = "Root";

		// Token: 0x0400004A RID: 74
		public const string ExpNodeName = "表情";
	}
}
