using UnityEngine;

public class EndTrigger : MonoBehaviour {

	public LevelManager levelManagerScript;
	private int timesCrossed = 0;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if(timesCrossed < 3){
				//cameraControllerScript.offsetSmoothing = 0f;
				levelManagerScript.respawnDelay = 0.5f;
				levelManagerScript.Respawn ();
				timesCrossed++;
			}else if (timesCrossed == 3) {
				//cameraControllerScript.offsetSmoothing = 1f;
				levelManagerScript.respawnDelay = 3f;
				levelManagerScript.CompleteGame();
			}
		}
	}

}
