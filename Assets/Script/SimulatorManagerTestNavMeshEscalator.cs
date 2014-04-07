//using UnityEngine;
//using System.Collections;
//
//public class SimulatorManagerTestNavMeshEscalator : MonoBehaviour {
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
//	public Transform monterHaut;
//	public Transform monterBas;
//	public Transform descendreHaut;
//	public Transform descendreBas;
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
//
//			//random pour que seulement une partie des humanoide y aille
//
//			//si on est proche du point de monterBas de l'escalator alors on va dans la fonction
//			if(Mathf.Abs(monterBas.position.x-GO.transform.position.x)<3 && Mathf.Abs(monterBas.position.z-GO.transform.position.z)<3)
//			{
//				GO.transform.position = monterBas.position;
//				if(Random.Range(0, 0.0005) == 0)
//				{
//					GO.transform.position = monterHaut.position;
//					walkSecondFloor();
//				}
//			}
//		}
//	}
//
//	void walkSecondFloor()
//	{
//	}
//	
//	void upStairs()
//	{
//		//aller de monterBas à monterHaut
//
//
//	}
//	
//	void downStairs()
//	{
//		//aller de descendreHaut à descendreBas
//		
//		//si curPathNode est = à descendreBas alors recupère le parcours walkFirstFloor
//	}
//
//	// Update is called once per frame
//	void Update ()
//	{
//		AI_Walk();
//	}
//}
