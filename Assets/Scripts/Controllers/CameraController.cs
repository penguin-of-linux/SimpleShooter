using Components;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Camera cam;
        private float targetZoom;
        private float zoomFactor = 3f;
        [SerializeField] private float zoomLerpSpeed = 10;
 
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            // ReSharper disable once PossibleNullReferenceException
            targetZoom = cam.orthographicSize;

            target = FindObjectOfType<CameraTargetComponent>().gameObject;
        }
 
        // Update is called once per frame
        void Update()
        {
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            targetZoom -= scrollData * zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

            if (target != null)
            {
                var position = target.transform.position;
                Camera.main.SetPosition(position.x, position.y);
            }
            else
            {
                target = FindObjectOfType<CameraTargetComponent>()?.gameObject;
            }
        }

        private GameObject target;
    }
}
