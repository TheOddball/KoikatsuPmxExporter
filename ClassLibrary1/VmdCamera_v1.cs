using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x0200003C RID: 60
	internal class VmdCamera_v1 : VmdFrameBase, IBytesConvert, ICloneable
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000176E8 File Offset: 0x000158E8
		public int ByteCount
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000176FC File Offset: 0x000158FC
		public VmdCamera_v1()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00017714 File Offset: 0x00015914
		public VmdCamera_v1(VmdCamera_v1 camera)
		{
			this.FrameIndex = camera.FrameIndex;
			this.Distance = camera.Distance;
			this.Position = camera.Position;
			this.Rotate = camera.Rotate;
			this.CameraIpl = camera.CameraIpl;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00017770 File Offset: 0x00015970
		public VmdCamera ToVmdCamera()
		{
			return new VmdCamera
			{
				FrameIndex = this.FrameIndex,
				Distance = this.Distance,
				Position = this.Position,
				Rotate = this.Rotate,
				IPL = 
				{
					MoveX = new VmdIplData(this.CameraIpl),
					MoveY = new VmdIplData(this.CameraIpl),
					MoveZ = new VmdIplData(this.CameraIpl),
					Rotate = new VmdIplData(this.CameraIpl),
					Distance = new VmdIplData(this.CameraIpl),
					Angle = new VmdIplData(this.CameraIpl)
				},
				Angle = 45f,
				Pers = 0
			};
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00017850 File Offset: 0x00015A50
		public byte[] ToBytes()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.FrameIndex));
			list.AddRange(BitConverter.GetBytes(this.Distance));
			list.AddRange(BitConverter.GetBytes(this.Position.x));
			list.AddRange(BitConverter.GetBytes(this.Position.y));
			list.AddRange(BitConverter.GetBytes(this.Position.z));
			list.AddRange(BitConverter.GetBytes(this.Rotate.x));
			list.AddRange(BitConverter.GetBytes(this.Rotate.y));
			list.AddRange(BitConverter.GetBytes(this.Rotate.z));
			list.Add((byte)this.CameraIpl.P1.X);
			list.Add((byte)this.CameraIpl.P2.X);
			list.Add((byte)this.CameraIpl.P1.Y);
			list.Add((byte)this.CameraIpl.P2.Y);
			return list.ToArray();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001797C File Offset: 0x00015B7C
		public void FromBytes(byte[] bytes, int startIndex)
		{
			this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
			int num = startIndex + 4;
			this.Distance = BitConverter.ToSingle(bytes, num);
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
			this.CameraIpl.P1.X = (int)bytes[num];
			num++;
			this.CameraIpl.P1.Y = (int)bytes[num];
			num++;
			this.CameraIpl.P2.X = (int)bytes[num];
			num++;
			this.CameraIpl.P2.Y = (int)bytes[num];
			num++;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00017A90 File Offset: 0x00015C90
		public object Clone()
		{
			return new VmdCamera_v1(this);
		}

		// Token: 0x04000174 RID: 372
		public float Distance;

		// Token: 0x04000175 RID: 373
		public Vector3 Position;

		// Token: 0x04000176 RID: 374
		public Vector3 Rotate;

		// Token: 0x04000177 RID: 375
		public VmdIplData CameraIpl = new VmdIplData();
	}
}
