using UnityEngine;

namespace TankBattle
{
    public class CameraControl : MonoBehaviour
    {
        public float DampTime = 0.2f;
        public float ScreenEdgeBuffer = 8f;
        public float MinSize = 6.5f;
        [HideInInspector] public Transform[] Targets;


        private Camera sceneCamera;
        private float zoomSpeed;
        private Vector3 moveVelocity;
        private Vector3 desiredPosition;


        private void Awake()
        {
            sceneCamera = GetComponentInChildren<Camera>();
        }


        private void FixedUpdate()
        {
            Move();
            Zoom();
        }


        private void Move()
        {
            FindAveragePosition();

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, DampTime);
        }


        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            for (int i = 0; i < Targets.Length; i++)
            {
                if (!Targets[i].gameObject.activeSelf)
                    continue;

                averagePos += Targets[i].position;
                numTargets++;
            }

            if (numTargets > 0)
                averagePos /= numTargets;

            averagePos.y = transform.position.y;

            desiredPosition = averagePos;
        }


        private void Zoom()
        {
            float requiredSize = FindRequiredSize();
            sceneCamera.orthographicSize = Mathf.SmoothDamp(sceneCamera.orthographicSize, requiredSize, ref zoomSpeed, DampTime);
        }


        private float FindRequiredSize()
        {
            Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);

            float size = 0f;

            for (int i = 0; i < Targets.Length; i++)
            {
                if (!Targets[i].gameObject.activeSelf)
                    continue;

                Vector3 targetLocalPos = transform.InverseTransformPoint(Targets[i].position);

                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / sceneCamera.aspect);
            }

            size += ScreenEdgeBuffer;

            size = Mathf.Max(size, MinSize);

            return size;
        }


        public void SetStartPositionAndSize()
        {
            FindAveragePosition();

            transform.position = desiredPosition;

            sceneCamera.orthographicSize = FindRequiredSize();
        }
    }
}