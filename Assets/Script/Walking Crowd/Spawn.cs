using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public int curFloor;
	public GameObject humanoidePrefab;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void spawnPeople(int nbPeople)
	{
		for(int i=0; i<nbPeople; i++)
		{
			humanoidePrefab.GetComponent<Humanoide>().setCurFloor(curFloor);
			Instantiate(humanoidePrefab, transform.position, transform.rotation);

		}
	}
}
