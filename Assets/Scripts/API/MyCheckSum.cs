using System;

public class MyCheckSum
{
    public static string CheckSum(string input)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);

        ulong sum = 0;
        foreach (byte b in bytes)
            sum += b;

        sum ^= 0xd5da66b43989fde5;
        sum = (sum << 10) | (sum >> 54);
        sum ^= 0x4bd1e3a416d8cf0d;

        return sum.ToString("x");
    }
}
