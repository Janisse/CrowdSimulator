using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public int minPeople = 1;
	public int nbPeople;
	public int percentSlider = 6;

	public GameObject prefab_people;
	public GameObject floor;

	public Vector3 _posNewPeople;
	Vector3 _minGround = new Vector3(2,2,2);
	Vector3 _dimGround = new Vector3(20,20,20);

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
			_posNewPeople.x = _minGround.x + Random.value * _dimGround.x;
			_posNewPeople.y = 1;
			_posNewPeople.z = _minGround.z + Random.value * _dimGround.z;
			
			Object newPeople;
			newPeople = Instantiate(prefab_people, _posNewPeople, prefab_people.transform.rotation);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
