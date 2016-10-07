using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
			
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.name);
		//if (other.tag == "Boundary" || other.tag == "Enemy") 
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		//if (other.tag == "Player") 
		if (other.CompareTag ("Player"))
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject); // Marks object to be destroyed until end of frame
		Destroy (gameObject);
	}
}
