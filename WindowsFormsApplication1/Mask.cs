using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Mask
    {
        string a, b, c, d;
        public Mask(string a, string b, string c, string d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
        public string getMask(Mask maska, int netNum, int subnets)
        {
            maska.a = "11111111";
            int test = 0;
            StringBuilder sb = new StringBuilder();
            if (netNum == 8)
            {
                if (subnets <= 8)
                {

                    for (int i = 0; i < subnets; i++)
                    {
                        sb.Append("1");
                        test++;
                    }
                    while (test < 8)
                    {
                        sb.Append("0");
                        test++;
                    }
                    maska.b = sb.ToString();
                    maska.c = "00000000";
                    maska.d = "00000000";

                }
                if (subnets > 8 && subnets <= 16)
                {
                    test = 0;
                    maska.b = "11111111";
                    for (int i = 0; i < subnets; i++)
                    {
                        sb.Append("1");
                        test++;
                    }
                    while (test < 8)
                    {
                        sb.Append("0");
                        test++;
                    }
                    maska.c = sb.ToString();
                }
                maska.d = "000000000";
            }
            if (netNum == 16)
            {
                maska.b = "11111111";
                if (subnets <= 8)
                {
                    for (int i = 0; i < subnets; i++)
                    {
                        sb.Append("1");
                        test++;
                    }
                    while (test < 8)
                    {
                        sb.Append("0");
                        test++;
                    }
                    maska.c = sb.ToString();
                    maska.d = "00000000";
                }
                if (subnets > 8 && subnets <= 16)
                {
                    maska.c = "11111111";
                    for (int i = 0; i < subnets; i++)
                    {
                        sb.Append("1");
                        test++;
                    }
                    while (test < 8)
                    {
                        sb.Append("0");
                        test++;
                    }
                    maska.d = sb.ToString();
                }
            }
            if (netNum == 24)
            {
                maska.b = "11111111";
                maska.c = "11111111";
                for (int i = 0; i < subnets; i++)
                {
                    sb.Append("1");
                    test++;
                }
                while (test < 8)
                {
                    sb.Append("0");
                    test++;
                }
                maska.d = sb.ToString();
            }
            sb.Clear();
            sb.Append(Convert.ToInt32(maska.a, 2)).Append(".");
            sb.Append(Convert.ToInt32(maska.b, 2)).Append(".");
            sb.Append(Convert.ToInt32(maska.c, 2)).Append(".");
            sb.Append(Convert.ToInt32(maska.d, 2));
            return sb.ToString();
        }
    }
}
