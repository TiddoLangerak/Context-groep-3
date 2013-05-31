using System;
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
        /// Indicates if the thread should be run
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
        /// The functionality of this thread is retrieving the current state of the users.
        /// </summary>
        private unsafe void RunThread()
        {
            while (this.shouldRun)
            {
                try
                {
                    kinectManager.Context.WaitAnyUpdateAll();
                    AddCurrentStatesToHistories();
                }
                catch (OpenNI.GeneralException ex)
                {
                    Logger.Log(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Creates and adds the current state of all users to their history
        /// </summary>
        private unsafe void AddCurrentStatesToHistories()
        {
            foreach (User user in kinectManager.TrackedUsers.Values)
            {
                UserState currState = GetCurrentStateOfUser(user.ID);
                lock (user)
                {
                    user.AddToHistory(currState);
                }
            }
        }

        /// <summary>
        /// Calculates the users current state
        /// </summary>
        /// <param name="userID">The id of the user</param>
        /// <returns>The current state of the user</returns>
        private unsafe UserState GetCurrentStateOfUser(int userID)
        {
            UserState currState;
            currState.torsoPos = GetSkeletonJointPosition(userID, SkeletonJoint.Torso);
            currState.headPos = GetSkeletonJointPosition(userID, SkeletonJoint.Head);
            currState.leftShoulderPos = GetSkeletonJointPosition(userID, SkeletonJoint.LeftShoulder);
            currState.rightShoulderPos = GetSkeletonJointPosition(userID, SkeletonJoint.RightShoulder);
            currState.timestamp = DateTime.Now.Ticks;
            return currState;
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
