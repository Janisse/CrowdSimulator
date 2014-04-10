using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class SharedData : MonoBehaviour {

	private string nameData;
	private int indexData;

	public void setData(float data)
	{
		MiddleVR.VRDeviceMgr.GetAxis(nameData).SetValue(0, data);
	}

	public float getData()
	{
		return MiddleVR.VRDeviceMgr.GetAxis(nameData).GetValue(0);
	}

	public void createData(string name)
	{
		nameData = name;
		MiddleVR.VRDeviceMgr.CreateAxis (nameData);
		MiddleVR.VRClusterMgr.AddSynchronizedObject(MiddleVR.VRDeviceMgr.GetAxis(nameData));
	}
}
