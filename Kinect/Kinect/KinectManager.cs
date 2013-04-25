using OpenNI;
using System.Collections;
using System.Threading;
using System;

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
        private readonly string OPENNI_XML_FILE = @"OpenNIConfig.xml";

        /// <summary>
        /// The context in which OpenNI operates (based on the configuration file)
        /// </summary>
        public Context context { get; private set; }

        /// <summary>
        /// Detects and creates new users when they enter the range of the Kinect.
        /// </summary>
        public UserGenerator userGenerator { get; private set; }

        /// <summary>
        /// Holds the depth node that has to be present in the OpenNI configuration file.
        /// </summary>
        private DepthGenerator depth;

        /// <summary>
        /// This object handles user calibration and tracking.
        /// </summary>
        public SkeletonCapability skeletonCapability { get; private set; }

        /// <summary>
        /// Constructor: initializes all attributes and properties
        /// </summary>
        public KinectManager()
        {
            this.Initialize();
        }

        /// <summary>
        /// Function to initialize this classes attributes and properties. 
        /// It also starts detecting users.
        /// </summary>
        public void Initialize()
        {
            //First we need to initialize the openni context
            ScriptNode scriptNode;
            context = Context.CreateFromXmlFile(OPENNI_XML_FILE, out scriptNode);
            this.depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
            {
                throw new Exception("Viewer must have a depth node!");
            }

            //Now we can initialize skeleton tracking
            userGenerator = new UserGenerator(context);
            skeletonCapability = userGenerator.SkeletonCapability;
            skeletonCapability.SetSkeletonProfile(SkeletonProfile.Upper);


            //And initialize event handlers
            userGenerator.NewUser += OnNewUser;
            userGenerator.LostUser += OnLostUser;
            skeletonCapability.CalibrationComplete += OnCalibrationComplete;

            //And start generating data
            userGenerator.StartGenerating();
            context.StartGeneratingAll();
        }

        /// <summary>
        /// Function that is called whenever a new user enters the range of the Kinect.
        /// This function requests calibration for the new user.
        /// </summary>
        /// <param name="sender">The object that called this function</param>
        /// <param name="e">The events associated with this call; used to retrieve the users id</param>
        private void OnNewUser(object sender, NewUserEventArgs e)
        {
            Console.WriteLine("New user: {0}", e.ID);
            Console.WriteLine("Requesting calibration for user: {0}", e.ID);
            skeletonCapability.RequestCalibration(e.ID, true);
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
            Console.WriteLine("Lost user: {0}", e.ID);
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
                Console.WriteLine("Calibration succeeded on user: {0}", e.ID);
                Console.WriteLine("Start tracking user: {0}", e.ID);
                skeletonCapability.StartTracking(e.ID);
            }
            else if (e.Status != CalibrationStatus.ManualAbort)
            {
                Console.WriteLine("Calibration failed on user: {0}", e.ID);
                Console.WriteLine("Retrying calibration on user: {0}", e.ID);
                skeletonCapability.RequestCalibration(e.ID, true);
            }
        }
    }
}