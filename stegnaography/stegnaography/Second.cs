using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace stegnaography
{
    class Second : Message
    {
        public void AddDoubleSpace()
        {
            File.WriteAllText("watermark.html", null);
            string One = "  ";
            string Zero = " ";
            string pattern = " ";
            string msgBin = Msg();
            string[] Lines = File.ReadAllLines("cover.html");
            int i = 0;

            foreach (var line in Lines)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(line, pattern))
                {
                    if (i < msgBin.Length && msgBin[i].Equals('0'))
                    {
                        File.AppendAllText("watermark.html", line.Replace(pattern, Zero) + "\n");
                        i++;
                    }
                    else
                    {
                        if (i < msgBin.Length && msgBin[i].Equals('1'))
                        {
                            File.AppendAllText("watermark.html", line.Replace(pattern, One) + "\n");
                            i++;
                        }
                    }
                }
                else
                    File.AppendAllText("watermark.html", line + "\n");
            }
            if (i < msgBin.Length)
            {
                Console.WriteLine("Nosnik jest za maly.\nNacisnij przycisk aby kontynowac.");
                Console.ReadKey();
            }
            else
                Console.WriteLine("Zanurzanie skonczone");
        }

        public override void GetMsg()
        {
            string Letter = null;
            string Hex = null;
            string One = "  ";
            string Zero = " ";
            File.WriteAllText("detect.txt", null);

            string[] Lines = File.ReadAllLines("watermark.html");
            List<int> binValues = new List<int>();

            foreach (var line in Lines)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(line, One))
                {
                    binValues.Add(1);
                }
                else
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(line, Zero))
                    {
                        binValues.Add(0);
                    }
                }
            }

            // Conversion from bin to dec
            foreach (var val in binValues)
            {
                Letter += val;
                if (Letter.Length == 4)
                {
                    Hex += Convert.ToInt32(Letter, 2).ToString("X");
                    if (Hex.Length == 2)
                    {
                        int Dec = Convert.ToInt32(Hex, 16);
                        File.AppendAllText("detect.txt", ((char)Dec).ToString());
                        Hex = null;
                    }
                    Letter = null;
                }
            }
        }
    }
}
