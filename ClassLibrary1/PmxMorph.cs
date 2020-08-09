using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000022 RID: 34
	public class PmxMorph : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00010C30 File Offset: 0x0000EE30
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Morph;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00010C44 File Offset: 0x0000EE44
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public string Name { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00010C55 File Offset: 0x0000EE55
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00010C5D File Offset: 0x0000EE5D
		public string NameE { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00010C66 File Offset: 0x0000EE66
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00010C6E File Offset: 0x0000EE6E
		public List<PmxBaseMorph> OffsetList { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00010C78 File Offset: 0x0000EE78
		public bool IsUV
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.UV || this.Kind == PmxMorph.OffsetKind.UVA1 || this.Kind == PmxMorph.OffsetKind.UVA2 || this.Kind == PmxMorph.OffsetKind.UVA3 || this.Kind == PmxMorph.OffsetKind.UVA4;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public bool IsVertex
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Vertex;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public bool IsBone
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Bone;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		public bool IsMaterial
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Material;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00010D10 File Offset: 0x0000EF10
		public bool IsFlip
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Flip;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00010D2C File Offset: 0x0000EF2C
		public bool IsImpulse
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Impulse;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00010D48 File Offset: 0x0000EF48
		public bool IsGroup
		{
			get
			{
				return this.Kind == PmxMorph.OffsetKind.Group;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00010D64 File Offset: 0x0000EF64
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00010D7C File Offset: 0x0000EF7C
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

		// Token: 0x060001F2 RID: 498 RVA: 0x00010D88 File Offset: 0x0000EF88
		public static string KindText(PmxMorph.OffsetKind kind)
		{
			string result = "-";
			switch (kind)
			{
			case PmxMorph.OffsetKind.Group:
				result = "グループ";
				break;
			case PmxMorph.OffsetKind.Vertex:
				result = "頂点";
				break;
			case PmxMorph.OffsetKind.Bone:
				result = "ボーン";
				break;
			case PmxMorph.OffsetKind.UV:
				result = "UV";
				break;
			case PmxMorph.OffsetKind.UVA1:
				result = "追加UV1";
				break;
			case PmxMorph.OffsetKind.UVA2:
				result = "追加UV2";
				break;
			case PmxMorph.OffsetKind.UVA3:
				result = "追加UV3";
				break;
			case PmxMorph.OffsetKind.UVA4:
				result = "追加UV4";
				break;
			case PmxMorph.OffsetKind.Material:
				result = "材質";
				break;
			case PmxMorph.OffsetKind.Flip:
				result = "フリップ";
				break;
			case PmxMorph.OffsetKind.Impulse:
				result = "インパルス";
				break;
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010E3A File Offset: 0x0000F03A
		public PmxMorph()
		{
			this.Name = "";
			this.NameE = "";
			this.Panel = 4;
			this.Kind = PmxMorph.OffsetKind.Vertex;
			this.OffsetList = new List<PmxBaseMorph>();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010E76 File Offset: 0x0000F076
		public PmxMorph(PmxMorph m, bool nonStr)
		{
			this.FromPmxMorph(m, nonStr);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010E8C File Offset: 0x0000F08C
		public void FromPmxMorph(PmxMorph m, bool nonStr)
		{
			bool flag = !nonStr;
			if (flag)
			{
				this.Name = m.Name;
				this.NameE = m.NameE;
			}
			this.Panel = m.Panel;
			this.Kind = m.Kind;
			int count = m.OffsetList.Count;
			this.OffsetList = new List<PmxBaseMorph>(count);
			for (int i = 0; i < count; i++)
			{
				this.OffsetList.Add(m.OffsetList[i].Clone());
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010F20 File Offset: 0x0000F120
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.Name = PmxStreamHelper.ReadString(s, f);
			this.NameE = PmxStreamHelper.ReadString(s, f);
			this.Panel = PmxStreamHelper.ReadElement_Int32(s, 1, true);
			this.Kind = (PmxMorph.OffsetKind)PmxStreamHelper.ReadElement_Int32(s, 1, true);
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			this.OffsetList.Clear();
			this.OffsetList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				switch (this.Kind)
				{
				case PmxMorph.OffsetKind.Group:
				case PmxMorph.OffsetKind.Flip:
				{
					PmxGroupMorph pmxGroupMorph = new PmxGroupMorph();
					pmxGroupMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxGroupMorph);
					break;
				}
				case PmxMorph.OffsetKind.Vertex:
				{
					PmxVertexMorph pmxVertexMorph = new PmxVertexMorph();
					pmxVertexMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxVertexMorph);
					break;
				}
				case PmxMorph.OffsetKind.Bone:
				{
					PmxBoneMorph pmxBoneMorph = new PmxBoneMorph();
					pmxBoneMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxBoneMorph);
					break;
				}
				case PmxMorph.OffsetKind.UV:
				case PmxMorph.OffsetKind.UVA1:
				case PmxMorph.OffsetKind.UVA2:
				case PmxMorph.OffsetKind.UVA3:
				case PmxMorph.OffsetKind.UVA4:
				{
					PmxUVMorph pmxUVMorph = new PmxUVMorph();
					pmxUVMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxUVMorph);
					break;
				}
				case PmxMorph.OffsetKind.Material:
				{
					PmxMaterialMorph pmxMaterialMorph = new PmxMaterialMorph();
					pmxMaterialMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxMaterialMorph);
					break;
				}
				case PmxMorph.OffsetKind.Impulse:
				{
					PmxImpulseMorph pmxImpulseMorph = new PmxImpulseMorph();
					pmxImpulseMorph.FromStreamEx(s, f);
					this.OffsetList.Add(pmxImpulseMorph);
					break;
				}
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			bool flag = !this.IsImpulse || f.Ver >= 2.1f;
			if (flag)
			{
				PmxStreamHelper.WriteString(s, this.Name, f);
				PmxStreamHelper.WriteString(s, this.NameE, f);
				PmxStreamHelper.WriteElement_Int32(s, this.Panel, 1, true);
				bool flag2 = this.IsFlip && f.Ver < 2.1f;
				if (flag2)
				{
					PmxStreamHelper.WriteElement_Int32(s, 0, 1, true);
				}
				else
				{
					PmxStreamHelper.WriteElement_Int32(s, (int)this.Kind, 1, true);
				}
				PmxStreamHelper.WriteElement_Int32(s, this.OffsetList.Count, 4, true);
				for (int i = 0; i < this.OffsetList.Count; i++)
				{
					switch (this.Kind)
					{
					case PmxMorph.OffsetKind.Group:
					case PmxMorph.OffsetKind.Flip:
						(this.OffsetList[i] as PmxGroupMorph).ToStreamEx(s, f);
						break;
					case PmxMorph.OffsetKind.Vertex:
						(this.OffsetList[i] as PmxVertexMorph).ToStreamEx(s, f);
						break;
					case PmxMorph.OffsetKind.Bone:
						(this.OffsetList[i] as PmxBoneMorph).ToStreamEx(s, f);
						break;
					case PmxMorph.OffsetKind.UV:
					case PmxMorph.OffsetKind.UVA1:
					case PmxMorph.OffsetKind.UVA2:
					case PmxMorph.OffsetKind.UVA3:
					case PmxMorph.OffsetKind.UVA4:
						(this.OffsetList[i] as PmxUVMorph).ToStreamEx(s, f);
						break;
					case PmxMorph.OffsetKind.Material:
						(this.OffsetList[i] as PmxMaterialMorph).ToStreamEx(s, f);
						break;
					case PmxMorph.OffsetKind.Impulse:
						(this.OffsetList[i] as PmxImpulseMorph).ToStreamEx(s, f);
						break;
					}
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0001126C File Offset: 0x0000F46C
		object ICloneable.Clone()
		{
			return new PmxMorph(this, false);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00011288 File Offset: 0x0000F488
		public PmxMorph Clone()
		{
			return new PmxMorph(this, false);
		}

		// Token: 0x040000E9 RID: 233
		public int Panel;

		// Token: 0x040000EA RID: 234
		public PmxMorph.OffsetKind Kind;

		// Token: 0x0200005A RID: 90
		public enum OffsetKind
		{
			// Token: 0x04000203 RID: 515
			Group,
			// Token: 0x04000204 RID: 516
			Vertex,
			// Token: 0x04000205 RID: 517
			Bone,
			// Token: 0x04000206 RID: 518
			UV,
			// Token: 0x04000207 RID: 519
			UVA1,
			// Token: 0x04000208 RID: 520
			UVA2,
			// Token: 0x04000209 RID: 521
			UVA3,
			// Token: 0x0400020A RID: 522
			UVA4,
			// Token: 0x0400020B RID: 523
			Material,
			// Token: 0x0400020C RID: 524
			Flip,
			// Token: 0x0400020D RID: 525
			Impulse
		}
	}
}
