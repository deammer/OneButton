using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[RangeAttribute(0f,20f)]
	public float NormalPlatformSpeed = 2f;
	public float PlatformSpeedMod = 1.5f;
	public float PlatformSpeed { get { return NormalPlatformSpeed * heightMod; } }
	private float currentPlatformSpeed;
	private float heightMod = 1f;
	private float distanceFromCenterToTop;
	private const float heightSpeedMod = 2f;

	[HideInInspector]
	public Transform SpawnZone;
	[HideInInspector]
	public Transform RemoveZone;

	public static int CoinsPickedUp = 0;
	public static float HeightReached = 0f;

	private PlatformSpawner spawner;
	private TrapSpawner trapSpawner;
	private Transform trapIndicator;

	private bool gameStarted = false;

	void Awake()
	{
		// there can be only one
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		// round up the children
		SpawnZone = transform.Find("SpawnZone");
		RemoveZone = transform.Find("RemoveZone");
		trapIndicator = transform.Find("TrapIndicator");

		// cache the spawners
		trapSpawner = transform.Find("TrapSpawner").GetComponent<TrapSpawner>();
		spawner = transform.Find("PlatformSpawner").GetComponent<PlatformSpawner>();
	}

	void Start()
	{
		// reset the game
		CoinsPickedUp = 0;
		HeightReached = 0f;
		gameStarted = false;

		currentPlatformSpeed = NormalPlatformSpeed;
		NormalPlatformSpeed = 0;

		distanceFromCenterToTop = transform.Find("CameraTop").position.y;

		spawner.Disable();
		trapSpawner.Disable();

		trapIndicator.gameObject.SetActive(false);
	}

	void Update()
	{
		heightMod = Mathf.Max(1f, 1f + PlayerController.instance.transform.position.y / distanceFromCenterToTop * heightSpeedMod);

		HeightReached += Time.deltaTime;
	}

	public void GameOver()
	{
		PlayerController.instance.gameObject.SetActive(false);
		trapSpawner.Disable();
		spawner.Disable();
		StartCoroutine(TweenToGameOver());
	}

	IEnumerator TweenToGameOver()
	{
		float duration = 3f;
		float tweenDuration = 1f;
		float elapsed = 0;
		currentPlatformSpeed = NormalPlatformSpeed;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			NormalPlatformSpeed = Mathf.Lerp(currentPlatformSpeed, 0, elapsed / tweenDuration);

			yield return 0;
		}

        SceneManager.LoadScene("GameOver");
	}

	IEnumerator _BeginGame()
	{
		float elapsed = 0;
		while (elapsed < 1.5f)
		{
			elapsed = Mathf.MoveTowards(elapsed, 1.5f, Time.deltaTime);
			NormalPlatformSpeed = Mathf.Lerp(0, currentPlatformSpeed, elapsed / 1.5f);
			yield return 0;
		}
		NormalPlatformSpeed = currentPlatformSpeed;

		// enable spawning
		spawner.Enable();
		trapSpawner.Enable();
	}

    public void BeginGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            StartCoroutine(_BeginGame());
        }
    }

	public void CoinCollected()
	{
		CoinsPickedUp ++;
	}

	public void ShowTrapIndicator(Transform trap)
	{
		trapIndicator.gameObject.SetActive(true);
		trapIndicator.SetX(trap.position.x);

		StopCoroutine(HideTrapIndicator());

		trapIndicator.localScale = new Vector3(.5f, .5f, 1f);
		StartCoroutine(trapIndicator.ScaleTo (new Vector3(1f, 1f, 1f), 1.0f, Ease.ElasticOut));

		StartCoroutine(HideTrapIndicator());
	}

	private IEnumerator HideTrapIndicator()
	{
		yield return new WaitForSeconds(1.2f);

		StartCoroutine(trapIndicator.ScaleTo(new Vector3(0, 0, 1f), .5f, Ease.ElasticIn));
		yield return new WaitForSeconds(.5f);
		trapIndicator.gameObject.SetActive(false);
	}
}
