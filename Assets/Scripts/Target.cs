using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gameManager;
    GameObject spawnManager;

    [SerializeField] GameObject slicedPrefab;

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
                gameManager.RemoveLife();
            }
        }
    }

    public void HandleHit()
	{
        if (!isLethal)
		{
            GameObject sliced = Instantiate(slicedPrefab, transform.position, transform.rotation, spawnManager.transform);
            foreach (Rigidbody s in sliced.GetComponentsInChildren<Rigidbody>())
			{
                s.velocity = myRigidbody.velocity;
                s.angularVelocity = myRigidbody.angularVelocity;
			}
		}
        Destroy(gameObject);
        if (gameObject != null)
		{
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
}
