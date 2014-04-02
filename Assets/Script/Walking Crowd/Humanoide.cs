using UnityEngine;
using System.Collections;

public class Humanoide : MonoBehaviour
{
	//point de depart
	public Vector3 startPath;
	//current point
	public Vector3 curPath;
	//point d'arriver
	public Vector3 arrivePath;
	//tableau de point d'arriver
	public Vector3[] arrivePathTab;
	//vitesse de deplacement
	public float speedMove;
	//tableau de personnes
	float[] peopleTab;
	
	//tableau d'Humanoides
	protected GameObject[] People;

	protected Vector3 _minGround = new Vector3(2,2,2);
	protected Vector3 _dimGround = new Vector3(20,20,20);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
