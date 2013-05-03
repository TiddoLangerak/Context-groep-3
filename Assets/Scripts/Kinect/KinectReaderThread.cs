using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenNI;
using UnityEngine;

namespace Kinect
{
    /// <summary>
    /// In this class we will process the skeleton data of the user.
    /// This data can be used to detect player movement, which is also the task of the thread 
    /// that is created in this class.
    /// </summary>
    class KinectReaderThread
    {
        /// <summary>
        /// Constant indicating the minimal ratio necessary to detect leaning of the user.
        /// This ratio is used together with the TRESHOLD_LEANING_HEAD ratio
        /// The leaning ratio is calculated as followed:
        ///     -a = The horizontal distance between right shoulder and torso joints
        ///     -b = The horizontal distance between left shoulder and torso joins
        ///     Leaning ratio left: b/a
        ///     Leaning ratio right: a/b
        /// </summary>
        private const double TRESHOLD_LEANING = 1.5;

        /// <summary>
        /// Constant indicating the minimal ratio necessary to detect leaning of the user.
        /// This ratio is used together with the TRESHOL_LEANING ratio.
        /// The leaning head ratio is calculated as followed:
        ///     -a = horizontal distance between head and torso joints
        ///     -b = horizontal distance between shoulders
        ///     Leaning head ratio = a/b
        /// </summary>
        private const double TRESHOLD_LEANING_HEAD = 0.5;

        /// <summary>
        /// Used to indicate the direction of a user.
        /// Note: We can't use the Movement enum from IUserInput, since the Kinect might send other movements to the game
        ///     than the movements the avatar will make. We're planning to combine inputs from multiple players
        ///     to one avatar movement.
        /// </summary>
        public enum Movement
        {
            None,
            Left,
            Right
        };

        /// <summary>
        /// The current direction of the user that is being tracked.
        /// </summary>
        public Movement CurrentMovement { get; private set; }

        /// <summary>
        /// Object needed to communicate with the Kinect.
        /// </summary>
        private KinectManager kinectManager;

        /// <summary>
        /// Thread object used to create a new thread and get this thread running.
        /// </summary>
        private Thread theThread;

        /// <summary>
        /// 
        /// </summary>
        private bool shouldRun;

        /// <summary>
        /// Constructor: initialize the kinectManager.
        /// </summary>
        /// <param name="kinectManager">KinectManager used to communicate with</param>
        public KinectReaderThread(KinectManager kinectManager)
            : base()
        {
            this.kinectManager = kinectManager;
            this.CurrentMovement = Movement.STRAIGHT;
        }

        /// <summary>
        /// Starts this thread. After this function is called, the thread starts executing its functionality.
        /// </summary>
        public void Start()
        {
            this.theThread = new Thread(new ThreadStart(this.RunThread));
            this.shouldRun = true;
            theThread.Start();
        }

        /// <summary>
        /// Stop the thread. After the thread is stopped, it will die.
        /// </summary>
        public void Stop()
        {
            this.shouldRun = false;
        }

        /// <summary>
        /// The function that is called when the thread is started. 
        /// This function is therefore responsible for the functionality of the thread.
        /// 
        /// The functionality of this thread is detecting player movement using the position of the shoulders, head and torso of the user. 
        /// We consider a player to be leaning to the left or the right when
        /// - His head is tilted
        /// - His shoulders don't have an equal horizontal distance to the torso
        /// </summary>
        private unsafe void RunThread()
        {
            while (this.shouldRun)
            {
                try
                {
                    kinectManager.Context.WaitAnyUpdateAll();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }

                lock (this)
                {
                    int[] users = kinectManager.UserGenerator.GetUsers();
                    if (users.Length > 0)
                    {
                        int currUser = users[0];
                        SkeletonJointPosition torsoPos = GetSkeletonJointPosition(currUser, SkeletonJoint.Torso);
                        SkeletonJointPosition headPos = GetSkeletonJointPosition(currUser, SkeletonJoint.Head);
                        SkeletonJointPosition leftShoulderPos = GetSkeletonJointPosition(currUser, SkeletonJoint.LeftShoulder);
                        SkeletonJointPosition rightShoulderPos = GetSkeletonJointPosition(currUser, SkeletonJoint.RightShoulder);

                        CurrentMovement = CalculateCurrentMovement(torsoPos, headPos, leftShoulderPos, rightShoulderPos);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the users current movement.
        /// It needs some joints as input, and then tries to calculate it's movement
        /// </summary>
        /// <param name="torsoPos">The position of the users torso</param>
        /// <param name="headPos">The position of the users head</param>
        /// <param name="leftShoulderPos">The position of the users left shoulder</param>
        /// <param name="rightShoulderPos">The position of the users right shoulder</param>
        /// <returns>The users current direction</returns>
        private Movement CalculateCurrentMovement(SkeletonJointPosition torsoPos, SkeletonJointPosition headPos, SkeletonJointPosition leftShoulderPos, SkeletonJointPosition rightShoulderPos)
        {
            float leftDistance = Math.Abs(leftShoulderPos.Position.X - torsoPos.Position.X);
            float rightDistance = Math.Abs(rightShoulderPos.Position.X - torsoPos.Position.X);
            float headDistance = Math.Abs(headPos.Position.X - torsoPos.Position.X);
            float shoulderDistance = Math.Abs(leftShoulderPos.Position.X - rightShoulderPos.Position.X);
            float normalizedHeadDistance = headDistance / shoulderDistance;

            /// If the head is far of center, the user will most likely be leaning to one way or the other
            if (normalizedHeadDistance > TRESHOLD_LEANING_HEAD)
            {
                //Now we can check if the player was indeed leaning one way or the other
                if (leftDistance / rightDistance > TRESHOLD_LEANING)
                {
                    return Movement.Left;
                }
                else if (rightDistance / leftDistance > TRESHOLD_LEANING)
                {
                    return Movement.Right;
                }
            }
            return Movement.None;
        }

        /// <summary>
        /// Returns the skelJointPos of user. Created to increase code readability.
        /// </summary>
        /// <param name="user">The user of which we want to get a certain position</param>
        /// <param name="skelJoint">The SkeletonJoint which we want to retrieve</param>
        /// <returns></returns>
        private SkeletonJointPosition GetSkeletonJointPosition(int user, SkeletonJoint skelJoint)
        {
            return kinectManager.SkeletonCapability.GetSkeletonJointPosition(user, skelJoint);
        }

        /// <summary>
        /// Prints the current direction of the user for debugging purposes
        /// </summary>
        private void PrintCurrentMovement()
        {
            switch (CurrentMovement)
            {
                case Movement.Left:
                    Debug.Log("Going to the left");
                    break;
                case Movement.Right:
                    Debug.Log("Going to the right");
                    break;
                case Movement.None:
                    Debug.Log("Going straight ahead");
                    break;
            }
        }
    }
}
