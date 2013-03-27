using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kinect
{
    class Program
    {
        static void Main(string[] args)
        {
            KinectManager km = new KinectManager();
            km.Start();
            System.Console.ReadLine();
        }
    }
}
