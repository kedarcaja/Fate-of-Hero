using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		TorchLight.GetComponent<Light>().intensity=IntensityLight;
	}
	
	void Update () {
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		TorchLight.GetComponent<Light>().intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));

		TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/1.5f,1f),Mathf.Min(IntensityLight/2f,1f),0f);		
	}
}
