using OpenNI;
using System.Collections;
using System.Threading;
using System;

namespace Kinect
{
    public class KinectManager
    {
        private readonly string OPENNI_XML_FILE = @"OpenNIConfig.xml";
        private Context context;
        private ScriptNode scriptNode;
        private UserGenerator userGenerator;
        private Thread kinectReaderThread;
        private DepthGenerator depth;
        private SkeletonCapability skeletonCapability;

        // Use this for initialization
        public void Start()
        {
            context = Context.CreateFromXmlFile(OPENNI_XML_FILE, out scriptNode);
            this.depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
            {
                throw new Exception("Viewer must have a depth node!");
            }

            userGenerator = new UserGenerator(context);
            skeletonCapability = userGenerator.SkeletonCapability;

            userGenerator.NewUser += OnNewUser;
            userGenerator.LostUser += OnLostUser;
            skeletonCapability.CalibrationComplete += OnCalibrationComplete;
            skeletonCapability.SetSkeletonProfile(SkeletonProfile.Upper);
            userGenerator.StartGenerating();

            kinectReaderThread = new Thread(KinectReaderThread);
            kinectReaderThread.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnNewUser(object sender, NewUserEventArgs e)
        {
            //Debug.Log("New user: "+ e.ID);
            System.Console.WriteLine("New user: {0}", e.ID);
            System.Console.WriteLine("Requesting calibration for user: {0}", e.ID);
            skeletonCapability.RequestCalibration(e.ID, true);
        }

        private void OnLostUser(object sender, UserLostEventArgs e)
        {
            //Debug.Log("Lost user: " + e.ID);
            System.Console.WriteLine("Lost user: {0}", e.ID);
        }

        private void OnCalibrationComplete(object sender, CalibrationProgressEventArgs e)
        {
            if (e.Status == CalibrationStatus.OK)
            {
                System.Console.WriteLine("Calibration succeeded on user: {0}", e.ID);
                System.Console.WriteLine("Start tracking user: {0}", e.ID);
                skeletonCapability.StartTracking(e.ID);
            }
            else if (e.Status != CalibrationStatus.ManualAbort)
            {
                System.Console.WriteLine("Calibration failed on user: {0}", e.ID);
                System.Console.WriteLine("Retrying calibration on user: {0}", e.ID);
                skeletonCapability.RequestCalibration(e.ID, true);
            }
        }

        private void KinectReaderThread()
        {
            while (true)
            {
                try
                {
                    context.WaitAndUpdateAll();

                    lock (this)
                    {
                        int[] users = userGenerator.GetUsers();
                        if (users.Length > 0)
                        {
                            int currUser = users[0];
                            Point3D centerOfMass = userGenerator.GetCoM(currUser);
                            Console.WriteLine("CoM: ({0};{1};{2})", centerOfMass.X, centerOfMass.Y, centerOfMass.Z);
                            SkeletonJointPosition leftShoulderPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.LeftShoulder);
                            SkeletonJointPosition rightShoulderPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.RightShoulder);
                            Console.WriteLine("left shoulder: ({0};{1};{2})", leftShoulderPos.Position.X, leftShoulderPos.Position.Y, leftShoulderPos.Position.Z);
                            Console.WriteLine("right shoulder: ({0};{1};{2})", rightShoulderPos.Position.X, rightShoulderPos.Position.Y, rightShoulderPos.Position.Z);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}