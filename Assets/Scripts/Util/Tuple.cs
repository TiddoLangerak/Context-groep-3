﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    class Tuple<T1, T2>
    {
        public T1 First {get; set;}
        public T2 Second {get; set;}
        public Tuple (T1 first, T2 second) {
            First = first;
            Second = second;
        }
    }
}