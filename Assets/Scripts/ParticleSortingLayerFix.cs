using UnityEngine;
using System.Collections;

public class ParticleSortingLayerFix : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		particleSystem.renderer.sortingLayerName = "Default";
		particleSystem.renderer.sortingOrder = 0;

		Destroy (this.gameObject, 3);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
