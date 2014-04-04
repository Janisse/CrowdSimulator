using UnityEngine;
using System.Collections;

public class SimulatorManager : MonoBehaviour {

	//Spawn
	public int nbPeople;
	public GameObject prefab_people;
	Vector3 _posNewPeopleFirstFloor;

	//Walk
	public GameObject[] People;
	Transform _transform;
	public Transform[] pathNodeFirstFloor;	
	protected int _speed = 2;
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
		nbPeople = 6;
		
		//création des gens aléatoirement sur la map
		for(int i=0 ; i<nbPeople ; ++i)
		{
			// il faut faire des zones de spawn comme devant les magasins par exemple
			//pour l'instant spawn aléatoirement sur la map
			
			_posNewPeopleFirstFloor.x = Random.value * 10;
			_posNewPeopleFirstFloor.y = 1;
			_posNewPeopleFirstFloor.z = Random.value * 13;
			
			Object newPeopleFirstFloor;
			newPeopleFirstFloor = Instantiate(prefab_people, _posNewPeopleFirstFloor, prefab_people.transform.rotation);
		}
	}

	void AI_Walk()
	{
		
		People = GameObject.FindGameObjectsWithTag("TagPeople");
		Vector3 zone = new Vector3();
		Vector3 movingTo = new Vector3();
		Vector3 velocity = new Vector3();

		foreach(GameObject GO in People)
		{   
			Debug.Log(People.Length.ToString());
			if((GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode < pathNodeFirstFloor.Length)
			{
				zone = pathNodeFirstFloor[(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode].position;
				movingTo = zone - GO.transform.position;
				velocity = GO.rigidbody.velocity;
				
				if(movingTo.magnitude < 1.5)
				{
					// Randomize newPoint
					int newNode = Random.Range(1, pathNodeFirstFloor.Length);
					(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = newNode;
				}
				else {velocity = movingTo.normalized*_speed;}
			}
			else
			{
				(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = 0;
			}
			GO.rigidbody.velocity = velocity;
			GO.rigidbody.AddForce(velocity, ForceMode.Acceleration);
			GO.transform.LookAt(zone);

			//suivant une probabilité
			//si le curPathNode est pres de monterBas l'escalator alors fonction UpStairs
		}
	}

	// Update is called once per frame
	void Update ()
	{
		AI_Walk();
	}
}
