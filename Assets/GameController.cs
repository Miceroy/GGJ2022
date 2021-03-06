using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioSource WinSound;
    public AudioSource LoseSound;
    bool hasPlayed = false;
    void onWinGame()
    {
        if (!WinSound.isPlaying && !hasPlayed )
        {
            WinSound.Play();
            hasPlayed = true;

        }
        // TODO
    }

    void onLoseGame()
    {
        if (!LoseSound.isPlaying && !hasPlayed)
        {
            LoseSound.Play();
            hasPlayed = true;
        }
        // TODO
    }

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

    public void hitsLight(PlayerController player, LightDetector detector)
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
            } else
            {
            }
        }
    }

    float loseTimer;
    bool outOfArea;

    public void notHitsLight(PlayerController player, LightDetector detector)
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
            else
            {
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
            //item.direction = player.lastDelta.normalized * player.lastDelta.magnitude;
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

    void gameWinSceneload()
    {
        hasPlayed = false;
        if (GameResults.Instance)
        {
            ++GameResults.Instance.lastLevel;

            if(GameResults.Instance.lastLevel >= GameResults.Instance.getLevelCount())
            {
                GameResults.Instance.didWin = true;
                SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
            }
            else
            {
                string levels = GameResults.Instance.lastLevel.ToString() + "/" + SceneManager.sceneCountInBuildSettings.ToString();
                Debug.Log("Level passed. Loading next level: " + levels);
                SceneManager.LoadScene(GameResults.Instance.getLevelName());
            }
        }
        else
        {
            Debug.Log("Level passed! Reloading scene, because not started from main menu.");
            SceneManager.LoadScene(GameResults.Instance.getLevelName());
        }
    }

    private int numPlayersInGoal;
    private int activePlayerCharacter;

    /*GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Debug.AssertFormat(gos.Length > 0, "Did not found game objects of type: " + tag);
        return gos;
    }*/

    bool gameDone = false;

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
                    onWinGame();
                    if (gameDone == false)
                    {
                        gameDone = true;
                        Invoke("gameWinSceneload", 1.5f);
                    }
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
        if (GameResults.Instance)
        {
            GameResults.Instance.didWin = false;
        }
        numFrames = 0;
        loseTimer = 0;
        activePlayerCharacter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ++numFrames;
        if (numFrames == 2)
        {
            swapPlayerCharacter();
        }
        numPlayersInGoal = 0;
        if (outOfArea)
        {
            loseTimer += Time.deltaTime;
            Debug.Log("loseTimer: " + loseTimer.ToString());
            if (loseTimer > 0.2f)
            {
                players[activePlayerCharacter].enabled = false;
                onLoseGame();
                Invoke("gameLoseSceneload", 1.5f);                
            }
        }
        else
        {
            //Debug.Log("Reset lose timer: " + loseTimer.ToString());
            //loseTimer = 0;
        }
        outOfArea = false;
    }

    void gameLoseSceneload()
    {
        hasPlayed = false;
        if (GameResults.Instance)
        {
            Debug.Log("Game lose! Loading next scene.");
            GameResults.Instance.didWin = false;
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
            loseTimer = 0;
        }
        else
        {
            Debug.Log("Game lose! Reloading scene, because not started from main menu.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            loseTimer = 0;
        }
    }

    int numFrames;
}
