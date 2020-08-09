using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200000E RID: 14
	public interface IPmxStreamIO
	{
		// Token: 0x06000081 RID: 129
		void FromStreamEx(Stream s, PmxElementFormat f);

		// Token: 0x06000082 RID: 130
		void ToStreamEx(Stream s, PmxElementFormat f);
	}
}
