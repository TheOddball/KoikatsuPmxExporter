using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000011 RID: 17
	public abstract class PmxBaseMorph : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.BaseMorph;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000DA90 File Offset: 0x0000BC90
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public virtual int BaseIndex { get; set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000DAA1 File Offset: 0x0000BCA1
		public void FromPmxBaseMorph(PmxBaseMorph sv)
		{
			this.BaseIndex = sv.BaseIndex;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public static PmxBaseMorph CreateOffsetObject(PmxMorph.OffsetKind kind)
		{
			PmxBaseMorph result = null;
			switch (kind)
			{
			case PmxMorph.OffsetKind.Group:
			case PmxMorph.OffsetKind.Flip:
				result = new PmxGroupMorph();
				break;
			case PmxMorph.OffsetKind.Vertex:
				result = new PmxVertexMorph();
				break;
			case PmxMorph.OffsetKind.Bone:
				result = new PmxBoneMorph();
				break;
			case PmxMorph.OffsetKind.UV:
			case PmxMorph.OffsetKind.UVA1:
			case PmxMorph.OffsetKind.UVA2:
			case PmxMorph.OffsetKind.UVA3:
			case PmxMorph.OffsetKind.UVA4:
				result = new PmxUVMorph();
				break;
			case PmxMorph.OffsetKind.Material:
				result = new PmxMaterialMorph();
				break;
			case PmxMorph.OffsetKind.Impulse:
				result = new PmxImpulseMorph();
				break;
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000DB38 File Offset: 0x0000BD38
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public virtual PmxBaseMorph Clone()
		{
			return null;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005711 File Offset: 0x00003911
		public virtual void FromStreamEx(Stream s, PmxElementFormat f)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005711 File Offset: 0x00003911
		public virtual void ToStreamEx(Stream s, PmxElementFormat f)
		{
		}
	}
}
