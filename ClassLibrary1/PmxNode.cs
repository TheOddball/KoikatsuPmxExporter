using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000023 RID: 35
	public class PmxNode : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000112A4 File Offset: 0x0000F4A4
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Node;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000112B8 File Offset: 0x0000F4B8
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public string Name { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000112C9 File Offset: 0x0000F4C9
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000112D1 File Offset: 0x0000F4D1
		public string NameE { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000112DC File Offset: 0x0000F4DC
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000112F4 File Offset: 0x0000F4F4
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

		// Token: 0x06000201 RID: 513 RVA: 0x000112FF File Offset: 0x0000F4FF
		public PmxNode()
		{
			this.Name = "";
			this.NameE = "";
			this.SystemNode = false;
			this.ElementList = new List<PmxNode.NodeElement>();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011333 File Offset: 0x0000F533
		public PmxNode(PmxNode node, bool nonStr) : this()
		{
			this.FromPmxNode(node, nonStr);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011348 File Offset: 0x0000F548
		public void FromPmxNode(PmxNode node, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = node.Name;
				this.NameE = node.NameE;
			}
			this.SystemNode = node.SystemNode;
			int count = node.ElementList.Count;
			this.ElementList.Clear();
			this.ElementList.Capacity = count;
			for (int i = 0; i < count; i++)
			{
				this.ElementList.Add(node.ElementList[i].Clone());
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000113DC File Offset: 0x0000F5DC
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.SystemNode = (s.ReadByte() != 0);
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.ElementList.Clear();
			this.ElementList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				PmxNode.NodeElement nodeElement = new PmxNode.NodeElement();
				nodeElement.FromStreamEx(s, f);
				this.ElementList.Add(nodeElement);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00011468 File Offset: 0x0000F668
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.Name, f);
			PmxStreamHelper.WriteString(s, this.NameE, f);
			s.WriteByte(this.SystemNode ? 1 : 0);
			PmxStreamHelper.WriteElement_Int32(s, this.ElementList.Count, 4, true);
			for (int i = 0; i < this.ElementList.Count; i++)
			{
				this.ElementList[i].ToStreamEx(s, f);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000114EC File Offset: 0x0000F6EC
		object ICloneable.Clone()
		{
			return new PmxNode(this, false);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011508 File Offset: 0x0000F708
		public PmxNode Clone()
		{
			return new PmxNode(this, false);
		}

		// Token: 0x040000EE RID: 238
		public bool SystemNode;

		// Token: 0x040000EF RID: 239
		public List<PmxNode.NodeElement> ElementList;

		// Token: 0x0200005B RID: 91
		public enum ElementType
		{
			// Token: 0x0400020F RID: 527
			Bone,
			// Token: 0x04000210 RID: 528
			Morph
		}

		// Token: 0x0200005C RID: 92
		public class NodeElement : IPmxObjectKey, IPmxStreamIO, ICloneable
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060003AE RID: 942 RVA: 0x00019D24 File Offset: 0x00017F24
			PmxObject IPmxObjectKey.ObjectKey
			{
				get
				{
					return PmxObject.NodeElement;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060003AF RID: 943 RVA: 0x00019D38 File Offset: 0x00017F38
			// (set) Token: 0x060003B0 RID: 944 RVA: 0x00019D40 File Offset: 0x00017F40
			public PmxBone RefBone { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060003B1 RID: 945 RVA: 0x00019D49 File Offset: 0x00017F49
			// (set) Token: 0x060003B2 RID: 946 RVA: 0x00019D51 File Offset: 0x00017F51
			public PmxMorph RefMorph { get; set; }

			// Token: 0x060003B3 RID: 947 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
			public NodeElement()
			{
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x00019D5A File Offset: 0x00017F5A
			public NodeElement(PmxNode.NodeElement e)
			{
				this.FromNodeElement(e);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x00019D6C File Offset: 0x00017F6C
			public void FromNodeElement(PmxNode.NodeElement e)
			{
				this.ElementType = e.ElementType;
				this.Index = e.Index;
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x00019D88 File Offset: 0x00017F88
			public void FromStreamEx(Stream s, PmxElementFormat f)
			{
				this.ElementType = (PmxNode.ElementType)s.ReadByte();
				PmxNode.ElementType elementType = this.ElementType;
				if (elementType != PmxNode.ElementType.Bone)
				{
					if (elementType == PmxNode.ElementType.Morph)
					{
						this.Index = PmxStreamHelper.ReadElement_Int32(s, f.MorphSize, true);
					}
				}
				else
				{
					this.Index = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
				}
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x00019DE0 File Offset: 0x00017FE0
			public void ToStreamEx(Stream s, PmxElementFormat f)
			{
				s.WriteByte((byte)this.ElementType);
				PmxNode.ElementType elementType = this.ElementType;
				if (elementType != PmxNode.ElementType.Bone)
				{
					if (elementType == PmxNode.ElementType.Morph)
					{
						PmxStreamHelper.WriteElement_Int32(s, this.Index, f.MorphSize, true);
					}
				}
				else
				{
					PmxStreamHelper.WriteElement_Int32(s, this.Index, f.BoneSize, true);
				}
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00019E3C File Offset: 0x0001803C
			public static PmxNode.NodeElement BoneElement(int index)
			{
				return new PmxNode.NodeElement
				{
					ElementType = PmxNode.ElementType.Bone,
					Index = index
				};
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x00019E64 File Offset: 0x00018064
			public static PmxNode.NodeElement MorphElement(int index)
			{
				return new PmxNode.NodeElement
				{
					ElementType = PmxNode.ElementType.Morph,
					Index = index
				};
			}

			// Token: 0x060003BA RID: 954 RVA: 0x00019E8C File Offset: 0x0001808C
			object ICloneable.Clone()
			{
				return new PmxNode.NodeElement(this);
			}

			// Token: 0x060003BB RID: 955 RVA: 0x00019EA4 File Offset: 0x000180A4
			public PmxNode.NodeElement Clone()
			{
				return new PmxNode.NodeElement(this);
			}

			// Token: 0x04000211 RID: 529
			public PmxNode.ElementType ElementType;

			// Token: 0x04000212 RID: 530
			public int Index;
		}
	}
}
