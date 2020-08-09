using System;

namespace PmxLib
{
	// Token: 0x02000017 RID: 23
	public class PmxFace : IPmxObjectKey, ICloneable
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000EE7C File Offset: 0x0000D07C
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.Face;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000EE8F File Offset: 0x0000D08F
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000EE97 File Offset: 0x0000D097
		public PmxVertex V0 { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public PmxVertex V1 { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000EEB1 File Offset: 0x0000D0B1
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000EEB9 File Offset: 0x0000D0B9
		public PmxVertex V2 { get; set; }

		// Token: 0x0600013A RID: 314 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
		public PmxFace()
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public PmxFace(PmxFace f)
		{
			this.FromPmxFace(f);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000EEDE File Offset: 0x0000D0DE
		public void FromPmxFace(PmxFace f)
		{
			this.V0 = f.V0;
			this.V1 = f.V1;
			this.V2 = f.V2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000EF08 File Offset: 0x0000D108
		object ICloneable.Clone()
		{
			return new PmxFace(this);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000EF20 File Offset: 0x0000D120
		public PmxFace Clone()
		{
			return new PmxFace(this);
		}
	}
}
