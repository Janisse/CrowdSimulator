using UnityEngine;
using System.Collections;

public class PlanTouche: MonoBehaviour {


	private bool isgrounded = false;

	//si il y a une collision entre le plan et humain
	void OnTriggerEnter(Collider people)
	{
		if(people.tag == "TagPeople")
		{
			Debug.Log("Sa fonctionne il y a collision entre plan et humain");
			isgrounded = true;

		}
	}
	
	void OnTriggerExit(Collider people)
	{
		Debug.Log("il ne touche plus le plan");
		isgrounded = false;
	}

	public bool getIsgrounded()
	{
		return isgrounded;
	}

}
