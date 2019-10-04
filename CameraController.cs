using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float offset;
	private Vector3 playerPosition;
	public float offsetSmoothing;

	public BoxCollider2D boundBox;
	private Vector3 minBounds;
	private Vector3 maxBounds;

	private Camera theCamera;
	private float halfWidth;

	void Start () {
		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;

		theCamera = GetComponent<Camera> ();
		halfWidth = theCamera.orthographicSize * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {

		playerPosition = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		if (player.GetComponent<SpriteRenderer> ().flipX) {
			playerPosition = new Vector3 (playerPosition.x - offset, playerPosition.y, playerPosition.z);
		} else {
			playerPosition = new Vector3 (playerPosition.x + offset, playerPosition.y, playerPosition.z);
		}
		transform.position = Vector3.Lerp (transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

		float clampedX = Mathf.Clamp (transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
		transform.position = new Vector3 (clampedX, transform.position.y, transform.position.z);
	}
}
