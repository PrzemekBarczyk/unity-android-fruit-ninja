using UnityEngine;

public class Blade : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField] GameObject bladeTrailPrefab;
    GameObject bladeTrailInstantion;

    [SerializeField] float minCutVelocity = 0.1f;

    Vector2 previousPosition;

    Rigidbody myRigidbody;
    BoxCollider boxCollider;

    bool isCutting;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            isCutting = true;
            boxCollider.enabled = false;
            myRigidbody.position = NewPosition();
			transform.position = myRigidbody.position;
			bladeTrailInstantion = Instantiate(bladeTrailPrefab, transform);
		}
        else if (Input.GetMouseButtonUp(0))
		{
            isCutting = false;
            boxCollider.enabled = false;
            Destroy(bladeTrailInstantion);
		}

        if (isCutting)
		{
            previousPosition = myRigidbody.position;
            myRigidbody.position = NewPosition();

            float velocity = (NewPosition() - previousPosition).magnitude / Time.deltaTime;

            if (velocity > minCutVelocity)
			{
                boxCollider.enabled = true;
			}
			else
			{
                boxCollider.enabled = false;
			}
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Target"))
		{
            if (other.gameObject != null)
			{
                other.GetComponent<Target>().HandleHit(transform.position);
            }
		}
	}

	Vector2 NewPosition()
	{
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(newPosition.x, newPosition.y);
    }
}
