using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000021 RID: 33
	public class PmxModelInfo : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.ModelInfo;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00010AB7 File Offset: 0x0000ECB7
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00010ABF File Offset: 0x0000ECBF
		public string ModelName { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public string ModelNameE { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00010AD9 File Offset: 0x0000ECD9
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00010AE1 File Offset: 0x0000ECE1
		public string Comment { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00010AEA File Offset: 0x0000ECEA
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00010AF2 File Offset: 0x0000ECF2
		public string CommentE { get; set; }

		// Token: 0x060001DA RID: 474 RVA: 0x00010AFB File Offset: 0x0000ECFB
		public PmxModelInfo()
		{
			this.Clear();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public PmxModelInfo(PmxModelInfo info)
		{
			this.FromModelInfo(info);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00010B1E File Offset: 0x0000ED1E
		public void FromModelInfo(PmxModelInfo info)
		{
			this.ModelName = info.ModelName;
			this.Comment = info.Comment;
			this.ModelNameE = info.ModelNameE;
			this.CommentE = info.CommentE;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00010B55 File Offset: 0x0000ED55
		public void Clear()
		{
			this.ModelName = "";
			this.Comment = "";
			this.ModelNameE = "";
			this.CommentE = "";
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00010B88 File Offset: 0x0000ED88
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			this.ModelName = PmxStreamHelper.ReadString(s, f);
			this.ModelNameE = PmxStreamHelper.ReadString(s, f);
			this.Comment = PmxStreamHelper.ReadString(s, f);
			this.CommentE = PmxStreamHelper.ReadString(s, f);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00010BC3 File Offset: 0x0000EDC3
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			PmxStreamHelper.WriteString(s, this.ModelName, f);
			PmxStreamHelper.WriteString(s, this.ModelNameE, f);
			PmxStreamHelper.WriteString(s, this.Comment, f);
			PmxStreamHelper.WriteString(s, this.CommentE, f);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00010C00 File Offset: 0x0000EE00
		object ICloneable.Clone()
		{
			return new PmxModelInfo(this);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00010C18 File Offset: 0x0000EE18
		public PmxModelInfo Clone()
		{
			return new PmxModelInfo(this);
		}
	}
}
