using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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

    GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Debug.AssertFormat(gos.Length > 0, "Did not found game objects of type: " + tag);
        return gos;
    }

    List<PlayerController> players = new List<PlayerController>();

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
                    action.effect.act();
                }
            }
        }
    }

    public void register(PlayerController player)
    {
        players.Add(player);
    }


    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        /*GameObject[] gos = getGameObjects("Player");
        foreach(GameObject go in gos)
        {
            PlayerController player = go.GetComponent<PlayerController>();
            Debug.AssertFormat(player, "No PlayerController in object, which is tagget to Player");
            players.Add(player);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        numPlayersInGoal = 0;
    }
}
