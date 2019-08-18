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
    private GameState[] BattleStates;

    [SerializeField]
    private Transform stateObjectsRoot;
    [SerializeField]
    private Transform battleObjectsRoot;

    public int gameState;

    public void SetGameState(int state)
    {
        if (gameState != state)
        {
            gameState = state;
            destroyChildren(stateObjectsRoot);
            LoadState(state, GameStates, stateObjectsRoot);
            ResetCameraPosition();
        }
    }

    private void destroyChildren(Transform obect)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in obect)
        {
            children.Add(child.gameObject);
        }
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
    }

    private void LoadState(int state, GameState[] gameStates, Transform root)
    {
        if (state <= gameStates.Length)
        {
            foreach (GameObject Object in gameStates[state].Objects)
            {
                Instantiate(Object, root);
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                SetCameraTarget(player.transform);
            }            
        }
        else { Debug.LogError("This Gamestate does not exist"); }
    }

    private void SetCameraTarget(Transform target)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraController>().TrackingPosition = target;
    }

    private void ResetCameraPosition()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = new Vector3(0,-11.9f,-13.7f);
    }
}