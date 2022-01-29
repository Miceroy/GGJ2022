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
            GetComponent<TMPro.TextMeshProUGUI>().text = GameResults.Instance.didWin ? "ShowResultsText.cs: Win!" : "ShowResultsText.cs: Lose!";
        } else
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
