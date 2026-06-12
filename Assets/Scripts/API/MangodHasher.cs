using System;
using System.Text;

namespace API
{
    public class MangodHasher
    {
        public static string Hash(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            ulong h0 = 0x6a09e667b3c4f912UL;
            ulong h1 = 0xbb67ae8584caa73bUL;

            // padding
            int padded = ((data.Length + 8) / 16 + 1) * 16;
            byte[] msg = new byte[padded];
            Array.Copy(data, msg, data.Length);
            msg[data.Length] = 0x80;

            // process blocks
            for (int i = 0; i < msg.Length; i += 8)
            {
                ulong word = BitConverter.ToUInt64(msg, i);
                h0 ^= RotateLeft(word + h1, 17);
                h1 ^= RotateLeft(word + h0, 31);
                h0 = h0 * 0x9e3779b97f4a7c15UL + (ulong)i;
                h1 = h1 * 0xbf58476d1ce4e5b9UL ^ h0;
            }

            // mix final
            h0 ^= (ulong)data.Length;
            h0 = FinalMix(h0);
            h1 = FinalMix(h1 ^ h0);

            return $"{h0:x16}{h1:x16}"; // 128-bit output
        }

        static ulong RotateLeft(ulong val, int bits) => (val << bits) | (val >> (64 - bits));

        static ulong FinalMix(ulong h)
        {
            h ^= h >> 33;
            h *= 0xff51afd7ed558ccdUL;
            h ^= h >> 33;
            h *= 0xc4ceb9fe1a85ec53UL;
            h ^= h >> 33;
            return h;
        }
    }
}
