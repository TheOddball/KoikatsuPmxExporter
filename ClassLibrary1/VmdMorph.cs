using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000041 RID: 65
	public class VmdMorph : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00017E58 File Offset: 0x00016058
		public int ByteCount
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00017E6C File Offset: 0x0001606C
		public VmdMorph()
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00017E81 File Offset: 0x00016081
		public VmdMorph(VmdMorph skin)
		{
			this.Name = skin.Name;
			this.FrameIndex = skin.FrameIndex;
			this.Value = skin.Value;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00017EBC File Offset: 0x000160BC
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			byte[] array = new byte[15];
			BytesStringProc.SetString(array, this.Name, 0, 253);
			list.AddRange(array);
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.AddRange(BitConverter.GetBytes(this.Value));
			return list.ToArray();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00017F24 File Offset: 0x00016124
		public void FromBytes(byte[] bytes, int startIndex)
		{
			byte[] array = new byte[15];
			Array.Copy(bytes, startIndex, array, 0, 15);
			this.Name = BytesStringProc.GetString(array, 0);
			int num = startIndex + 15;
			this.FrameIndex = BitConverter.ToInt32(bytes, num);
			num += 4;
			this.Value = BitConverter.ToSingle(bytes, num);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00017F78 File Offset: 0x00016178
		public object Clone()
		{
			return new VmdMorph(this);
		}

		// Token: 0x0400017F RID: 383
		private const int NameBytes = 15;

		// Token: 0x04000180 RID: 384
		public string Name = "";

		// Token: 0x04000181 RID: 385
		public float Value;
	}
}
