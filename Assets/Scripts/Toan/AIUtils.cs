using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIUtils 
    {
        public static readonly FlockBehavior flockBehaviorInstance = new FlockBehavior();
        public static readonly SteerBehavior steerBehaviorInstance = new SteerBehavior();

        public static Vector3 ToLocalPoint(Transform local, Vector3 point)
        {
            return local.InverseTransformDirection(point);
        }
    }
}
