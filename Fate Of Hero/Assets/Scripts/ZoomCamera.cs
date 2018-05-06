using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
	private float zoomSpeed = 1;
    [SerializeField]
    private float targetOrtho;
    [SerializeField]
    private float smoothSpeed = 2.0f;
    [SerializeField]
    private float minOrtho = 1.0f;
    [SerializeField]
    private float maxOrtho = 20.0f;

	void Start() {
		targetOrtho = Camera.main.orthographicSize;
	}

	void Update () {

		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0.0f) {
			targetOrtho -= scroll * zoomSpeed;
			targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
		}

		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
	}
}