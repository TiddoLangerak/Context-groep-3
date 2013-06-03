using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kinect
{
    /// <summary>
    /// This class is responsible for user input from the kinect.
    /// </summary>
    public class KinectUserInput : MonoBehaviour, IUserInput
    {
        /// <summary>
        /// The KinectManager associated with the input
        /// </summary>
        private KinectManager kinectMgr;

        /// <summary>
        /// The Kinect thread associated with the input
        /// </summary>
        private KinectReaderThread kinectThread;

        /// <summary>
        /// The nr. for each movement
        /// </summary>
        private Dictionary<UserMovement, int> movementFreqencies;

        /// <summary>
        /// Indicates if the kinect input is initialized
        /// </summary>
        private bool initialized = false;

        /// <summary>
        /// Doesn't destroy this object when the scene is reloaded
        /// </summary>
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Sets up and starts the kinect thread, such that input can be processed, if the input
        /// is not initialized already
        /// </summary>
        public void Initialize()
        {
            if (!initialized)
            {
                kinectMgr = new KinectManager();
                kinectThread = new KinectReaderThread(kinectMgr);
                kinectThread.Start();
                initialized = true;
            }
        }

        /// <summary>
        /// Calculate the current movement of the avatar, based on the user movements.
        /// </summary>
        /// <returns>The current movement of the avatar</returns>
        public AvatarMovement CurrentMovement()
        {
            movementFreqencies = new Dictionary<UserMovement, int>();
            Dictionary<int, User> trackedUsers;
            lock (kinectMgr)
            {
                trackedUsers = new Dictionary<int, User>(kinectMgr.TrackedUsers);
            }
            FillMovementFrequencies(trackedUsers);
            return GetCurrAvatarMovement();
        }

        /// <summary>
        /// Fills the movementfrequencies dictionary based on the movement of the tracked users
        /// </summary>
        /// <param name="trackedUsers">The tracked users</param>
        private void FillMovementFrequencies(Dictionary<int, User> trackedUsers)
        {
            foreach (User user in trackedUsers.Values)
            {
                if (user.Active)
                {
                    UserMovement movement = user.CurrentMovement;
                    UpdateMovementFreqEntry(movement);
                }
            }
        }

        /// <summary>
        /// Increments the frequency of movement with 1 if it is already in the dictionary,
        /// add the movement with frequency 1 otherwise.
        /// </summary>
        /// <param name="movement">The movement entry to update</param>
        private void UpdateMovementFreqEntry(UserMovement movement)
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

        /// <summary>
        /// Returns the current movement of the avatar
        /// </summary>
        /// <returns>The current movement of the avatar</returns>
        private AvatarMovement GetCurrAvatarMovement()
        {
            if (movementFreqencies.Count > 0)
            {
                //retrieve the movement with the maximum frequency
                UserMovement currMovement = movementFreqencies.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                if (movementFreqencies[currMovement] > StateManager.Instance.NumberOfPlayers / 2)
                {
                    return GetCorrectAvatarMovement(currMovement);
                }
            }
            return AvatarMovement.None;
        }

        /// <summary>
        /// Returns the AvatarMovement that corresponds with currMovement
        /// </summary>
        /// <param name="currMovement">The current movement of all users</param>
        /// <returns>The current movement of the avatar</returns>
        private AvatarMovement GetCorrectAvatarMovement(UserMovement currMovement)
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
            return AvatarMovement.None;
        }

        /// <summary>
        /// Stop the kinect thread and reset the nr. of players
        /// </summary>
        public void OnDestroy()
        {
            if (this.kinectThread != null)
            {
                this.kinectThread.Stop();
            }
            StateManager.Instance.NumberOfPlayers = 0;
        }
    }
}