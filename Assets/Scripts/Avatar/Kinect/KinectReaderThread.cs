using System;
using System.Collections.Generic;
using System.Collections;
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
                    foreach (User user in kinectManager.TrackedUsers.Values)
                    {
                        UserState currState;
                        currState.torsoPos = GetSkeletonJointPosition(user.ID, SkeletonJoint.Torso);
                        currState.headPos = GetSkeletonJointPosition(user.ID, SkeletonJoint.Head);
                        currState.leftShoulderPos = GetSkeletonJointPosition(user.ID, SkeletonJoint.LeftShoulder);
                        currState.rightShoulderPos = GetSkeletonJointPosition(user.ID, SkeletonJoint.RightShoulder);
                        currState.timestamp = DateTime.Now.Ticks;
                        lock (user)
                        {
                            user.AddToHistory(currState);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }

            }
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
    }
}
