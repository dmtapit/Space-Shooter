using UnityEngine;
using System.Collections;

[System.Serializable] // Make it serializable and visible in the Inspector to Unity
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb; // Unity 5, no more shorthand helper references
	private AudioSource audioSource;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Update ()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = //used to create as a reference
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
			audioSource.Play();
		}
	}

	void Start ()
	{
		rb = GetComponent<Rigidbody> (); // Must use GetComponent, Unity 5
		audioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
