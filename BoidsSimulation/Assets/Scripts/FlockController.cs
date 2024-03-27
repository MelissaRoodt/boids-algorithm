using UnityEngine;
using UnityEngine.UI;

public class FlockController : MonoBehaviour
{
    // Static reference to the instance of the singleton
    private static FlockController _instance;

    [Header("Flock Settings")]
    public Toggle btnSeperation;
    public Toggle btnAlignment;
    public Toggle btnCohesion;

    [Header("Boid Settings")]
    public Slider speedSlider;
    public Slider neighbourRadiusSlider;
    public Slider seperationRadiusSlider;
    public Slider seperationWeightSlider;

    // Public property to access the singleton instance
    public static FlockController Instance
    {
        get
        {
            // If the instance hasn't been set yet, find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<FlockController>();

                // If it still hasn't been found, log an error
                if (_instance == null)
                {
                    Debug.LogError("FlockController instance not found in the scene.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this object if it hasn't been set yet
        _instance = this;

        // Ensure that the singleton persists between scene changes
        DontDestroyOnLoad(gameObject);
    }

    // Public getter methods for the Toggle properties
    public bool GetSeperationToggleState()
    {
        return btnSeperation.isOn;
    }

    public bool GetAlignmentToggleState()
    {
        return btnAlignment.isOn;
    }

    public bool GetCohesionToggleState()
    {
        return btnCohesion.isOn;
    }

    public float GetSpeed()
    {
        return speedSlider.value;
    }
    public float GetNeigbourRadius()
    {
        return neighbourRadiusSlider.value;
    }
    public float GetSeperationRadius()
    {
        return seperationRadiusSlider.value;
    }
    public float GetSeperationWeight()
    {
        return seperationWeightSlider.value;
    }
}
