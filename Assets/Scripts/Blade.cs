using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] GameObject bladeTrailPrefab;
    GameObject bladeTrailInstantion;

    [SerializeField] LayerMask targetLayer;

    [SerializeField] Vector3 raycastOffset = new Vector3(0f, 0f, -5f); // to avoid casting rays from inside of targets

    [SerializeField] float minCutVelocity = 0.1f; // to lock cutting when blade not moving

    Vector3 previousPosition;

    bool mouseLeftKeyPressed;

	void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            mouseLeftKeyPressed = true;

			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;
            previousPosition = transform.position;

			bladeTrailInstantion = Instantiate(bladeTrailPrefab, transform);
		}
        else if (Input.GetMouseButtonUp(0))
		{
            mouseLeftKeyPressed = false;

            Destroy(bladeTrailInstantion);
		}

        if (mouseLeftKeyPressed)
		{
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;

            float velocity = ((transform.position - previousPosition).magnitude) / Time.deltaTime;

            if (velocity > minCutVelocity)
			{
                RaycastHit rayHit;
                if (Physics.Raycast(transform.position + raycastOffset, Vector3.forward, out rayHit, 100f, targetLayer))
                {
                    rayHit.transform.GetComponent<Target>().CutWithBlade(transform.position);
                }
            }

            previousPosition = transform.position;
        }
    }
}
