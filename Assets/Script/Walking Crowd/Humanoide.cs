using UnityEngine;
using System.Collections;

public class Humanoide : MonoBehaviour
{
	//tableau d'Humanoides
	public GameObject[] People;

	Transform _transform1;
	Transform _transform2;

	public Transform[] pathNodeFirstFloor;
	//public Transform[] pathNodeSecondFloor;
	
	protected int _speed = 10;
	int curPathNode;

	void Awake()
	{
		pathNodeFirstFloor[0] = transform;
	}

	// Use this for initialization
	void Start ()
	{
		_transform1 = transform;
		//_transform2 = transform;

		walkFirstFloor();
	}

	void walkFirstFloor()
	{
		People = GameObject.FindGameObjectsWithTag("TagPeople");
		Vector3 zone = new Vector3();
		Vector3 movingTo = new Vector3();
		Vector3 velocity = new Vector3();

		for (int i= 0; i < People.Length; i++)
		{   
			if((People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode < pathNodeFirstFloor.Length)
			{
				zone = pathNodeFirstFloor[(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode].position;
				movingTo = zone - People[i].transform.position;
				velocity = People[i].rigidbody.velocity;
				Debug.Log(velocity.ToString());
				Debug.Log(zone.ToString());

				if(movingTo.magnitude < 1.5)
				{
					
					// Randomize newPoint
					int newNode = Random.Range(1, pathNodeFirstFloor.Length);
					(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = newNode;
				}
				else {velocity = movingTo.normalized*_speed;}
			}
			else
			{
				(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = 0;
			}
			People[i].rigidbody.velocity = velocity;
			People[i].transform.LookAt(zone);
			Debug.Log(velocity.ToString());
			Debug.Log(zone.ToString());
			//suivant une probabilité
			//si le curPathNode est pres de monterBas l'escalator alors fonction UpStairs

		}
	}

	void walkSecondFloor()
	{
//		People = GameObject.FindGameObjectsWithTag("TagPeople") ;
//		
//		int i = 0;
//		
//		Vector3 zone = pathNode[(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode].position;
//		Vector3 movingTo = zone - People[i].transform.position;
//		Vector3 velocity = People[i].rigidbody.velocity;
//		
//		for (i= 0; i < People.Length; i++)
//		{   
//			if((People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode < pathNode.Length)
//			{
//				
//				if(movingTo.magnitude < 1.5)
//				{
//					
//					// Randomize newPoint
//					int newNode = Random.Range(1, pathNode.Length);
//					(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = newNode;
//				}
//				else {velocity = movingTo.normalized*_speed;}
//			}
//			else
//			{
//				(People[i].GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = 0;
//			}
//			People[i].rigidbody.velocity = velocity;
//			People[i].rigidbody.AddForce(velocity, ForceMode.Acceleration);
//			People[i].transform.LookAt(zone);
//			
//			
//			//suivant une probabilité
//			//si le curPathNode est pres de descendreHaut l'escalator alors fonction DownStairs
//		}
	}

	void upStairs()
	{
		//aller de monterBas à monterHaut

		//si curPathNode est = à monterHaut alors recupère le parcours walkSecondFloor
	}

	void downStairs()
	{
		//aller de descendreHaut à descendreBas

		//si curPathNode est = à descendreBas alors recupère le parcours walkFirstFloor
	}
	
	// Update is called once per frame
	void Update () {

	}
}
