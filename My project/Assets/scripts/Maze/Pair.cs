using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Pair<T1, T2>
    {
        public T1 x { get; set; }
        public T2 y { get; set; }
        
        public Pair(T1 _x, T2 _y)
        {
            x = _x;
            y = _y;
        }
    }
}
