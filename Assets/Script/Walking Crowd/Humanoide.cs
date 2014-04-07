﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoide : MonoBehaviour
{
	//Variable
	private List<Transform> pathNodeTab;
	public int curFloor;
	private int destPathNode;
	private NavMeshAgent navMesh;
	private Animator animControl;
	private Vector3 lastPosition;
	private float speed;

	//Setter
	public void setCurFloor(int newCurFloor){curFloor = newCurFloor;}

	// Use this for initialization
	void Start ()
	{
		animControl = GetComponentInChildren<Animator>();
		//init variable
		pathNodeTab = new List<Transform>();
		lastPosition = Vector3.zero;
		speed = 0f;
		//init le navMesh
		navMesh = GetComponent<NavMeshAgent> ();
		//initialise le tableau des pathNodes ou il peut aller
		getPathNodeTab ();
		//Generation aléatoire du point de destination lors du spawn et le fait aller à ce point
		destPathNode = Random.Range (0, pathNodeTab.Count-1);
		walk ();
	}
	
	// Update is called once per frame
	void Update () {
		if(isArrive())
		{
			walk ();
		}

		//Change l'animation en fonction de la vitesse
		//animControl.SetFloat("Velocity", 1f);
	}

	void walk()
	{
		navMesh.destination = pathNodeTab [destPathNode].position;
	}

	bool isArrive()
	{
		//si l'humanoide est pres du point
		if(Mathf.Abs(pathNodeTab [destPathNode].position.x-transform.position.x)<1 && Mathf.Abs(pathNodeTab [destPathNode].position.z-transform.position.z)<1)
		{
			float angle = 0;
			int i = 0;
			int randomPathNode = 0;
			while(angle < 0.1 && i < pathNodeTab.Count)
			{
				//on cherche un point autre point random
				randomPathNode = Random.Range(1, pathNodeTab.Count);
				
				//on cherche le vecteur entre l'humanoide et le point random
				Vector3 A = pathNodeTab[randomPathNode].position - transform.position;
				A.Normalize();
				// le vecteur de l'humanoide
				Vector3 B = transform.forward;
				B.Normalize();
				//Produit scalaire des deux (a.x * b.x) + (a.y * b.y) + (a.z * b.z) = angle
				angle = Vector3.Dot(A,B);
				i++;
			}
			destPathNode = randomPathNode;
			return true;
		}
		return false;
	}

	//Recupere tout les pathNodes de l'etage en cours
	void getPathNodeTab()
	{
		GameObject[] pathNodeGO = GameObject.FindGameObjectsWithTag ("pathNodeFloor"+curFloor.ToString());
		for(int i=0; i<pathNodeGO.Length; i++)
		{
			pathNodeTab.Add(pathNodeGO[i].transform);
		}
	}
}
