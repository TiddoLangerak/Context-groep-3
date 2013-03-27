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

        private const double TRESHOLD_LEANING = 1.5;
        private const double TRESHOLD_LEANING_HEAD = 0.5;

        private enum Direction {
            STRAIGHT,
            LEFT,
            RIGHT
        };

        private Direction currentDirection = Direction.STRAIGHT;

        // Use this for initialization
        public void Start()
        {
            //First we need to initialize the openni context
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

            //And start everything
            userGenerator.StartGenerating();
            kinectReaderThread = new Thread(KinectReaderThread);
            kinectReaderThread.Start();
        }
        
        private void OnNewUser(object sender, NewUserEventArgs e)
        {
            //new user, we only log things for now
            //Debug.Log("New user: "+ e.ID);
            System.Console.WriteLine("New user: {0}", e.ID);
            System.Console.WriteLine("Requesting calibration for user: {0}", e.ID);
            skeletonCapability.RequestCalibration(e.ID, true);
        }

        private void OnLostUser(object sender, UserLostEventArgs e)
        {
            //lost user, we don't de anything here yet
            //Debug.Log("Lost user: " + e.ID);
            System.Console.WriteLine("Lost user: {0}", e.ID);
        }

        private void OnCalibrationComplete(object sender, CalibrationProgressEventArgs e)
        {
            //When calibration is complete and finished we can start tracking the user.
            //Otherwise, we simply try to calibrate again.
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
                        //Here we will process the skeleton data of the user.
                        //Using the shoulders, head and torso position of the user we can determine if the user leans to the left or to the right.
                        //We do this by measuring the horizontal distance between each shoulder and the torso. 
                        //If the distance between one shoulder and the torso is far greater than 
                        //  the distance between the other shoulder and the torso we know the player is probably leaning one way.
                        //We have added a second check which checks if the normalized distance between the head and the torso
                        //  is also greater than a certain treshold. 
                        //To summarize the above:
                        //We consider a player to be leaning to the left or the right when
                        // - His head is tilted
                        // - His shoulders don't have an equal horizontal distance to the torso

                        int[] users = userGenerator.GetUsers();
                        if (users.Length > 0)
                        {
                            int currUser = users[0];
                            SkeletonJointPosition torsoPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.Torso);
                            SkeletonJointPosition headPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.Head);
                            SkeletonJointPosition leftShoulderPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.LeftShoulder);
                            SkeletonJointPosition rightShoulderPos = skeletonCapability.GetSkeletonJointPosition(currUser, SkeletonJoint.RightShoulder);

                            var leftDistance = Math.Abs(leftShoulderPos.Position.X - torsoPos.Position.X);
                            var rightDistance = Math.Abs(rightShoulderPos.Position.X - torsoPos.Position.X);
                            var headDistance = Math.Abs(headPos.Position.X - torsoPos.Position.X);
                            var shoulderDistance = Math.Abs(leftShoulderPos.Position.X - rightShoulderPos.Position.X);
                            var normalizedHeadDistance = headDistance / shoulderDistance;

                            if (normalizedHeadDistance > TRESHOLD_LEANING_HEAD)
                            {

                                if (leftDistance / rightDistance > TRESHOLD_LEANING)
                                {
                                    currentDirection = Direction.LEFT;
                                }
                                else if (rightDistance / leftDistance > TRESHOLD_LEANING)
                                {
                                    currentDirection = Direction.RIGHT;
                                }
                                else
                                {
                                    currentDirection = Direction.STRAIGHT;
                                }
                            }
                            else
                            {
                                currentDirection = Direction.STRAIGHT;
                            }

                            //Print the direction for debugging purposes
                            switch (currentDirection)
                            {
                                case Direction.LEFT:
                                    Console.WriteLine("Going to the left");
                                    break;
                                case Direction.RIGHT:
                                    Console.WriteLine("Going to the right");
                                    break;
                                case Direction.STRAIGHT:
                                    Console.WriteLine("Going straight ahead");
                                    break;
                            }

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