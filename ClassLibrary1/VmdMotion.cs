using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000042 RID: 66
	public class VmdMotion : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00017F90 File Offset: 0x00016190
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00017F98 File Offset: 0x00016198
		public bool PhysicsOff { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00017FA4 File Offset: 0x000161A4
		public int ByteCount
		{
			get
			{
				return 47 + this.IPL.ByteCount + this.NoDataCount;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00017FCB File Offset: 0x000161CB
		public VmdMotion()
		{
			this.PhysicsOff = false;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00018008 File Offset: 0x00016208
		public VmdMotion(VmdMotion motion) : this()
		{
			this.Name = motion.Name;
			this.FrameIndex = motion.FrameIndex;
			this.Position = motion.Position;
			this.Rotate = motion.Rotate;
			this.IPL = (VmdMotionIPL)motion.IPL.Clone();
			this.PhysicsOff = motion.PhysicsOff;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00018070 File Offset: 0x00016270
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			byte[] array = new byte[15];
			BytesStringProc.SetString(array, this.Name, 0, 253);
			list.AddRange(array);
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.AddRange(BitConverter.GetBytes(this.Position.x));
			list.AddRange(BitConverter.GetBytes(this.Position.y));
			list.AddRange(BitConverter.GetBytes(this.Position.z));
			list.AddRange(BitConverter.GetBytes(this.Rotate.x));
			list.AddRange(BitConverter.GetBytes(this.Rotate.y));
			list.AddRange(BitConverter.GetBytes(this.Rotate.z));
			list.AddRange(BitConverter.GetBytes(this.Rotate.w));
			byte[] array2 = new byte[this.IPL.ByteCount * 4];
			byte[] array3 = this.IPL.ToBytes();
			int num = array3.Length;
			for (int i = 0; i < num; i++)
			{
				array2[i * 4] = array3[i];
			}
			bool physicsOff = this.PhysicsOff;
			if (physicsOff)
			{
				byte[] bytes = BitConverter.GetBytes(3939);
				array2[2] = bytes[0];
				array2[3] = bytes[1];
			}
			list.AddRange(array2);
			return list.ToArray();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000181E0 File Offset: 0x000163E0
		public void FromBytes(byte[] bytes, int startIndex)
		{
			byte[] array = new byte[15];
			Array.Copy(bytes, startIndex, array, 0, 15);
			this.Name = BytesStringProc.GetString(array, 0);
			int num = startIndex + 15;
			this.FrameIndex = BitConverter.ToInt32(bytes, num);
			num += 4;
			this.Position.x = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Position.y = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Position.z = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.x = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.y = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.z = BitConverter.ToSingle(bytes, num);
			num += 4;
			this.Rotate.w = BitConverter.ToSingle(bytes, num);
			num += 4;
			int byteCount = this.IPL.ByteCount;
			byte[] array2 = new byte[byteCount * 4];
			byte[] array3 = new byte[this.IPL.ByteCount];
			Array.Copy(bytes, num, array2, 0, array2.Length);
			ushort num2 = BitConverter.ToUInt16(array2, 2);
			this.PhysicsOff = (num2 == 3939);
			for (int i = 0; i < byteCount; i++)
			{
				array3[i] = array2[i * 4];
			}
			this.IPL.FromBytes(array3, 0);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00018340 File Offset: 0x00016540
		public object Clone()
		{
			return new VmdMotion(this);
		}

		// Token: 0x04000182 RID: 386
		private const int NameBytes = 15;

		// Token: 0x04000183 RID: 387
		private const ushort PhysicsOffNum = 3939;

		// Token: 0x04000184 RID: 388
		public string Name = "";

		// Token: 0x04000185 RID: 389
		public Vector3 Position;

		// Token: 0x04000186 RID: 390
		public Quaternion Rotate = Quaternion.identity;

		// Token: 0x04000187 RID: 391
		public VmdMotionIPL IPL = new VmdMotionIPL();

		// Token: 0x04000188 RID: 392
		protected int NoDataCount = 48;
	}
}
