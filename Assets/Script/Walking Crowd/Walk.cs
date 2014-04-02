using UnityEngine;
using System.Collections;

public class Walk : Humanoide
{

	// Use this for initialization
	void Start ()
	{
	
	}

	//function calcul un arrivePath random de la carte
	void randomPath()
	{
		arrivePath.x = _minGround.x + Random.value * _dimGround.x;
		arrivePath.y = 1;
		arrivePath.z = _minGround.z + Random.value * _dimGround.z;
		Debug.Log(arrivePath.ToString());
	}
	
	//function people walk randomly in the floor
	void walk()
	{
		//se deplacer aller de startPath(point de départ) à arrivePath(point d'arriver)
		
		// initialisation du tableau d'Humanoide
		People = GameObject.FindGameObjectsWithTag("TagPeople") ;
		
		
		for (int i = 0; i < People.Length; i++)
		{   
			//People[i].transform.position = arrivePath;
			
			NavMeshAgent m_objAgent = People[i].GetComponent<NavMeshAgent>();
			
			
			if((arrivePath.x - m_objAgent.transform.position.x < 1 && arrivePath.x - m_objAgent.transform.position.x > -1) && (arrivePath.z - m_objAgent.transform.position.z < 1 && arrivePath.z - m_objAgent.transform.position.z > -1))
			{
				randomPath();
				m_objAgent.SetDestination(arrivePath);
				
			}
			else
			{
				m_objAgent.SetDestination(arrivePath);
				
			}
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
