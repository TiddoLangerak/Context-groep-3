using OpenNI;
using System.Collections;
using System.Threading;
using System;
using UnityEngine;

namespace Kinect
{
    /// <summary>
    /// KinectManager: establishes a connection with the Kinect and it keeps track of the number of users.
    /// </summary>
    public class KinectManager
    {
        /// <summary>
        /// The file name and location of the OpenNI configuration file used in this class.
        /// </summary>
        private readonly string OPENNI_XML_FILE = Application.dataPath + @"/Scripts/Kinect/OpenNIConfig.xml";

        /// <summary>
        /// The context in which OpenNI operates (based on the configuration file)
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        /// Detects and creates new users when they enter the range of the Kinect.
        /// </summary>
        public UserGenerator UserGenerator { get; private set; }

        /// <summary>
        /// Holds the depth node that has to be present in the OpenNI configuration file.
        /// </summary>
        private DepthGenerator depth;

        /// <summary>
        /// This object handles user calibration and tracking.
        /// </summary>
        public SkeletonCapability SkeletonCapability { get; private set; }

        /// <summary>
        /// Constructor: initializes all attributes and properties.
        /// It also starts detecting users.
        /// </summary>
        public KinectManager()
        {
            //First we need to initialize the openni context
            ScriptNode scriptNode;
            Context = Context.CreateFromXmlFile(OPENNI_XML_FILE, out scriptNode);
            this.depth = Context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
            {
                throw new Exception("Viewer must have a depth node!");
            }

            //Now we can initialize skeleton tracking
            UserGenerator = Context.FindExistingNode(NodeType.User) as UserGenerator;
            if (this.UserGenerator == null)
            {
                throw new Exception("Viewer must have a user node!");
            }

            SkeletonCapability = UserGenerator.SkeletonCapability;
            SkeletonCapability.SetSkeletonProfile(SkeletonProfile.Upper);

            //And initialize event handlers
            UserGenerator.NewUser += OnNewUser;
            UserGenerator.LostUser += OnLostUser;
            SkeletonCapability.CalibrationComplete += OnCalibrationComplete;
        }

        /// <summary>
        /// Function that is called whenever a new user enters the range of the Kinect.
        /// This function requests calibration for the new user.
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnNewUser(object sender, NewUserEventArgs e)
        {
            Logger.Log("New user: " + e.ID);
            Logger.Log("Requesting calibration for user: " + e.ID);
            SkeletonCapability.RequestCalibration(e.ID, true);
        }

        /// <summary>
        /// Function that is called whenever a user can't be tracked anymore, 
        /// most likely because it left the range of the Kinect.
        /// This function doesn't have any useful functionality yet, it just prints some debug info.
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnLostUser(object sender, UserLostEventArgs e)
        {
            Logger.Log("Lost user: " + e.ID);
        }

        /// <summary>
        /// Function that is called whenever the calibration of a new is complete.
        /// On a succesful calibration, the user will be tracked from now on.
        /// On a failure (not manual), the calibration for this user it retried.
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnCalibrationComplete(object sender, CalibrationProgressEventArgs e)
        {
            if (e.Status == CalibrationStatus.OK)
            {
                Logger.Log("Calibration succeeded on user: " + e.ID);
                Logger.Log("Start tracking user: " + e.ID);
                SkeletonCapability.StartTracking(e.ID);
            }
            else if (e.Status != CalibrationStatus.ManualAbort)
            {
                Logger.Log("Calibration failed on user: " + e.ID);
                Logger.Log("Retrying calibration on user: " + e.ID);
                SkeletonCapability.RequestCalibration(e.ID, true);
            }
        }
    }
}