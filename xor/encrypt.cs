using System.IO;
using System.Text;

namespace xor
{
    class encrypt
    {
        string plain = File.ReadAllText("plain.txt");
        static string key = File.ReadAllText("key.txt");
        public static int keyLength = key.Length;
        int xor;
        public void encryptplain()
        {
            File.WriteAllText("crypto.txt", null);
            byte[] plainASCII = Encoding.ASCII.GetBytes(plain);
            byte[] keyASCII = Encoding.ASCII.GetBytes(key);
            int j = 0;
            int b;

            for (var i = 0; i < plain.Length; i++)
            {
                if (plainASCII[i] != 10)
                {
                    b = i % (key.Length + 1);
                    xor = plainASCII[i] ^ keyASCII[b];

                    File.AppendAllText("crypto.txt", ((char)xor).ToString());
                }
                else
                {
                    j++;
                }
            }
        }
    }
}
