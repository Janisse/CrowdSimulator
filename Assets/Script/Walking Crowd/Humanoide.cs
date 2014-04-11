using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoide : MonoBehaviour
{
	//Variable
	private List<Transform> pathNodeTab;			//Liste des pathNodes de l'etage actuel
	public int curFloor;							//Etage actuel de l'humanoide
	private int destPathNode;						//Index du pathNode vers lequel l'humanoide se dirige
	private NavMeshAgent navMesh;					//Acces vers le navMeshAgent
	private Animator animControl;					//Acces vers l'Animator
	public static int ID;							//ID unique de l'humanoide
	private SharedData state;						//Variable d'etat de l'humanoide (idle / marche / course)

	public enum interaction
		{nothing, escalator, goToGroupe, inGroupe};	//Declaration des variables d'interaction
	private interaction interactionState;			//Variable d'interaction

	//Travaux à reprendre plustard
//	private List<GameObject> Magasins;
//	private int RandomMagasin;
//	public GameObject PlanTouchePrefab;



	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////
	//Initialisation
	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////
	
	// Use this for initialization
	void Start ()
	{
		//Defini l'ID de l'humanoide
		if(ID == 0) ID = 1;  else ID++;

		//Creation d'une variable d'etat synchronisé
		state = gameObject.AddComponent<SharedData>();
		state.createData ("Humanoide" + ID.ToString ());

		//Recupere l'Animator
		animControl = GetComponentInChildren<Animator>();

		//Initialise le tableau des pathNodes
		pathNodeTab = new List<Transform>();
		getPathNodeTab ();

		//Initialise le niveau d'interaction avec l'environement à nothing (aucune interaction à l'instanciation)
		interactionState = interaction.nothing;

		//Recupere le navMeshAgent
		navMesh = GetComponent<NavMeshAgent> ();

		//Generation aléatoire du point de destination lors du spawn et le fait aller à ce point
		destPathNode = Random.Range (0, pathNodeTab.Count);
		walk ();

		//Initialise son état (marche / course)
		setState (Random.Range(1,3));
		
		//???
		//Magasins = new List<GameObject>();
		
		//Initialise le tableau des magasins qu'ils peuvent regarder
		//getMagasins();
	}




	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////
	//Setter et getter
	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Change l'etage actuel de l'humanoide
	public void setCurFloor(int newCurFloor){curFloor = newCurFloor;}

	//Recupere l'etage actuel de l'humanoide
	public int getCurFloor(){return curFloor;}

	//Recupere l'etat d'interaction de l'humanoide
	public interaction getInteractionState(){return interactionState;}

	//Change l'etat d'interaction de l'humanoide
	public void setInteractionState(interaction newInteraction){interactionState = newInteraction;}

	//Change manuellement la destination de l'humanoide
	public void setDestination(Vector3 newDest){navMesh.destination = newDest;}



	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////
	//Update
	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////

	// Update is called once per frame
	void Update () {
		switch(interactionState)
		{
		case (interaction.nothing):			//Si aucune interaction
			updateWalk();
			break;
		case (interaction.escalator):		//Si l'humanoide est dans un escalator
			updateEscalator();
			break;
		case (interaction.goToGroupe):
			updateGoToGroupe();
			break;
		}

		//Synchronisation de l'état de l'humanoide entre les clusters
		updateState ();

		//???
		//Travaux à reprendre plustard
		//fonction regarder la vitrine
		//StartCoroutine(lookAtShowcase());
		//lookAtShowcase();
	}


	//Synchronisation de l'état de l'humanoide entre les clusters
	void updateState()
	{
		animControl.SetInteger("state", (int)state.getData());
		navMesh.speed = state.getData()*2;
	}

	//Comportement de l'IA dans l'escalator
	void updateEscalator()
	{
		//On recupère la position de la fin de l'escalator
		Vector3 posPathNodeOut = pathNodeTab [destPathNode].GetComponent<Escalator>().pathNodeOut.transform.position;

		//Si l'humanoide a atteint la fin de l'escalator
		if(Mathf.Abs(posPathNodeOut.x - transform.position.x) < 0.1f && Mathf.Abs(posPathNodeOut.z - transform.position.z) < 0.1f)
		{
			//On recupere le nouveau tableau des pathNodes
			getPathNodeTab();

			//On reactive le navMeshAgent
			navMesh.enabled = true;

			//On met fin a l'interaction avec l'escalator
			interactionState = interaction.nothing;

			//On choisi une nouvelle direction
			newDestination();

			//On envoi l'humanoide vers cette destination
			walk ();

			//On definit si il marche ou il court
			setState (Random.Range(1,3));
		}

		//Sinon on continu sur l'escalator
		else
		{
			transform.Translate(Vector3.Normalize((posPathNodeOut-transform.position))*Time.deltaTime*(pathNodeTab [destPathNode].GetComponent<Escalator>().speed), Space.World);
		}
	}

	//Verifie si le joueur a atteind le groupe
	void updateGoToGroupe()
	{
		Debug.Log ("J'y vais !");
		if(Mathf.Abs(transform.position.x-navMesh.destination.x)<0.5f && Mathf.Abs(transform.position.z-navMesh.destination.z)<0.5f)
		{
			Debug.Log("Jsuis dans le groupe chef !");
			//On passe l'animation en IDLE
			setState (0);

			//Et on change son interaction
			interactionState = interaction.inGroupe;
		}
	}

	//Comportement de l'IA sans interaction
	void updateWalk()
	{
		//Si on est arrivé à destination
		if(isArrive())
		{
			//Si il ne s'agit pas d'une entrée d'escalator
			if(pathNodeTab[destPathNode].name != "PathNodeLink")
			{
				//On definit une autre destination
				newDestination();
				//On l'envoie vers la destination
				walk ();
			}

			//Sinon on entre dans un escalator
			else
			{
				//On lui indique qu'il est en interaction avec un escalator
				interactionState = interaction.escalator;
				//On desactive son navMesh
				navMesh.enabled = false;
				//On passe l'animation en IDLE
				setState (0);
				//On actualise l'etage
				if(pathNodeTab [destPathNode].GetComponent<Escalator>().pathNodeOut.transform.position.y>transform.position.y) curFloor++;  else curFloor--;
			}
		}
	}



	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////
	//fonctions
	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Change l'animation de l'humanoide
	void setState(int newState)
	{
		//L'etat pouvant etre aléatoire, seul le serveur le définit et le transmet aux clients
		if(MiddleVR.VRClusterMgr.IsServer())
			state.setData ((float)newState);
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
	
	//Marche vers la destination de l'humanoide
	public void walk()
	{
		navMesh.destination = pathNodeTab [destPathNode].position;
	}
	
	//Test pour connaitre si l'humanoide est arrivé a destniation ou non
	bool isArrive()
	{
		//si l'humanoide est pres du point
		if(Mathf.Abs(pathNodeTab [destPathNode].position.x-transform.position.x)<1 && Mathf.Abs(pathNodeTab [destPathNode].position.z-transform.position.z)<1)
		{
			return true;
		}
		return false;
	}

	//Definit une nouvelle destination pour l'humanoide (de preference en adéquation avec sa direction)
	public void newDestination()
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















//Travaux à reprendre plustard
//	IEnumerator lookAtShowcase()
//	{
//
//		//on parcours tout les points de la liste de transform Magasins
//		for(int i =0; i < Magasins.Count; i++)
//		{
//			//stop a un nodePath aléatoire mais devant une vitrine avec un timer pendant 5 sec
////			if(PlanTouchePrefab.GetComponent<PlanTouche>().getIsgrounded() == true)
////			{
////				Debug.Log("les humains vont s'arréter FIN DU MOOOOOOOONDE ! ");
////				setState(0);
////				yield return new WaitForSeconds(5); 
////				
////			}
//
//			bool boolean = PlanTouchePrefab.GetComponent<PlanTouche>().getIsgrounded();
//			//stop a un nodePath aléatoire mais devant une vitrine avec un timer pendant 5 sec
//			if(boolean == true)
//			{
//				//timer += Time.deltaTime;
//				//if(timer > delay)
//				Physics.
//				rigidbody.useGravity = true;
//				yield return new WaitForSeconds(5); 
//			}
//			else
//				walk ();
//		}
//		
//	}

	//Travaux à reprendre plustard
	//Recupere tout les pathNodes des magasins en cours
//	void getMagasins()
//	{
//		GameObject[] planeMagGO = GameObject.FindGameObjectsWithTag ("Plane"+curFloor.ToString());
//		for(int i=0; i<planeMagGO.Length; i++)
//		{
//			Magasins.Add(planeMagGO[i]);
//		}
//	}
}
