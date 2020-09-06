using UnityEngine;

public class Power : MonoBehaviour
{
	Rigidbody2D rb;
	
	public float rotateSpeed = 100f;

	int direction;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		direction = Random.Range (-1,2);
	}

	void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, direction * Time.fixedDeltaTime * -rotateSpeed);
		transform.position = Vector3.MoveTowards (transform.position, Vector3.zero, 0.01f * (rotateSpeed / rotateSpeed));
		if (Vector3.Distance (transform.position, Vector3.zero) < .005f)
			Destroy (gameObject);
    }
}