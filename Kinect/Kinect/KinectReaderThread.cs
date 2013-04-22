using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenNI;

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
        /// The update frequency, set to 30 times per seconds
        /// </summary>
        private const double UPDATE_FREQUENCY = 30;

        /// <summary>
        /// The update interval, set to 1/UPDATE_FREQUENCY seconds 
        /// </summary>
        private TimeSpan UPDATE_INTERVAL = TimeSpan.FromMilliseconds(1000.0 / UPDATE_FREQUENCY);

        /// <summary>
        /// Constant indicating the minimal ratio necessary to detect leaning of the user.
        /// </summary>
        private const double TRESHOLD_LEANING = 1.5;

        /// <summary>
        /// Constant indicating the minimal ratio necessary to detect leaning of the user.
        /// </summary>
        private const double TRESHOLD_LEANING_HEAD = 0.5;

        /// <summary>
        /// Used to indicate the direction of a user.
        /// </summary>
        public enum Movement
        {
            STRAIGHT,
            LEFT,
            RIGHT
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
        /// Constructor: initialize the kinectManager and the standardThread attribute. 
        /// </summary>
        /// <param name="kinectManager">KinectManager used to communicate with</param>
        public KinectReaderThread(KinectManager kinectManager)
            : base()
        {
            this.kinectManager = kinectManager;
            this.kinectManager.Initialize();
            this.CurrentMovement = Movement.STRAIGHT;
            this.theThread = new Thread(new ThreadStart(this.RunThread));
        }

        /// <summary>
        /// Starts this thread. After this function is called, the thread starts executing its functionality.
        /// </summary>
        public void Start()
        {
            theThread.Start();
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
        private void RunThread()
        {
            while (true)
            {
                try
                {
                    //DateTime timeBeforeUpdate = DateTime.Now;
                    kinectManager.Context.WaitAnyUpdateAll();

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
                            PrintCurrentMovement();
                        }
                    }

                    /*DateTime timeAfterUpdate = DateTime.Now;
                    TimeSpan updateDuration = timeAfterUpdate.Subtract(timeBeforeUpdate);
                    if(UPDATE_INTERVAL.Subtract(updateDuration).Milliseconds > 0)
                    {
                        Thread.Sleep(UPDATE_INTERVAL.Subtract(updateDuration).Milliseconds);
                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Calculates the users current direction based on the position of his shoulders, head and torso.
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

            if (normalizedHeadDistance > TRESHOLD_LEANING_HEAD)
            {
                if (leftDistance / rightDistance > TRESHOLD_LEANING)
                {
                    return Movement.LEFT;
                }
                else if (rightDistance / leftDistance > TRESHOLD_LEANING)
                {
                    return Movement.RIGHT;
                }
            }
            return Movement.STRAIGHT;
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
                case Movement.LEFT:
                    Console.WriteLine("Going to the left");
                    break;
                case Movement.RIGHT:
                    Console.WriteLine("Going to the right");
                    break;
                case Movement.STRAIGHT:
                    Console.WriteLine("Going straight ahead");
                    break;
            }
        }
    }
}
