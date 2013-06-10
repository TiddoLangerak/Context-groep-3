using System;
using System.Collections.Generic;
using System.Linq;
using OpenNI;
using Util;

namespace Kinect
{
    /// <summary>
    /// Contains the state of a user, i.e. its positions and the timestamp of the state
    /// </summary>
    public struct UserState
    {
        public SkeletonJointPosition torsoPos, headPos, leftShoulderPos, rightShoulderPos;
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

    /// <summary>
    /// Defines the business logic of the individuel users
    /// </summary>
    public class User
    {
        /// <summary>
        /// The ID of the user
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The history of the user, i.e. its states in time
        /// </summary>
        private LinkedList<UserState> movementHistory;

        /// <summary>
        /// Indicates if the user is active, i.e. if it is tracked
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Constant indicating the minimal angle needed to detect leaning (in radian)
        /// </summary>
        private const double TRESHOLD_LEANING = 0.3;

        /// <summary>
        /// Constant indicating the minimal ration needed to detect a jump of the user.
        /// This jumping ratio is calculated as followed:
        ///     -a = vertical difference in torso position during the history
        ///     -b = distance between the shoulders (needed for normalization)
        ///     Jumping ration = a/b
        /// </summary>
        private const double TRESHOLD_JUMPING = 0.8;

        /// <summary>
        /// The timestamp (in ticks) of the last jump
        /// </summary>
        private long lastJumpTime = 0;

        /// <summary>
        /// Time-out used to fix the problem of users not jumping at exactly the same time
        /// </summary>
        private const int JUMP_TIMEOUT = 250;

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>
        /// Indicates how far the user leans over.
        /// </value>
        public double angle { get; private set; }

        /// <summary>
        /// The current movement of the user
        /// </summary>
        public UserMovement CurrentMovement
        {
            get
            {
                return CalculateCurrentMovement();
            }
            private set
            {
            }
        }

        /// <summary>
        /// Construcor: initializes the attributes
        /// </summary>
        /// <param name="id">The id of the new user</param>
        public User(int id)
        {
            this.ID = id;
            this.movementHistory = new LinkedList<UserState>();
            this.Active = true;
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
                    return DetectMovement();
                }
            }
            return UserMovement.None;
        }

        /// <summary>
        /// Detects and returns the user's movement
        /// </summary>
        /// <returns>The user's movement</returns>
        private UserMovement DetectMovement()
        {
            if (IsJumping())
            {
                return UserMovement.Jump;
            }

            UserState currState = movementHistory.Last.Value;
            Point3D vector = PointUtils.CreateDirectionVector(currState.torsoPos.Position, currState.headPos.Position);
            angle = Math.Atan(vector.X / vector.Y);
            return DetectLeaning(angle);
        }

        /// <summary>
        /// Detects and returns the leaning movement
        /// </summary>
        /// <param name="angle">The angle of the vector (head-torso)</param>
        /// <returns>The movement of the user</returns>
        private UserMovement DetectLeaning(double angle)
        {
            if (angle < -TRESHOLD_LEANING)
            {
                return UserMovement.Left;
            }
            else if (angle > TRESHOLD_LEANING)
            {
                return UserMovement.Right;
            }
            return UserMovement.None;
        }

        /// <summary>
        /// Returns true iff the user is jumping. Jumping is detected based on the last torso position
        /// and the minimal torso position in the movement history.
        /// </summary>
        /// <returns>True iff the user is jumping.</returns>
        private bool IsJumping()
        {
            UserState minTorsoPosition = movementHistory.Aggregate((l, r) => l.torsoPos.Position.Y < r.torsoPos.Position.Y ? l : r);
            UserState currState = movementHistory.Last.Value;
            double heightDifference = currState.torsoPos.Position.Y - minTorsoPosition.torsoPos.Position.Y;
            double shoulderDistance = PointUtils.PointDistance(currState.leftShoulderPos.Position, currState.rightShoulderPos.Position);
            double normalizedDifference = (shoulderDistance != 0) ? (heightDifference / shoulderDistance) : 0;

            if (normalizedDifference > TRESHOLD_JUMPING)
            {
                lastJumpTime = currState.timestamp;
                return true;
            }
            return (currState.timestamp - lastJumpTime) < JUMP_TIMEOUT * 10000;
        }
    }
}
