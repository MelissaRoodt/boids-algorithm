using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obsticle;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = new Vector3(0, 0, 10);

        if (Input.GetMouseButtonDown(1))
        {
            GameObject boidObj = Instantiate(obsticle, mousePosition + offset, Quaternion.identity);
        }
    }
}
