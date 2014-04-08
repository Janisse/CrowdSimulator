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
	public bool isOnEscalator;
	private GameObject destinationEscalator;
	private float escalatorSpeed;
	private NavMeshAgent navMesh;

	// Use this for initialization
	void Start () {
		pause = false;
		isOnEscalator = false;
		//init le navMesh
		navMesh = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(pause == false)
		{
			if(isOnEscalator == false)
			{
				rotation();
				deplacement();
			}
			else
			{
				rotation();
				escalator();
			}
		}
	}

	void rotation()
	{
		if(isControllerPlugIn == true)
		{
			//Rotation
			float rotationAxis = MiddleVR.VRDeviceMgr.GetJoystick ().GetAxisValue (3);
			//Regle le probleme d'hypersensibilite du controller
			if(rotationAxis>sensibilite || rotationAxis<-sensibilite)
				transform.Rotate(new Vector3 (0, rotationAxis, 0) * rotationSpeed/50);
		}
		else
		{
			//Rotation
			float rotationAxis = Input.GetAxis("Mouse X");
			Debug.Log(rotationAxis.ToString());
			//Regle le probleme d'hypersensibilite du controller
			if(rotationAxis>sensibilite || rotationAxis<-sensibilite)
				transform.Rotate(new Vector3 (0, rotationAxis, 0) * rotationSpeed/50);
		}
	}

	void deplacement()
	{
		if(isControllerPlugIn == true)
		{
			//Translation
			float translationX = MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(0);
			float translationZ = -MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(1);
			//Regle le probleme d'hypersensibilite du controller
			if((translationX>sensibilite || translationX<-sensibilite) || (translationZ>sensibilite || translationZ<-sensibilite))
				transform.rigidbody.AddRelativeForce(new Vector3 (translationX, 0, translationZ) * moveSpeed);
			else
				transform.rigidbody.velocity = new Vector3 (transform.rigidbody.velocity.x * 0.5f, transform.rigidbody.velocity.y, transform.rigidbody.velocity.z * 0.5f);
		}
		else
		{
			//Translation
			float translationX = Input.GetAxis("Horizontal");
			float translationZ = Input.GetAxis("Vertical");
			//Regle le probleme d'hypersensibilite du controller
			if((translationX>sensibilite || translationX<-sensibilite) || (translationZ>sensibilite || translationZ<-sensibilite))
				transform.rigidbody.AddRelativeForce(new Vector3 (translationX, 0, translationZ) * moveSpeed*2);
			else
				transform.rigidbody.velocity = new Vector3 (transform.rigidbody.velocity.x * 0.5f, transform.rigidbody.velocity.y, transform.rigidbody.velocity.z * 0.5f);
		}
	}

	void escalator()
	{
		Vector3 posPathNodeOut = destinationEscalator.transform.position;
		if(Mathf.Abs(posPathNodeOut.x - transform.position.x) < 0.1f && Mathf.Abs(posPathNodeOut.z - transform.position.z) < 0.1f)
		{
			isOnEscalator = false;
			navMesh.enabled = true;
		}
		else	//On continu sur l'escalator
		{
			transform.Translate(Vector3.Normalize((posPathNodeOut-transform.position))*Time.deltaTime*(escalatorSpeed), Space.World);
		}
	}

	public void startEscalator(GameObject dest, float speed)
	{
		destinationEscalator = dest;
		isOnEscalator = true;
		escalatorSpeed = speed;
		navMesh.enabled = false;
	}

	public void setPause(bool p)
	{
		pause = p;
	}
}
