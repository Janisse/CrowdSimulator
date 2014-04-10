using UnityEngine;
using System.Collections;

public class Escalator : MonoBehaviour {

	public GameObject pathNodeOut;			//pathNode de sorti de l'escalator
	public float speed;						//Vitesse de l'escalator
	private GameObject player;				//Instance du player

	// Use this for initialization
	void Start () {
		//Recupere le Player
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//On verifit si il s'agit d'une entree ou une sortie
		if(pathNodeOut != null)
		{
			//Si le Player est proche de l'entrée, on lui fait prendre l'escalator
			if(Mathf.Abs(player.transform.position.x-transform.position.x) < 0.5f && Mathf.Abs(player.transform.position.z-transform.position.z) < 0.5f)
				player.GetComponent<moveController>().startEscalator(pathNodeOut, speed);
		}
	}
}
