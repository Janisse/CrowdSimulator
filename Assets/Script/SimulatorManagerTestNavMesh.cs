using UnityEngine;
using System.Collections;

public class SimulatorManagerTestNavMesh : MonoBehaviour {

	//Spawn
	public int nbPeople;
	public GameObject prefab_people;
	Vector3 _posNewPeopleFirstFloor;

	//Walk
	public GameObject[] People;
	Transform _transform;
	public Transform[] pathNodeFirstFloor;	
	protected int _speed = 1;
	int curPathNode;
	
	void Awake()
	{
		pathNodeFirstFloor[0] = transform;
	}

	// Use this for initialization
	void Start ()
	{
		_transform = transform;
		AI_Spawn();
	}

	void AI_Spawn()
	{
		
		//création des gens aléatoirement sur la map
		for(int i=0 ; i<nbPeople ; ++i)
		{
			// il faut faire des zones de spawn comme devant les magasins par exemple
			//pour l'instant spawn aléatoirement sur la map
			
			_posNewPeopleFirstFloor.x = Random.value * 10;
			_posNewPeopleFirstFloor.y = 1;
			_posNewPeopleFirstFloor.z = Random.value * 13;
			//instantiation de l'objet
			Object newPeopleFirstFloor;
			newPeopleFirstFloor = Instantiate(prefab_people, _posNewPeopleFirstFloor, prefab_people.transform.rotation);
		}
	}

	void AI_Walk()
	{

		People = GameObject.FindGameObjectsWithTag("TagPeople");


		foreach(GameObject GO in People)
		{   
			// position d'un point sur la map
			Vector3 _posNewPathNode = pathNodeFirstFloor[(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode].position;
			// le gameObject (humanoide) se dirige vers un nouveau point sur la map
			GO.GetComponent<NavMeshAgent>().destination = _posNewPathNode;
			//si un humanoide est pres du point alors direction vers un autre point aléatoire
			if(Mathf.Abs(_posNewPathNode.x-GO.transform.position.x)<1 && Mathf.Abs(_posNewPathNode.z-GO.transform.position.z)<1)
			{
				int _NewRadomPathNode = Random.Range(1, pathNodeFirstFloor.Length);
				Debug.Log ("old path "+(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode.ToString()+", new path "+_NewRadomPathNode.ToString());
				(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = _NewRadomPathNode;
			}

		}
	}

	// Update is called once per frame
	void Update ()
	{
		AI_Walk();
	}
}
