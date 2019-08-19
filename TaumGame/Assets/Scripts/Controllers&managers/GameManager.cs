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

    [SerializeField]
    private GameState[] GameStates;

    [SerializeField]
    private GameState[] BattleStates;

    [SerializeField]
    private Transform stateObjectsRoot;
    [SerializeField]
    private Transform battleObjectsRoot;

    private int currentGameState;
    private int currentBattleState;
    private bool inBattle;

    //public functions
    public void SetGameState(int state)
    {
        if (currentGameState != state)
        {
            currentGameState = state;
            destroyChildren(stateObjectsRoot);
            LoadState(state, GameStates, stateObjectsRoot);
            ResetCameraPosition();
        }
    }

    public void SetBattleState(int state)
    {
        if (currentBattleState != state)
        {
            currentGameState = state;
            destroyChildren(battleObjectsRoot);
            LoadState(state, BattleStates, battleObjectsRoot);
            ResetCameraPosition();
        }
    }

    public void StartBattle()
    {
        if (inBattle == false)
        {
            inBattle = true;
            //fancy graphical transition
            stateObjectsRoot.gameObject.SetActive(false);
            battleObjectsRoot.gameObject.SetActive(true);
        }
    }

    public void EndBattle()
    {
        if (inBattle)
        {
            inBattle = false;
            //fancy graphical transition
            stateObjectsRoot.gameObject.SetActive(true);
            battleObjectsRoot.gameObject.SetActive(false);
        }
    }

    //support functions
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

    private void LoadState(int stateInd, GameState[] States, Transform root) //replaces all children under the root object with the objects with thoose stored in the specified gamestate
    {
        if (stateInd <= States.Length)
        {
            foreach (GameObject Object in States[stateInd].Objects)
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

    private void SetCameraTarget(Transform target) //resets camera target
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraController>().TrackingPosition = target;
    }

    private void ResetCameraPosition()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = new Vector3(0,-11.9f,-13.7f);
    }
}