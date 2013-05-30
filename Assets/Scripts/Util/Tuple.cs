using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    /// <summary>
    /// Defines a generic tuple for two objects (= a pair)
    /// </summary>
    /// <typeparam name="T1">The first object of the tuple</typeparam>
    /// <typeparam name="T2">The second object of the tuple</typeparam>
    class Tuple<T1, T2>
    {
        /// <summary>
        /// The first object of the tuple
        /// </summary>
        public T1 First { get; set; }

        /// <summary>
        /// The second object of the tuple
        /// </summary>
        public T2 Second { get; set; }

        /// <summary>
        /// Constructor: initializes the First and Second property
        /// </summary>
        /// <param name="first">The object that should be used as the first object of the tuple</param>
        /// <param name="second">The object that should be used as the second object of the tuple</param>
        public Tuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }
}
