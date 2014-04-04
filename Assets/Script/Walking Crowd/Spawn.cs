using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public int minPeople = 1;
	public int nbPeople;
	public int percentSlider = 6;

	public GameObject prefab_people;

	public Vector3 _posNewPeopleUpStairs;
	public Vector3 _posNewPeopleDownStairs;

	//Vector3 _minGround = new Vector3(20,20,20);
	//Vector3 _dimGround = new Vector3(50,50,50);

	// Use this for initialization
	void Start ()
	{
	//call function spawn people randomly
		peopleSpawn();
	}

	void peopleSpawn()
	{
		// nombre aléatoire de gens par rapport au pourcentage du slider
		//nbPeople = Random.Range(minPeople, percentSlider);
		nbPeople = 10;
		
		//création des gens aléatoirement sur la map
		for(int i=0 ; i<nbPeople ; ++i)
		{

	// il faut faire des zones de spawn comme devant les magasins par exemple
			//pour l'instant spawn aléatoirement sur la map
			_posNewPeopleUpStairs.x = Random.value * 20;
			_posNewPeopleUpStairs.y = 10;
			_posNewPeopleUpStairs.z = Random.value * 50;

			Object newPeopleUpStairs;
			newPeopleUpStairs = Instantiate(prefab_people, _posNewPeopleUpStairs, prefab_people.transform.rotation);

			_posNewPeopleDownStairs.x = Random.value * 20;
			_posNewPeopleDownStairs.y = 1;
			_posNewPeopleDownStairs.z = Random.value * 50;
			
			Object newPeopleUpDown;
			newPeopleUpDown = Instantiate(prefab_people, _posNewPeopleDownStairs, prefab_people.transform.rotation);


		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
