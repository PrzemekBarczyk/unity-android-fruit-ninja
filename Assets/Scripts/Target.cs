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

    [SerializeField] float maxVerticalForce = 14f;
    [SerializeField] float minVerticalForce = 8f;
    [SerializeField] float maxHorizontalForce = 5f;
    [SerializeField] float maxTorque = 10f;

    [SerializeField] bool isLethal;

    [SerializeField] float destroyPosY = -6f;

    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager");
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        myRigidbody.AddTorque(RandomTorque(), ForceMode.Impulse);
    }

    void Update()
    {
        if (transform.position.y < destroyPosY)
		{
            Destroy(gameObject);
            if (!isLethal)
            {
                AudioSource.PlayClipAtPoint(missedSFX, transform.position);
                gameManager.RemoveLife();
            }
        }
    }

    public void HandleHit(Vector3 hitPosititon)
	{
        if (gameObject != null)
        {
            if (!isLethal)
		    {
                GameObject sliced = Instantiate(slicedPrefab, transform.position, transform.rotation, spawnManager.transform);
                foreach (Rigidbody s in sliced.GetComponentsInChildren<Rigidbody>())
			    {
                    s.velocity = myRigidbody.velocity;
                    s.angularVelocity = myRigidbody.angularVelocity;
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
            Destroy(gameObject);

            if (isLethal)
            {
                gameManager.RemoveLife();
            }
            else
            {
                gameManager.AddScore(points);
            }
        }
    }

	public Vector3 RandomForce()
	{
        return new Vector3(Random.Range(-maxHorizontalForce, maxHorizontalForce), Random.Range(minVerticalForce, maxVerticalForce), 0f);
	}

    public Vector3 RandomTorque()
	{
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
	}

    public AudioClip RandomSound()
	{
        int randomIndex = Random.Range(0, sliceSFXs.Length);
        return sliceSFXs[randomIndex];
	}
}
