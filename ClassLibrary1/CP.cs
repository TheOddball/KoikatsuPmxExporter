using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x02000008 RID: 8
	internal static class CP
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public static void Swap<T>(ref T v0, ref T v1) where T : struct
		{
			T t = v0;
			v0 = v1;
			v1 = t;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000B818 File Offset: 0x00009A18
		public static void Swap<T>(IList<T> list, int ix1, int ix2)
		{
			T value = list[ix1];
			list[ix1] = list[ix2];
			list[ix2] = value;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000B848 File Offset: 0x00009A48
		public static List<T> CloneList<T>(List<T> list) where T : ICloneable
		{
			List<T> list2 = new List<T>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				List<T> list3 = list2;
				T t = list[i];
				list3.Add((T)((object)t.Clone()));
			}
			return list2;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public static List<T> CloneList_ValueType<T>(List<T> list) where T : struct
		{
			return new List<T>(list.ToArray());
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public static T[] CloneArray<T>(T[] src) where T : ICloneable
		{
			T[] array = new T[src.Length];
			for (int i = 0; i < src.Length; i++)
			{
				array[i] = (T)((object)src[i].Clone());
			}
			return array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000B914 File Offset: 0x00009B14
		public static T[] CloneArray_ValueType<T>(T[] src) where T : struct
		{
			T[] array = new T[src.Length];
			Array.Copy(src, array, src.Length);
			return array;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000B93C File Offset: 0x00009B3C
		public static Dictionary<Tv, int> ArrayToTable<Tl, Tv>(Tl[] arr, Func<int, Tv> objProc)
		{
			Dictionary<Tv, int> dictionary = new Dictionary<Tv, int>(arr.Length);
			for (int i = 0; i < arr.Length; i++)
			{
				Tv tv = objProc(i);
				bool flag = tv != null && !dictionary.ContainsKey(tv);
				if (flag)
				{
					dictionary.Add(tv, i);
				}
			}
			return dictionary;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000B99C File Offset: 0x00009B9C
		public static Dictionary<T, int> ArrayToTable<T>(T[] arr)
		{
			Dictionary<T, int> dictionary = new Dictionary<T, int>(arr.Length);
			for (int i = 0; i < arr.Length; i++)
			{
				T t = arr[i];
				bool flag = t != null && !dictionary.ContainsKey(t);
				if (flag)
				{
					dictionary.Add(t, i);
				}
			}
			return dictionary;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public static Dictionary<Tv, int> ListToTable<Tl, Tv>(List<Tl> list, Func<int, Tv> objProc)
		{
			Dictionary<Tv, int> dictionary = new Dictionary<Tv, int>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				Tv tv = objProc(i);
				bool flag = tv != null && !dictionary.ContainsKey(tv);
				if (flag)
				{
					dictionary.Add(tv, i);
				}
			}
			return dictionary;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000BA64 File Offset: 0x00009C64
		public static Dictionary<T, int> ListToTable<T>(List<T> list)
		{
			Dictionary<T, int> dictionary = new Dictionary<T, int>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				T t = list[i];
				bool flag = t != null && !dictionary.ContainsKey(t);
				if (flag)
				{
					dictionary.Add(t, i);
				}
			}
			return dictionary;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000BACC File Offset: 0x00009CCC
		public static bool InRange<T>(T[] arr, int index)
		{
			return 0 <= index && index < arr.Length;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public static bool InRange<T>(List<T> list, int index)
		{
			return 0 <= index && index < list.Count;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000BB10 File Offset: 0x00009D10
		public static bool InRange<T>(IList<T> list, int index)
		{
			return 0 <= index && index < list.Count;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000BB34 File Offset: 0x00009D34
		public static bool InRange(int min, int max, int val)
		{
			return min <= val && val <= max;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000BB54 File Offset: 0x00009D54
		public static T SafeGet<T>(T[] arr, int index) where T : class
		{
			bool flag = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag)
			{
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000BB90 File Offset: 0x00009D90
		public static T SafeGet<T>(IList<T> arr, int index) where T : class
		{
			bool flag = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag)
			{
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public static T SafeGetV<T>(T[] arr, int index) where T : struct
		{
			bool flag = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag)
			{
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000BC08 File Offset: 0x00009E08
		public static T SafeGetV<T>(T[] arr, int index, out bool flag) where T : struct
		{
			flag = false;
			bool flag2 = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag2)
			{
				flag = true;
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000BC48 File Offset: 0x00009E48
		public static T SafeGetV<T>(IList<T> arr, int index) where T : struct
		{
			bool flag = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag)
			{
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000BC84 File Offset: 0x00009E84
		public static T SafeGetV<T>(IList<T> arr, int index, out bool flag) where T : struct
		{
			flag = false;
			bool flag2 = arr != null && CP.InRange<T>(arr, index);
			T result;
			if (flag2)
			{
				flag = true;
				result = arr[index];
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		public static int[] SortIndexForRemove(int[] ix)
		{
			List<int> list = new List<int>(ix);
			list.Sort((int l, int r) => r - l);
			return list.ToArray();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public static void SSort<T>(List<T> list, Comparison<T> comp)
		{
			List<KeyValuePair<int, T>> list2 = new List<KeyValuePair<int, T>>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(new KeyValuePair<int, T>(i, list[i]));
			}
			list2.Sort(delegate(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = comp(x.Value, y.Value);
				bool flag = num == 0;
				if (flag)
				{
					num = x.Key.CompareTo(y.Key);
				}
				return num;
			});
			for (int j = 0; j < list.Count; j++)
			{
				list[j] = list2[j].Value;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public static int[] ComposeIndices(int[] ix1, int[] ix2)
		{
			List<int> list = new List<int>(ix1);
			Dictionary<int, int> dictionary = new Dictionary<int, int>(ix1.Length);
			for (int i = 0; i < ix1.Length; i++)
			{
				dictionary.Add(ix1[i], 0);
			}
			for (int j = 0; j < ix2.Length; j++)
			{
				bool flag = !dictionary.ContainsKey(ix2[j]);
				if (flag)
				{
					dictionary.Add(ix2[j], 0);
					list.Add(ix2[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000BE38 File Offset: 0x0000A038
		public static int[] RemoveIndices(int[] ix1, int[] ix2)
		{
			List<int> list = new List<int>(ix1.Length);
			Dictionary<int, int> dictionary = new Dictionary<int, int>(ix2.Length);
			for (int i = 0; i < ix2.Length; i++)
			{
				dictionary.Add(ix2[i], 0);
			}
			for (int j = 0; j < ix1.Length; j++)
			{
				bool flag = !dictionary.ContainsKey(ix1[j]);
				if (flag)
				{
					dictionary.Add(ix1[j], 0);
					list.Add(ix1[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public static bool IsSameIndices(int[] arr1, int[] arr2)
		{
			bool flag = arr1.Length != arr2.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < arr1.Length; i++)
				{
					bool flag2 = arr1[i] != arr2[i];
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}
	}
}
