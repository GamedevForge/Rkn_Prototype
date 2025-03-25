using UnityEngine;
using UnityEngine.AI;

namespace Project.Common.Core
{
    public class PlayerComponents
    {
        public readonly NavMeshAgent Agent;
        public readonly Transform CameraTransform;
        public readonly Transform PlayerTransform;

        public PlayerComponents(NavMeshAgent agent,
            Transform transform,
            Transform playerTransform) 
        { 
            Agent = agent;
            CameraTransform = transform;
            PlayerTransform = playerTransform;
        }
    }
}


