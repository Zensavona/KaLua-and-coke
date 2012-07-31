using System;

namespace Emulator.Packets
{
	public enum LOGIN_ERROR
	{
		UNDEFINED     = 0x01,
		WRONGID       = 0x02,
		WRONG_PASS    = 0x03,
		CONNECT_LATER = 0x04,
		BLOCKED       = 0x05,
		ID_EXPIRED    = 0x06,
		TOO_YOUNG     = 0x07,
		NOT_ALLOWED   = 0x08,
	}

	public class LoginError : PacketOut
	{
		public LoginError(LOGIN_ERROR reason) : base(0x2b, 1)
		{
			writer.Write((byte)reason);
		}
	}
}
