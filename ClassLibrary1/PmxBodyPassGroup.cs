using System;
using System.Text;

namespace PmxLib
{
	// Token: 0x02000013 RID: 19
	public class PmxBodyPassGroup : ICloneable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000DF75 File Offset: 0x0000C175
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000DF7D File Offset: 0x0000C17D
		public bool[] Flags { get; private set; }

		// Token: 0x060000E4 RID: 228 RVA: 0x0000DF86 File Offset: 0x0000C186
		public PmxBodyPassGroup()
		{
			this.Flags = new bool[16];
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public PmxBodyPassGroup(PmxBodyPassGroup pg) : this()
		{
			for (int i = 0; i < 16; i++)
			{
				this.Flags[i] = pg.Flags[i];
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public ushort ToFlagBits()
		{
			int num = 1;
			int num2 = 0;
			for (int i = 0; i < this.Flags.Length; i++)
			{
				bool flag = !this.Flags[i];
				if (flag)
				{
					num2 |= num << i;
				}
			}
			return (ushort)num2;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000E028 File Offset: 0x0000C228
		public void FromFlagBits(ushort bits)
		{
			ushort num = 1;
			for (int i = 0; i < this.Flags.Length; i++)
			{
				this.Flags[i] = (((int)bits & (int)num << i) <= 0);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000E068 File Offset: 0x0000C268
		public void FromFlagBits(bool[] flags)
		{
			int num = Math.Min(this.Flags.Length, flags.Length);
			for (int i = 0; i < num; i++)
			{
				this.Flags[i] = flags[i];
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public string ToText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.Flags.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.Flags[i];
				if (flag)
				{
					stringBuilder.Append((i + 1).ToString() + " ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000E10C File Offset: 0x0000C30C
		public void FromText(string text)
		{
			try
			{
				string[] array = text.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries);
				int num = this.Flags.Length;
				for (int i = 0; i < num; i++)
				{
					this.Flags[i] = false;
				}
				int num2 = array.Length;
				for (int j = 0; j < num2; j++)
				{
					int num3;
					bool flag = int.TryParse(array[j], out num3);
					if (flag)
					{
						num3--;
						bool flag2 = 0 <= num3 && num3 < num;
						if (flag2)
						{
							this.Flags[num3] = true;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		object ICloneable.Clone()
		{
			return new PmxBodyPassGroup(this);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public PmxBodyPassGroup Clone()
		{
			return new PmxBodyPassGroup(this);
		}

		// Token: 0x0400006D RID: 109
		private const int PassGroupCount = 16;
	}
}
