using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Generators
{
    public class RandomCodeGenerators
    {
        public static string ActivationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999000).ToString();
        }

        public static string FactorNumberCode()
        {
            Random random = new Random();
            return random.Next(10000000, 99990000).ToString();
        }

        public static string CreateFileCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
