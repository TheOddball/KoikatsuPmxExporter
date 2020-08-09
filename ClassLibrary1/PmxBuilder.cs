using System;
using System.Collections.Generic;
using System.IO;
using PmxLib;
using UnityEngine;

// Token: 0x02000003 RID: 3
internal class PmxBuilder
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002140 File Offset: 0x00000340
	public PmxBuilder()
	{
		this.pmxFile = new Pmx();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000038A4 File Offset: 0x00001AA4
	public string BuildStart()
	{
		try
		{
			Random random = new Random();
			this.pass = this.pass + random.Next(9999) + "\\";
			Directory.CreateDirectory(this.pass);
			this.hitomi[0] = new List<int>();
			this.hitomi[1] = new List<int>();
			this.setSkinnedMeshList();
			this.CreateModelInfo();
			this.CreateBoneList();
			this.CreateMeshList();
			this.addAccessory();
			this.createmorph();
			this.setmaterial();
			this.changebonename();
			this.addbone();
			this.changeboneinfo();
			this.addmorph();
			this.addphysics();
			this.addhitomimorph();
			this.addnode();
			this.phymune();
			this.CreatePmxHeader();
			this.Save();
			this.msg += "\n";
		}
		catch (Exception arg)
		{
			this.msg = this.msg + arg + "\n";
		}
		return this.msg;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000039CC File Offset: 0x00001BCC
	private void test_bones()
	{
		Transform transform = GameObject.Find("BodyTop").transform;
		List<Transform> list = new List<Transform>();
		Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
		int count = this.pmxFile.BoneList.Count;
		PmxBone pmxBone = new PmxBone();
		pmxBone.Name = transform.name;
		pmxBone.Parent = list.IndexOf(transform.parent) + count;
		Vector3 vector = transform.transform.position * (float)this.scale;
		pmxBone.Position = new Vector3(-vector.x, vector.y, -vector.z);
		list.Add(transform);
		this.pmxFile.BoneList.Add(pmxBone);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			pmxBone = new PmxBone();
			pmxBone.Name = componentsInChildren[i].name;
			pmxBone.Parent = list.IndexOf(componentsInChildren[i].parent) + count;
			vector = componentsInChildren[i].transform.position * (float)this.scale;
			pmxBone.Position = new Vector3(-vector.x, vector.y, -vector.z);
			list.Add(componentsInChildren[i]);
			this.pmxFile.BoneList.Add(pmxBone);
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00003B38 File Offset: 0x00001D38
	private void addhitomimorph()
	{
		int num = 0;
		Transform transform = GameObject.Find("BodyTop").transform;
		SkinnedMeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			num++;
			bool flag = componentsInChildren[i].name.Contains("hitomi");
			if (flag)
			{
				break;
			}
		}
		int num2 = 0;
		for (int j = 0; j < num + 1; j++)
		{
			num2 += this.vertics_num[j];
		}
		bool flag2 = num2 >= this.pmxFile.VertexList.Count;
		if (!flag2)
		{
			this.hitomi[0] = new List<int>();
			int k;
			for (k = 0; k < this.vertics_num[num + 1]; k++)
			{
				this.hitomi[0].Add(num2 + k);
			}
			this.hitomi[1] = new List<int>();
			for (int l = 0; l < this.vertics_num[num + 2]; l++)
			{
				this.hitomi[1].Add(num2 + k + l);
			}
			List<Vector2> list = new List<Vector2>();
			List<int> list2 = new List<int>();
			float num3 = 0f;
			float num4 = 0f;
			for (int m = 0; m < this.hitomi[0].Count; m++)
			{
				num3 += this.pmxFile.VertexList[this.hitomi[0][m]].UV.x;
				num4 += this.pmxFile.VertexList[this.hitomi[0][m]].UV.y;
			}
			num3 /= (float)this.hitomi[0].Count;
			num4 /= (float)this.hitomi[0].Count;
			float num5 = 0f;
			float num6 = 0f;
			for (int n = 0; n < this.hitomi[1].Count; n++)
			{
				num5 += this.pmxFile.VertexList[this.hitomi[1][n]].UV.x;
				num6 += this.pmxFile.VertexList[this.hitomi[1][n]].UV.y;
			}
			num5 /= (float)this.hitomi[0].Count;
			num6 /= (float)this.hitomi[0].Count;
			PmxMorph pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomiX-small";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num7 = 0; num7 < this.hitomi[0].Count; num7++)
			{
				PmxUVMorph item = new PmxUVMorph(this.hitomi[0][num7], new Vector2(this.pmxFile.VertexList[this.hitomi[0][num7]].UV.x - num3, 0f));
				pmxMorph.OffsetList.Add(item);
			}
			for (int num8 = 0; num8 < this.hitomi[1].Count; num8++)
			{
				PmxUVMorph item2 = new PmxUVMorph(this.hitomi[1][num8], new Vector2(this.pmxFile.VertexList[this.hitomi[1][num8]].UV.x - num5, 0f));
				pmxMorph.OffsetList.Add(item2);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomiY-small";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num9 = 0; num9 < this.hitomi[0].Count; num9++)
			{
				PmxUVMorph item3 = new PmxUVMorph(this.hitomi[0][num9], new Vector2(0f, this.pmxFile.VertexList[this.hitomi[0][num9]].UV.y - num4));
				pmxMorph.OffsetList.Add(item3);
			}
			for (int num10 = 0; num10 < this.hitomi[1].Count; num10++)
			{
				PmxUVMorph item4 = new PmxUVMorph(this.hitomi[1][num10], new Vector2(0f, this.pmxFile.VertexList[this.hitomi[1][num10]].UV.y - num6));
				pmxMorph.OffsetList.Add(item4);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomiX-big";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num11 = 0; num11 < this.hitomi[0].Count; num11++)
			{
				PmxUVMorph item5 = new PmxUVMorph(this.hitomi[0][num11], new Vector2(-(this.pmxFile.VertexList[this.hitomi[0][num11]].UV.x - num3), 0f));
				pmxMorph.OffsetList.Add(item5);
			}
			for (int num12 = 0; num12 < this.hitomi[1].Count; num12++)
			{
				PmxUVMorph item6 = new PmxUVMorph(this.hitomi[1][num12], new Vector2(-(this.pmxFile.VertexList[this.hitomi[1][num12]].UV.x - num5), 0f));
				pmxMorph.OffsetList.Add(item6);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomiY-big";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num13 = 0; num13 < this.hitomi[0].Count; num13++)
			{
				PmxUVMorph item7 = new PmxUVMorph(this.hitomi[0][num13], new Vector2(0f, -(this.pmxFile.VertexList[this.hitomi[0][num13]].UV.y - num4)));
				pmxMorph.OffsetList.Add(item7);
			}
			for (int num14 = 0; num14 < this.hitomi[1].Count; num14++)
			{
				PmxUVMorph item8 = new PmxUVMorph(this.hitomi[1][num14], new Vector2(0f, -(this.pmxFile.VertexList[this.hitomi[1][num14]].UV.y - num6)));
				pmxMorph.OffsetList.Add(item8);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomi-up";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num15 = 0; num15 < this.hitomi[0].Count; num15++)
			{
				PmxUVMorph item9 = new PmxUVMorph(this.hitomi[0][num15], new Vector2(0f, 0.5f));
				pmxMorph.OffsetList.Add(item9);
			}
			for (int num16 = 0; num16 < this.hitomi[1].Count; num16++)
			{
				PmxUVMorph item10 = new PmxUVMorph(this.hitomi[1][num16], new Vector2(0f, 0.5f));
				pmxMorph.OffsetList.Add(item10);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomi-down";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num17 = 0; num17 < this.hitomi[0].Count; num17++)
			{
				PmxUVMorph item11 = new PmxUVMorph(this.hitomi[0][num17], new Vector2(0f, -0.5f));
				pmxMorph.OffsetList.Add(item11);
			}
			for (int num18 = 0; num18 < this.hitomi[1].Count; num18++)
			{
				PmxUVMorph item12 = new PmxUVMorph(this.hitomi[1][num18], new Vector2(0f, -0.5f));
				pmxMorph.OffsetList.Add(item12);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomi-left";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num19 = 0; num19 < this.hitomi[0].Count; num19++)
			{
				PmxUVMorph item13 = new PmxUVMorph(this.hitomi[0][num19], new Vector2(-0.5f, 0f));
				pmxMorph.OffsetList.Add(item13);
			}
			for (int num20 = 0; num20 < this.hitomi[1].Count; num20++)
			{
				PmxUVMorph item14 = new PmxUVMorph(this.hitomi[1][num20], new Vector2(0.5f, 0f));
				pmxMorph.OffsetList.Add(item14);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
			pmxMorph = new PmxMorph();
			pmxMorph.Name = "hitomi-right";
			pmxMorph.NameE = "";
			pmxMorph.Panel = 1;
			pmxMorph.Kind = PmxMorph.OffsetKind.UV;
			for (int num21 = 0; num21 < this.hitomi[0].Count; num21++)
			{
				PmxUVMorph item15 = new PmxUVMorph(this.hitomi[0][num21], new Vector2(0.5f, 0f));
				pmxMorph.OffsetList.Add(item15);
			}
			for (int num22 = 0; num22 < this.hitomi[1].Count; num22++)
			{
				PmxUVMorph item16 = new PmxUVMorph(this.hitomi[1][num22], new Vector2(-0.5f, 0f));
				pmxMorph.OffsetList.Add(item16);
			}
			this.pmxFile.MorphList.Add(pmxMorph);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000470C File Offset: 0x0000290C
	private void addmorph()
	{
		PmxMorph pmxMorph = new PmxMorph();
		pmxMorph.Name = "あ";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		PmxGroupMorph pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_ha.ha00_a_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_face.f00_a_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "い";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_ha.ha00_i_l_cl");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_face.f00_i_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "う";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_ha.ha00_a_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_face.f00_u_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "え";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_ha.ha00_e_s_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_face.f00_e_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "お";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_ha.ha00_a_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("kuti_face.f00_o_l_op");
		pmxGroupMorph.Ratio = 1f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "まばたき";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_line_u.elu00_def_cl");
		pmxGroupMorph.Ratio = 0.36f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_line_l.ell00_def_cl");
		pmxGroupMorph.Ratio = 0.36f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_face.f00_def_cl");
		pmxGroupMorph.Ratio = 0.72f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "笑い";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Group;
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_line_u.elu00_egao_cl");
		pmxGroupMorph.Ratio = 0.36f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_line_l.ell00_egao_cl");
		pmxGroupMorph.Ratio = 0.36f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		pmxGroupMorph = new PmxGroupMorph();
		pmxGroupMorph.Index = this.smi("eye_face.f00_egao_cl");
		pmxGroupMorph.Ratio = 0.72f;
		pmxMorph.OffsetList.Add(pmxGroupMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
		pmxMorph = new PmxMorph();
		pmxMorph.Name = "bounse";
		pmxMorph.NameE = "";
		pmxMorph.Panel = 1;
		pmxMorph.Kind = PmxMorph.OffsetKind.Bone;
		PmxBoneMorph pmxBoneMorph = new PmxBoneMorph();
		pmxBoneMorph.Index = this.sbi("右腕");
		Quaternion quaternion = Quaternion.Euler(0f, 0f, 35f);
		pmxBoneMorph.Rotaion = new Quaternion(new Vector3(quaternion.x, quaternion.y, quaternion.z), quaternion.w);
		pmxMorph.OffsetList.Add(pmxBoneMorph);
		pmxBoneMorph = new PmxBoneMorph();
		pmxBoneMorph.Index = this.sbi("左腕");
		quaternion = Quaternion.Euler(0f, 0f, -35f);
		pmxBoneMorph.Rotaion = new Quaternion(new Vector3(quaternion.x, quaternion.y, quaternion.z), quaternion.w);
		pmxMorph.OffsetList.Add(pmxBoneMorph);
		this.pmxFile.MorphList.Add(pmxMorph);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00004CBC File Offset: 0x00002EBC
	private void changebonename()
	{
		for (int i = 0; i < this.bone_change_name_base.Length; i++)
		{
			try
			{
				PmxBone pmxBone = this.serchBone(this.bone_change_name_base[i]);
				pmxBone.Name = this.bone_change_name[i];
			}
			catch (Exception ex)
			{
			}
		}
		PmxBone pmxBone2 = new PmxBone();
		pmxBone2.Name = "胸親";
		this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_L")), pmxBone2);
		pmxBone2 = new PmxBone();
		pmxBone2.Name = "左AH2";
		this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust02_L")) + 1, pmxBone2);
		pmxBone2 = new PmxBone();
		pmxBone2.Name = "右AH2";
		this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust02_R")) + 1, pmxBone2);
		pmxBone2 = new PmxBone();
		pmxBone2.Name = "左胸操作";
		this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_L")), pmxBone2);
		pmxBone2 = new PmxBone();
		pmxBone2.Name = "右胸操作";
		this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_R")), pmxBone2);
		pmxBone2 = this.sb("cf_j_bust02_L");
		pmxBone2.Name = "左AH1";
		pmxBone2.To_Bone = this.sbi("左AH2");
		pmxBone2.Position = new Vector3(this.sb("cf_j_bust01_L").Position);
		pmxBone2.Flags = (PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("左胸操作");
		pmxBone2.Name = "左胸操作";
		pmxBone2.Parent = this.sbi("胸親");
		pmxBone2.Position = this.sb("左AH1").Position + new Vector3(0f, 0f, -0.213f);
		pmxBone2.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("左AH2");
		pmxBone2.Parent = this.sbi("左AH1");
		pmxBone2.Position = this.sb("左AH1").Position + new Vector3(0f, 0f, -1.515f);
		pmxBone2.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("cf_j_bust02_R");
		pmxBone2.Name = "右AH1";
		pmxBone2.To_Bone = this.sbi("右AH2");
		pmxBone2.Position = new Vector3(this.sb("cf_j_bust01_R").Position);
		pmxBone2.Flags = (PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("右胸操作");
		pmxBone2.Name = "右胸操作";
		pmxBone2.Parent = this.sbi("胸親");
		pmxBone2.Position = this.sb("右AH1").Position + new Vector3(0f, 0f, -0.213f);
		pmxBone2.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("右AH2");
		pmxBone2.Parent = this.sbi("右AH1");
		pmxBone2.Position = this.sb("右AH1").Position + new Vector3(0f, 0f, -1.515f);
		pmxBone2.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Enable);
		pmxBone2 = this.sb("胸親");
		pmxBone2.Parent = this.sbi("上半身2");
		pmxBone2.Position = new Vector3(this.sb("右胸操作").Position);
		pmxBone2.Position.x = 0f;
		pmxBone2.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000507C File Offset: 0x0000327C
	private void createmorph()
	{
		ChaControl instance = Singleton<ChaControl>.Instance;
		FBSTargetInfo[] fbstarget = instance.eyesCtrl.FBSTarget;
		for (int i = 0; i < fbstarget.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = fbstarget[i].GetSkinnedMeshRenderer();
			int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
			Vector3[] array = new Vector3[skinnedMeshRenderer.sharedMesh.vertices.Length];
			Vector3[] array2 = new Vector3[skinnedMeshRenderer.sharedMesh.normals.Length];
			Vector3[] array3 = new Vector3[skinnedMeshRenderer.sharedMesh.tangents.Length];
			int num = 0;
			for (int j = 0; j < this.vertics_num.Length; j++)
			{
				bool flag = this.vertics_num[j] == array.Length;
				if (flag)
				{
					break;
				}
				num += this.vertics_num[j];
			}
			bool flag2 = num >= this.pmxFile.VertexList.Count;
			if (!flag2)
			{
				for (int k = 0; k < blendShapeCount; k++)
				{
					skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameVertices(k, 0, array, array2, array3);
					PmxMorph pmxMorph = new PmxMorph();
					pmxMorph.Name = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(k);
					pmxMorph.NameE = "";
					pmxMorph.Panel = 1;
					pmxMorph.Kind = PmxMorph.OffsetKind.Vertex;
					for (int l = 0; l < array.Length; l++)
					{
						PmxVertexMorph pmxVertexMorph = new PmxVertexMorph(num + l, new Vector3(-array[l].x, array[l].y, -array[l].z));
						pmxVertexMorph.Offset *= (float)this.scale;
						pmxMorph.OffsetList.Add(pmxVertexMorph);
					}
					this.pmxFile.MorphList.Add(pmxMorph);
				}
			}
		}
		fbstarget = instance.mouthCtrl.FBSTarget;
		for (int m = 0; m < fbstarget.Length; m++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer2 = fbstarget[m].GetSkinnedMeshRenderer();
			int blendShapeCount2 = skinnedMeshRenderer2.sharedMesh.blendShapeCount;
			Vector3[] array4 = new Vector3[skinnedMeshRenderer2.sharedMesh.vertices.Length];
			Vector3[] array5 = new Vector3[skinnedMeshRenderer2.sharedMesh.normals.Length];
			Vector3[] array6 = new Vector3[skinnedMeshRenderer2.sharedMesh.tangents.Length];
			int num2 = 0;
			for (int n = 0; n < this.vertics_num.Length; n++)
			{
				bool flag3 = this.vertics_num[n] == array4.Length;
				if (flag3)
				{
					break;
				}
				num2 += this.vertics_num[n];
			}
			bool flag4 = num2 >= this.pmxFile.VertexList.Count;
			if (!flag4)
			{
				for (int num3 = 0; num3 < blendShapeCount2; num3++)
				{
					skinnedMeshRenderer2.sharedMesh.GetBlendShapeFrameVertices(num3, 0, array4, array5, array6);
					PmxMorph pmxMorph2 = new PmxMorph();
					pmxMorph2.Name = skinnedMeshRenderer2.sharedMesh.GetBlendShapeName(num3);
					pmxMorph2.NameE = "";
					pmxMorph2.Panel = 1;
					pmxMorph2.Kind = PmxMorph.OffsetKind.Vertex;
					for (int num4 = 0; num4 < array4.Length; num4++)
					{
						PmxVertexMorph pmxVertexMorph2 = new PmxVertexMorph(num2 + num4, new Vector3(-array4[num4].x, array4[num4].y, -array4[num4].z));
						pmxVertexMorph2.Offset *= (float)this.scale;
						pmxMorph2.OffsetList.Add(pmxVertexMorph2);
					}
					bool flag5 = true;
					for (int num5 = 0; num5 < this.pmxFile.MorphList.Count; num5++)
					{
						bool flag6 = this.pmxFile.MorphList[num5].Name.Equals(pmxMorph2.Name);
						if (flag6)
						{
							flag5 = false;
						}
					}
					bool flag7 = flag5;
					if (flag7)
					{
						this.pmxFile.MorphList.Add(pmxMorph2);
					}
				}
			}
		}
		fbstarget = instance.eyebrowCtrl.FBSTarget;
		for (int num6 = 0; num6 < fbstarget.Length; num6++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer3 = fbstarget[num6].GetSkinnedMeshRenderer();
			int blendShapeCount3 = skinnedMeshRenderer3.sharedMesh.blendShapeCount;
			Vector3[] array7 = new Vector3[skinnedMeshRenderer3.sharedMesh.vertices.Length];
			Vector3[] array8 = new Vector3[skinnedMeshRenderer3.sharedMesh.normals.Length];
			Vector3[] array9 = new Vector3[skinnedMeshRenderer3.sharedMesh.tangents.Length];
			int num7 = 0;
			for (int num8 = 0; num8 < this.vertics_num.Length; num8++)
			{
				bool flag8 = this.vertics_num[num8] == array7.Length;
				if (flag8)
				{
					break;
				}
				num7 += this.vertics_num[num8];
			}
			bool flag9 = num7 >= this.pmxFile.VertexList.Count;
			if (!flag9)
			{
				for (int num9 = 0; num9 < blendShapeCount3; num9++)
				{
					skinnedMeshRenderer3.sharedMesh.GetBlendShapeFrameVertices(num9, 0, array7, array8, array9);
					PmxMorph pmxMorph3 = new PmxMorph();
					pmxMorph3.Name = skinnedMeshRenderer3.sharedMesh.GetBlendShapeName(num9);
					pmxMorph3.NameE = "";
					pmxMorph3.Panel = 1;
					pmxMorph3.Kind = PmxMorph.OffsetKind.Vertex;
					for (int num10 = 0; num10 < array7.Length; num10++)
					{
						PmxVertexMorph pmxVertexMorph3 = new PmxVertexMorph(num7 + num10, new Vector3(-array7[num10].x, array7[num10].y, -array7[num10].z));
						pmxVertexMorph3.Offset *= (float)this.scale;
						pmxMorph3.OffsetList.Add(pmxVertexMorph3);
					}
					bool flag10 = true;
					for (int num11 = 0; num11 < this.pmxFile.MorphList.Count; num11++)
					{
						bool flag11 = this.pmxFile.MorphList[num11].Name.Equals(pmxMorph3.Name);
						if (flag11)
						{
							flag10 = false;
						}
					}
					bool flag12 = flag10;
					if (flag12)
					{
						this.pmxFile.MorphList.Add(pmxMorph3);
					}
				}
			}
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00005711 File Offset: 0x00003911
	private void setboneparent()
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00005714 File Offset: 0x00003914
	private void setmaterial()
	{
		for (int i = 0; i < this.pmxFile.MaterialList.Count; i++)
		{
			this.pmxFile.MaterialList[i].SetFlag(PmxMaterial.MaterialFlags.Edge, true);
			bool flag = this.pmxFile.MaterialList[i].Name.Contains("hair") || this.pmxFile.MaterialList[i].Name.Contains("ahoge") || this.pmxFile.MaterialList[i].Name.Contains("mayuge");
			if (flag)
			{
				this.pmxFile.MaterialList[i].Ambient = new Color(0f, 0f, 0f, 0f);
				this.pmxFile.MaterialList[i].Specular = new Color(1f, 1f, 1f, 0f);
			}
			else
			{
				this.pmxFile.MaterialList[i].Diffuse = new Color(1f, 1f, 1f, 1f);
				this.pmxFile.MaterialList[i].Specular = new Color(0f, 0f, 0f, 0f);
				this.pmxFile.MaterialList[i].Ambient = new Color(1f, 1f, 1f, 1f);
				bool flag2 = this.pmxFile.MaterialList[i].Name.Contains("shadowcast");
				if (flag2)
				{
					this.pmxFile.MaterialList[i].Diffuse = new Color(0f, 0f, 0f, 0f);
					this.pmxFile.MaterialList[i].SetFlag(PmxMaterial.MaterialFlags.Edge, false);
				}
			}
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00005930 File Offset: 0x00003B30
	private void setSkinnedMeshList()
	{
		this.skinnedMeshList = new List<SkinnedMeshRenderer>();
		SkinnedMeshRenderer[] array = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
		SkinnedMeshRenderer[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			this.skinnedMeshList.Add(array2[i]);
		}
		this.meshList = new List<MeshFilter>();
		MeshFilter[] array3 = Object.FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[];
		for (int j = 0; j < array3.Length; j++)
		{
			this.meshList.Add(array3[j]);
		}
		this.vertics_num = new int[array2.Length];
		this.vertics_name = new string[array2.Length];
		this.msg += "\n";
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00005A04 File Offset: 0x00003C04
	public void CreatePmxHeader()
	{
		PmxElementFormat pmxElementFormat = new PmxElementFormat(1f);
		pmxElementFormat.VertexSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.VertexList.Count);
		int num = int.MinValue;
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			num = Math.Max(num, Math.Abs(this.pmxFile.BoneList[i].IK.LinkList.Count));
		}
		num = Math.Max(num, this.pmxFile.BoneList.Count);
		pmxElementFormat.BoneSize = PmxElementFormat.GetSignedBufSize(num);
		bool flag = pmxElementFormat.BoneSize < 2;
		if (flag)
		{
			pmxElementFormat.BoneSize = 2;
		}
		pmxElementFormat.MorphSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.MorphList.Count);
		pmxElementFormat.MaterialSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.MaterialList.Count);
		pmxElementFormat.BodySize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.BodyList.Count);
		PmxHeader pmxHeader = new PmxHeader(2.1f);
		pmxHeader.FromElementFormat(pmxElementFormat);
		this.pmxFile.Header = pmxHeader;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00005B40 File Offset: 0x00003D40
	public void CreateModelInfo()
	{
		PmxModelInfo pmxModelInfo = new PmxModelInfo();
		pmxModelInfo.ModelName = "koikatu";
		pmxModelInfo.ModelNameE = "";
		pmxModelInfo.Comment = "exported koikatu";
		pmxModelInfo.Comment = "";
		this.pmxFile.ModelInfo = pmxModelInfo;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00005B94 File Offset: 0x00003D94
	private PmxVertex.BoneWeight[] ConvertBoneWeight(BoneWeight unityWeight, Transform[] bones)
	{
		PmxVertex.BoneWeight[] array = new PmxVertex.BoneWeight[4];
		bool flag = unityWeight.boneIndex0 >= 0 && unityWeight.boneIndex0 < bones.Length;
		if (flag)
		{
			array[0].Bone = this.sbi(bones[unityWeight.boneIndex0].name);
		}
		array[0].Value = unityWeight.weight0;
		bool flag2 = unityWeight.boneIndex1 >= 0 && unityWeight.boneIndex0 < bones.Length;
		if (flag2)
		{
			array[1].Bone = this.sbi(bones[unityWeight.boneIndex1].name);
		}
		array[1].Value = unityWeight.weight1;
		bool flag3 = unityWeight.boneIndex2 >= 0 && unityWeight.boneIndex0 < bones.Length;
		if (flag3)
		{
			array[2].Bone = this.sbi(bones[unityWeight.boneIndex2].name);
		}
		array[2].Value = unityWeight.weight2;
		bool flag4 = unityWeight.boneIndex3 >= 0 && unityWeight.boneIndex0 < bones.Length;
		if (flag4)
		{
			array[3].Bone = this.sbi(bones[unityWeight.boneIndex3].name);
		}
		array[3].Value = unityWeight.weight3;
		return array;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00005CF4 File Offset: 0x00003EF4
	private void AddFaceList(int[] faceList, int count)
	{
		for (int i = 0; i < faceList.Length; i++)
		{
			faceList[i] += count;
			this.pmxFile.FaceList.Add(faceList[i]);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00005D38 File Offset: 0x00003F38
	private Vector3 MultiplyVec3s(Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00005D78 File Offset: 0x00003F78
	private Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		return angle * (point - pivot) + pivot;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00005DA0 File Offset: 0x00003FA0
	public void CreateMeshList()
	{
		Transform transform = GameObject.Find("BodyTop").transform;
		SkinnedMeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.vertics_num[i] = componentsInChildren[i].sharedMesh.vertices.Length;
			this.vertics_name[i] = componentsInChildren[i].sharedMaterial.name;
			GameObject gameObject = componentsInChildren[i].gameObject;
			Mesh mesh = componentsInChildren[i].sharedMesh;
			BoneWeight[] boneWeights = mesh.boneWeights;
			Mesh mesh2 = new Mesh();
			componentsInChildren[i].BakeMesh(mesh2);
			mesh = mesh2;
			Vector2[] uv = mesh.uv;
			Vector2[] uv2 = mesh.uv2;
			Vector3[] normals = mesh.normals;
			Vector3[] vertices = mesh.vertices;
			for (int j = 0; j < mesh.subMeshCount; j++)
			{
				int[] triangles = mesh.GetTriangles(j);
				this.AddFaceList(triangles, this.vertexCount);
				this.CreateMaterial(componentsInChildren[i].sharedMaterials[j], triangles.Length);
			}
			this.vertexCount += mesh.vertexCount;
			for (int k = 0; k < mesh.vertexCount; k++)
			{
				PmxVertex pmxVertex = new PmxVertex();
				pmxVertex.UV = new Vector2(uv[k].x, -uv[k].y + 1f);
				pmxVertex.Weight = this.ConvertBoneWeight(boneWeights[k], componentsInChildren[i].bones);
				Vector3 vector = normals[k];
				vector = this.RotateAroundPoint(vector, Vector3.zero, gameObject.transform.rotation);
				pmxVertex.Normal = new Vector3(-vector.x, vector.y, -vector.z);
				Vector3 vector2 = vertices[k];
				vector2 = this.RotateAroundPoint(vector2, Vector3.zero, gameObject.transform.rotation);
				vector2 += gameObject.transform.position;
				pmxVertex.Position = new Vector3(-vector2.x * (float)this.scale, vector2.y * (float)this.scale, -vector2.z * (float)this.scale);
				pmxVertex.Deform = PmxVertex.DeformType.BDEF4;
				this.pmxFile.VertexList.Add(pmxVertex);
			}
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00006014 File Offset: 0x00004214
	private Vector3 TransToParent(Vector3 v, int index)
	{
		Transform transform = this.boneList[index];
		int num = -1;
		bool flag = this.bonesMap.ContainsKey(transform.parent);
		if (flag)
		{
			num = this.bonesMap[transform.parent];
		}
		bool flag2 = num != -1;
		if (flag2)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			v = (this.bindposeList[index] * this.boneList[num].worldToLocalMatrix.inverse).MultiplyVector(v);
			v = this.TransToParent(v, num);
		}
		return v;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000060B8 File Offset: 0x000042B8
	private Vector3 CalcPostion(Vector3 v, BoneWeight boneWeight, Transform[] bones)
	{
		Transform key = bones[boneWeight.boneIndex0];
		bool flag = this.bonesMap.ContainsKey(key);
		if (flag)
		{
			int index = this.bonesMap[key];
			v = this.TransToParent(v, index);
		}
		return v;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00006100 File Offset: 0x00004300
	public void CreateMaterial(Material material, int count)
	{
		PmxMaterial pmxMaterial = new PmxMaterial();
		pmxMaterial.Name = material.name;
		pmxMaterial.NameE = material.name;
		pmxMaterial.Flags = (PmxMaterial.MaterialFlags.DrawBoth | PmxMaterial.MaterialFlags.Shadow | PmxMaterial.MaterialFlags.SelfShadowMap | PmxMaterial.MaterialFlags.SelfShadow);
		bool flag = material.mainTexture != null;
		if (flag)
		{
			string text = material.name;
			bool flag2 = text.Contains("Instance");
			bool flag3 = flag2;
			if (flag3)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"_(",
					material.GetInstanceID(),
					")"
				});
			}
			pmxMaterial.Tex = text + ".png";
			Texture mainTexture = material.mainTexture;
			TextureWriter.WriteTexture2D(this.pass + pmxMaterial.Tex, mainTexture);
		}
		bool flag4 = material.HasProperty("_Color");
		if (flag4)
		{
			pmxMaterial.Diffuse = material.GetColor("_Color");
		}
		bool flag5 = material.HasProperty("_AmbColor");
		if (flag5)
		{
			pmxMaterial.Ambient = material.GetColor("_AmbColor");
		}
		bool flag6 = material.HasProperty("_Opacity");
		if (flag6)
		{
			pmxMaterial.Diffuse.a = material.GetFloat("_Opacity");
		}
		bool flag7 = material.HasProperty("_SpecularColor");
		if (flag7)
		{
			pmxMaterial.Specular = material.GetColor("_SpecularColor");
		}
		bool flag8 = material.HasProperty("_Shininess");
		if (flag8)
		{
		}
		bool flag9 = material.HasProperty("_OutlineColor");
		if (flag9)
		{
			pmxMaterial.EdgeSize = material.GetFloat("_OutlineWidth");
			pmxMaterial.EdgeColor = material.GetColor("_OutlineColor");
		}
		pmxMaterial.FaceCount = count;
		this.pmxFile.MaterialList.Add(pmxMaterial);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000062C4 File Offset: 0x000044C4
	public void CreateBoneList()
	{
		Transform transform = GameObject.Find("BodyTop").transform;
		List<Transform> list = new List<Transform>();
		Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			PmxBone pmxBone = new PmxBone();
			pmxBone.Name = componentsInChildren[i].name;
			pmxBone.Parent = list.IndexOf(componentsInChildren[i].parent);
			Vector3 vector = componentsInChildren[i].transform.position * (float)this.scale;
			pmxBone.Position = new Vector3(-vector.x, vector.y, -vector.z);
			list.Add(componentsInChildren[i]);
			this.pmxFile.BoneList.Add(pmxBone);
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00006392 File Offset: 0x00004592
	public void Save()
	{
		this.pmxFile.ToFile(this.pass + "model.pmx");
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000063B4 File Offset: 0x000045B4
	private int serchbone(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Name == name;
			if (flag)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00006410 File Offset: 0x00004610
	public void sortmaterial()
	{
		int num = 0;
		List<int>[] array = new List<int>[this.pmxFile.MaterialList.Count];
		for (int i = 0; i < this.pmxFile.MaterialList.Count; i++)
		{
			PmxMaterial pmxMaterial = this.pmxFile.MaterialList[i];
			bool flag = pmxMaterial.Name.Contains("SkinHi");
			if (flag)
			{
				num = i;
			}
			array[i] = new List<int>();
			for (int j = 0; j < pmxMaterial.FaceCount; j++)
			{
				array[i].Add(this.pmxFile.FaceList[j]);
			}
			this.pmxFile.FaceList.RemoveRange(0, pmxMaterial.FaceCount);
		}
		bool flag2 = num != 0 && num - 2 >= 0;
		if (flag2)
		{
			int num2 = num;
			List<int> list = array[num2];
			array[num2] = array[num2 - 2];
			array[num2 - 2] = list;
			PmxMaterial value = this.pmxFile.MaterialList[num2];
			this.pmxFile.MaterialList[num2] = this.pmxFile.MaterialList[num2 - 2];
			this.pmxFile.MaterialList[num2 - 2] = value;
		}
		for (int k = array.Length - 1; k >= 0; k--)
		{
			for (int l = 0; l < array[k].Count; l++)
			{
				this.pmxFile.FaceList.Add(array[k][l]);
			}
		}
		for (int m = 0; m < this.pmxFile.MaterialList.Count; m++)
		{
			PmxMaterial item = this.pmxFile.MaterialList[m];
			this.pmxFile.MaterialList.RemoveAt(m);
			this.pmxFile.MaterialList.Insert(0, item);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00006624 File Offset: 0x00004824
	private void setParent()
	{
		for (int i = 0; i < this.skinnedMeshList.Count; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = this.skinnedMeshList[i];
			for (int j = 0; j < skinnedMeshRenderer.bones.Length; j++)
			{
				PmxBone pmxBone = this.serchBone(skinnedMeshRenderer.bones[j].name);
				bool flag = pmxBone == null;
				if (!flag)
				{
					int parent = this.serchBonei(skinnedMeshRenderer.bones[j].parent.name);
					pmxBone.Parent = parent;
				}
			}
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000066C0 File Offset: 0x000048C0
	private int serchBonei(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Name == name;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00006718 File Offset: 0x00004918
	private PmxBone serchBone(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Name == name;
			if (flag)
			{
				return this.pmxFile.BoneList[i];
			}
		}
		return null;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00006780 File Offset: 0x00004980
	private void sortbone(int boneindex, int index)
	{
		for (int i = 0; i < this.pmxFile.VertexList.Count; i++)
		{
			for (int j = 0; j < this.pmxFile.VertexList[i].Weight.Length; j++)
			{
				bool flag = index >= this.pmxFile.VertexList[i].Weight[j].Bone && this.pmxFile.VertexList[i].Weight[j].Bone > boneindex;
				if (flag)
				{
					PmxVertex.BoneWeight[] weight = this.pmxFile.VertexList[i].Weight;
					int num = j;
					weight[num].Bone = weight[num].Bone - 1;
				}
				else
				{
					bool flag2 = this.pmxFile.VertexList[i].Weight[j].Bone == boneindex;
					if (flag2)
					{
						this.pmxFile.VertexList[i].Weight[j].Bone = index;
					}
				}
			}
		}
		for (int k = 0; k < this.pmxFile.BoneList.Count; k++)
		{
			bool flag3 = index >= this.pmxFile.BoneList[k].Parent && this.pmxFile.BoneList[k].Parent > boneindex;
			if (flag3)
			{
				this.pmxFile.BoneList[k].Parent--;
			}
			else
			{
				bool flag4 = this.pmxFile.BoneList[k].Parent == boneindex;
				if (flag4)
				{
					this.pmxFile.BoneList[k].Parent = index;
				}
			}
			bool flag5 = index >= this.pmxFile.BoneList[k].To_Bone && this.pmxFile.BoneList[k].To_Bone > boneindex;
			if (flag5)
			{
				this.pmxFile.BoneList[k].To_Bone--;
			}
			else
			{
				bool flag6 = this.pmxFile.BoneList[k].To_Bone == boneindex;
				if (flag6)
				{
					this.pmxFile.BoneList[k].To_Bone = index;
				}
			}
		}
		PmxBone item = this.pmxFile.BoneList[boneindex];
		this.pmxFile.BoneList.RemoveAt(boneindex);
		this.pmxFile.BoneList.Insert(index, item);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00006A48 File Offset: 0x00004C48
	public void addbone()
	{
		this.insertbone(0, new PmxBone
		{
			Name = "全ての親",
			Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible)
		});
		this.insertbone(1, new PmxBone
		{
			Name = "センター",
			Position = new Vector3(0f, this.sb("下半身").Position.Y * 0.75f, 0f),
			Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible)
		});
		PmxBone pmxBone = this.sb("BodyTop");
		pmxBone.Parent = this.sbi("センター");
		bool flag = this.sb("上半身") != null;
		if (flag)
		{
			pmxBone = new PmxBone();
			pmxBone.Name = "左つま先";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左足首")) + 1, pmxBone);
			pmxBone = new PmxBone();
			pmxBone.Name = "左足ＩＫ";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左つま先")) + 1, pmxBone);
			pmxBone = new PmxBone();
			pmxBone.Name = "左つま先ＩＫ";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左足ＩＫ")) + 1, pmxBone);
			pmxBone = new PmxBone();
			pmxBone.Name = "右つま先";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右足首")) + 1, pmxBone);
			pmxBone = new PmxBone();
			pmxBone.Name = "右足ＩＫ";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右つま先")) + 1, pmxBone);
			pmxBone = new PmxBone();
			pmxBone.Name = "右つま先ＩＫ";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右足ＩＫ")) + 1, pmxBone);
		}
		bool flag2 = this.sb("頭") != null;
		if (flag2)
		{
			pmxBone = new PmxBone();
			pmxBone.Name = "両目x";
			this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("頭")) + 1, pmxBone);
		}
		bool flag3 = this.sb("上半身") != null;
		if (flag3)
		{
			pmxBone = this.sb("左足首");
			pmxBone.Parent = this.sbi("左ひざ");
			pmxBone = this.sb("右足首");
			pmxBone.Parent = this.sbi("右ひざ");
			pmxBone = this.sb("左つま先");
			pmxBone.Position = this.sb("cf_j_toes_L").Position;
			pmxBone.To_Bone = this.sb("cf_j_toes_L").To_Bone;
			pmxBone.Flags = (PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
			pmxBone.Parent = this.sbi("左足首");
			pmxBone = this.sb("左足ＩＫ");
			pmxBone.Position = this.sb("左足首").Position;
			pmxBone.To_Offset = new Vector3(0f, -1.3f, 0f);
			pmxBone.Parent = this.sbi("全ての親");
			pmxBone.Flags = (PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK);
			pmxBone.IK.Angle = 2f;
			pmxBone.IK.LoopCount = 40;
			pmxBone.IK.Target = this.sbi("左足首");
			PmxIK.IKLink iklink = new PmxIK.IKLink();
			iklink.Bone = this.sbi("左ひざ");
			iklink.IsLimit = true;
			iklink.High = new Vector3(-0.008726646f, 0f, 0f);
			iklink.Low = new Vector3(-3.14159274f, 0f, 0f);
			pmxBone.IK.LinkList.Add(iklink);
			PmxIK.IKLink iklink2 = new PmxIK.IKLink();
			iklink2.Bone = this.sbi("左足");
			pmxBone.IK.LinkList.Add(iklink2);
			pmxBone = this.sb("左つま先ＩＫ");
			pmxBone.Position = this.sb("左つま先").Position;
			pmxBone.To_Offset = new Vector3(0f, -1.3081f, 0f);
			pmxBone.Parent = this.sbi("左足ＩＫ");
			pmxBone.Flags = (PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK);
			pmxBone.IK.Angle = 4f;
			pmxBone.IK.LoopCount = 3;
			pmxBone.IK.Target = this.sbi("左つま先");
			iklink = new PmxIK.IKLink();
			iklink.Bone = this.sbi("左足首");
			pmxBone.IK.LinkList.Add(iklink);
			pmxBone = this.sb("右つま先");
			pmxBone.Position = this.sb("cf_j_toes_R").Position;
			pmxBone.To_Bone = this.sb("cf_j_toes_R").To_Bone;
			pmxBone.Flags = (PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable);
			pmxBone.Parent = this.sbi("右足首");
			pmxBone = this.sb("右足ＩＫ");
			pmxBone.Position = this.sb("右足首").Position;
			pmxBone.To_Offset = new Vector3(0f, -1.3f, 0f);
			pmxBone.Parent = this.sbi("全ての親");
			pmxBone.Flags = (PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK);
			pmxBone.IK.Angle = 2f;
			pmxBone.IK.LoopCount = 40;
			pmxBone.IK.Target = this.sbi("右足首");
			iklink = new PmxIK.IKLink();
			iklink.Bone = this.sbi("右ひざ");
			iklink.IsLimit = true;
			iklink.High = new Vector3(-0.008726646f, 0f, 0f);
			iklink.Low = new Vector3(-3.14159274f, 0f, 0f);
			pmxBone.IK.LinkList.Add(iklink);
			iklink2 = new PmxIK.IKLink();
			iklink2.Bone = this.sbi("右足");
			pmxBone.IK.LinkList.Add(iklink2);
			pmxBone = this.sb("右つま先ＩＫ");
			pmxBone.Position = this.sb("右つま先").Position;
			pmxBone.To_Offset = new Vector3(0f, -1.3081f, 0f);
			pmxBone.Parent = this.sbi("右足ＩＫ");
			pmxBone.Flags = (PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK);
			pmxBone.IK.Angle = 4f;
			pmxBone.IK.LoopCount = 3;
			pmxBone.IK.Target = this.sbi("右つま先");
			iklink = new PmxIK.IKLink();
			iklink.Bone = this.sbi("右足首");
			pmxBone.IK.LinkList.Add(iklink);
		}
		bool flag4 = this.sb("頭") != null;
		if (flag4)
		{
			pmxBone = this.sb("両目x");
			pmxBone.Position = new Vector3(0f, this.sb("頭").Position.Y + 3f, 0f);
			pmxBone.To_Offset = new Vector3(0f, 0f, -1f);
			pmxBone.Parent = this.sbi("頭");
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000071A8 File Offset: 0x000053A8
	private void insertbone(int index, PmxBone b)
	{
		for (int i = 0; i < this.pmxFile.VertexList.Count; i++)
		{
			for (int j = 0; j < this.pmxFile.VertexList[i].Weight.Length; j++)
			{
				bool flag = this.pmxFile.VertexList[i].Weight[j].Bone >= index;
				if (flag)
				{
					PmxVertex.BoneWeight[] weight = this.pmxFile.VertexList[i].Weight;
					int num = j;
					weight[num].Bone = weight[num].Bone + 1;
				}
			}
		}
		for (int k = 0; k < this.pmxFile.BoneList.Count; k++)
		{
			bool flag2 = this.pmxFile.BoneList[k].Parent >= index;
			if (flag2)
			{
				this.pmxFile.BoneList[k].Parent++;
			}
			bool flag3 = this.pmxFile.BoneList[k].To_Bone >= index;
			if (flag3)
			{
				this.pmxFile.BoneList[k].To_Bone++;
			}
		}
		this.pmxFile.BoneList.Insert(index, b);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00007324 File Offset: 0x00005524
	private PmxBone sb(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Name.Equals(name);
			if (flag)
			{
				return this.pmxFile.BoneList[i];
			}
		}
		return null;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00007390 File Offset: 0x00005590
	private void cn(PmxBone b, string name)
	{
		bool flag = b == null;
		if (!flag)
		{
			b.Name = name;
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000073B0 File Offset: 0x000055B0
	private int sbi(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Name == name;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00007408 File Offset: 0x00005608
	private void sortbone2(int boneindex, int index)
	{
		for (int i = 0; i < this.pmxFile.VertexList.Count; i++)
		{
			for (int j = 0; j < this.pmxFile.VertexList[i].Weight.Length; j++)
			{
				bool flag = index <= this.pmxFile.VertexList[i].Weight[j].Bone && this.pmxFile.VertexList[i].Weight[j].Bone < boneindex;
				if (flag)
				{
					PmxVertex.BoneWeight[] weight = this.pmxFile.VertexList[i].Weight;
					int num = j;
					weight[num].Bone = weight[num].Bone + 1;
				}
				else
				{
					bool flag2 = this.pmxFile.VertexList[i].Weight[j].Bone == boneindex;
					if (flag2)
					{
						this.pmxFile.VertexList[i].Weight[j].Bone = index;
					}
				}
			}
		}
		for (int k = 0; k < this.pmxFile.BoneList.Count; k++)
		{
			bool flag3 = index <= this.pmxFile.BoneList[k].Parent && this.pmxFile.BoneList[k].Parent < boneindex;
			if (flag3)
			{
				this.pmxFile.BoneList[k].Parent++;
			}
			else
			{
				bool flag4 = this.pmxFile.BoneList[k].Parent == boneindex;
				if (flag4)
				{
					this.pmxFile.BoneList[k].Parent = index;
				}
			}
			bool flag5 = index <= this.pmxFile.BoneList[k].To_Bone && this.pmxFile.BoneList[k].To_Bone < boneindex;
			if (flag5)
			{
				this.pmxFile.BoneList[k].To_Bone++;
			}
			else
			{
				bool flag6 = this.pmxFile.BoneList[k].To_Bone == boneindex;
				if (flag6)
				{
					this.pmxFile.BoneList[k].To_Bone = index;
				}
			}
		}
		PmxBone item = this.pmxFile.BoneList[boneindex];
		this.pmxFile.BoneList.RemoveAt(boneindex);
		this.pmxFile.BoneList.Insert(index, item);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000076D0 File Offset: 0x000058D0
	private void changeboneinfo()
	{
		bool flag = this.sb("頭") != null;
		PmxBone pmxBone;
		if (flag)
		{
			pmxBone = this.sb("左目x");
			bool flag2 = pmxBone != null;
			if (flag2)
			{
				pmxBone.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.AppendRotation);
				pmxBone.AppendRatio = 0.5f;
				pmxBone.AppendParent = this.sbi("両目x");
			}
			pmxBone = this.sb("右目x");
			bool flag3 = pmxBone != null;
			if (flag3)
			{
				pmxBone.Flags = (PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.AppendRotation);
				pmxBone.AppendRatio = 0.5f;
				pmxBone.AppendParent = this.sbi("両目x");
			}
		}
		pmxBone = this.sb("センター");
		pmxBone.Parent = this.sbi("全ての親");
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000778C File Offset: 0x0000598C
	private PmxMorph sm(string name)
	{
		for (int i = 0; i < this.pmxFile.MorphList.Count; i++)
		{
			bool flag = this.pmxFile.MorphList[i].Name.Equals(name);
			if (flag)
			{
				return this.pmxFile.MorphList[i];
			}
		}
		return null;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000077F8 File Offset: 0x000059F8
	private int smi(string name)
	{
		for (int i = 0; i < this.pmxFile.MorphList.Count; i++)
		{
			bool flag = this.pmxFile.MorphList[i].Name.Equals(name);
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00007854 File Offset: 0x00005A54
	public void addphysics()
	{
		List<PmxBone> list = new List<PmxBone>();
		List<PmxBone> list2 = new List<PmxBone>();
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = !this.pmxFile.BoneList[i].Name.Contains("top") && this.pmxFile.BoneList[i].Name.Contains("cf_J_hair");
			if (flag)
			{
				list.Add(this.pmxFile.BoneList[i]);
			}
			bool flag2 = this.pmxFile.BoneList[i].Name.Contains("cf_j_sk");
			if (flag2)
			{
				list2.Add(this.pmxFile.BoneList[i]);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			bool flag3 = list[j].Parent == -1;
			if (!flag3)
			{
				PmxBone pmxBone = this.pmxFile.BoneList[list[j].Parent];
				Vector3 vector = default(Vector3);
				int num = this.serchchild(list[j].Name);
				bool flag4 = num != -1;
				if (flag4)
				{
					vector = this.pmxFile.BoneList[num].Position;
				}
				else
				{
					vector = new Vector3(0f, -0.5f, 0f) + list[j].Position;
				}
				PmxBody pmxBody = new PmxBody();
				pmxBody.Name = list[j].Name;
				pmxBody.Bone = this.sbi(list[j].Name);
				pmxBody.Position = (list[j].Position + vector) / 2f;
				pmxBody.BoxType = PmxBody.BoxKind.Capsule;
				pmxBody.Group = 2;
				pmxBody.Mass = 1f;
				pmxBody.PositionDamping = 0.9f;
				pmxBody.RotationDamping = 0.99f;
				PmxBodyPassGroup pmxBodyPassGroup = new PmxBodyPassGroup();
				pmxBodyPassGroup.Flags[2] = true;
				pmxBody.PassGroup = pmxBodyPassGroup;
				pmxBody.Mode = PmxBody.ModeType.Dynamic;
				bool flag5 = pmxBone.Name.Equals("cf_J_hairF_top") || pmxBone.Name.Equals("cf_J_hairB_top");
				if (flag5)
				{
					pmxBody.Mode = PmxBody.ModeType.Static;
				}
				pmxBody.BoxSize = new Vector3(0.2f, this.getDistance(list[j].Position, vector) / 2f, 0f);
				pmxBody.Rotation = this.getDirection(list[j].Position, vector);
				this.pmxFile.BodyList.Add(pmxBody);
				bool flag6 = this.sbdi(pmxBone.Name) == -1;
				if (flag6)
				{
					pmxBody.Mode = PmxBody.ModeType.Static;
				}
				else
				{
					bool flag7 = pmxBone.Name.Equals("cf_J_hairF_top") || pmxBone.Name.Equals("cf_J_hairB_top");
					if (!flag7)
					{
						PmxJoint pmxJoint = new PmxJoint();
						pmxJoint.Name = list[j].Name;
						pmxJoint.Position = list[j].Position;
						pmxJoint.Rotation = pmxBody.Rotation;
						pmxJoint.BodyA = this.sbdi(pmxBone.Name);
						pmxJoint.BodyB = this.sbdi(pmxBody.Name);
						pmxJoint.Limit_AngleLow = new Vector3(-0.17453292f, -0.08726646f, -0.17453292f);
						pmxJoint.Limit_AngleHigh = new Vector3(0.17453292f, 0.08726646f, 0.17453292f);
						this.pmxFile.JointList.Add(pmxJoint);
					}
				}
			}
		}
		PmxBone pmxBone2 = this.sb("下半身");
		PmxBody pmxBody2 = new PmxBody();
		pmxBody2.Name = pmxBone2.Name;
		pmxBody2.Bone = this.sbi(pmxBone2.Name);
		pmxBody2.Position = new Vector3(0f, this.sb("下半身").Position.y, 0.6f);
		Quaternion quaternion = Quaternion.Euler(0f, 0f, 1.57079637f);
		pmxBody2.Rotation = new Vector3(0f, 0f, 1.57079637f);
		pmxBody2.BoxType = PmxBody.BoxKind.Capsule;
		pmxBody2.BoxSize = new Vector3(1f, 1.3f, 0f);
		this.pmxFile.BodyList.Add(pmxBody2);
		for (int k = 0; k < list2.Count; k++)
		{
			PmxBone pmxBone3 = this.pmxFile.BoneList[list2[k].Parent];
			Vector3 vector2 = default(Vector3);
			int num2 = this.serchchild(list2[k].Name);
			bool flag8 = num2 != -1;
			if (flag8)
			{
				vector2 = this.pmxFile.BoneList[num2].Position;
			}
			else
			{
				vector2 = new Vector3(0f, -0.5f, 0f) + list2[k].Position;
			}
			PmxBody pmxBody3 = new PmxBody();
			pmxBody3.Name = list2[k].Name;
			pmxBody3.Bone = this.sbi(list2[k].Name);
			pmxBody3.Position = (list2[k].Position + vector2) / 2f;
			pmxBody3.BoxType = PmxBody.BoxKind.Capsule;
			pmxBody3.Group = 3;
			pmxBody3.Mass = 1f;
			pmxBody3.PositionDamping = 0.99f;
			pmxBody3.RotationDamping = 0.99f;
			PmxBodyPassGroup pmxBodyPassGroup2 = new PmxBodyPassGroup();
			pmxBodyPassGroup2.Flags[3] = true;
			pmxBody3.PassGroup = pmxBodyPassGroup2;
			pmxBody3.Mode = PmxBody.ModeType.Dynamic;
			bool flag9 = pmxBone3.Name == "cf_s_waist01" || pmxBone3.Name == "下半身" || pmxBone3.Name.Contains("cf_d_sk");
			if (flag9)
			{
				pmxBody3.Mode = PmxBody.ModeType.Static;
			}
			pmxBody3.BoxSize = new Vector3(0.2f, this.getDistance(list2[k].Position, vector2) / 2f, 0f);
			pmxBody3.Rotation = this.getDirection(list2[k].Position, vector2);
			this.pmxFile.BodyList.Add(pmxBody3);
			bool flag10 = pmxBone3.Name == "cf_s_waist01" || pmxBone3.Name == "下半身" || pmxBone3.Name.Contains("cf_d_sk");
			if (!flag10)
			{
				PmxJoint pmxJoint2 = new PmxJoint();
				pmxJoint2.Name = list2[k].Name;
				pmxJoint2.Position = list2[k].Position;
				pmxJoint2.Rotation = pmxBody3.Rotation;
				pmxJoint2.BodyA = this.sbdi(pmxBone3.Name);
				pmxJoint2.BodyB = this.sbdi(pmxBody3.Name);
				pmxJoint2.Limit_AngleLow = new Vector3(-0.5235988f, -0.2617994f, -0.5235988f);
				pmxJoint2.Limit_AngleHigh = new Vector3(0.5235988f, 0.2617994f, 0.5235988f);
				this.pmxFile.JointList.Add(pmxJoint2);
			}
		}
		int num3 = 0;
		for (int l = 0; l < list2.Count; l++)
		{
			int num4 = int.Parse(list2[l].Name[9].ToString() ?? "");
			bool flag11 = num3 < num4;
			if (flag11)
			{
				num3 = num4;
			}
		}
		for (int m = 0; m < list2.Count; m++)
		{
			int num5 = int.Parse(list2[m].Name[9].ToString() ?? "");
			int num6 = int.Parse(list2[m].Name[12].ToString() ?? "");
			PmxBone pmxBone4 = null;
			int num7 = 0;
			bool flag12 = num5 != num3;
			if (flag12)
			{
				num7 = num5 + 1;
			}
			for (int n = 0; n < list2.Count; n++)
			{
				bool flag13 = int.Parse(list2[n].Name[9].ToString() ?? "") == num7;
				if (flag13)
				{
					bool flag14 = int.Parse(list2[n].Name[12].ToString() ?? "") == num6;
					if (flag14)
					{
						pmxBone4 = list2[n];
					}
				}
			}
			bool flag15 = pmxBone4 == null;
			if (!flag15)
			{
				PmxJoint pmxJoint3 = new PmxJoint();
				pmxJoint3.Name = list2[m].Name + "[side]";
				pmxJoint3.Position = list2[m].Position;
				pmxJoint3.Rotation = this.pmxFile.BodyList[this.sbdi(list2[m].Name)].Rotation;
				pmxJoint3.BodyA = this.sbdi(list2[m].Name);
				pmxJoint3.BodyB = this.sbdi(pmxBone4.Name);
				pmxJoint3.Limit_MoveLow = new Vector3(0f, 0f, 0f);
				pmxJoint3.Limit_MoveHigh = new Vector3(0f, 0f, 0f);
				pmxJoint3.Limit_AngleLow = new Vector3(-0.5235988f, -0.2617994f, -0.5235988f);
				pmxJoint3.Limit_AngleHigh = new Vector3(0.5235988f, 0.2617994f, 0.5235988f);
				this.pmxFile.JointList.Add(pmxJoint3);
			}
		}
		pmxBone2 = this.sb("右足");
		pmxBone2.To_Bone = this.sbi("右ひざ");
		PmxBody pmxBody4 = new PmxBody();
		pmxBody4.Name = pmxBone2.Name;
		pmxBody4.Bone = this.sbi(pmxBone2.Name);
		bool flag16 = pmxBone2.To_Bone != -1;
		if (flag16)
		{
			pmxBody4.Position = (pmxBone2.Position + this.pmxFile.BoneList[pmxBone2.To_Bone].Position) / 2f;
		}
		else
		{
			pmxBody4.Position = pmxBone2.Position + pmxBone2.To_Offset / 2f;
		}
		pmxBody4.BoxType = PmxBody.BoxKind.Capsule;
		pmxBody4.BoxSize = new Vector3(0.8f, this.getDistance(this.pmxFile.BoneList[pmxBone2.To_Bone].Position, pmxBone2.Position) / 2f * 1.5f, 0f);
		pmxBody4.Rotation = this.getDirection(this.pmxFile.BoneList[pmxBone2.To_Bone].Position, pmxBone2.Position);
		this.pmxFile.BodyList.Add(pmxBody4);
		pmxBone2 = this.sb("左足");
		pmxBone2.To_Bone = this.sbi("左ひざ");
		pmxBody4 = new PmxBody();
		pmxBody4.Name = pmxBone2.Name;
		pmxBody4.Bone = this.sbi(pmxBone2.Name);
		bool flag17 = pmxBone2.To_Bone != -1;
		if (flag17)
		{
			pmxBody4.Position = (pmxBone2.Position + this.pmxFile.BoneList[pmxBone2.To_Bone].Position) / 2f;
		}
		else
		{
			pmxBody4.Position = pmxBone2.Position + pmxBone2.To_Offset / 2f;
		}
		pmxBody4.BoxType = PmxBody.BoxKind.Capsule;
		pmxBody4.BoxSize = new Vector3(0.8f, this.getDistance(this.pmxFile.BoneList[pmxBone2.To_Bone].Position, pmxBone2.Position) / 2f * 1.5f, 0f);
		pmxBody4.Rotation = this.getDirection(this.pmxFile.BoneList[pmxBone2.To_Bone].Position, pmxBone2.Position);
		this.pmxFile.BodyList.Add(pmxBody4);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00008568 File Offset: 0x00006768
	private int serchchild(string name)
	{
		for (int i = 0; i < this.pmxFile.BoneList.Count; i++)
		{
			bool flag = this.pmxFile.BoneList[i].Parent == -1;
			if (!flag)
			{
				bool flag2 = this.pmxFile.BoneList[this.pmxFile.BoneList[i].Parent].Name == name;
				if (flag2)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000085F8 File Offset: 0x000067F8
	private int sbdi(string name)
	{
		for (int i = 0; i < this.pmxFile.BodyList.Count; i++)
		{
			bool flag = this.pmxFile.BodyList[i].Name == name;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00008650 File Offset: 0x00006850
	private Vector3 getDirection(Vector3 first, Vector3 last)
	{
		Vector3 result = default(Vector3);
		float num = (float)Math.Sqrt((double)((last.X - first.X) * (last.X - first.X) + (last.Z - first.Z) * (last.Z - first.Z)));
		result.X = (float)(Math.Atan2((double)(last.Y - first.Y), (double)num) + 1.5707963267948966);
		result.Y = -(float)(Math.Atan2((double)(last.Z - first.Z), (double)(last.X - first.X)) + 1.5707963267948966);
		return result;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00008718 File Offset: 0x00006918
	private float getDistance(Vector3 one, Vector3 two)
	{
		return (float)Math.Sqrt((double)((one.X - two.X) * (one.X - two.X) + (one.Y - two.Y) * (one.Y - two.Y) + (one.Z - two.Z) * (one.Z - two.Z)));
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00008794 File Offset: 0x00006994
	private void addAccessory()
	{
		Transform transform = GameObject.Find("BodyTop").transform;
		MeshFilter[] componentsInChildren = transform.GetComponentsInChildren<MeshFilter>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = componentsInChildren[i].gameObject;
			Mesh sharedMesh = componentsInChildren[i].sharedMesh;
			BoneWeight[] boneWeights = sharedMesh.boneWeights;
			Vector2[] uv = sharedMesh.uv;
			Vector2[] uv2 = sharedMesh.uv2;
			Vector3[] normals = sharedMesh.normals;
			Vector3[] vertices = sharedMesh.vertices;
			for (int j = 0; j < sharedMesh.subMeshCount; j++)
			{
				int[] triangles = sharedMesh.GetTriangles(j);
				this.AddFaceList(triangles, this.vertexCount);
				MeshRenderer meshRenderer = componentsInChildren[i].gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
				this.CreateMaterial(meshRenderer.sharedMaterials[j], triangles.Length);
			}
			this.vertexCount += sharedMesh.vertexCount;
			PmxBone pmxBone = new PmxBone();
			pmxBone.Name = componentsInChildren[i].name;
			pmxBone.Parent = this.sbi(componentsInChildren[i].name);
			Vector3 vector = componentsInChildren[i].transform.position * (float)this.scale;
			pmxBone.Position = new Vector3(-vector.x, vector.y, -vector.z);
			this.pmxFile.BoneList.Add(pmxBone);
			for (int k = 0; k < sharedMesh.vertexCount; k++)
			{
				PmxVertex pmxVertex = new PmxVertex();
				pmxVertex.UV = new Vector2(uv[k].x, -uv[k].y + 1f);
				pmxVertex.Weight = new PmxVertex.BoneWeight[4];
				pmxVertex.Weight[0].Bone = this.pmxFile.BoneList.Count - 1;
				pmxVertex.Weight[0].Value = 1f;
				Vector3 vector2 = normals[k];
				vector2 = this.RotateAroundPoint(vector2, Vector3.zero, gameObject.transform.rotation);
				pmxVertex.Normal = new Vector3(-vector2.x, vector2.y, -vector2.z);
				vector = vertices[k];
				vector = this.MultiplyVec3s(vector, gameObject.transform.lossyScale);
				vector = this.RotateAroundPoint(vector, Vector3.zero, gameObject.transform.rotation);
				vector += gameObject.transform.position;
				pmxVertex.Position = new Vector3(-vector.x * (float)this.scale, vector.y * (float)this.scale, -vector.z * (float)this.scale);
				pmxVertex.Deform = PmxVertex.DeformType.BDEF4;
				this.pmxFile.VertexList.Add(pmxVertex);
			}
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00008AA0 File Offset: 0x00006CA0
	public void phymune()
	{
		PmxBody pmxBody = new PmxBody();
		pmxBody.Name = "左胸操作";
		pmxBody.Bone = this.sbi("左胸操作");
		pmxBody.Position = this.sb("左胸操作").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Box;
		pmxBody.Group = 13;
		pmxBody.Mass = 1f;
		pmxBody.PositionDamping = 0.5f;
		pmxBody.RotationDamping = 0.5f;
		PmxBodyPassGroup pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Static;
		pmxBody.BoxSize = new Vector3(0.1f, 0.1f, 0.1f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "左AH1";
		pmxBody.Bone = this.sbi("左AH1");
		pmxBody.Position = this.sb("左AH1").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.4f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.3f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "左AH2";
		pmxBody.Bone = this.sbi("左AH2");
		pmxBody.Position = this.sb("左AH2").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.3f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "左胸操作接続";
		pmxBody.Bone = -1;
		pmxBody.Position = (this.sb("左AH2").Position + this.sb("左AH1").Position) / 2f;
		pmxBody.BoxType = PmxBody.BoxKind.Capsule;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.1f, 1.515f, 0f);
		pmxBody.Rotation = new Vector3(0f, 1.57079637f, -1.57079637f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "左胸操作衝突";
		pmxBody.Bone = -1;
		pmxBody.Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f);
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 7;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.4f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "右胸操作";
		pmxBody.Bone = this.sbi("右胸操作");
		pmxBody.Position = this.sb("右胸操作").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Box;
		pmxBody.Group = 13;
		pmxBody.Mass = 1f;
		pmxBody.PositionDamping = 0.5f;
		pmxBody.RotationDamping = 0.5f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Static;
		pmxBody.BoxSize = new Vector3(0.1f, 0.1f, 0.1f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "右AH1";
		pmxBody.Bone = this.sbi("右AH1");
		pmxBody.Position = this.sb("右AH1").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.4f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.3f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "右AH2";
		pmxBody.Bone = this.sbi("右AH2");
		pmxBody.Position = this.sb("右AH2").Position;
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.3f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "右胸操作接続";
		pmxBody.Bone = -1;
		pmxBody.Position = (this.sb("右AH2").Position + this.sb("右AH1").Position) / 2f;
		pmxBody.BoxType = PmxBody.BoxKind.Capsule;
		pmxBody.Group = 13;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[1] = true;
		pmxBodyPassGroup.Flags[2] = true;
		pmxBodyPassGroup.Flags[3] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.1f, 1.515f, 0f);
		pmxBody.Rotation = new Vector3(0f, 1.57079637f, -1.57079637f);
		this.pmxFile.BodyList.Add(pmxBody);
		pmxBody = new PmxBody();
		pmxBody.Name = "右胸操作衝突";
		pmxBody.Bone = -1;
		pmxBody.Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f);
		pmxBody.BoxType = PmxBody.BoxKind.Sphere;
		pmxBody.Group = 7;
		pmxBody.Mass = 0.1f;
		pmxBody.PositionDamping = 0.9999f;
		pmxBody.RotationDamping = 0.99f;
		pmxBodyPassGroup = new PmxBodyPassGroup();
		pmxBodyPassGroup.Flags[0] = true;
		pmxBodyPassGroup.Flags[13] = true;
		pmxBody.PassGroup = pmxBodyPassGroup;
		pmxBody.Mode = PmxBody.ModeType.Dynamic;
		pmxBody.BoxSize = new Vector3(0.4f, 0f, 0f);
		this.pmxFile.BodyList.Add(pmxBody);
		PmxJoint pmxJoint = new PmxJoint();
		pmxJoint.Name = "左胸操作着衣用";
		pmxJoint.Position = (this.sb("左AH2").Position + this.sb("右AH2").Position) / 2f;
		pmxJoint.BodyA = this.sbdi("左AH2");
		pmxJoint.BodyB = this.sbdi("右AH2");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint.Limit_MoveLow = new Vector3(0f, 0f, 0f);
		pmxJoint.Limit_MoveHigh = new Vector3(0f, 0f, 0f);
		pmxJoint.Limit_AngleLow = new Vector3(0f, 0f, 0f);
		pmxJoint.Limit_AngleHigh = new Vector3(0f, 0f, 0f);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左胸操作着衣用-";
		pmxJoint.Position = (this.sb("左AH2").Position + this.sb("右AH2").Position) / 2f;
		pmxJoint.BodyB = this.sbdi("左AH2");
		pmxJoint.BodyA = this.sbdi("右AH2");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左胸操作調整用";
		pmxJoint.Position = this.sb("左胸操作").Position;
		pmxJoint.BodyA = this.sbdi("左胸操作");
		pmxJoint.BodyB = this.sbdi("左胸操作接続");
		pmxJoint.Limit_MoveHigh = new Vector3(0f, 0.3f, 0f);
		pmxJoint.Limit_AngleLow = new Vector3(-0.87266463f, -0.87266463f, -0.87266463f);
		pmxJoint.Limit_AngleHigh = new Vector3(0.87266463f, 0.87266463f, 0.87266463f);
		pmxJoint.SpConst_Move = new Vector3(0f, 20f, 0f);
		pmxJoint.SpConst_Rotate = new Vector3(100f, 100f, 100f);
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左胸操作接続";
		pmxJoint.Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f);
		pmxJoint.BodyA = this.sbdi("左胸操作接続");
		pmxJoint.BodyB = this.sbdi("左胸操作衝突");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左AH1";
		pmxJoint.Position = this.sb("左AH1").Position;
		pmxJoint.BodyA = this.sbdi("左AH1");
		pmxJoint.BodyB = this.sbdi("左胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左AH2";
		pmxJoint.Position = this.sb("左AH2").Position;
		pmxJoint.BodyA = this.sbdi("左AH2");
		pmxJoint.BodyB = this.sbdi("左胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左胸操作接続";
		pmxJoint.Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f);
		pmxJoint.BodyB = this.sbdi("左胸操作接続");
		pmxJoint.BodyA = this.sbdi("左胸操作衝突");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左AH1";
		pmxJoint.Position = this.sb("左AH1").Position;
		pmxJoint.BodyB = this.sbdi("左AH1");
		pmxJoint.BodyA = this.sbdi("左胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "左AH2";
		pmxJoint.Position = this.sb("左AH2").Position;
		pmxJoint.BodyB = this.sbdi("左AH2");
		pmxJoint.BodyA = this.sbdi("左胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右胸操作調整用";
		pmxJoint.Position = this.sb("右胸操作").Position;
		pmxJoint.BodyA = this.sbdi("右胸操作");
		pmxJoint.BodyB = this.sbdi("右胸操作接続");
		pmxJoint.Limit_MoveHigh = new Vector3(0f, 0.3f, 0f);
		pmxJoint.Limit_AngleLow = new Vector3(-0.87266463f, -0.87266463f, -0.87266463f);
		pmxJoint.Limit_AngleHigh = new Vector3(0.87266463f, 0.87266463f, 0.87266463f);
		pmxJoint.SpConst_Move = new Vector3(0f, 20f, 0f);
		pmxJoint.SpConst_Rotate = new Vector3(100f, 100f, 100f);
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右胸操作接続";
		pmxJoint.Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f);
		pmxJoint.BodyA = this.sbdi("右胸操作接続");
		pmxJoint.BodyB = this.sbdi("右胸操作衝突");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右AH1";
		pmxJoint.Position = this.sb("右AH1").Position;
		pmxJoint.BodyA = this.sbdi("右AH1");
		pmxJoint.BodyB = this.sbdi("右胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右AH2";
		pmxJoint.Position = this.sb("右AH2").Position;
		pmxJoint.BodyA = this.sbdi("右AH2");
		pmxJoint.BodyB = this.sbdi("右胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右胸操作接続";
		pmxJoint.Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f);
		pmxJoint.BodyB = this.sbdi("右胸操作接続");
		pmxJoint.BodyA = this.sbdi("右胸操作衝突");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右AH1";
		pmxJoint.Position = this.sb("右AH1").Position;
		pmxJoint.BodyB = this.sbdi("右AH1");
		pmxJoint.BodyA = this.sbdi("右胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
		pmxJoint = new PmxJoint();
		pmxJoint.Name = "右AH2";
		pmxJoint.Position = this.sb("右AH2").Position;
		pmxJoint.BodyB = this.sbdi("右AH2");
		pmxJoint.BodyA = this.sbdi("右胸操作接続");
		this.pmxFile.JointList.Add(pmxJoint);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00009C00 File Offset: 0x00007E00
	public void vasicphy()
	{
		Transform transform = GameObject.Find("BodyTop").transform;
		CapsuleCollider[] componentsInChildren = transform.GetComponentsInChildren<CapsuleCollider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			PmxBody pmxBody = new PmxBody();
			pmxBody.Name = componentsInChildren[i].name;
			pmxBody.Bone = this.sbi(componentsInChildren[i].name);
			pmxBody.Position = new Vector3(-componentsInChildren[i].transform.position.x, componentsInChildren[i].transform.position.y, -componentsInChildren[i].transform.position.z) * (float)this.scale;
			pmxBody.Rotation = new Vector3(-componentsInChildren[i].transform.rotation.x, componentsInChildren[i].transform.rotation.y, -componentsInChildren[i].transform.rotation.z);
			pmxBody.BoxSize = new Vector3(componentsInChildren[i].radius * (float)this.scale / 2f, componentsInChildren[i].height * (float)this.scale / 3f * 2f, 0f);
			pmxBody.BoxType = PmxBody.BoxKind.Capsule;
			pmxBody.Mode = PmxBody.ModeType.Static;
			pmxBody.Group = 13;
			pmxBody.Mass = 0.1f;
			pmxBody.PositionDamping = 0.9999f;
			pmxBody.RotationDamping = 0.99f;
			this.pmxFile.BodyList.Add(pmxBody);
		}
		SphereCollider[] componentsInChildren2 = transform.GetComponentsInChildren<SphereCollider>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			PmxBody pmxBody2 = new PmxBody();
			pmxBody2.Name = componentsInChildren2[j].name;
			pmxBody2.Bone = this.sbi(componentsInChildren2[j].name);
			pmxBody2.Position = new Vector3(-componentsInChildren2[j].transform.position.x, componentsInChildren2[j].transform.position.y, -componentsInChildren2[j].transform.position.z) * (float)this.scale;
			pmxBody2.Rotation = new Vector3(-componentsInChildren2[j].transform.rotation.x, componentsInChildren2[j].transform.rotation.y, -componentsInChildren2[j].transform.rotation.z);
			pmxBody2.BoxSize = new Vector3(componentsInChildren2[j].radius, 0f, 0f) * (float)this.scale / 3f * 2f;
			pmxBody2.BoxType = PmxBody.BoxKind.Sphere;
			pmxBody2.Mode = PmxBody.ModeType.Static;
			pmxBody2.Group = 13;
			pmxBody2.Mass = 0.1f;
			pmxBody2.PositionDamping = 0.9999f;
			pmxBody2.RotationDamping = 0.99f;
			this.pmxFile.BodyList.Add(pmxBody2);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00009F18 File Offset: 0x00008118
	public void addnode()
	{
		PmxNode pmxNode = new PmxNode();
		pmxNode.Name = "センター";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("センター")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "ＩＫ";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左足ＩＫ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左つま先ＩＫ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右足ＩＫ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右つま先ＩＫ")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "体(上)";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("上半身")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("上半身2")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("首")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("頭")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("両目")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右目")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左目")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "腕";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左肩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左腕")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左腕捩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左ひじ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左手捩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左手首")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右肩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右腕")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右腕捩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右ひじ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右手捩")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右手首")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "指";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左薬指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左薬指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左薬指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左親指０")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左親指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左親指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左中指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左中指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左中指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左小指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左小指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左小指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左人指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左人指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左人指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右薬指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右薬指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右薬指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右親指０")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右親指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右親指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右中指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右中指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右中指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右小指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右小指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右小指３")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右人指１")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右人指２")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右人指３")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "体(下)";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("下半身")));
		this.pmxFile.NodeList.Add(pmxNode);
		pmxNode = new PmxNode();
		pmxNode.Name = "足";
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左足")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左ひざ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("左足首")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右足")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右ひざ")));
		pmxNode.ElementList.Add(PmxNode.NodeElement.BoneElement(this.sbi("右足首")));
		this.pmxFile.NodeList.Add(pmxNode);
	}

	// Token: 0x04000004 RID: 4
	private string msg = "";

	// Token: 0x04000005 RID: 5
	private Pmx pmxFile;

	// Token: 0x04000006 RID: 6
	private string pass = "C:\\koikatsu_model\\";

	// Token: 0x04000007 RID: 7
	private int vertexCount = 0;

	// Token: 0x04000008 RID: 8
	private int scale = 12;

	// Token: 0x04000009 RID: 9
	private Dictionary<Transform, int> bonesMap;

	// Token: 0x0400000A RID: 10
	private List<Transform> boneList = new List<Transform>();

	// Token: 0x0400000B RID: 11
	private List<Matrix4x4> bindposeList = new List<Matrix4x4>();

	// Token: 0x0400000C RID: 12
	public List<Bones> bones2 = new List<Bones>();

	// Token: 0x0400000D RID: 13
	private Vers[] vers;

	// Token: 0x0400000E RID: 14
	private int[] vertics_num;

	// Token: 0x0400000F RID: 15
	private string[] vertics_name;

	// Token: 0x04000010 RID: 16
	private List<int>[] hitomi = new List<int>[2];

	// Token: 0x04000011 RID: 17
	private List<SkinnedMeshRenderer> skinnedMeshList;

	// Token: 0x04000012 RID: 18
	private List<MeshFilter> meshList;

	// Token: 0x04000013 RID: 19
	private string[] bone_name_list = new string[]
	{
		"cf_s_leg01_L",
		"cf_s_thigh01_L",
		"cf_s_waist01",
		"cf_s_leg02_L",
		"cf_j_foot_L",
		"cf_j_toes_L",
		"cf_s_thigh02_L",
		"cf_s_kneeB_L",
		"cf_s_thigh03_L",
		"cf_s_thigh01_R",
		"cf_s_leg02_R",
		"cf_j_foot_R",
		"cf_j_toes_R",
		"cf_s_leg01_R",
		"cf_s_thigh02_R",
		"cf_s_kneeB_R",
		"cf_s_thigh03_R",
		"cf_s_waist02",
		"cf_j_kokan",
		"cf_s_siri_L",
		"cf_s_siri_R",
		"cf_s_leg_L",
		"cf_s_leg_R",
		"cf_s_neck",
		"cf_s_head",
		"cf_s_hand_L",
		"cf_s_wrist_L",
		"cf_d_hand_L",
		"cf_s_forearm01_L",
		"cf_s_forearm02_L",
		"cf_s_arm01_L",
		"cf_s_arm02_L",
		"cf_s_arm03_L",
		"cf_s_elbo_L",
		"cf_s_shoulder02_L",
		"cf_s_forearm01_R",
		"cf_s_wrist_R",
		"cf_d_hand_R",
		"cf_s_forearm02_R",
		"cf_s_arm01_R",
		"cf_s_arm02_R",
		"cf_s_arm03_R",
		"cf_s_elbo_R",
		"cf_s_shoulder02_R",
		"cf_s_bust02_L",
		"cf_s_bust03_L",
		"cf_s_bnip01_L",
		"cf_s_bust02_R",
		"cf_s_bust03_R",
		"cf_s_bnip01_R",
		"cf_s_leg03_L",
		"cf_s_leg03_R",
		"cf_s_elboback_L",
		"cf_s_elboback_R",
		"cf_s_bust01_L",
		"cf_s_bust01_R",
		"cf_s_spine01",
		"cf_s_spine03",
		"cf_s_spine02",
		"cf_s_hand_R",
		"cf_s_ana",
		"cf_j_bnip02_L",
		"cf_j_bnip02_R",
		"cf_j_little01_L",
		"cf_j_little02_L",
		"cf_j_little03_L",
		"cf_j_ring01_L",
		"cf_j_ring02_L",
		"cf_j_ring03_L",
		"cf_j_middle01_L",
		"cf_j_middle02_L",
		"cf_j_middle03_L",
		"cf_j_index01_L",
		"cf_j_index02_L",
		"cf_j_index03_L",
		"cf_j_thumb01_L",
		"cf_j_thumb02_L",
		"cf_j_thumb03_L",
		"cf_j_thumb01_R",
		"cf_j_thumb02_R",
		"cf_j_thumb03_R",
		"cf_j_index01_R",
		"cf_j_index02_R",
		"cf_j_index03_R",
		"cf_j_middle01_R",
		"cf_j_middle02_R",
		"cf_j_middle03_R",
		"cf_j_ring01_R",
		"cf_j_ring02_R",
		"cf_j_ring03_R",
		"cf_j_little01_R",
		"cf_j_little02_R",
		"cf_j_little03_R",
		"cf_s_bnip025_R",
		"cf_s_bnip025_L",
		"cf_d_kneeF_L",
		"cf_d_kneeF_R",
		"cf_j_ana",
		"cf_s_bnip015_L",
		"cf_s_bnip015_R",
		"cf_J_FaceRoot",
		"cf_J_NoseBase",
		"cf_J_Nose_tip",
		"cf_J_NoseBridge_rx",
		"cf_J_FaceUp_ty",
		"cf_J_FaceUp_tz",
		"cf_J_Eye_rz_L",
		"cf_J_Eye01_s_L",
		"cf_J_Eye02_s_L",
		"cf_J_Eye03_s_L",
		"cf_J_Eye04_s_L",
		"cf_J_Eye05_s_L",
		"cf_J_Eye06_s_L",
		"cf_J_Eye07_s_L",
		"cf_J_Eye08_s_L",
		"cf_J_CheekUp2_L",
		"cf_J_EarBase_ry_L",
		"cf_J_EarUp_L",
		"cf_J_EarLow_L",
		"cf_J_Eye_rz_R",
		"cf_J_Eye01_s_R",
		"cf_J_Eye02_s_R",
		"cf_J_Eye03_s_R",
		"cf_J_Eye04_s_R",
		"cf_J_Eye05_s_R",
		"cf_J_Eye06_s_R",
		"cf_J_Eye07_s_R",
		"cf_J_Eye08_s_R",
		"cf_J_CheekUp2_R",
		"cf_J_EarBase_ry_R",
		"cf_J_EarUp_R",
		"cf_J_EarLow_R",
		"cf_J_ChinLow",
		"cf_J_Chin_s",
		"cf_J_CheekLow_s_L",
		"cf_J_CheekLow_s_R",
		"cf_J_ChinTip_Base",
		"cf_J_CheekUp_s_L",
		"cf_J_CheekUp_s_R",
		"cf_J_MouthMove",
		"cf_J_Mouthup",
		"cf_J_MouthCavity",
		"cf_J_MouthLow",
		"cf_J_Mouth_L",
		"cf_J_Mouth_R",
		"cf_J_MayuTip_s_L",
		"cf_J_MayuMid_s_L",
		"cf_J_MayuTip_s_R",
		"cf_J_MayuMid_s_R",
		"cf_J_Nose_rx",
		"cf_J_Mayumoto_L",
		"cf_J_Mayu_L",
		"cf_J_Mayumoto_R",
		"cf_J_Mayu_R",
		"cf_J_hitomi_tx_L",
		"cf_J_hitomi_tx_R",
		"cf_J_hairF_top",
		"cf_J_hairF_00",
		"cf_J_hairF_01",
		"cf_J_hairF_02",
		"cf_J_hairFR_02_00",
		"cf_J_hairFR_02_01",
		"cf_J_hairFR_02_02",
		"cf_J_hairFL_02_00",
		"cf_J_hairFL_02_01",
		"cf_J_hairFL_02_02",
		"cf_j_sk_00_00",
		"cf_j_sk_00_01",
		"cf_j_sk_00_02",
		"cf_j_sk_00_03",
		"cf_j_sk_00_04",
		"cf_j_sk_00_05",
		"cf_j_sk_01_00",
		"cf_j_sk_01_01",
		"cf_j_sk_01_02",
		"cf_j_sk_01_03",
		"cf_j_sk_01_04",
		"cf_j_sk_01_05",
		"cf_j_sk_02_00",
		"cf_j_sk_02_01",
		"cf_j_sk_02_02",
		"cf_j_sk_02_03",
		"cf_j_sk_02_04",
		"cf_j_sk_02_05",
		"cf_j_sk_06_00",
		"cf_j_sk_06_01",
		"cf_j_sk_06_02",
		"cf_j_sk_06_03",
		"cf_j_sk_06_04",
		"cf_j_sk_06_05",
		"cf_j_sk_07_00",
		"cf_j_sk_07_01",
		"cf_j_sk_07_02",
		"cf_j_sk_07_03",
		"cf_j_sk_07_04",
		"cf_j_sk_07_05",
		"cf_j_sk_03_00",
		"cf_j_sk_03_01",
		"cf_j_sk_03_02",
		"cf_j_sk_03_03",
		"cf_j_sk_03_04",
		"cf_j_sk_03_05",
		"cf_j_sk_04_00",
		"cf_j_sk_04_01",
		"cf_j_sk_04_02",
		"cf_j_sk_04_03",
		"cf_j_sk_04_04",
		"cf_j_sk_04_05",
		"cf_j_sk_05_00",
		"cf_j_sk_05_01",
		"cf_j_sk_05_02",
		"cf_j_sk_05_03",
		"cf_j_sk_05_04",
		"cf_j_sk_05_05",
		"cf_j_spinesk_00",
		"cf_j_spinesk_01",
		"cf_j_spinesk_02",
		"cf_j_spinesk_03",
		"cf_j_spinesk_04",
		"cf_j_spinesk_05",
		"cf_J_hairB_top",
		"j_01",
		"j1",
		"N_move2",
		"N_j_Bwing_R_01",
		"cf_J_hairFL_00",
		"joint1",
		"cf_J_hairS_top",
		"cf_J_hairFL_01_00",
		"cf_J_hairFR_01_00",
		"cf_J_hairB_00",
		"cf_J_hairBR_00",
		"cf_J_hairBL_00",
		"cf_J_hairFR_00"
	};

	// Token: 0x04000014 RID: 20
	private string[] bone_parent_name_list = new string[]
	{
		"cf_s_thigh01_L",
		"cf_s_waist02",
		"",
		"cf_s_leg01_L",
		"cf_s_leg01_L",
		"cf_j_foot_L",
		"cf_s_thigh01_L",
		"cf_s_leg01_L",
		"cf_s_thigh02_L",
		"cf_s_waist02",
		"cf_s_leg01_R",
		"cf_s_leg01_R",
		"cf_j_foot_R",
		"cf_s_thigh01_R",
		"cf_s_thigh01_R",
		"cf_s_leg01_R",
		"cf_s_thigh02_R",
		"",
		"cf_s_waist02",
		"cf_s_waist02",
		"cf_s_waist02",
		"cf_s_waist02",
		"cf_s_waist02",
		"cf_s_spine03",
		"cf_s_neck",
		"cf_s_forearm01_L",
		"cf_s_forearm01_L",
		"cf_s_forearm01_L",
		"cf_s_arm01_L",
		"cf_s_forearm01_L",
		"cf_s_shoulder02_L",
		"cf_s_arm01_L",
		"cf_s_arm02_L",
		"cf_s_forearm01_L",
		"cf_s_spine03",
		"cf_s_arm01_R",
		"cf_s_forearm02_R",
		"cf_s_wrist_R",
		"cf_s_forearm01_R",
		"cf_s_shoulder02_R",
		"cf_s_arm01_R",
		"cf_s_arm02_R",
		"cf_s_forearm01_R",
		"cf_s_spine03",
		"cf_s_bust01_L",
		"cf_s_bust02_L",
		"cf_s_bust03_L",
		"cf_s_bust01_R",
		"cf_s_bust02_R",
		"cf_s_bust03_R",
		"cf_s_leg02_L",
		"cf_s_leg02_R",
		"cf_s_elbo_L",
		"cf_s_elbo_R",
		"cf_s_spine03",
		"cf_s_spine03",
		"cf_s_waist01",
		"cf_s_spine02",
		"cf_s_spine01",
		"cf_d_hand_R",
		"cf_j_ana",
		"cf_s_bnip025_L",
		"cf_s_bnip025_R",
		"cf_s_hand_L",
		"cf_j_little01_L",
		"cf_j_little02_L",
		"cf_s_hand_L",
		"cf_j_ring01_L",
		"cf_j_ring02_L",
		"cf_s_hand_L",
		"cf_j_middle01_L",
		"cf_j_middle02_L",
		"cf_s_hand_L",
		"cf_j_index01_L",
		"cf_j_index02_L",
		"cf_s_hand_L",
		"cf_j_thumb01_L",
		"cf_j_thumb02_L",
		"cf_s_hand_R",
		"cf_j_thumb01_R",
		"cf_j_thumb02_R",
		"cf_s_hand_R",
		"cf_j_index01_R",
		"cf_j_index02_R",
		"cf_s_hand_R",
		"cf_j_middle01_R",
		"cf_j_middle02_R",
		"cf_s_hand_R",
		"cf_j_ring01_R",
		"cf_j_ring02_R",
		"cf_s_hand_R",
		"cf_j_little01_R",
		"cf_j_little02_R",
		"cf_s_bnip015_R",
		"cf_s_bnip015_L",
		"cf_s_leg01_L",
		"cf_s_leg01_R",
		"cf_s_waist02",
		"cf_s_bnip01_L",
		"cf_s_bnip01_R",
		"cf_s_head",
		"cf_J_FaceRoot",
		"cf_J_Nose_rx",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_L",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_EarBase_ry_L",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_Eye_rz_R",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_EarBase_ry_R",
		"cf_J_Chin_s",
		"cf_J_ChinTip_Base",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_Eye_rz_L",
		"cf_J_Eye_rz_R",
		"cf_J_FaceRoot",
		"cf_J_FaceRoot",
		"cf_J_hairF_top",
		"cf_J_hairF_00",
		"cf_J_hairF_01",
		"cf_J_hairF_top",
		"cf_J_hairFR_02_00",
		"cf_J_hairFR_02_01",
		"cf_J_hairF_top",
		"cf_J_hairFL_02_00",
		"cf_J_hairFL_02_01",
		"cf_s_waist01",
		"cf_j_sk_00_00",
		"cf_j_sk_00_01",
		"cf_j_sk_00_02",
		"cf_j_sk_00_03",
		"cf_j_sk_00_04",
		"cf_s_waist01",
		"cf_j_sk_01_00",
		"cf_j_sk_01_01",
		"cf_j_sk_01_02",
		"cf_j_sk_01_03",
		"cf_j_sk_01_04",
		"cf_s_waist01",
		"cf_j_sk_02_00",
		"cf_j_sk_02_01",
		"cf_j_sk_02_02",
		"cf_j_sk_02_03",
		"cf_j_sk_02_04",
		"cf_s_waist01",
		"cf_j_sk_06_00",
		"cf_j_sk_06_01",
		"cf_j_sk_06_02",
		"cf_j_sk_06_03",
		"cf_j_sk_06_04",
		"cf_s_waist01",
		"cf_j_sk_07_00",
		"cf_j_sk_07_01",
		"cf_j_sk_07_02",
		"cf_j_sk_07_03",
		"cf_j_sk_07_04",
		"cf_s_waist01",
		"cf_j_sk_03_00",
		"cf_j_sk_03_01",
		"cf_j_sk_03_02",
		"cf_j_sk_03_03",
		"cf_j_sk_03_04",
		"cf_s_waist01",
		"cf_j_sk_04_00",
		"cf_j_sk_04_01",
		"cf_j_sk_04_02",
		"cf_j_sk_04_03",
		"cf_j_sk_04_04",
		"cf_s_waist01",
		"cf_j_sk_05_00",
		"cf_j_sk_05_01",
		"cf_j_sk_05_02",
		"cf_j_sk_05_03",
		"cf_j_sk_05_04",
		"cf_s_neck",
		"cf_j_spinesk_00",
		"cf_j_spinesk_01",
		"cf_j_spinesk_02",
		"cf_j_spinesk_03",
		"cf_j_spinesk_04",
		"cf_J_FaceRoot",
		"cf_J_hairF_top",
		"cf_s_spine03",
		"j1",
		"cf_s_spine03",
		"cf_J_hairF_top",
		"cf_J_hairB_top",
		"cf_J_hairF_top",
		"cf_J_hairF_top",
		"cf_J_hairF_top",
		"cf_J_hairB_top",
		"cf_J_hairB_top",
		"cf_J_hairB_top",
		"cf_J_hairF_top",
		"cf_J_hairF_top"
	};

	// Token: 0x04000015 RID: 21
	private string[] bone_change_name_base = new string[]
	{
		"cf_j_spine01",
		"cf_j_spine02",
		"cf_j_neck",
		"cf_j_head",
		"cf_j_waist01",
		"cf_J_hitomi_tx_L",
		"cf_J_hitomi_tx_R",
		"cf_d_shoulder02_L",
		"cf_j_arm00_L",
		"cf_j_arm03_L",
		"cf_j_forearm01_L",
		"cf_j_forearm02_L",
		"cf_j_hand_L",
		"cf_j_ring01_L",
		"cf_j_ring02_L",
		"cf_j_ring03_L",
		"cf_j_thumb01_L",
		"cf_j_thumb02_L",
		"cf_j_thumb03_L",
		"cf_j_middle01_L",
		"cf_j_middle02_L",
		"cf_j_middle03_L",
		"cf_j_little01_L",
		"cf_j_little02_L",
		"cf_j_little03_L",
		"cf_j_index01_L",
		"cf_j_index02_L",
		"cf_j_index03_L",
		"cf_j_thigh00_L",
		"cf_j_leg01_L",
		"cf_j_foot_L",
		"cf_d_shoulder02_R",
		"cf_j_arm00_R",
		"cf_j_arm03_R",
		"cf_j_forearm01_R",
		"cf_j_forearm02_R",
		"cf_j_hand_R",
		"cf_j_ring01_R",
		"cf_j_ring02_R",
		"cf_j_ring03_R",
		"cf_j_thumb01_R",
		"cf_j_thumb02_R",
		"cf_j_thumb03_R",
		"cf_j_middle01_R",
		"cf_j_middle02_R",
		"cf_j_middle03_R",
		"cf_j_little01_R",
		"cf_j_little02_R",
		"cf_j_little03_R",
		"cf_j_index01_R",
		"cf_j_index02_R",
		"cf_j_index03_R",
		"cf_j_thigh00_R",
		"cf_j_leg01_R",
		"cf_j_foot_R"
	};

	// Token: 0x04000016 RID: 22
	private string[] bone_change_name = new string[]
	{
		"上半身",
		"上半身2",
		"首",
		"頭",
		"下半身",
		"左目x",
		"右目x",
		"左肩",
		"左腕",
		"左腕捩",
		"左ひじ",
		"左手捩",
		"左手首",
		"左薬指１",
		"左薬指２",
		"左薬指３",
		"左親指０",
		"左親指１",
		"左親指２",
		"左中指１",
		"左中指２",
		"左中指３",
		"左小指１",
		"左小指２",
		"左小指３",
		"左人指１",
		"左人指２",
		"左人指３",
		"左足",
		"左ひざ",
		"左足首",
		"右肩",
		"右腕",
		"右腕捩",
		"右ひじ",
		"右手捩",
		"右手首",
		"右薬指１",
		"右薬指２",
		"右薬指３",
		"右親指０",
		"右親指１",
		"右親指２",
		"右中指１",
		"右中指２",
		"右中指３",
		"右小指１",
		"右小指２",
		"右小指３",
		"右人指１",
		"右人指２",
		"右人指３",
		"右足",
		"右ひざ",
		"右足首"
	};
}
