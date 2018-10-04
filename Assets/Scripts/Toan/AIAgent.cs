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
        private Vector3 velocity;

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
        public Vector3 Velocity
        {
            // using projection
            get
            {
                velocity = Vector3.ProjectOnPlane(rigid.velocity, Vector3.up);
                return velocity;
            }
        }
        public float MaxSpeed
        {
            get { return maxSpeed; }
            protected set { maxSpeed = value; }
        }
        public Vector3 Position
        {
            //get
            //{
            //    position = transform.position;
            //    position.y = 0;
            //    return position;
            //}

            // using projection
            get
            {
                position = Vector3.ProjectOnPlane(transform.position, Vector3.up);
                return position;
            }
        }
        public Vector3 Heading
        {
            //get
            //{
            //    heading = transform.forward;
            //    heading.y = 0;
            //    heading = heading.normalized;
            //    return heading;
            //}

            // using projection
            get
            {
                heading = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
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
            steering += steerBh.Seek(this, target.Position);
            // steering += steerBh.Arrive(this, target.Position);
            steering += flockBh.Separation(this, neighbours) * separation;
            steering += flockBh.Alignment(this, neighbours) * alignment;
            steering += flockBh.Cohesion(this, neighbours) * cohesion;


            rigid.velocity += steering / rigid.mass;
            transform.forward += rigid.velocity;
#if UNITY_EDITOR
            Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }

        private Vector3 Truncate()
        {
            return rigid.velocity.magnitude > maxSpeed ? rigid.velocity.normalized * maxSpeed : rigid.velocity;
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
