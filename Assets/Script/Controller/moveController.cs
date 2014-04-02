using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class moveController : MonoBehaviour {

	//variables
	public float rotationSpeed;
	public float moveSpeed;
	public bool canJump;
	public float jumpSpeed;
	public float sensibilite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotationAxis = MiddleVR.VRDeviceMgr.GetJoystick ().GetAxisValue (3);
		//Regle le probleme d'hypersensibilite du controller
		if(rotationAxis>sensibilite || rotationAxis<-sensibilite)
			transform.Rotate(new Vector3 (0, rotationAxis, 0) * rotationSpeed);

		//Translation
		float translationX = MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(0);
		float translationZ = -MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(1);
		//Regle le probleme d'hypersensibilite du controller
		if((translationX>sensibilite || translationX<-sensibilite) || (translationZ>sensibilite || translationZ<-sensibilite))
			transform.Translate(new Vector3 (translationX, 0, translationZ) * moveSpeed);


		//Jump
		if(MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_SPACE) == true)
		{
			rigidbody.AddForce(Vector3.up * jumpSpeed);
		}
	}
}
