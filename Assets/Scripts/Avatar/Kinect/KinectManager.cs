using OpenNI;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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
        private readonly string OPENNI_XML_FILE = Application.dataPath + @"/OpenNIConfig.xml";

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
        /// Keeps tracks of the users that are currently being tracked.
        /// </summary>
        public Dictionary<int, User> TrackedUsers { get; private set; }

        /// <summary>
        /// Constructor: initializes all attributes and properties of the Kinect (incl. OpenNI)
        /// It also starts detecting users.
        /// </summary>
        public KinectManager()
        {
            ScriptNode scriptNode;
            Context = Context.CreateFromXmlFile(OPENNI_XML_FILE, out scriptNode);
            InitializeDepth();
            InitializeUserGenerator();
            SkeletonCapability = UserGenerator.SkeletonCapability;
            SkeletonCapability.SetSkeletonProfile(SkeletonProfile.Upper);
            InitializeEventHandlers();
            this.TrackedUsers = new Dictionary<int, User>();
        }

        /// <summary>
        /// Initializes the usergenerator based on the config file
        /// </summary>
        private void InitializeUserGenerator()
        {
            UserGenerator = Context.FindExistingNode(NodeType.User) as UserGenerator;
            if (this.UserGenerator == null)
            {
                throw new OpenNI.GeneralException("Viewer must have a user node!");
            }
        }

        /// <summary>
        /// Initializes the depth variable based on the config file
        /// </summary>
        private void InitializeDepth()
        {
            this.depth = Context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
            {
                throw new OpenNI.GeneralException("Viewer must have a depth node!");
            }
        }

        /// <summary>
        /// Initializes the event handler for the OpenNI events
        /// </summary>
        private void InitializeEventHandlers()
        {
            UserGenerator.NewUser += OnNewUser;
            UserGenerator.UserReEnter += OnUserReEnter;
            UserGenerator.UserExit += OnUserExit;
            UserGenerator.LostUser += OnLostUser;
            SkeletonCapability.CalibrationComplete += OnCalibrationComplete;
        }

        /// <summary>
        /// Function that is called whenever a new user enters the range of the Kinect.
        /// This function requests calibration for the new user after 3 seconds.
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnNewUser(object sender, NewUserEventArgs e)
        {
            Logger.Log("New user: " + e.ID);
            System.Timers.Timer calibrationTimer = new System.Timers.Timer();
            calibrationTimer.Interval = 3000;
            calibrationTimer.Elapsed += (src, a) => RequestCalibrationForUser(src, a, e.ID);
            calibrationTimer.Start();
        }

        /// <summary>
        /// Function that is called whenever a user re-enters the range of the Kinect.
        /// This function set the user to active and increases the nr. of players
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnUserReEnter(object sender, UserReEnterEventArgs e)
        {
            Logger.Log("Re-enter user: " + e.ID);
            if (TrackedUsers.ContainsKey(e.ID))
            {
                StateManager.Instance.NumberOfPlayers++;
                TrackedUsers[e.ID].Active = true;
            }
        }

        /// <summary>
        /// Function that is called whenever a user can't be tracked anymore, 
        /// most likely because it left the range of the Kinect. It sets the user to inactive and 
        /// decreases the nr. of players
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnUserExit(object sender, UserExitEventArgs e)
        {
            Logger.Log("Exit user: " + e.ID);
            if (TrackedUsers.ContainsKey(e.ID))
            {
                StateManager.Instance.NumberOfPlayers--;
                TrackedUsers[e.ID].Active = false;
            }
        }

        /// <summary>
        /// Function that is called whenever a user is lost, most likely because it left the range of the Kinect for a while. 
        /// It removes the user from the tracked users and corrects the nr. of players
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnLostUser(object sender, UserLostEventArgs e)
        {
            if (TrackedUsers.ContainsKey(e.ID))
            {
                TrackedUsers.Remove(e.ID);
                int nr = TrackedUsers.Where(u => u.Value.Active).Count();
                StateManager.Instance.NumberOfPlayers = nr;
            }
            Logger.Log("Lost user: " + e.ID);
        }

        /// <summary>
        /// Function that is called whenever the calibration of a new user is complete.
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
                TrackedUsers.Add(e.ID, new User(e.ID));
                StateManager.Instance.NumberOfPlayers++;
            }
            else if (e.Status != CalibrationStatus.ManualAbort)
            {
                Logger.Log("Calibration failed on user: " + e.ID);
                Logger.Log("Retrying calibration on user: " + e.ID);
                SkeletonCapability.RequestCalibration(e.ID, false);
            }
        }

        /// <summary>
        /// Stop timer and request calibration for the user with ID == userID
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call</param>
        /// <param name="userID">The id of the user</param>
        private void RequestCalibrationForUser(object sender, ElapsedEventArgs e, int userID)
        {
            if (sender.GetType() == typeof(System.Timers.Timer))
            {
                ((System.Timers.Timer)sender).Dispose();
            }

            Logger.Log("Requesting calibration for user: " + userID);
            SkeletonCapability.RequestCalibration(userID, true);
        }
    }
}