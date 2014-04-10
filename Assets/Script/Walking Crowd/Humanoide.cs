using UnityEngine;
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
	private bool isOnEscalator;
	public static int ID;
	private SharedData state;

	//Setter
	public void setCurFloor(int newCurFloor){curFloor = newCurFloor;}

	// Use this for initialization
	void Start ()
	{
		//Defini l'ID de l'humanoide
		if(ID == 0)
			ID = 1;
		else
			ID++;

		//Creation d'une variable d'etat synchronisé
		state = gameObject.AddComponent<SharedData>();
		state.createData ("Humanoide" + ID.ToString ());
		animControl = GetComponentInChildren<Animator>();
		//init variable
		pathNodeTab = new List<Transform>();
		isOnEscalator = false;
		//init le navMesh
		navMesh = GetComponent<NavMeshAgent> ();
		//initialise le tableau des pathNodes ou il peut aller
		getPathNodeTab ();
		//Generation aléatoire du point de destination lors du spawn et le fait aller à ce point
		destPathNode = Random.Range (0, pathNodeTab.Count-1);
		walk ();
		setState (Random.Range(1,3));
	}
	
	// Update is called once per frame
	void Update () {
		//Si on est hors d'un escalator
		if(isOnEscalator == false)
		{
			if(isArrive())
			{
				if(pathNodeTab[destPathNode].name != "PathNodeLink") //Si il ne s'agit pas d'une entrée d'escalator
				{
					newDestination();
					walk ();
				}
				else //On entre dans un escalator
				{
					isOnEscalator = true;
					navMesh.enabled = false;
					setState (0);
				}
			}
		}
		else	//Si on est dans un escalator
		{
			Vector3 posPathNodeOut = pathNodeTab [destPathNode].GetComponent<Escalator>().pathNodeOut.transform.position;
			if(Mathf.Abs(posPathNodeOut.x - transform.position.x) < 0.1f && Mathf.Abs(posPathNodeOut.z - transform.position.z) < 0.1f)
			{
				//On actualise l'etage
				if(posPathNodeOut.y>transform.position.y)
					curFloor++;
				else
					curFloor--;
				getPathNodeTab();
				navMesh.enabled = true;
				isOnEscalator = false;
				newDestination();
				walk ();
				setState (Random.Range(1,3));
			}
			else	//On continu sur l'escalator
			{
				transform.Translate(Vector3.Normalize((posPathNodeOut-transform.position))*Time.deltaTime*(pathNodeTab [destPathNode].GetComponent<Escalator>().speed), Space.World);
			}
		}
		updateState ();
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
			return true;
		}
		return false;
	}

	void newDestination()
	{
		float angle = 0;
		int i = 0;
		int randomPathNode = 0;
		while(angle < 0.1 && i < pathNodeTab.Count)
		{
			//on cherche un point autre point random
			randomPathNode = Random.Range(0, pathNodeTab.Count-1);
			
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
	}

	//Recupere tout les pathNodes de l'etage en cours
	void getPathNodeTab()
	{
		pathNodeTab.Clear ();
		GameObject[] pathNodeGO = GameObject.FindGameObjectsWithTag ("pathNodeFloor"+curFloor.ToString());
		for(int i=0; i<pathNodeGO.Length; i++)
		{
			pathNodeTab.Add(pathNodeGO[i].transform);
		}
	}

	void setState(int newState)
	{
		if(MiddleVR.VRClusterMgr.IsServer())
			state.setData ((float)newState);
	}

	void updateState()
	{
		//Change l'animation en fonction de la vitesse
		animControl.SetInteger("state", (int)state.getData());
		navMesh.speed = state.getData()*2;
	}
}
