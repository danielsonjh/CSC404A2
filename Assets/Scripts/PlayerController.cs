using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotationSpeed;

	private Rigidbody rb;

	void Start (){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		Vector3 dir = Vector3.zero;
		dir.x = Input.GetAxis ("Horizontal");
		dir.z = Input.GetAxis ("Vertical");

		/*
		if (dir.x > 0 && dir.z > 0) {
			transform.rotation = new Vector3 (0, 45, 0);
		}*/
		float angle = Mathf.Rad2Deg*Mathf.Atan(dir.z/dir.x);
		//Quaternion target = Quaternion.Euler (0, angle, 0);
		//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
		//transform.eulerAngles = new Vector3(0.0f, angle, 0.0f);
		rb.AddForce (dir * speed);

		//transform.Translate (movement * speed);
	}

}
