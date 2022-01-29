using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResultsText : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<TMPro.TextMeshProUGUI>().text = GameResults.Instance.didWin ? "ShowResultsText.cs: Win!" : "ShowResultsText.cs: Lose!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
