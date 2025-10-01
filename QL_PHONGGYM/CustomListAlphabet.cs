using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM
{
    public class CustomListAlphabet
    {
        public static List<char> Alphabet()
        {
            var list = new List<char>();
            for (char c = 'A'; c <= 'Z'; c++)
                list.Add(c);
            for (char c = '0'; c <= '9'; c++)
                list.Add(c);
            for (char c = 'a'; c <= 'z'; c++)
                list.Add(c);
            string specialChars = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            foreach (char c in specialChars)
                list.Add(c);

            return list;

        }
    }
}
