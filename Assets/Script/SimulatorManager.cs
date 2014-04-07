//using UnityEngine;
//using System.Collections;
//
//public class SimulatorManager : MonoBehaviour {
//	
//	//Spawn
//	public int nbPeople;
//	public GameObject prefab_people;
//	Vector3 _posNewPeopleFirstFloor;
//	
//	//Walk
//	public GameObject[] People;
//	Transform _transform;
//	public Transform[] pathNodeFirstFloor;	
//	protected int _speed = 1;
//	int curPathNode;
//	
//	void Awake()
//	{
//		pathNodeFirstFloor[0] = transform;
//	}
//	
//	// Use this for initialization
//	void Start ()
//	{
//		_transform = transform;
//		AI_Spawn();
//	}
//	
//	void AI_Spawn()
//	{
//		
//		//création des gens aléatoirement sur la map
//		for(int i=0 ; i<nbPeople ; ++i)
//		{
//			// il faut faire des zones de spawn comme devant les magasins par exemple
//			//pour l'instant spawn aléatoirement sur la map
//			
//			_posNewPeopleFirstFloor.x = Random.value * 10;
//			_posNewPeopleFirstFloor.y = 1;
//			_posNewPeopleFirstFloor.z = Random.value * 13;
//			//instantiation de l'objet
//			Object newPeopleFirstFloor;
//			newPeopleFirstFloor = Instantiate(prefab_people, _posNewPeopleFirstFloor, prefab_people.transform.rotation);
//		}
//	}
//	
//	void AI_Walk()
//	{
//		
//		People = GameObject.FindGameObjectsWithTag("TagPeople");
//		
//		
//		foreach(GameObject GO in People)
//		{   
//			// position d'un point sur la map
//			Vector3 _posNewPathNode = pathNodeFirstFloor[(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode].position;
//			// le gameObject (humanoide) se dirige vers un nouveau point sur la map
//			GO.GetComponent<NavMeshAgent>().destination = _posNewPathNode;
//			
//			//			//si il y a des points dans la meme direction que regarde l'humanoide alors go
//			//			while(angle < 0.8 && i < 5)
//			//			{
//			//				//on cherche le vecteur entre l'humanoide et le point suivant
//			//				Vector3 A = _posNewPathNode - GO.transform.position;
//			//				A.Normalize();
//			//				// le vecteur de l'humanoide
//			//				Vector3 B = GO.transform.forward;
//			//				B.Normalize();
//			//				//Produit scalaire des deux (a.x * b.x) + (a.y * b.y) + (a.z * b.z) = angle
//			//				angle = Vector3.Dot(A,B);
//			//
//			//				//va a un point suivant
//			//				//int _PathSameDirection = pathNodeFirstFloor.Length - 1;
//			//				//Debug.Log ("old path "+(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode.ToString()+", new path "+_NewRadomPathNode.ToString());
//			//				//(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = _PathSameDirection;
//			//
//			//				i++;
//			//			}
//			
//			//si un humanoide est pres du point
//			if(Mathf.Abs(_posNewPathNode.x-GO.transform.position.x)<1 && Mathf.Abs(_posNewPathNode.z-GO.transform.position.z)<1)
//			{
//				float angle = 0;
//				int i = 0;
//				int _RandomPathNode = 0;
//				while(angle < 0.1 && i < pathNodeFirstFloor.Length)
//				{
//					//on cherche un point autre point random
//					_RandomPathNode = Random.Range(1, pathNodeFirstFloor.Length);
//					
//					//on cherche le vecteur entre l'humanoide et le point random
//					Vector3 A = (pathNodeFirstFloor.GetValue((long)_RandomPathNode) as Transform).position - GO.transform.position;
//					A.Normalize();
//					// le vecteur de l'humanoide
//					Vector3 B = GO.transform.forward;
//					B.Normalize();
//					//Produit scalaire des deux (a.x * b.x) + (a.y * b.y) + (a.z * b.z) = angle
//					angle = Vector3.Dot(A,B);
//					i++;
//				}
//				(GO.GetComponent("VariablesFirstFloor") as VariablesFirstFloor).curPathNode = _RandomPathNode;
//			}
//		}
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		AI_Walk();
//	}
//}
