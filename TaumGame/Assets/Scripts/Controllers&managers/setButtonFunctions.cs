using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class setButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private Button testAreaButton;

    void Start()
    {
        if (testAreaButton != null)
        {
            testAreaButton.onClick.AddListener(delegate { GameManager.instance.SetGameState(0); });
        }
    }
}