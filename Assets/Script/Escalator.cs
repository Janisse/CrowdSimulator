using UnityEngine;
using System.Collections;

public class Escalator : MonoBehaviour {

	public GameObject pathNodeOut;
	public float speed;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(pathNodeOut != null)//On verifit si il s'agit d'une entree ou une sortie
		{
			if(Mathf.Abs(player.transform.position.x-transform.position.x) < 0.5f && Mathf.Abs(player.transform.position.z-transform.position.z) < 0.5f)
				player.GetComponent<moveController>().startEscalator(pathNodeOut, speed);
		}
	}
}
