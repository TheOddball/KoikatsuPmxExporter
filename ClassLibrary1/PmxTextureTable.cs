using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200002A RID: 42
	public class PmxTextureTable : IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00012ED4 File Offset: 0x000110D4
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.TexTable;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00012EE7 File Offset: 0x000110E7
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00012EEF File Offset: 0x000110EF
		public Dictionary<string, int> NameToIndex { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00012EF8 File Offset: 0x000110F8
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00012F00 File Offset: 0x00011100
		public Dictionary<int, string> IndexToName { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00012F0C File Offset: 0x0001110C
		public int Count
		{
			get
			{
				return this.NameToIndex.Count;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00012F2C File Offset: 0x0001112C
		public int GetIndex(string name)
		{
			int result = -1;
			bool flag = this.NameToIndex.ContainsKey(name);
			if (flag)
			{
				result = this.NameToIndex[name];
			}
			return result;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00012F60 File Offset: 0x00011160
		public string GetName(int ix)
		{
			string result = "";
			bool flag = this.IndexToName.ContainsKey(ix);
			if (flag)
			{
				result = this.IndexToName[ix];
			}
			return result;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00012F98 File Offset: 0x00011198
		public PmxTextureTable()
		{
			this.NameToIndex = new Dictionary<string, int>();
			this.IndexToName = new Dictionary<int, string>();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00012FBA File Offset: 0x000111BA
		public PmxTextureTable(PmxTextureTable tx)
		{
			this.FromPmxTextureTable(tx);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00012FCC File Offset: 0x000111CC
		public void FromPmxTextureTable(PmxTextureTable tx)
		{
			string[] array = new string[tx.Count];
			tx.NameToIndex.Keys.CopyTo(array, 0);
			this.CreateTable(array);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00013001 File Offset: 0x00011201
		public PmxTextureTable(List<PmxMaterial> ml) : this()
		{
			this.CreateTable(ml);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00013014 File Offset: 0x00011214
		public void CreateTable(List<PmxMaterial> ml)
		{
			this.NameToIndex.Clear();
			this.IndexToName.Clear();
			int num = 0;
			for (int i = 0; i < ml.Count; i++)
			{
				PmxMaterial pmxMaterial = ml[i];
				bool flag = !string.IsNullOrEmpty(pmxMaterial.Tex) && !this.NameToIndex.ContainsKey(pmxMaterial.Tex);
				if (flag)
				{
					this.NameToIndex.Add(pmxMaterial.Tex, num);
					this.IndexToName.Add(num, pmxMaterial.Tex);
					num++;
				}
				bool flag2 = !string.IsNullOrEmpty(pmxMaterial.Sphere) && !this.NameToIndex.ContainsKey(pmxMaterial.Sphere);
				if (flag2)
				{
					this.NameToIndex.Add(pmxMaterial.Sphere, num);
					this.IndexToName.Add(num, pmxMaterial.Sphere);
					num++;
				}
				bool flag3 = !string.IsNullOrEmpty(pmxMaterial.Toon) && !this.NameToIndex.ContainsKey(pmxMaterial.Toon) && !SystemToon.IsSystemToon(pmxMaterial.Toon);
				if (flag3)
				{
					this.NameToIndex.Add(pmxMaterial.Toon, num);
					this.IndexToName.Add(num, pmxMaterial.Toon);
					num++;
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00013170 File Offset: 0x00011370
		public void CreateTable(string[] names)
		{
			this.NameToIndex.Clear();
			this.IndexToName.Clear();
			for (int i = 0; i < names.Length; i++)
			{
				this.NameToIndex.Add(names[i], i);
				this.IndexToName.Add(i, names[i]);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000131CC File Offset: 0x000113CC
		public void FromStreamEx(Stream s, PmxElementFormat f)
		{
			int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = PmxStreamHelper.ReadString(s, f);
			}
			this.CreateTable(array);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00013210 File Offset: 0x00011410
		public void ToStreamEx(Stream s, PmxElementFormat f)
		{
			int count = this.Count;
			PmxStreamHelper.WriteElement_Int32(s, count, 4, true);
			for (int i = 0; i < count; i++)
			{
				PmxStreamHelper.WriteString(s, this.IndexToName[i], f);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00013258 File Offset: 0x00011458
		object ICloneable.Clone()
		{
			return new PmxTextureTable(this);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00013270 File Offset: 0x00011470
		public PmxTextureTable Clone()
		{
			return new PmxTextureTable(this);
		}
	}
}
