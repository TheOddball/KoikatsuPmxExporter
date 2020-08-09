using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
	// Token: 0x02000006 RID: 6
	internal static class CExt
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000A88C File Offset: 0x00008A8C
		public static Dictionary<string, int> ScriptExtTable
		{
			get
			{
				bool flag = CExt.m_scriptExtTable == null;
				if (flag)
				{
					CExt.m_scriptExtTable = new Dictionary<string, int>();
					CExt.m_scriptExtTable.Add(".exe", 0);
					CExt.m_scriptExtTable.Add(".bat", 1);
					CExt.m_scriptExtTable.Add(".asp", 2);
					CExt.m_scriptExtTable.Add(".js", 3);
					CExt.m_scriptExtTable.Add(".vbs", 4);
					CExt.m_scriptExtTable.Add(".wsf", 5);
					CExt.m_scriptExtTable.Add(".wsh", 6);
					CExt.m_scriptExtTable.Add(".cmd", 7);
					CExt.m_scriptExtTable.Add(".url", 8);
				}
				return CExt.m_scriptExtTable;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000A958 File Offset: 0x00008B58
		public static string GetDlgFilter(string ext)
		{
			return string.Concat(new string[]
			{
				"ファイル (*.",
				ext.ToLower(),
				")|*.",
				ext.ToLower(),
				"|すべてのファイル (*.*)|*.*"
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		public static bool IsExtPath(string path, string ext)
		{
			return !string.IsNullOrEmpty(path) && Path.GetExtension(path).ToLower() == ext;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public static bool IsPmxPath(string path)
		{
			return CExt.IsExtPath(path, ".pmx");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public static bool IsPmdPath(string path)
		{
			return CExt.IsExtPath(path, ".pmd");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000AA10 File Offset: 0x00008C10
		public static bool IsXPath(string path)
		{
			return CExt.IsExtPath(path, ".x");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000AA30 File Offset: 0x00008C30
		public static bool IsCSVPath(string path)
		{
			return CExt.IsExtPath(path, ".csv");
		}

		// Token: 0x04000019 RID: 25
		public const string Ext_X = ".x";

		// Token: 0x0400001A RID: 26
		public const string Ext_Pmd = ".pmd";

		// Token: 0x0400001B RID: 27
		public const string Ext_Pmx = ".pmx";

		// Token: 0x0400001C RID: 28
		public const string Ext_Vmd = ".vmd";

		// Token: 0x0400001D RID: 29
		public const string Ext_Vpd = ".vpd";

		// Token: 0x0400001E RID: 30
		public const string Ext_CSV = ".csv";

		// Token: 0x0400001F RID: 31
		public const string Ext_PSK = ".psk";

		// Token: 0x04000020 RID: 32
		public const string Ext_FX = ".fx";

		// Token: 0x04000021 RID: 33
		public const string Ext_CFX = ".cfx";

		// Token: 0x04000022 RID: 34
		public const string Ext_XML = ".xml";

		// Token: 0x04000023 RID: 35
		public const string Ext_TXT = ".txt";

		// Token: 0x04000024 RID: 36
		public const string DlgFilter_All = "すべてのファイル (*.*)|*.*";

		// Token: 0x04000025 RID: 37
		public const string DlgFilter_Pmd = "PMDファイル (*.pmd)|*.pmd|すべてのファイル (*.*)|*.*";

		// Token: 0x04000026 RID: 38
		public const string DlgFilter_Vmd = "MMDモーションファイル (*.vmd)|*.vmd|すべてのファイル (*.*)|*.*";

		// Token: 0x04000027 RID: 39
		public const string DlgFilter_Txt = "txtファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";

		// Token: 0x04000028 RID: 40
		public const string DlgFilter_CSV = "csvファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";

		// Token: 0x04000029 RID: 41
		public const string DlgFilter_X = "Xファイル (*.x)|*.x|すべてのファイル (*.*)|*.*";

		// Token: 0x0400002A RID: 42
		public const string DlgFilter_Pmx = "拡張モデルファイル (*.pmx)|*.pmx|すべてのファイル (*.*)|*.*";

		// Token: 0x0400002B RID: 43
		public const string DlgFilter_PmxPmd = "PMX／PMD (*.pmx;*.pmd)|*.pmx;*.pmd|すべてのファイル (*.*)|*.*";

		// Token: 0x0400002C RID: 44
		public const string DlgFilter_PmxPmdX = "PMX／PMD／X (*.pmx;*.pmd;*.x)|*.pmx;*.pmd;*.x|すべてのファイル (*.*)|*.*";

		// Token: 0x0400002D RID: 45
		public const string DlgFilter_PmxPmdXPsk = "PMX／PMD／X／PSK (*.pmx;*.pmd;*.x;*.psk)|*.pmx;*.pmd;*.x;*.psk|すべてのファイル (*.*)|*.*";

		// Token: 0x0400002E RID: 46
		public const string DlgFilter0_PmxPmdX = "PMX／PMD／X (*.pmx;*.pmd;*.x)|*.pmx;*.pmd;*.x";

		// Token: 0x0400002F RID: 47
		public const string DlgFilter0_PmxPmdXPsk = "PMX／PMD／X／PSK (*.pmx;*.pmd;*.x;*.psk)|*.pmx;*.pmd;*.x;*.psk";

		// Token: 0x04000030 RID: 48
		public const string DlgFilter_Vpd = "MMDポーズファイル (*.vpd)|*.vpd|すべてのファイル (*.*)|*.*";

		// Token: 0x04000031 RID: 49
		public const string DlgFilter_Psk = "PSKファイル (*.psk)|*.psk|すべてのファイル (*.*)|*.*";

		// Token: 0x04000032 RID: 50
		public const string DlgFilter_Fx = "FXファイル (*.fx)|*.fx|すべてのファイル (*.*)|*.*";

		// Token: 0x04000033 RID: 51
		public const string DlgFilter_Xml = "XMLファイル (*.xml)|*.xml|すべてのファイル (*.*)|*.*";

		// Token: 0x04000034 RID: 52
		private static Dictionary<string, int> m_scriptExtTable;
	}
}
