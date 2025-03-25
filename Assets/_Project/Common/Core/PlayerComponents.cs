using UnityEngine.AI;

namespace Project.Common.Core
{
    public class PlayerComponents
    {
        public readonly NavMeshAgent Agent;

        public PlayerComponents(NavMeshAgent agent) 
        { 
            Agent = agent;
        }
    }
}


