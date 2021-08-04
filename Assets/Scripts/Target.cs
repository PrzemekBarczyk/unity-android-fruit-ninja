using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gameManager;
    GameObject spawnManager;

    [SerializeField] GameObject slicedPrefab;
    [SerializeField] ParticleSystem sliceParticles;
    [SerializeField] AudioClip[] sliceSFXs;
    [SerializeField] AudioClip missedSFX;

    [SerializeField] int points = 1;

    [SerializeField] bool isLethal;

    [SerializeField] float destroyPosY = -6f;

    public Rigidbody MyRigidbody { get; private set; }

	void Awake()
	{
        MyRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        spawnManager = GameObject.Find("Spawn Manager");
    }

    void Update()
    {
        if (transform.position.y < destroyPosY)
		{
            gameObject.SetActive(false);
            if (!isLethal)
            {
                AudioSource.PlayClipAtPoint(missedSFX, transform.position);
                gameManager.RemoveLife();
            }
        }
    }

    public void CutWithBlade(Vector3 hitPosititon)
	{
        if (gameObject != null)
        {
            if (!isLethal)
		    {
                GameObject sliced = Instantiate(slicedPrefab, transform.position, transform.rotation, spawnManager.transform);
                foreach (Rigidbody s in sliced.GetComponentsInChildren<Rigidbody>())
			    {
                    s.velocity = MyRigidbody.velocity;
                    s.angularVelocity = MyRigidbody.angularVelocity;
			    }
                var particles = Instantiate(sliceParticles, transform.position, Quaternion.LookRotation(-(hitPosititon - transform.position).normalized));
                Destroy(particles.gameObject, 2f);

            }
            else
			{
                var particles = Instantiate(sliceParticles, transform.position, Quaternion.Euler(Vector3.up));
                Destroy(particles.gameObject, 5f);
            }

            AudioSource.PlayClipAtPoint(RandomSound(), transform.position);
            gameObject.SetActive(false);

            if (isLethal)
            {
                gameManager.RemoveAllLives();
            }
            else
            {
                gameManager.AddScore(points);
            }
        }
    }

    public AudioClip RandomSound()
	{
        int randomIndex = Random.Range(0, sliceSFXs.Length);
        return sliceSFXs[randomIndex];
	}
}
