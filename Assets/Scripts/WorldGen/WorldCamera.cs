﻿using UnityEngine;
using UnityEngine.UI;

public class WorldCamera : MonoBehaviour {
	private float maxHeight;
	[SerializeField] private float minHeight = 25;
	[SerializeField] private float zoomSensitivity = 10;
	private float height;

	private const int mouseButtonPan = 2;

	[SerializeField] private float panSensitivity = 0.1f;

	private Vector3 initialMousePosition;
	private Vector3 initialPosition;

	private Vector3 targetPos;

	[SerializeField] private Toggle perspectiveToggle;
	private new Camera camera;

	private void Awake() {
		height = maxHeight = transform.position.y;
		targetPos = transform.position;
		camera = GetComponent<Camera>();
	}

	private void Update() {
		float mouseWheel = -Input.GetAxis("Mouse ScrollWheel");

		height = Mathf.Clamp(height + mouseWheel * zoomSensitivity, minHeight, maxHeight);
		camera.orthographic = !perspectiveToggle.isOn;
		if (camera.orthographic) camera.orthographicSize = height;

		if (Input.GetMouseButtonDown(mouseButtonPan)) {
			initialMousePosition = Input.mousePosition;
			initialPosition = transform.position;
		}

		if (Input.GetMouseButton(mouseButtonPan)) {
			Vector3 mousePosDiff = initialMousePosition - Input.mousePosition;
			Vector3 cameraPosDiff = new Vector3(mousePosDiff.x, 0, mousePosDiff.y) * panSensitivity * height / 100;
			targetPos = initialPosition + cameraPosDiff;
		}

		targetPos.y = height;

		transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
	}

}