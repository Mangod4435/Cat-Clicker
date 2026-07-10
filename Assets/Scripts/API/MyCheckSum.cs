using System;

public class MyCheckSum
{
    public static string CheckSum(string input)
    {
        ulong inbyte = Convert.ToUInt64(input);
        inbyte ^= 0xd5da66b43989fde5;
        inbyte = (inbyte << 10) | (inbyte >> 54);
        inbyte ^= 0x4bd1e3a416d8cf0d;
        return inbyte.ToString("x");
    }
}
