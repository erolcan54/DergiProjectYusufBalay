using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.PasswordCreate
{
    public static class PasswordCr
    {
        public static string CodeGenereate()
        {
            char[] dizi = new char[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V',
                'W', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            Random rnd=new Random();
            string sifre = "";
            
            
            for (int i = 0; i < 6; i++)
            {
                int sayi = rnd.Next(0, dizi.Length);
                sifre += dizi[sayi];
            }
            return sifre;
        }
    }
}
