using System;
using System.Threading;
using BepInEx;
using UnityEngine;

// Token: 0x02000002 RID: 2
[BepInPlugin("com.bepis.bepinex.pmxexporter", "PmxExporter", "1.2")]
public class PmxExpoter : BaseUnityPlugin
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void OnGUI()
	{
		bool flag = GUI.Button(new Rect(0f, 0f, 50f, 30f), "Export");
		if (flag)
		{
			bool flag2 = this.builder != null;
			if (!flag2)
			{
				this.builder = new PmxBuilder();
				this.msg += this.builder.BuildStart();
				Thread.Sleep(1000);
				this.builder = null;
			}
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
	private void Update()
	{
		bool flag = !this.destroyed;
		if (flag)
		{
			Animator[] array = Object.FindObjectsOfType<Animator>();
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				this.destroyed = true;
			}
			Animator[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Object.Destroy(array2[i]);
			}
		}
	}

	// Token: 0x04000001 RID: 1
	private PmxBuilder builder;

	// Token: 0x04000002 RID: 2
	private string msg = "";

	// Token: 0x04000003 RID: 3
	private bool destroyed;
}
