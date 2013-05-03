using System;

namespace Kinect
{
    /// <summary>
    /// This class is responsible for user input from the kinect.
    /// </summary>
    public class KinectUserInput : IUserInput
    {
        private KinectReaderThread kinectThread;

        public KinectUserInput()
        {
        }

        /// <summary>
        /// Sets up and starts the kinect thread, such that input can be processed
        /// </summary>
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