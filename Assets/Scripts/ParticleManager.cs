using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        particleSystem.renderer.sortingLayerName = "Particle";
        particleSystem.renderer.sortingOrder = 1;
        Invoke("DestroyParticle", 1f);
	}
	
	// Update is called once per frame
	void DestroyParticle () {
        Destroy(this.gameObject);
	}
}
