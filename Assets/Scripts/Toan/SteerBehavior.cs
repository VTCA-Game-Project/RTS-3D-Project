using UnityEngine;
using Agent;
using EnumCollection;

namespace AI
{
    public class SteerBehavior
    {
        private const float DecelerationTweaker = 0.3f;

        public Deceleration deceleration;
        public float SafeDist { get; set; }
        public SteerBehavior()
        {
            deceleration = Deceleration.Normal;
        }
        public Vector3 Seek(AIAgent agent, Vector3 target)
        {
            Vector3 desireDir = target - agent.Position;
            float maxSpeed = agent.MaxSpeed;
            if (desireDir.sqrMagnitude > 0.1f)
            {
                // Vector3 result = desireDir.normalized * maxSpeed - agent.Velocity;
                Vector3 result = desireDir - agent.Velocity;
                return result;
            }
            return Vector3.zero;
        }

        public Vector3 Flee(AIAgent agent, Vector3 target)
        {
            float distToTarget = Vector3.SqrMagnitude(target - agent.Position);
            float panicDist = Mathf.Pow(agent.BoundRadius + SafeDist,2);

            if (distToTarget < panicDist)
            {
                Vector3 desireVel = - Seek(agent, target);
                // scale the force inversely proportional to the agent's target
                desireVel /= Mathf.Sqrt(distToTarget);
                return desireVel;
            }
            return Vector3.zero;
        }

        public Vector3 Arrive(AIAgent agent, Vector3 target)
        {
            Vector3 toTarget = target - agent.Position;
            float dist = toTarget.magnitude;
            if (dist > 0)
            {
                float speed = dist / ((float)deceleration * DecelerationTweaker);
                speed = Mathf.Min(speed, agent.MaxSpeed);
                toTarget = toTarget * speed / dist;

                Vector3 desireVel = toTarget - agent.Velocity;
                return desireVel;
            }
            return Vector3.zero;
        }
    }
}
