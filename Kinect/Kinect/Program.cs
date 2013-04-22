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
            KinectReaderThread krt = new KinectReaderThread(new KinectManager());
            krt.Start();
            Console.ReadLine();
        }
    }
}
