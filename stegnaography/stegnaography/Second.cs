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
            
            string msgBin = Msg();
            string[] Lines = File.ReadAllLines("cover.html");
            int i = 0;

            foreach (var line in Lines)
            {
                if (i < msgBin.Length && line.Contains(" "))
                {
                    foreach (var ch in line)
                    {
                        if (i < msgBin.Length && Char.IsWhiteSpace(ch) && msgBin[i].Equals('1'))
                        {
                            File.AppendAllText("watermark.html", "  ");
                            i++;
                        }
                        else
                        {
                            if (i < msgBin.Length && Char.IsWhiteSpace(ch) && msgBin[i].Equals('0'))
                            {
                                File.AppendAllText("watermark.html", ch.ToString());
                                i++;
                            }
                            else
                                File.AppendAllText("watermark.html", ch.ToString(), Encoding.GetEncoding("Windows-1250"));
                        }

                        
                    }
                }
                else
                    File.AppendAllText("watermark.html", line + "\n");
            }
        }

        public override void GetMsg()
        {
            string BlankSpace = "  ";
            string Letter = null;
            string Hex = null;
            File.WriteAllText("detect.txt", null);

            string[] Lines = File.ReadAllLines("watermark.html");
            List<int> binValues = new List<int>();

            foreach (var line in Lines)
            {
                if (line.Contains(BlankSpace))
                {
                    for (var i = 0; i < line.Length; i++)
                    {
                        if (Char.IsWhiteSpace(line[i]))
                        {
                            int j = i;
                            j++;
                            if (j < line.Length && Char.IsWhiteSpace(line[j]))
                            {
                                binValues.Add(1);
                                i++;
                            }
                            else
                            {
                                binValues.Add(0);
                                i++;
                            }
                        }
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
