using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Groupe : MonoBehaviour
{

	//Variables
	private int nbHumanoide;						//Nombre d'humanoide du groupe
	private List<GameObject> humanoideList;			//Liste des humanoides du groupe
	public int curFloor;							//Etage actuel du groupe
	private bool isUse;								//Connaitre si le groupe est utilisé ou non


	// Use this for initialization
	void Start () {
		//Initialise l'etat du groupe à inactif
		isUse = false;

		//Initialise le tableau d'humanoide du groupe
		humanoideList = new List<GameObject> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateGroupe()
	{		
		//Initialise aléatoirement nbHumanoide
		nbHumanoide = Random.Range (2,6);

		//initialise aléatoirement la liste des Humanoides
		GameObject[] tabHumanoide = GameObject.FindGameObjectsWithTag ("Humanoide");
		for(int i=0; i<nbHumanoide && tabHumanoide.Length>nbHumanoide; i++)
		{
			bool ok = false;
			while(!ok)
			{
				int randomIndex = Random.Range(0, tabHumanoide.Length);
				//Si l'humanoide n'est pas en interaction avec quelque chose et qu'il est au bon etage, on l'ajoute au groupe.
				//Sinon on le laisse tranquille
				if(tabHumanoide[randomIndex].GetComponent<Humanoide>().getInteractionState() == Humanoide.interaction.nothing
				   && tabHumanoide[randomIndex].GetComponent<Humanoide>().getCurFloor() == curFloor)
				{
					humanoideList.Add(tabHumanoide[randomIndex]);
					tabHumanoide[randomIndex].GetComponent<Humanoide>().setInteractionState(Humanoide.interaction.goToGroupe);
					ok = true;
				}
			}
		}

		//On fait venir les Humanoides (en cercle) dans le groupe
		for(int i=0; i<nbHumanoide; i++)
		{
			humanoideList[i].GetComponent<Humanoide>().setDestination(new Vector3(transform.position.x + Mathf.Cos(2*(i/nbHumanoide)*Mathf.PI), transform.position.y, transform.position.z + Mathf.Sin(2*(i/nbHumanoide)*Mathf.PI)));
		}
	}

	public void DeleteGroupe()
	{
		//On fait partir les Humanoides du groupe
		for(int i=0; i<nbHumanoide; i++)
		{
			humanoideList[i].GetComponent<Humanoide>().newDestination();
			humanoideList[i].GetComponent<Humanoide>().setInteractionState(Humanoide.interaction.nothing);
			humanoideList[i].GetComponent<Humanoide>().walk();
		}

		//On vide la liste
		humanoideList.Clear ();
	}
}
