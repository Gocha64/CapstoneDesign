using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
	C_Request_Name_input = 1,
	S_Reply_Name_input = 2,
	C_Request_Load_Star = 3,
	S_Challenge_Load_Star = 4,
	C_Request_Challenge_MyPage = 5,
	S_Challenge_MyPage = 6,
	C_Request_Challenge_Top30Rank = 7,
	S_Challenge_Top30Rank = 8,
	C_ChallengeUpdateStars = 9,
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}

public class C_Request_Name_input : IPacket
{
	public String name;
	public String Uid;
	public ushort Protocol { get { return (ushort)PacketID.C_Request_Name_input; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort nameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.name = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, nameLen);
		count += nameLen;
		ushort Uidlen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.Uid = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, Uidlen);
		count += Uidlen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Request_Name_input), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort nameLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(nameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += nameLen;
		ushort Uidlen = (ushort)Encoding.Unicode.GetBytes(this.Uid, 0, this.Uid.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(Uidlen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += Uidlen;


		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Reply_Name_input : IPacket
{
	public byte reply;
	public ushort Protocol { get { return (ushort)PacketID.S_Reply_Name_input; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.reply = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Reply_Name_input), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		segment.Array[segment.Offset + count] = (byte)this.reply;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Request_Load_Star : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_Request_Load_Star; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Request_Load_Star), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}


public class S_Challenge_Load_Star : IPacket
{
	public class StageStar
	{
		public byte stageId;
		public byte numberOfStars;

		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			this.stageId = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
			this.numberOfStars = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
		}

		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			segment.Array[segment.Offset + count] = (byte)this.stageId;
			count += sizeof(byte);
			segment.Array[segment.Offset + count] = (byte)this.numberOfStars;
			count += sizeof(byte);
			return success;
		}
	}
	public List<StageStar> stageStars = new List<StageStar>();

	public ushort Protocol { get { return (ushort)PacketID.S_Challenge_Load_Star; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.stageStars.Clear();
		ushort stageStarLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < stageStarLen; i++)
		{
			StageStar stageStar = new StageStar();
			stageStar.Read(segment, ref count);
			stageStars.Add(stageStar);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Challenge_Load_Star), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)this.stageStars.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (StageStar stageStar in this.stageStars)
			stageStar.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Request_Challenge_MyPage : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_Request_Challenge_MyPage; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Request_Challenge_MyPage), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Challenge_MyPage : IPacket
{
	public string name;
	public uint ranking;
	public byte TotalStars;

	public ushort Protocol { get { return (ushort)PacketID.S_Challenge_MyPage; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.name = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
		this.ranking = BitConverter.ToUInt32(segment.Array, segment.Offset + count);
		count += sizeof(uint);
		this.TotalStars = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Challenge_MyPage), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;
		Array.Copy(BitConverter.GetBytes(this.ranking), 0, segment.Array, segment.Offset + count, sizeof(uint));
		count += sizeof(uint);
		segment.Array[segment.Offset + count] = (byte)this.TotalStars;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Request_Challenge_Top30Rank : IPacket
{
	public ushort Protocol { get { return (ushort)PacketID.C_Request_Challenge_Top30Rank; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Request_Challenge_Top30Rank), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Challenge_Top30Rank : IPacket
{
	public class Rank
	{
		public string UId;
		public uint ranking;
		public byte totalStars;

		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
			count += UIdLen;
			this.ranking = BitConverter.ToUInt32(segment.Array, segment.Offset + count);
			count += sizeof(uint);
			this.totalStars = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
		}

		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			count += UIdLen;
			Array.Copy(BitConverter.GetBytes(this.ranking), 0, segment.Array, segment.Offset + count, sizeof(uint));
			count += sizeof(uint);
			segment.Array[segment.Offset + count] = (byte)this.totalStars;
			count += sizeof(byte);
			return success;
		}
	}
	public List<Rank> ranks = new List<Rank>();

	public ushort Protocol { get { return (ushort)PacketID.S_Challenge_Top30Rank; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.ranks.Clear();
		ushort rankLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < rankLen; i++)
		{
			Rank rank = new Rank();
			rank.Read(segment, ref count);
			ranks.Add(rank);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Challenge_Top30Rank), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)this.ranks.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (Rank rank in this.ranks)
			rank.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_ChallengeUpdateStars : IPacket
{
	public string UId;
	public byte stageId;
	public byte numberOfStars;

	public ushort Protocol { get { return (ushort)PacketID.C_ChallengeUpdateStars; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
		this.stageId = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
		this.numberOfStars = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_ChallengeUpdateStars), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;
		segment.Array[segment.Offset + count] = (byte)this.stageId;
		count += sizeof(byte);
		segment.Array[segment.Offset + count] = (byte)this.numberOfStars;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}