using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp_Forms_.Utils
{
    public class Validation
    {
        static public bool CheckOptionValidation(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
