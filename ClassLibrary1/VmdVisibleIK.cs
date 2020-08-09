using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000045 RID: 69
	public class VmdVisibleIK : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0001888C File Offset: 0x00016A8C
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00018894 File Offset: 0x00016A94
		public List<VmdVisibleIK.IK> IKList { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000188A0 File Offset: 0x00016AA0
		public int ByteCount
		{
			get
			{
				return 9 + 21 * this.IKList.Count;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000188C3 File Offset: 0x00016AC3
		public VmdVisibleIK()
		{
			this.Visible = true;
			this.IKList = new List<VmdVisibleIK.IK>();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000188E0 File Offset: 0x00016AE0
		public VmdVisibleIK(VmdVisibleIK vik)
		{
			this.Visible = vik.Visible;
			this.IKList = CP.CloneList<VmdVisibleIK.IK>(vik.IKList);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00018908 File Offset: 0x00016B08
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.Add(this.Visible ? 1 : 0);
			list.AddRange(BitConverter.GetBytes(this.IKList.Count));
			for (int i = 0; i < this.IKList.Count; i++)
			{
				byte[] array = new byte[20];
				BytesStringProc.SetString(array, this.IKList[i].IKName, 0, 253);
				list.AddRange(array);
				list.Add(this.IKList[i].Enable ? 1 : 0);
			}
			return list.ToArray();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000189CC File Offset: 0x00016BCC
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
			int num = startIndex + 4;
			this.Visible = (bytes[num++] > 0);
			int num2 = BitConverter.ToInt32(bytes, num);
			num += 4;
			byte[] array = new byte[20];
			for (int i = 0; i < num2; i++)
			{
				VmdVisibleIK.IK ik = new VmdVisibleIK.IK();
				Array.Copy(bytes, num, array, 0, 20);
				ik.IKName = BytesStringProc.GetString(array, 0);
				num += 20;
				ik.Enable = (bytes[num++] > 0);
				this.IKList.Add(ik);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00018A6C File Offset: 0x00016C6C
		public object Clone()
		{
			return new VmdVisibleIK(this);
		}

		// Token: 0x04000190 RID: 400
		private const int NameBytes = 20;

		// Token: 0x04000191 RID: 401
		public bool Visible;

		// Token: 0x02000068 RID: 104
		public class IK : ICloneable
		{
			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001A1B1 File Offset: 0x000183B1
			// (set) Token: 0x060003D8 RID: 984 RVA: 0x0001A1B9 File Offset: 0x000183B9
			public string IKName { get; set; }

			// Token: 0x060003D9 RID: 985 RVA: 0x0001A1C2 File Offset: 0x000183C2
			public IK()
			{
				this.IKName = "";
				this.Enable = true;
			}

			// Token: 0x060003DA RID: 986 RVA: 0x0001A1DF File Offset: 0x000183DF
			public IK(VmdVisibleIK.IK ik)
			{
				this.IKName = ik.IKName;
				this.Enable = ik.Enable;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x0001A204 File Offset: 0x00018404
			object ICloneable.Clone()
			{
				return new VmdVisibleIK.IK(this);
			}

			// Token: 0x04000256 RID: 598
			public bool Enable;
		}
	}
}
