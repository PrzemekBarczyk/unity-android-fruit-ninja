using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gameManager;

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
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        myRigidbody.AddTorque(RandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
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
