using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool isOn = false;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button btnQuit;

    private void Start()
    {
        btnQuit.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isOn = !isOn;
        }
        mainMenu.SetActive(isOn);
    }
}
