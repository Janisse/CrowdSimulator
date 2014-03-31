using UnityEngine;
using System.Collections;

public class moveController : MonoBehaviour {

	//variables
	public float moveSpeed;
	public bool canJump;
	public float jumpSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3 (0, Input.GetAxis("Mouse X"), 0));
		transform.Translate(new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

		//Jump
		if(Input.GetButtonDown("Jump") == true)
		{
			rigidbody.AddForce(Vector3.up * jumpSpeed);
		}
	}
}
