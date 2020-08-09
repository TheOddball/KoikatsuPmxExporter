using System;
using System.IO;

namespace PmxLib
{
	// Token: 0x0200001B RID: 27
	internal class PmxImpulseMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		PmxObject IPmxObjectKey.ObjectKey
		{
			get
			{
				return PmxObject.ImpulseMorph;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000F508 File Offset: 0x0000D708
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000F520 File Offset: 0x0000D720
		public override int BaseIndex
		{
			get
			{
				return this.Index;
			}
			set
			{
				this.Index = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000F52A File Offset: 0x0000D72A
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000F532 File Offset: 0x0000D732
		public PmxBody RefBody { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000F53B File Offset: 0x0000D73B
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000F543 File Offset: 0x0000D743
		public bool ZeroFlag { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x0000F54C File Offset: 0x0000D74C
		public PmxImpulseMorph()
		{
			this.Local = false;
			this.Velocity = Vector3.zero;
			this.Torque = Vector3.zero;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000F573 File Offset: 0x0000D773
		public PmxImpulseMorph(int index, bool local, Vector3 t, Vector3 r)
		{
			this.Index = index;
			this.Local = local;
			this.Velocity = t;
			this.Torque = r;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000F59A File Offset: 0x0000D79A
		public PmxImpulseMorph(PmxImpulseMorph sv) : this()
		{
			this.FromPmxImpulseMorph(sv);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public void FromPmxImpulseMorph(PmxImpulseMorph sv)
		{
			this.Index = sv.Index;
			this.Local = sv.Local;
			this.Velocity = sv.Velocity;
			this.Torque = sv.Torque;
			this.ZeroFlag = sv.ZeroFlag;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public bool UpdateZeroFlag()
		{
			this.ZeroFlag = (this.Velocity == Vector3.zero && this.Torque == Vector3.zero);
			return this.ZeroFlag;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000F630 File Offset: 0x0000D830
		public void Clear()
		{
			this.Velocity = Vector3.zero;
			this.Torque = Vector3.zero;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000F649 File Offset: 0x0000D849
		public override void FromStreamEx(Stream s, PmxElementFormat size)
		{
			this.Index = PmxStreamHelper.ReadElement_Int32(s, size.BodySize, true);
			this.Local = (s.ReadByte() != 0);
			this.Velocity = V3_BytesConvert.FromStream(s);
			this.Torque = V3_BytesConvert.FromStream(s);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000F688 File Offset: 0x0000D888
		public override void ToStreamEx(Stream s, PmxElementFormat size)
		{
			PmxStreamHelper.WriteElement_Int32(s, this.Index, size.BodySize, true);
			s.WriteByte(this.Local ? 1 : 0);
			V3_BytesConvert.ToStream(s, this.Velocity);
			V3_BytesConvert.ToStream(s, this.Torque);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		object ICloneable.Clone()
		{
			return new PmxImpulseMorph(this);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		public override PmxBaseMorph Clone()
		{
			return new PmxImpulseMorph(this);
		}

		// Token: 0x040000A1 RID: 161
		public int Index;

		// Token: 0x040000A2 RID: 162
		public bool Local;

		// Token: 0x040000A3 RID: 163
		public Vector3 Velocity;

		// Token: 0x040000A4 RID: 164
		public Vector3 Torque;
	}
}
