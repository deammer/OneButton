using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour
{
	public float ChargeDuration = .8f;
	public float ShootDuration = .5f;

	private ParticleSystem chargeEmitter;
	private ParticleSystem shootEmitter;
	private LaserBeam beam;

	void Awake()
	{
		chargeEmitter = transform.Find("Charge").GetComponent<ParticleSystem>();
		shootEmitter = transform.Find("Shoot").GetComponent<ParticleSystem>();
		beam = transform.Find("LaserBeam").GetComponent<LaserBeam>();
	}

	void Start ()
	{
		chargeEmitter.gameObject.SetActive(false);
		shootEmitter.gameObject.SetActive(false);
		beam.gameObject.SetActive(false);
	}
	
	void Update ()
	{
		
	}
}
