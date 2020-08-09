using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000039 RID: 57
	public class Vmd : IBytesConvert, ICloneable
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00016460 File Offset: 0x00014660
		public Vmd.VmdVersion Version
		{
			get
			{
				return this.m_ver;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00016478 File Offset: 0x00014678
		public int ByteCount
		{
			get
			{
				return 30 + this.ModelNameBytes + Vmd.GetListBytes<VmdMotion>(this.MotionList) + Vmd.GetListBytes<VmdMorph>(this.MorphList) + Vmd.GetListBytes<VmdVisibleIK>(this.VisibleIKList) + Vmd.GetListBytes<VmdCamera>(this.CameraList) + Vmd.GetListBytes<VmdLight>(this.LightList) + Vmd.GetListBytes<VmdSelfShadow>(this.SelfShadowList);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000164DC File Offset: 0x000146DC
		public Vmd()
		{
			PmxLibClass.IsLocked();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00016558 File Offset: 0x00014758
		public Vmd(Vmd vmd) : this()
		{
			this.VMDHeader = vmd.VMDHeader;
			this.ModelName = vmd.ModelName;
			this.MotionList = CP.CloneList<VmdMotion>(vmd.MotionList);
			this.MorphList = CP.CloneList<VmdMorph>(vmd.MorphList);
			this.CameraList = CP.CloneList<VmdCamera>(vmd.CameraList);
			this.LightList = CP.CloneList<VmdLight>(vmd.LightList);
			this.SelfShadowList = CP.CloneList<VmdSelfShadow>(vmd.SelfShadowList);
			this.VisibleIKList = CP.CloneList<VmdVisibleIK>(vmd.VisibleIKList);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000165EB File Offset: 0x000147EB
		public Vmd(string path) : this()
		{
			this.FromFile(path);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00016600 File Offset: 0x00014800
		public void FromFile(string path)
		{
			try
			{
				byte[] bytes = File.ReadAllBytes(path);
				this.FromBytes(bytes, 0);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00016638 File Offset: 0x00014838
		public void NormalizeList(Vmd.NormalizeDataType type)
		{
			switch (type)
			{
			case Vmd.NormalizeDataType.All:
				this.MotionList.Sort(new Comparison<VmdMotion>(VmdFrameBase.Compare));
				this.MorphList.Sort(new Comparison<VmdMorph>(VmdFrameBase.Compare));
				this.CameraList.Sort(new Comparison<VmdCamera>(VmdFrameBase.Compare));
				this.LightList.Sort(new Comparison<VmdLight>(VmdFrameBase.Compare));
				this.SelfShadowList.Sort(new Comparison<VmdSelfShadow>(VmdFrameBase.Compare));
				this.VisibleIKList.Sort(new Comparison<VmdVisibleIK>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.Motion:
				this.MotionList.Sort(new Comparison<VmdMotion>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.Skin:
				this.MorphList.Sort(new Comparison<VmdMorph>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.Camera:
				this.CameraList.Sort(new Comparison<VmdCamera>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.Light:
				this.LightList.Sort(new Comparison<VmdLight>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.SelfShadow:
				this.SelfShadowList.Sort(new Comparison<VmdSelfShadow>(VmdFrameBase.Compare));
				break;
			case Vmd.NormalizeDataType.VisibleIK:
				this.VisibleIKList.Sort(new Comparison<VmdVisibleIK>(VmdFrameBase.Compare));
				break;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000167AC File Offset: 0x000149AC
		public byte[] ToBytes()
		{
			this.NormalizeList(Vmd.NormalizeDataType.All);
			List<byte> list = new List<byte>();
			byte[] array = new byte[30];
			BytesStringProc.SetString(array, this.VMDHeader, 0, 0);
			list.AddRange(array);
			byte[] array2 = new byte[this.ModelNameBytes];
			BytesStringProc.SetString(array2, this.ModelName, 0, 253);
			list.AddRange(array2);
			int count = this.MotionList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int i = 0; i < count; i++)
			{
				list.AddRange(this.MotionList[i].ToBytes());
			}
			count = this.MorphList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int j = 0; j < count; j++)
			{
				list.AddRange(this.MorphList[j].ToBytes());
			}
			count = this.VisibleIKList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int k = 0; k < count; k++)
			{
				list.AddRange(this.VisibleIKList[k].ToBytes());
			}
			count = this.CameraList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int l = 0; l < count; l++)
			{
				list.AddRange(this.CameraList[l].ToBytes());
			}
			count = this.LightList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int m = 0; m < count; m++)
			{
				list.AddRange(this.LightList[m].ToBytes());
			}
			count = this.SelfShadowList.Count;
			list.AddRange(BitConverter.GetBytes(count));
			for (int n = 0; n < count; n++)
			{
				list.AddRange(this.SelfShadowList[n].ToBytes());
			}
			return list.ToArray();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000169D8 File Offset: 0x00014BD8
		public void FromBytes(byte[] bytes, int startIndex)
		{
			byte[] array = new byte[30];
			Array.Copy(bytes, startIndex, array, 0, 30);
			this.VMDHeader = BytesStringProc.GetString(array, 0);
			bool flag = string.Compare(this.VMDHeader, "Vocaloid Motion Data file", true) == 0;
			int num;
			if (flag)
			{
				this.ModelNameBytes = 10;
				num = 1;
				this.m_ver = Vmd.VmdVersion.v1;
			}
			else
			{
				bool flag2 = string.Compare(this.VMDHeader, "Vocaloid Motion Data 0002", true) != 0;
				if (flag2)
				{
					throw new Exception("対応したVMDファイルではありません");
				}
				this.ModelNameBytes = 20;
				num = 2;
				this.m_ver = Vmd.VmdVersion.v2;
			}
			int num2 = startIndex + 30;
			Array.Copy(bytes, num2, array, 0, this.ModelNameBytes);
			this.ModelName = BytesStringProc.GetString(array, 0);
			num2 += this.ModelNameBytes;
			int num3 = BitConverter.ToInt32(bytes, num2);
			num2 += 4;
			this.MotionList.Clear();
			this.MotionList.Capacity = num3;
			for (int i = 0; i < num3; i++)
			{
				VmdMotion vmdMotion = new VmdMotion();
				vmdMotion.FromBytes(bytes, num2);
				num2 += vmdMotion.ByteCount;
				this.MotionList.Add(vmdMotion);
			}
			bool flag3 = bytes.Length > num2;
			if (flag3)
			{
				num3 = BitConverter.ToInt32(bytes, num2);
				num2 += 4;
				this.MorphList.Clear();
				this.MorphList.Capacity = num3;
				for (int j = 0; j < num3; j++)
				{
					VmdMorph vmdMorph = new VmdMorph();
					vmdMorph.FromBytes(bytes, num2);
					num2 += vmdMorph.ByteCount;
					this.MorphList.Add(vmdMorph);
				}
				bool flag4 = bytes.Length > num2;
				if (flag4)
				{
					num3 = BitConverter.ToInt32(bytes, num2);
					num2 += 4;
					this.CameraList.Clear();
					this.CameraList.Capacity = num3;
					bool flag5 = num == 1;
					if (flag5)
					{
						for (int k = 0; k < num3; k++)
						{
							VmdCamera_v1 vmdCamera_v = new VmdCamera_v1();
							vmdCamera_v.FromBytes(bytes, num2);
							num2 += vmdCamera_v.ByteCount;
							this.CameraList.Add(vmdCamera_v.ToVmdCamera());
						}
					}
					else
					{
						bool flag6 = num == 2;
						if (flag6)
						{
							for (int l = 0; l < num3; l++)
							{
								VmdCamera vmdCamera = new VmdCamera();
								vmdCamera.FromBytes(bytes, num2);
								num2 += vmdCamera.ByteCount;
								this.CameraList.Add(vmdCamera);
							}
						}
					}
					bool flag7 = bytes.Length > num2;
					if (flag7)
					{
						num3 = BitConverter.ToInt32(bytes, num2);
						num2 += 4;
						this.LightList.Clear();
						this.LightList.Capacity = num3;
						for (int m = 0; m < num3; m++)
						{
							VmdLight vmdLight = new VmdLight();
							vmdLight.FromBytes(bytes, num2);
							num2 += vmdLight.ByteCount;
							this.LightList.Add(vmdLight);
						}
						bool flag8 = bytes.Length > num2;
						if (flag8)
						{
							num3 = BitConverter.ToInt32(bytes, num2);
							num2 += 4;
							this.SelfShadowList.Clear();
							this.SelfShadowList.Capacity = num3;
							for (int n = 0; n < num3; n++)
							{
								VmdSelfShadow vmdSelfShadow = new VmdSelfShadow();
								vmdSelfShadow.FromBytes(bytes, num2);
								num2 += vmdSelfShadow.ByteCount;
								this.SelfShadowList.Add(vmdSelfShadow);
							}
							bool flag9 = bytes.Length > num2;
							if (flag9)
							{
								num3 = BitConverter.ToInt32(bytes, num2);
								num2 += 4;
								this.VisibleIKList.Clear();
								this.VisibleIKList.Capacity = num3;
								for (int num4 = 0; num4 < num3; num4++)
								{
									VmdVisibleIK vmdVisibleIK = new VmdVisibleIK();
									vmdVisibleIK.FromBytes(bytes, num2);
									num2 += vmdVisibleIK.ByteCount;
									this.VisibleIKList.Add(vmdVisibleIK);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00016DB4 File Offset: 0x00014FB4
		public object Clone()
		{
			return new Vmd(this);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00016DCC File Offset: 0x00014FCC
		public static int GetListBytes<T>(List<T> list) where T : IBytesConvert
		{
			int count = list.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				int num2 = num;
				T t = list[i];
				num = num2 + t.ByteCount;
			}
			return num;
		}

		// Token: 0x04000158 RID: 344
		private const int HeaderBytes = 30;

		// Token: 0x04000159 RID: 345
		private const string HeaderString_V1 = "Vocaloid Motion Data file";

		// Token: 0x0400015A RID: 346
		private const string HeaderString_V2 = "Vocaloid Motion Data 0002";

		// Token: 0x0400015B RID: 347
		public const string CameraHeaderName = "カメラ・照明";

		// Token: 0x0400015C RID: 348
		private const int ModelNameBytes_V1 = 10;

		// Token: 0x0400015D RID: 349
		private const int ModelNameBytes_V2 = 20;

		// Token: 0x0400015E RID: 350
		public string VMDHeader = "Vocaloid Motion Data 0002";

		// Token: 0x0400015F RID: 351
		public string ModelName = "";

		// Token: 0x04000160 RID: 352
		public int ModelNameBytes = 20;

		// Token: 0x04000161 RID: 353
		private Vmd.VmdVersion m_ver;

		// Token: 0x04000162 RID: 354
		public List<VmdMotion> MotionList = new List<VmdMotion>();

		// Token: 0x04000163 RID: 355
		public List<VmdMorph> MorphList = new List<VmdMorph>();

		// Token: 0x04000164 RID: 356
		public List<VmdCamera> CameraList = new List<VmdCamera>();

		// Token: 0x04000165 RID: 357
		public List<VmdLight> LightList = new List<VmdLight>();

		// Token: 0x04000166 RID: 358
		public List<VmdSelfShadow> SelfShadowList = new List<VmdSelfShadow>();

		// Token: 0x04000167 RID: 359
		public List<VmdVisibleIK> VisibleIKList = new List<VmdVisibleIK>();

		// Token: 0x02000066 RID: 102
		public enum VmdVersion
		{
			// Token: 0x0400024C RID: 588
			v2,
			// Token: 0x0400024D RID: 589
			v1
		}

		// Token: 0x02000067 RID: 103
		public enum NormalizeDataType
		{
			// Token: 0x0400024F RID: 591
			All,
			// Token: 0x04000250 RID: 592
			Motion,
			// Token: 0x04000251 RID: 593
			Skin,
			// Token: 0x04000252 RID: 594
			Camera,
			// Token: 0x04000253 RID: 595
			Light,
			// Token: 0x04000254 RID: 596
			SelfShadow,
			// Token: 0x04000255 RID: 597
			VisibleIK
		}
	}
}
