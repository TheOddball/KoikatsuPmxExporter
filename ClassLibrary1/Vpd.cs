using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PmxLib
{
	// Token: 0x02000046 RID: 70
	public class Vpd
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00018A84 File Offset: 0x00016C84
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00018A8C File Offset: 0x00016C8C
		public string ModelName { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00018A95 File Offset: 0x00016C95
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00018A9D File Offset: 0x00016C9D
		public List<Vpd.PoseData> PoseList { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00018AA6 File Offset: 0x00016CA6
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00018AAE File Offset: 0x00016CAE
		public List<Vpd.MorphData> MorphList { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00018AB7 File Offset: 0x00016CB7
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00018ABF File Offset: 0x00016CBF
		public bool Extend { get; set; }

		// Token: 0x0600037C RID: 892 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public Vpd()
		{
			this.PoseList = new List<Vpd.PoseData>();
			this.MorphList = new List<Vpd.MorphData>();
			this.Extend = true;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00018AF4 File Offset: 0x00016CF4
		public static bool IsVpdText(string text)
		{
			Regex regex = new Regex(Vpd.HeadGetReg, RegexOptions.IgnoreCase);
			return regex.IsMatch(text);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00018B1C File Offset: 0x00016D1C
		public bool FromText(string text)
		{
			bool result = false;
			try
			{
				bool flag = !Vpd.IsVpdText(text);
				if (flag)
				{
					return result;
				}
				Regex regex = new Regex(Vpd.InfoGetReg, RegexOptions.IgnoreCase);
				Match match = regex.Match(text);
				bool success = match.Success;
				if (success)
				{
					string text2 = match.Groups["name"].Value;
					bool flag2 = text2.ToLower().Contains(Vpd.NameExt);
					if (flag2)
					{
						text2 = text2.Replace(Vpd.NameExt, "");
					}
					this.ModelName = text2;
				}
				this.PoseList.Clear();
				Regex regex2 = new Regex(Vpd.BoneGetReg, RegexOptions.IgnoreCase);
				match = regex2.Match(text);
				while (match.Success)
				{
					Vector3 t = new Vector3(0f, 0f, 0f);
					Quaternion identity = Quaternion.Identity;
					string value = match.Groups["name"].Value;
					float x;
					float.TryParse(match.Groups["trans_x"].Value, out x);
					float y;
					float.TryParse(match.Groups["trans_y"].Value, out y);
					float z;
					float.TryParse(match.Groups["trans_z"].Value, out z);
					t.x = x;
					t.y = y;
					t.z = z;
					float.TryParse(match.Groups["rot_x"].Value, out x);
					float.TryParse(match.Groups["rot_y"].Value, out y);
					float.TryParse(match.Groups["rot_z"].Value, out z);
					float w;
					float.TryParse(match.Groups["rot_w"].Value, out w);
					identity.x = x;
					identity.y = y;
					identity.z = z;
					identity.w = w;
					this.PoseList.Add(new Vpd.PoseData(value, identity, t));
					match = match.NextMatch();
				}
				this.MorphList.Clear();
				regex2 = new Regex(Vpd.MorphGetReg, RegexOptions.IgnoreCase);
				match = regex2.Match(text);
				while (match.Success)
				{
					float val = 0f;
					string value2 = match.Groups["name"].Value;
					float.TryParse(match.Groups["val"].Value, out val);
					this.MorphList.Add(new Vpd.MorphData(value2, val));
					match = match.NextMatch();
				}
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00018E04 File Offset: 0x00017004
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(Vpd.VpdHeader);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(this.ModelName + Vpd.NameExt + ";");
			stringBuilder.AppendLine(this.PoseList.Count.ToString() + ";");
			stringBuilder.AppendLine();
			for (int i = 0; i < this.PoseList.Count; i++)
			{
				stringBuilder.AppendLine("Bone" + i.ToString() + this.PoseList[i].ToString());
			}
			bool extend = this.Extend;
			if (extend)
			{
				for (int j = 0; j < this.MorphList.Count; j++)
				{
					stringBuilder.AppendLine("Morph" + j.ToString() + this.MorphList[j].ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000193 RID: 403
		public static string VpdHeader = "Vocaloid Pose Data file";

		// Token: 0x04000194 RID: 404
		private static string HeadGetReg = "^[^V]*Vocaloid Pose Data file";

		// Token: 0x04000195 RID: 405
		private static string InfoGetReg = "^[^V]*Vocaloid Pose Data file[^\\n]*\\n[\\n\\s]*(?<name>[^;]+);[\\s]*[^\\n]*\\n+(?<num>[^;]+);";

		// Token: 0x04000196 RID: 406
		private static string BoneGetReg = "\\n+\\s*Bone(?<no>\\d+)\\s*\\{\\s*(?<name>[^\\r\\n]+)[\\r\\n]+\\s*(?<trans_x>[^,]+),(?<trans_y>[^,]+),(?<trans_z>[^;]+);[^\\n]*\\n+(?<rot_x>[^,]+),(?<rot_y>[^,]+),(?<rot_z>[^,]+),(?<rot_w>[^;]+);[^\\n]*\\n+\\s*\\}";

		// Token: 0x04000197 RID: 407
		private static string MorphGetReg = "\\n+\\s*Morph(?<no>\\d+)\\s*\\{\\s*(?<name>[^\\r\\n]+)[\\r\\n]+\\s*(?<val>[^;]+);[^\\n]*\\n+\\s*\\}";

		// Token: 0x04000198 RID: 408
		private static string NameExt = ".osm";

		// Token: 0x02000069 RID: 105
		public class PoseData
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0001A21C File Offset: 0x0001841C
			// (set) Token: 0x060003DD RID: 989 RVA: 0x0001A224 File Offset: 0x00018424
			public string BoneName { get; set; }

			// Token: 0x060003DE RID: 990 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
			public PoseData()
			{
			}

			// Token: 0x060003DF RID: 991 RVA: 0x0001A22D File Offset: 0x0001842D
			public PoseData(string name, Quaternion r, Vector3 t)
			{
				this.BoneName = name;
				this.Rotation = r;
				this.Translation = t;
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x0001A250 File Offset: 0x00018450
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("{" + this.BoneName);
				string format = "0.000000";
				StringBuilder stringBuilder2 = stringBuilder;
				string[] array = new string[7];
				array[0] = "  ";
				string[] array2 = array;
				int num = 1;
				array2[num] = this.Translation.X.ToString(format);
				array[2] = ",";
				string[] array3 = array;
				int num2 = 3;
				array3[num2] = this.Translation.Y.ToString(format);
				array[4] = ",";
				string[] array4 = array;
				int num3 = 5;
				array4[num3] = this.Translation.Z.ToString(format);
				array[6] = ";";
				stringBuilder2.AppendLine(string.Concat(array));
				StringBuilder stringBuilder3 = stringBuilder;
				array = new string[9];
				array[0] = "  ";
				string[] array5 = array;
				int num4 = 1;
				array5[num4] = this.Rotation.X.ToString(format);
				array[2] = ",";
				string[] array6 = array;
				int num5 = 3;
				array6[num5] = this.Rotation.Y.ToString(format);
				array[4] = ",";
				string[] array7 = array;
				int num6 = 5;
				array7[num6] = this.Rotation.Z.ToString(format);
				array[6] = ",";
				string[] array8 = array;
				int num7 = 7;
				array8[num7] = this.Rotation.W.ToString(format);
				array[8] = ";";
				stringBuilder3.AppendLine(string.Concat(array));
				stringBuilder.AppendLine("}");
				return stringBuilder.ToString();
			}

			// Token: 0x04000258 RID: 600
			public Quaternion Rotation;

			// Token: 0x04000259 RID: 601
			public Vector3 Translation;
		}

		// Token: 0x0200006A RID: 106
		public class MorphData
		{
			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001A3F0 File Offset: 0x000185F0
			// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001A3F8 File Offset: 0x000185F8
			public string MorphName { get; set; }

			// Token: 0x060003E3 RID: 995 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
			public MorphData()
			{
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x0001A401 File Offset: 0x00018601
			public MorphData(string name, float val)
			{
				this.MorphName = name;
				this.Value = val;
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x0001A41C File Offset: 0x0001861C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("{" + this.MorphName);
				stringBuilder.AppendLine("  " + this.Value.ToString() + ";");
				stringBuilder.AppendLine("}");
				return stringBuilder.ToString();
			}

			// Token: 0x0400025B RID: 603
			public float Value;
		}
	}
}
