using UnityEngine;
using System.Collections;

public class VariablesFirstFloor : MonoBehaviour
{

	public int curPathNode; 

	void Start ()
	{
		curPathNode = Random.Range(1,28); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
