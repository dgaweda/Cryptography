using System.IO;

namespace xor
{
    class prepare
    {
        string orig = File.ReadAllText("orig.txt");
        string key = File.ReadAllText("key.txt");
        int i = 1;

        public void prepareOrig()
        {
            File.WriteAllText("plain.txt", null);

            foreach (var character in orig)
                if (character == 32 || (character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                {
                    File.AppendAllText("plain.txt", character.ToString().ToLower());
                    if (i == key.Length)
                    {
                        File.AppendAllText("plain.txt", "\n");
                        i = 0;
                    }
                    i++;
                }
        }

    }
}
