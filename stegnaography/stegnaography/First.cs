using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace stegnaography
{
    class First : Message
    {
        public void AddSpace()
        {
            File.WriteAllText("watermark.html", null);
            string[] Lines = File.ReadAllLines("cover.html");
            string msgBin = Msg();
            int i = 0;
            
            foreach (var line in Lines)
            {
                if (i < msgBin.Length && msgBin[i].Equals('1'))
                {
                    File.AppendAllText("watermark.html", line + " " + Environment.NewLine, Encoding.GetEncoding("Windows-1250"));
                }
                else
                    File.AppendAllText("watermark.html", line + "\n", Encoding.GetEncoding("Windows-1250"));

                i++;
            }
        }

        public override void GetMsg()
        {
            File.WriteAllText("detect.txt", null);
            string[] Lines = File.ReadAllLines("watermark.html");

            string chars = null;
            foreach (var line in Lines)
            {
                if (line.EndsWith(" "))
                {
                    chars += "1";
                    if (chars.Length == 4)
                    {
                        Console.WriteLine(chars);
                        File.AppendAllText("detect.txt", Convert.ToInt32(chars, 2).ToString("x"));
                        chars = null;
                    }
                }
                else
                {
                    chars += "0";
                    if (chars.Length == 4)
                    {
                        File.AppendAllText("detect.txt", Convert.ToInt32(chars, 2).ToString("x"));
                        chars = null;
                    };

                }
            }


        }

        public void FromHexToLetter()
        {
            string HexValue = File.ReadAllText("detect.txt");
            File.WriteAllText("detect.txt", null);
            string Letter = null;

            for (var i = 0; i < HexValue.Length; i++)
            {
                Letter += HexValue[i];
                if (Letter.Length == 2) {

                    int dec = Convert.ToInt32(Letter, 16);

                    File.AppendAllText("detect.txt", ((char)dec).ToString());
                    Letter = null;
                }
            }
        }
        
    }
}
