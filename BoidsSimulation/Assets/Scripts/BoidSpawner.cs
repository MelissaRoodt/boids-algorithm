using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] List<Boid> boids = new List<Boid>();
    [SerializeField] private Boid boid;

    [SerializeField] private TextMeshProUGUI txtBoids;
    [SerializeField] private MainMenu menu;

    private void Update()
    {
        if (menu.isOn) return; //do not spawn boids if menu is on

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = new Vector3(0, 0, 10);

        if (Input.GetMouseButtonDown(0))
        {
            Boid boidGameObject = Instantiate(boid, mousePosition + offset, Quaternion.identity);
            boids.Add(boidGameObject);
            txtBoids.text = "Boids: " + boids.Count.ToString();
        }
    }
}
