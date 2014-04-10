using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Interface : MonoBehaviour {

	//Variables
	private bool interfaceOn;							//true -> interface activé, false -> interface désactivé
	public float widthInterface;						//Largeur de l'interface
	public float heightInterface;						//Hauteur de l'interface
	private Rect windowRect;							//Taille de l'interface
	private int curPosMenu;								//Position du curseur dans le menu
	private bool xAxisStep;								//Si le stick est relaché ou non
	private bool yAxisStep;								//Si le stick est relaché ou non
	
	public System.Collections.SortedList valeurMenu;	//Dictionnaire des options et de leur valeur correspondante

	// Use this for initialization
	void Start () {
		interfaceOn = false;
		xAxisStep = false;
		yAxisStep = false;
		windowRect = new Rect(Screen.width/2-widthInterface/2, Screen.height/2-heightInterface/2, widthInterface, heightInterface);

		//Init menu
		valeurMenu = new System.Collections.SortedList();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		valeurMenu.Add ("Sensibilité vue", (int)player.GetComponent<moveController>().rotationSpeed);
		valeurMenu.Add ("test bool", true);
		valeurMenu.Add ("test int", 0);
	}
	
	// Update is called once per frame
	void Update () {
		//Affiche le menu
		if(MiddleVR.VRDeviceMgr.GetJoystick().IsButtonToggled(7))
		{
			interfaceOn = !interfaceOn;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<moveController>().setPause(interfaceOn);
		}
		if(interfaceOn == true)
		{
			//Input navigation
			float xAxis = MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(1);
			if(Mathf.Abs(xAxis) > 0.5f)
			{
				if(xAxis < 0)
					upDown(-1);
				else
					upDown(1);
				xAxisStep = true;
			}
			else
				xAxisStep = false;
			float yAxis = MiddleVR.VRDeviceMgr.GetJoystick().GetAxisValue(0);
			if(Mathf.Abs(yAxis) > 0.5f)
			{
				if(yAxis < 0)
					rightLeft(-1);
				else
					rightLeft(1);
				yAxisStep = true;
			}
			else
				yAxisStep = false;
		}
	}

	void OnGUI()
	{
		if(interfaceOn == true)
		{
			//On affiche le menu sur l'écran principal
			if(MiddleVR.VRClusterMgr.IsServer())
			{
				windowRect = GUILayout.Window(0, windowRect, menu, "Menu");
			}
		}
	}

	void menu(int windowID)
	{
		//On parcourt toutes les cases du menu
		for(int i=0; i<valeurMenu.Count; i++)
		{
			//Met en evidence la propriete en cours
			if(curPosMenu == i)
				GUI.color = Color.red;
			else
				GUI.color = Color.white;

			GUILayout.BeginHorizontal();
			GUILayout.Label(valeurMenu.GetKey(i).ToString());
			GUILayout.FlexibleSpace();
			//Agir de façon diffrente en fonction du type de la variable
			if(valeurMenu.GetByIndex(i).GetType() == interfaceOn.GetType())
			{
				if((bool)valeurMenu.GetByIndex(i) == true)
					GUILayout.Label("Activé");
				else
					GUILayout.Label("Désactivé");
			}
			else if(valeurMenu.GetByIndex(i).GetType() == curPosMenu.GetType())
			{
				GUILayout.Label(valeurMenu.GetByIndex(i).ToString());
			}
			GUILayout.EndHorizontal();
		}
	}

	//Se dirige verticalement dans le menu
	void upDown(int dir)
	{
		//Verification sortie menu
		if(curPosMenu+dir<valeurMenu.Count && curPosMenu+dir>=0 && xAxisStep==false)
			curPosMenu += dir;
	}

	//Se dirige horizontalement dans le menu
	void rightLeft(int dir)
	{
		//Agir de façon diffrente en fonction du type de la variable
		//Int
		if(valeurMenu.GetByIndex(curPosMenu).GetType() == curPosMenu.GetType())
		{
			if(yAxisStep == false)
			{
				int newValue = (int)valeurMenu.GetByIndex(curPosMenu)+dir*10;
				if(newValue<=100 && newValue>=0)
					valeurMenu.SetByIndex(curPosMenu, newValue);
			}
		}
		//Bool
		else if(valeurMenu.GetByIndex(curPosMenu).GetType() == interfaceOn.GetType())
		{
			if(yAxisStep == false)
			{
				valeurMenu.SetByIndex(curPosMenu, !(bool)valeurMenu.GetByIndex(curPosMenu));
			}
		}

		//Synchronise le parametre avec le programme
		action ();
	}

	void action()
	{
		string param = (string)valeurMenu.GetKey (curPosMenu);
		switch(param)
		{
		case "Sensibilité vue":
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<moveController>().rotationSpeed = (int)valeurMenu.GetByIndex(curPosMenu);
			break;
		}
	}
}
