using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{

    public static inputManager instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }


    public Vector2 movementDirection;
    public bool interactButton;

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactButton = true;
        } else { interactButton = false; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.SetGameState(1);
        }
    }
}
