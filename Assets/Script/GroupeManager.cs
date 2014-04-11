using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class GroupeManager : MonoBehaviour {

	//Variables
	private float timerGestGroupe;			//Timer pour gerer les groupes à intervals réguliers
	private GameObject[] listGroupe;		//Liste tout les groupes de la scene
	public float varTimeUpdate;				//Temps entre chaque update des groupes
	public int nbGroupe;					//Nombre de groupe sur la scene

	// Use this for initialization
	void Start () {
		//Initialise la variable de temps
		timerGestGroupe = 0;

		//Initialise la liste des groupes
		listGroupe = GameObject.FindGameObjectsWithTag ("GroupeNode");
	}
	
	// Update is called once per frame
	void Update () {
		//On update le temps
		timerGestGroupe += Time.deltaTime;
		if(timerGestGroupe >= varTimeUpdate)
		{
			timerGestGroupe = 0;
			Debug.Log("Groupe Update !!!");
			updateGroupe();
		}
	}

	void updateGroupe()
	{
		//On ajoute un groupe aléatoirement
		listGroupe[Random.Range(0, listGroupe.Length)].GetComponent<Groupe>().CreateGroupe ();

		//On supprime un groupe aleatoirement
		listGroupe[Random.Range(0, listGroupe.Length)].GetComponent<Groupe>().DeleteGroupe ();
	}
}
