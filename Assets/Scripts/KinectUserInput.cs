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
            List<KinectReaderThread.KinectMovement> kinectMovements;
            lock (kinectThread)
            {
                kinectMovements = new List<KinectReaderThread.KinectMovement>(kinectThread.UserMovements);
            }

            foreach (KinectReaderThread.KinectMovement movement in kinectMovements)
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

                if (movementFreqencies[currMovement] > kinectMovements.Count/2)
                {
                    switch (currMovement)
                    {
                        case KinectReaderThread.KinectMovement.Left:
                            return AvatarMovement.Left;
                        case KinectReaderThread.KinectMovement.Right:
                            return AvatarMovement.Right;
                    }
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

        /// <summary>
        /// Returns a string representation of the user input, containing the nr. of players and the current movement of
        /// each player.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            List<KinectReaderThread.KinectMovement> kinectMovements;
            lock (kinectThread)
            {
                kinectMovements = new List<KinectReaderThread.KinectMovement>(kinectThread.UserMovements);
            }

            String res = "Nr. of players: " + kinectMovements.Count + "\n\n";
            for (int idx = 0; idx < kinectMovements.Count; idx++)
            {
                res += idx + ": ";
                switch(kinectMovements[idx])
                {
                    case KinectReaderThread.KinectMovement.Left:
                        res += "Left";
                        break;
                    case KinectReaderThread.KinectMovement.Right:
                        res += "Right";
                        break;
                    default:
                        res+= "None";
                        break;
                }
                res += "\n";
            }
            return res;
        }
    }
}