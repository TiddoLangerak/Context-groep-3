using System;
using System.Collections.Generic;
using System.Linq;
using OpenNI;
using System.Text;

namespace Kinect
{
    public struct UserState
    {
        public SkeletonJointPosition torsoPos,headPos,leftShoulderPos,rightShoulderPos;
        public long timestamp;
    }

    /// <summary>
    /// Used to indicate the direction of a user.
    /// Note: We can't use the Movement enum from IUserInput, since the Kinect might send other movements to the game
    ///     than the movements the avatar will make. We're planning to combine inputs from multiple players
    ///     to one avatar movement.
    /// </summary>
    public enum UserMovement
    {
        None,
        Left,
        Right,
        Jump
    };

    class User
    {
        public int ID { get; private set; }
        private LinkedList<UserState> movementHistory;        

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

        public UserMovement currentMovement
        {
            get
            {
                return CalculateCurrentMovement();
            }
            private set;
        }

        public User(int id)
        {
            this.ID = id;
        }

        /// <summary>
        /// Adds currState to the history and removes old states.
        /// </summary>
        /// <param name="currState">The current state of the user</param>
        public void AddToHistory(UserState currState)
        {
            movementHistory.AddLast(currState);
            while (movementHistory.First.Value.timestamp < (currState.timestamp - 500 * 10000))
            {
                movementHistory.RemoveFirst();
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
        private UserMovement CalculateCurrentMovement()
        {
            lock (this)
            {
                if (movementHistory.Count > 0)
                {
                    UserState currState = movementHistory.Last.Value;
                    float leftDistance = Math.Abs(currState.leftShoulderPos.Position.X - currState.torsoPos.Position.X);
                    float rightDistance = Math.Abs(currState.rightShoulderPos.Position.X - currState.torsoPos.Position.X);
                    float headDistance = Math.Abs(currState.headPos.Position.X - currState.torsoPos.Position.X);
                    float shoulderDistance = Math.Abs(currState.leftShoulderPos.Position.X - currState.rightShoulderPos.Position.X);
                    float normalizedHeadDistance = headDistance / shoulderDistance;

                    /// If the head is far of center, the user will most likely be leaning to one way or the other
                    if (normalizedHeadDistance > TRESHOLD_LEANING_HEAD)
                    {
                        //Now we can check if the player was indeed leaning one way or the other
                        if (leftDistance / rightDistance > TRESHOLD_LEANING)
                        {
                            return UserMovement.Left;
                        }
                        else if (rightDistance / leftDistance > TRESHOLD_LEANING)
                        {
                            return UserMovement.Right;
                        }
                    }
                }
            }
            return UserMovement.None;
        }
    }
}
