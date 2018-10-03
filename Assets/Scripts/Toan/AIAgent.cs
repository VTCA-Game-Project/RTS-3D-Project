using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using Manager;

namespace Agent
{
    public class AIAgent : MonoBehaviour
    {
        private SteerBehavior steerBh;
        private FlockBehavior flockBh;

        private Vector3 position;
        private Vector3 heading;
        private Vector3 steering;

        private bool isSelected = false;
        public Pointer target;
        public Rigidbody rigid;
        public float BoundRadius;
        public float NeighbourRadius;

        public float separation;
        public float cohesion;
        public float alignment;
        public float maxSpeed;
        public int index;
        public AIAgent[] neighbours;
        [Header("Debug")]
        public bool drawGizmos = true;

        #region Properties
        public float MaxSpeed
        {
            get { return maxSpeed; }
            protected set { maxSpeed = value; }
        }
        public Vector3 Position
        {
            get
            {
                position = transform.position;
                position.y = 0;
                return position;
            }
        }
        public Vector3 Heading
        {
            get
            {
                heading = transform.forward;
                heading.y = 0;
                heading = heading.normalized;
                return heading;
            }
        }
        public bool IsSelected
        {
            get { return isSelected; }
        }
        #endregion

        private void Awake()
        {
            StoredManager.AddAgent(this);
        }
        private void Start()
        {
            steerBh = AIUtils.steerBehaviorInstance;
            flockBh = AIUtils.flockBehaviorInstance;
        }
        private void FixedUpdate()
        {
            steering = Vector3.zero;
            neighbours = StoredManager.GetNeighbours(this);
            //steering += steerBh.Seek(this, target.Position);
            steering += steerBh.Arrive(this, target.Position);
            steering += flockBh.Separation(this, neighbours) * separation;
            steering += flockBh.Alignment(this, neighbours) * alignment;
            steering += flockBh.Cohesion(this, neighbours) * cohesion;

            rigid.velocity += steering / rigid.mass;
            transform.forward +=  steering / rigid.mass;;
#if UNITY_EDITOR
            Debug.Log("steer: " + steering + " velocity: " + rigid.velocity );
#endif
        }

        public void Select() { isSelected = true; }
        public void UnSelect() { isSelected = false; }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, NeighbourRadius);
            }
        }
#endif
    }
}
