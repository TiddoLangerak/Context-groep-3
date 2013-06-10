using System;
using OpenNI;

namespace Util
{
    /// <summary>
    /// Contains helper functions for the OpenNI Point3D struct
    /// </summary>
    class PointUtils
    {
        /// <summary>
        /// Calculates the length of the vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The length of the vector</returns>
        public static double VectorLength(Point3D vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        /// <summary>
        /// Substract point1 from point2 and creates a new point.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>Point2 - point1</returns>
        public static Point3D CreateDirectionVector(Point3D point1, Point3D point2)
        {
            Point3D res = new Point3D();
            res.X = point2.X - point1.X;
            res.Y = point2.Y - point1.Y;
            res.Z = point2.Z - point1.Z;
            return res;
        }

        /// <summary>
        /// Returns the distance between point1 and point2
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>The distance between point1 and point2</returns>
        public static double PointDistance(Point3D point1, Point3D point2)
        {
            return VectorLength(CreateDirectionVector(point1, point2));
        }
    }
}
