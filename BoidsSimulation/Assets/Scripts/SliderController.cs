using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Header("Boid Settings")]
    public Slider speedSlider;
    public Slider neighbourRadiusSlider;
    public Slider seperationRadiusSlider;
    public Slider seperationWeightSlider;

    [Header("Boid Settings")]
    public TextMeshProUGUI txtspeed;
    public TextMeshProUGUI txtNeighbourRadius;
    public TextMeshProUGUI txtSeperationRadius;
    public TextMeshProUGUI txtSeperationWeight;

    private void Update()
    {
        txtspeed.text = speedSlider.value.ToString("0.0");
        txtNeighbourRadius.text = neighbourRadiusSlider.value.ToString("0.0");
        txtSeperationRadius.text = seperationRadiusSlider.value.ToString("0.0");
        txtSeperationWeight.text = seperationWeightSlider.value.ToString("0.0");

    }

}
