using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public float respawnDelay;
	public PlayerMovement gamePlayer;
	public GameObject leafnode;
	private GameObject lastJointedLeaf;
	private GameObject newJointedLeaf;
	private Vector3 LeafHangingPosition;
	private float LeafPositionX = -6.169105f;
	private float LeafPositionY;
	private float LeafPositionZ = -0.06515082f;
	private float distanceX = 10.80f;
	private float distanceY = 2f;

	public AudioSource backgroundSoundTrack;
	public AudioSource losingSoundPart1;
	public AudioSource losingSoundPart2;
	public AudioSource losingSoundPart3;

	void Awake(){
		createLeafRope (0);
		for (int i = 0; i < 5; i++) {
			createLeafRope (distanceX);
		}
	}
	void Start () {
		backgroundSoundTrack.Play ();
		gamePlayer = FindObjectOfType<PlayerMovement> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Respawn() {
		StartCoroutine (RespawnCoroutine ());
	}

	public IEnumerator RespawnCoroutine() {
		gamePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (respawnDelay);
		gamePlayer.transform.position = gamePlayer.respawnPoint;
		gamePlayer.gameObject.SetActive (true);
	}

	void createLeafRope(float distanceFromThePrev){
		LeafPositionX += distanceFromThePrev;
		LeafPositionY = 4.08f;

		LeafHangingPosition = new Vector3 (LeafPositionX, LeafPositionY, LeafPositionZ);
		lastJointedLeaf = Instantiate (leafnode, LeafHangingPosition, Quaternion.identity);

		for(int i=0;i<4;i++){
			LeafPositionY -= distanceY;
			LeafHangingPosition = new Vector3 (LeafPositionX, LeafPositionY, LeafPositionZ);
		
			newJointedLeaf = Instantiate (leafnode, LeafHangingPosition, Quaternion.identity);
			newJointedLeaf.GetComponent<HingeJoint2D> ().connectedBody = lastJointedLeaf.GetComponent<Rigidbody2D> ();

			lastJointedLeaf = newJointedLeaf;
		}
	}

	public void CompleteGame() {
		backgroundSoundTrack.Stop ();
		SceneManager.LoadScene (3);
	}
	public void GameOver() {
		backgroundSoundTrack.Stop ();
		StartCoroutine (GameOverCoroutine ());
	}
	private IEnumerator GameOverCoroutine(){
		gamePlayer.gameObject.SetActive (false);
		losingSoundPart1.Play ();
		losingSoundPart2.PlayDelayed (1.145f);
		losingSoundPart3.PlayDelayed (1.57f);
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene (2);
	}
}
