using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace stegnaography
{
    public abstract class Message
    {
        public static string Msg()
        {
            string messageHex = File.ReadAllText("mess.txt");
            string messageBin = String.Join(String.Empty, messageHex.Select
                (
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0') // converting from hex to binary
                )
            );

            return messageBin;
        }

        public abstract void GetMsg();
    }
}
