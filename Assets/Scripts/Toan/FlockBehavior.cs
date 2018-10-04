using Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class FlockBehavior
    {
        public Vector3 Separation(AIAgent agent, AIAgent[] neighbours)
        {
            Vector3 steerForce = Vector3.zero;
            AIAgent neighbour;
            Vector3 toAgent = Vector3.zero;
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbour = neighbours[i];
                if(neighbour != null)
                {
                    toAgent =  agent.Position - neighbour.Position;
                    steerForce += toAgent.normalized / toAgent.magnitude;
                }
            }
            return steerForce;

        }

        public Vector3 Alignment(AIAgent agent,AIAgent[] neighbours)
        {
            Vector3 averageHeading = Vector3.zero;
            AIAgent neighbour;
            Vector3 toAgent = Vector3.zero;
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbour = neighbours[i];
                if (neighbour != null)
                {
                    averageHeading += neighbour.Heading;
                }
            }
            if(neighbours.Length > 0)
            {
                averageHeading /= neighbours.Length;
                averageHeading -= agent.Velocity;
            }
            return averageHeading;
        }

        public Vector3 Cohesion(AIAgent agent, AIAgent[] neighbours)
        {
            Vector3 centerOfMass = Vector3.zero;
            int count = 0;
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null)
                {
                    centerOfMass += neighbours[i].Position;
                    count++;
                }
            }
            if (count <= 0) return Vector3.zero;
            centerOfMass /= count;
            return AIUtils.steerBehaviorInstance.Seek(agent, centerOfMass);
        }

    }
}
