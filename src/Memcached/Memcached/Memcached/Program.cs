using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace Memcached
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppHost().Init().Start("http://*:2337/");
            "ServiceStack SelfHost listening at http://localhost:2337 ".Print();
            Process.Start("http://localhost:2337/");

            Console.ReadLine();
        }
    }
}
