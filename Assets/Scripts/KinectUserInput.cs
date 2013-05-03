using System;

namespace Kinect
{

    public class KinectUserInput : IUserInput
    {
        private KinectReaderThread kinectThread;

        public KinectUserInput()
        {
        }

        public void Initialize()
        {
            KinectManager kinectMgr = new KinectManager();
            kinectThread = new KinectReaderThread(kinectMgr);
            kinectThread.Start();
        }

        public Movement CurrentMovement()
        {
            switch (kinectThread.CurrentMovement)
            {
                case KinectReaderThread.Movement.Left:
                    return Movement.Left;
                case KinectReaderThread.Movement.Right:
                    return Movement.Right;
                default:
                    return 0;
            }
        }

        public void Destroy()
        {
            this.kinectThread.Stop();
        }
    }
}