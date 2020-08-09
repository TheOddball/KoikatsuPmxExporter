using System;

namespace PmxLib
{
	// Token: 0x02000009 RID: 9
	public interface IBytesConvert
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006D RID: 109
		int ByteCount { get; }

		// Token: 0x0600006E RID: 110
		byte[] ToBytes();

		// Token: 0x0600006F RID: 111
		void FromBytes(byte[] bytes, int startIndex);
	}
}
