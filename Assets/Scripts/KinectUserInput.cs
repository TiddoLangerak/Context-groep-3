using System;
using Kinect;

public class KinectUserInput : IUserInput
{
    private KinectReaderThread kinectThread;
	
    public KinectUserInput()
    {
    }

    public void Initialize()
    {
        KinectManager kinectMgr = new KinectManager();
        kinectThread = new KinectReaderThread(kinectMgr);
        kinectThread.Start();
    }

    public int CurrentMovement()
    {
        switch (kinectThread.CurrentMovement)
        {
            case KinectReaderThread.Movement.LEFT:
                return 1;
            case KinectReaderThread.Movement.RIGHT:
                return 2;
            default:
                return 0;
        }
    }

    public void Destroy()
    {
        this.kinectThread.Stop();
    }
}