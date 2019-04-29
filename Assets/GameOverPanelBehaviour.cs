using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanelBehaviour : MonoBehaviour
{

    public TextMeshProUGUI AgeTxt;
    
    public void ShowResult(int age) {
        AgeTxt.text = age + " years";
        gameObject.SetActive(true);
    }

}
