using UnityEngine;

public class Blade : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField] GameObject bladeTrailPrefab;
    GameObject bladeTrailInstantion;

    Rigidbody myRigidbody;
    SphereCollider sphereCollider;

    bool isCutting;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            isCutting = true;
            sphereCollider.enabled = true;
            myRigidbody.position = NewPosition();
			transform.position = myRigidbody.position;
			bladeTrailInstantion = Instantiate(bladeTrailPrefab, transform);
		}
        else if (Input.GetMouseButtonUp(0))
		{
            isCutting = false;
            sphereCollider.enabled = false;
            Destroy(bladeTrailInstantion);
		}

        if (isCutting)
		{
            myRigidbody.position = NewPosition();
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Target"))
		{
            if (other.gameObject != null)
			{
                other.GetComponent<Target>().HandleHit();
            }
		}
	}

	Vector2 NewPosition()
	{
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(newPosition.x, newPosition.y);
    }
}
