using UnityEngine;
using System.Collections;

public class LaserTrap : Trap
{
	public float ChargeDuration = .8f;
	public float ShootDuration = .5f;

	private ParticleSystem chargeEmitter;
	private ParticleSystem shootEmitter;
	private LaserBeam beam;

	private enum States {Waiting, Charging, Shooting, Depleted};
	private States state;

	void Awake()
	{
		chargeEmitter = transform.Find("Charge").GetComponent<ParticleSystem>();
		shootEmitter = transform.Find("Shoot").GetComponent<ParticleSystem>();
		beam = transform.Find("LaserBeam").GetComponent<LaserBeam>();

		state = States.Waiting;
	}

	void Start ()
	{
		chargeEmitter.gameObject.SetActive(false);
		shootEmitter.gameObject.SetActive(false);
		beam.gameObject.SetActive(false);
	}
	
	void Update ()
	{
		if (state == States.Waiting && Mathf.Abs(PlayerController.instance.transform.position.y - transform.position.y) <= 1f)
		{
			state = States.Charging;
			StartCoroutine(Charge());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (state != States.Shooting)
			return;

		if (other.gameObject.tag == "Player")
		{
			GameManager.instance.GameOver();
		}
	}

	IEnumerator Charge()
	{
		chargeEmitter.gameObject.SetActive(true);

		yield return new WaitForSeconds(ChargeDuration);

		chargeEmitter.gameObject.SetActive(false);

		// start shooting
		state = States.Shooting;
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		shootEmitter.gameObject.SetActive(true);
		beam.gameObject.SetActive(true);

		// flip the beam
		if (Flipped)
		{
			Vector3 rotation = beam.transform.localEulerAngles;
			rotation.y = 180;
			beam.transform.localEulerAngles = rotation;
		}

		yield return new WaitForSeconds(ShootDuration);

		shootEmitter.gameObject.SetActive(false);
		beam.gameObject.SetActive(false);

		state = States.Depleted;
	}
}
