using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agent;

namespace Manager
{
    public class StoredManager
    {
        public static List<AIAgent> agents = new List<AIAgent>();
        public static List<Construct> constructs = new List<Construct>();

        // agent
        public static void AddAgent(AIAgent agent)
        {
            agents.Add(agent);
        }
        public static void RemoveAgent(AIAgent agent)
        {
            agents.RemoveAt(agents.IndexOf(agent));
        }
        // AI uitls
        public static AIAgent[] GetNeighbours(AIAgent agent)
        {
            List<AIAgent> result = new List<AIAgent>();
            float sqrBoundRadius = agent.NeighbourRadius * agent.NeighbourRadius;
            for(int i = 0; i < agents.Count; i++)
            {
                if(agents[i] != agent && Vector3.SqrMagnitude(agent.Position - agents[i].Position) <= sqrBoundRadius)
                {
                    result.Add(agents[i]);
                }
            }
            return result.ToArray();
        }
        // constructs
        public static void AddConstruct(Construct construct)
        {
            constructs.Add(construct);
            GlobalGameStatus.NewConstructBuilded(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " added");
#endif
        }
        public static void RemoveConstruct(Construct construct)
        {
            constructs.RemoveAt(constructs.IndexOf(construct));
            GlobalGameStatus.ConstructDestroyed(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " destroyed");
#endif
        }
    }
}
