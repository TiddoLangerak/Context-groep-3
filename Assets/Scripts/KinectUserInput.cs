using System.Linq;
using System;
using System.Collections.Generic;

namespace Kinect
{
    /// <summary>
    /// This class is responsible for user input from the kinect.
    /// </summary>
    public class KinectUserInput : IUserInput
    {
        private KinectReaderThread kinectThread;

        /// <summary>
        /// Sets up and starts the kinect thread, such that input can be processed
        /// </summary>
        public void Initialize()
        {
            KinectManager kinectMgr = new KinectManager();
            kinectThread = new KinectReaderThread(kinectMgr);
            kinectThread.Start();
        }

        /// <summary>
        /// Calculate the current movement of the avatar, based on the user movements.
        /// </summary>
        /// <returns>The current movement of the avatar</returns>
        public AvatarMovement CurrentMovement()
        {
            Dictionary<KinectReaderThread.KinectMovement, int> movementFreqencies = new Dictionary<KinectReaderThread.KinectMovement, int>();
            foreach (KinectReaderThread.KinectMovement movement in kinectThread.UserMovements)
            {
                if (movementFreqencies.ContainsKey(movement))
                {
                    movementFreqencies[movement]++;
                }
                else
                {
                    movementFreqencies.Add(movement, 1);
                }
            }

            if (movementFreqencies.Count > 0)
            {
                //retrieve the movement with the maximum frequency
                KinectReaderThread.KinectMovement currMovement = movementFreqencies.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                switch (currMovement)
                {
                    case KinectReaderThread.KinectMovement.Left:
                        return AvatarMovement.Left;
                    case KinectReaderThread.KinectMovement.Right:
                        return AvatarMovement.Right;
                    default:
                        return AvatarMovement.None;
                }
            }
            return AvatarMovement.None;
        }

        /// <summary>
        /// Stop the kinect thread.
        /// </summary>
        public void Destroy()
        {
            this.kinectThread.Stop();
        }
    }
}