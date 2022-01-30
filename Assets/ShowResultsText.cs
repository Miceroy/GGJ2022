using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResultsText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameResults.Instance)
        {
            if (GameResults.Instance.didWin)
            {
                buttonText.gameObject.transform.parent.gameObject.SetActive(false);
                GetComponent<TMPro.TextMeshProUGUI>().text = "You win the game!";
                buttonText.text = "Return to main menu";
            } else {
                GetComponent<TMPro.TextMeshProUGUI>().text = "You lose the game!";
                buttonText.text = "Retry";
            }
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Game not started from main menu, so no results available.";
            buttonText.text = "Restart level";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
