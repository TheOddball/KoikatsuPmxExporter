using System;
using System.Collections.Generic;

namespace PmxLib
{
	// Token: 0x0200000A RID: 10
	internal class IDObject<T>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000BF28 File Offset: 0x0000A128
		public int Count
		{
			get
			{
				return this.m_table.Keys.Count;
			}
		}

		// Token: 0x17000004 RID: 4
		public T this[uint i]
		{
			get
			{
				return this.Get(i);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000BF68 File Offset: 0x0000A168
		public IEnumerable<uint> IDs
		{
			get
			{
				foreach (uint current in this.m_table.Keys)
				{
					bool flag = current > 0U;
					if (flag)
					{
						yield return current;
					}
				}
				Dictionary<uint, T>.KeyCollection.Enumerator enumerator = default(Dictionary<uint, T>.KeyCollection.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000BF87 File Offset: 0x0000A187
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000BF8F File Offset: 0x0000A18F
		public bool IsIDOverflow { get; private set; }

		// Token: 0x06000075 RID: 117 RVA: 0x0000BF98 File Offset: 0x0000A198
		public IDObject(uint limit)
		{
			bool flag = limit == 0U;
			if (flag)
			{
				limit = uint.MaxValue;
			}
			this.m_limit = limit;
			this.Clear();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public void Clear()
		{
			bool flag = this.m_table != null;
			if (flag)
			{
				this.m_table.Clear();
				this.m_table = null;
			}
			this.m_table = new Dictionary<uint, T>();
			this.m_table.Add(0U, default(T));
			this.m_lastID = 0U;
			this.IsIDOverflow = false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000C028 File Offset: 0x0000A228
		public uint NewObject(T obj)
		{
			bool isIDOverflow = this.IsIDOverflow;
			uint num;
			if (isIDOverflow)
			{
				num = this.SearchNextID(this.m_lastID + 1U);
				bool flag = num == 0U;
				if (flag)
				{
					num = this.SearchNextID(1U);
					bool flag2 = num == 0U;
					if (flag2)
					{
						this.m_lastID = 0U;
						throw new IDOverflowException();
					}
				}
			}
			else
			{
				num = (this.m_lastID += 1U);
				bool flag3 = num >= this.m_limit;
				if (flag3)
				{
					this.IsIDOverflow = true;
					this.m_lastID = 0U;
					return this.NewObject(obj);
				}
			}
			bool flag4 = num > 0U;
			if (flag4)
			{
				this.m_table.Add(num, obj);
				this.m_lastID = num;
			}
			return num;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		private uint SearchNextID(uint st)
		{
			for (uint num = st; num < this.m_limit; num += 1U)
			{
				bool flag = !this.m_table.ContainsKey(num);
				if (flag)
				{
					return num;
				}
			}
			return 0U;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000C134 File Offset: 0x0000A334
		public bool ContainsID(uint id)
		{
			return id != 0U && this.m_table.ContainsKey(id);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000C158 File Offset: 0x0000A358
		public T Get(uint id)
		{
			bool flag = id == 0U;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				bool flag2 = this.m_table.ContainsKey(id);
				if (flag2)
				{
					result = this.m_table[id];
				}
				else
				{
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public void Remove(uint id)
		{
			bool flag = id > 0U;
			if (flag)
			{
				bool flag2 = this.m_table.ContainsKey(id);
				if (flag2)
				{
					this.m_table.Remove(id);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		public IEnumerator<T> GetEnumerator()
		{
			foreach (uint current in this.IDs)
			{
				yield return this.m_table[current];
			}
			IEnumerator<uint> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000035 RID: 53
		private Dictionary<uint, T> m_table;

		// Token: 0x04000036 RID: 54
		private readonly uint m_limit;

		// Token: 0x04000037 RID: 55
		private uint m_lastID;
	}
}
