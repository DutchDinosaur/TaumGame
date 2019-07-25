using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    [System.Serializable]
    class GameState
    {
        public GameObject[] Objects;
    }

    private GameState state;

    [SerializeField]
    private GameState[] GameStates;

    [SerializeField]
    private Transform stateObjectsRoot;

    public int gameState;

    public void SetGameState(int state)
    {
        if (gameState != state)
        {
            gameState = state;
            ClearState();
            LoadState(state);
            ResetCameraPosition();
        }
    }

    private void ClearState()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in stateObjectsRoot)
        {
            children.Add(child.gameObject);
        }
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
    }

    private void LoadState(int state)
    {
        if (state <= GameStates.Length)
        {
            foreach (GameObject Object in GameStates[state].Objects)
            {
                Instantiate(Object, stateObjectsRoot);
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraController>().TrackingPosition = player.transform;
            }

            
        }
        else { Debug.LogError("This Gamestate does not exist"); }


    }

    private void ResetCameraPosition()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = new Vector3(0,-11.9f,-13.7f);
    }
}