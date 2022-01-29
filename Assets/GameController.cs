using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object of type: " + tag);
        return go;
    }

    GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Debug.AssertFormat(gos.Length > 0, "Did not found game object of type: " + tag);
        return gos;
    }

    public void playerHitsLight(PlayerController player, LightDetector detector)
    {
        if(player.lightState == PlayerController.LightState.Unknown)
        {
            player.lightState = PlayerController.LightState.InLight;
        }
        else
        {
            if (player.lightState == PlayerController.LightState.InShadow)
            {
                Debug.Log("WARNING: Character going in light: " + player.gameObject.name);
                outOfArea = true;
            }
        }
    }

    float loseTimer;
    bool outOfArea;

    public void playerNotHitsLight(PlayerController player, LightDetector detector)
    {
        if (player.lightState == PlayerController.LightState.Unknown)
        {
            player.lightState = PlayerController.LightState.InShadow;
        }
        else
        {
            if (player.lightState == PlayerController.LightState.InLight)
            {
                Debug.Log("WARNING: Character going in shawod: " + player.gameObject.name);
                outOfArea = true;
            }
        }
    }

    public void itemCollisionEnter(PushableItemController item, GameObject go)
    {
        PlayerController player = go.GetComponent<PlayerController>();
        if (player)
        {
            item.direction = player.lastDelta;
            item.direction.y = 0;
            item.direction.Normalize();
        }
    }

    public void itemCollisionExit(PushableItemController item, GameObject go)
    {
    }

    public void swapPlayerCharacter()
    {
        Cinemachine.CinemachineFreeLook cam = getGameObject("CinematicCamera").GetComponent<Cinemachine.CinemachineFreeLook>();
        players[activePlayerCharacter].enabled = false;
        activePlayerCharacter = (activePlayerCharacter + 1) % players.Count;
        players[activePlayerCharacter].enabled = true;
        cam.Follow = players[activePlayerCharacter].transform;
        cam.LookAt = players[activePlayerCharacter].transform;
    }

    void levelPassed()
    {
        if (GameResults.Instance)
        {
            ++sceneIndex;
            string levels = sceneIndex.ToString() + "/" + SceneManager.sceneCountInBuildSettings.ToString();
            if (sceneIndex < (SceneManager.sceneCountInBuildSettings - 1))
            {
                Debug.Log("Level passed. Loading next level: " + levels);
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.Log("Game WIN!");
                sceneIndex = SceneManager.sceneCountInBuildSettings - 1;
                GameResults.Instance.didWin = true;
                SceneManager.LoadScene(sceneIndex);
            }
        }
        else
        {
            Debug.Log("Level passed! Reloading scene, because not started from main menu.");
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private int numPlayersInGoal;
    private int sceneIndex;
    private int activePlayerCharacter;

    /*GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Debug.AssertFormat(gos.Length > 0, "Did not found game objects of type: " + tag);
        return gos;
    }*/

    List<PlayerController> players;

    public void checkPlayerNearGoal(GoalHandler goal)
    {
        Vector3 pos = goal.transform.position;
        float radius = 1;

        foreach (PlayerController player in players)
        {
            Vector3 playerPos = player.transform.position;
            if ((playerPos - pos).magnitude < radius)
            {
                Debug.Log("PlayerInGoal");
                ++numPlayersInGoal;
                if(numPlayersInGoal == players.Count)
                {
                    levelPassed();
                }
            }
        }
    }

    public void checkPlayerNearAction(InteractController action)
    {
        Vector3 pos = action.transform.position;
        float radius = action.radius;

        foreach (PlayerController player in players)
        {
            Vector3 playerPos = player.transform.position;
            if ((playerPos - pos).magnitude < radius)
            {
                if (player.isMakingAction())
                {
                    action.actAll();
                }
            }
        }
    }

    public void register(PlayerController player)
    {
        if(players == null)
        {
            players = new List<PlayerController>();
        }
        player.enabled = false;
        players.Add(player);
        players[activePlayerCharacter].enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        loseTimer = 0;
        activePlayerCharacter = 0;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        numPlayersInGoal = 0;
        if (outOfArea)
        {
            loseTimer += Time.deltaTime;
            Debug.Log("loseTimer: " + loseTimer.ToString());
            if (loseTimer > 0.1f)
            {
                if (GameResults.Instance)
                {
                    Debug.Log("Game lose! Loading next scene.");
                    sceneIndex = SceneManager.sceneCountInBuildSettings - 1;
                    GameResults.Instance.didWin = false;
                    SceneManager.LoadScene(sceneIndex);
                    loseTimer = 0;
                }
                else
                {
                    Debug.Log("Game lose! Reloading scene, because not started from main menu.");
                    SceneManager.LoadScene(sceneIndex);
                    loseTimer = 0;
                }
            }
        }
        else
        {
            //Debug.Log("Reset lose timer: " + loseTimer.ToString());
            //loseTimer = 0;
        }
        outOfArea = false;
    }
}
