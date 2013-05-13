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
        private KinectManager kinectMgr;
        private KinectReaderThread kinectThread;

        /// <summary>
        /// Sets up and starts the kinect thread, such that input can be processed
        /// </summary>
        public void Initialize()
        {
            kinectMgr = new KinectManager();
            kinectThread = new KinectReaderThread(kinectMgr);
            kinectThread.Start();
        }

        /// <summary>
        /// Calculate the current movement of the avatar, based on the user movements.
        /// </summary>
        /// <returns>The current movement of the avatar</returns>
        public AvatarMovement CurrentMovement()
        {
            Dictionary<UserMovement, int> movementFreqencies = new Dictionary<UserMovement, int>();
            Dictionary<int, User> trackedUsers;
            lock (kinectMgr)
            {
                trackedUsers = new Dictionary<int, User>(kinectMgr.TrackedUsers);
            }

            foreach (User user in trackedUsers.Values)
            {
                UserMovement movement = user.currentMovement;
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
                UserMovement currMovement = movementFreqencies.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                if (movementFreqencies[currMovement] > trackedUsers.Count/2)
                {
                    switch (currMovement)
                    {
                        case UserMovement.Left:
                            return AvatarMovement.Left;
                        case UserMovement.Right:
                            return AvatarMovement.Right;
                        case UserMovement.Jump:
                            return AvatarMovement.Jump;
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
            Dictionary<int, User> trackedUsers;
            lock (kinectMgr)
            {
                trackedUsers = new Dictionary<int, User>(kinectMgr.TrackedUsers);
            }

            String res = "Nr. of players: " + trackedUsers.Count + "\n\n";
            foreach (User user in trackedUsers.Values )
            {
                res += user.ID + ": ";
                switch(user.currentMovement)
                {
                    case UserMovement.Left:
                        res += "Left";
                        break;
                    case UserMovement.Right:
                        res += "Right";
                        break;
                    case UserMovement.Jump:
                        res += "Jump";
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