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
	public bool isControllerPlugIn;
	public bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(pause == false)
		{
			if(isControllerPlugIn == true)
			{
				//Rotation
				float rotationAxis = MiddleVR.VRDeviceMgr.GetJoystick ().GetAxisValue (3);
				//Regle le probleme d'hypersensibilite du controller
				if(rotationAxis>sensibilite || rotationAxis<-sensibilite)
					transform.Rotate(new Vector3 (0, rotationAxis, 0) * rotationSpeed/50);

				//Translation
				float translationX = MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(0);
				float translationZ = -MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(1);
				//Regle le probleme d'hypersensibilite du controller
				if((translationX>sensibilite || translationX<-sensibilite) || (translationZ>sensibilite || translationZ<-sensibilite))
					transform.rigidbody.AddRelativeForce(new Vector3 (translationX, 0, translationZ) * moveSpeed);
				else
					transform.rigidbody.velocity = new Vector3 (transform.rigidbody.velocity.x * 0.5f, transform.rigidbody.velocity.y * 1.1f, transform.rigidbody.velocity.z * 0.5f);


				//Jump
				if(MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_SPACE) == true)
				{
					rigidbody.AddForce(Vector3.up * jumpSpeed);
				}
			}
			else
			{
				//Rotation
				float rotationAxis = Input.GetAxis("Mouse X");
				Debug.Log(rotationAxis.ToString());
				//Regle le probleme d'hypersensibilite du controller
				if(rotationAxis>sensibilite || rotationAxis<-sensibilite)
					transform.Rotate(new Vector3 (0, rotationAxis, 0) * rotationSpeed/50);
				
				//Translation
				float translationX = Input.GetAxis("Horizontal");
				float translationZ = Input.GetAxis("Vertical");
				//Regle le probleme d'hypersensibilite du controller
				if((translationX>sensibilite || translationX<-sensibilite) || (translationZ>sensibilite || translationZ<-sensibilite))
					transform.rigidbody.AddRelativeForce(new Vector3 (translationX, 0, translationZ) * moveSpeed);
				else
					transform.rigidbody.velocity = new Vector3 (transform.rigidbody.velocity.x * 0.5f, transform.rigidbody.velocity.y * 1.1f, transform.rigidbody.velocity.z * 0.5f);
				
				
				//Jump
				if(MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_SPACE) == true)
				{
					rigidbody.AddForce(Vector3.up * jumpSpeed);
				}
			}
		}
	}

	public void setPause(bool p)
	{
		pause = p;
	}
}
