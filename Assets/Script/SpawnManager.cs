using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	GameObject[] spawnTab;
	public int nbPeople;

	// Use this for initialization
	void Start () {
		//Creer la list de tous les spawns
		spawnTab = GameObject.FindGameObjectsWithTag ("Spawn");
		startSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void startSpawn()
	{
		//Spawn aleatoirement des humanoides
		for(int i=0; i<nbPeople; i++)
		{
			spawnTab[Random.Range(0, spawnTab.Length-1)].GetComponent<Spawn>().spawnPeople(1);
		}
	}
}
