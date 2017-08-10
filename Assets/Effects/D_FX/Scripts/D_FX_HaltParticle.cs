using UnityEngine;
using System.Collections;

public class D_FX_HaltParticle : MonoBehaviour {

	private ParticleSystem[] systems;
	
	void OnEnable()
	{
		systems = GetComponentsInChildren<ParticleSystem>();
		
		foreach(ParticleSystem ps in systems)
			//ps.emission.enabled = false;
		
		StartCoroutine("WaitFrame");
	}
	
	IEnumerator WaitFrame()
	{
		yield return null;
		
		foreach(ParticleSystem ps in systems)
		{
			//ps.enableEmission = true;
			ps.Play(true);
		}
	}
}
