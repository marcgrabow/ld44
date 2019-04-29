using System.Collections;
using System.Collections.Generic;
using _42.Events;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public GameOverPanelBehaviour GameOverPanel;

    IEnumerator Start()
    {
        yield return null;
        //yield return new WaitForSeconds(1);
        Game.Events.Register(this);
        //Game.Events.Post(this, EventEnum.PlayerAged, 40);   
        //Game.Events.Post(this, EventEnum.BoughtWeapon, 0);   
        //Game.Events.Post(this, EventEnum.TextEvent, "hello world");   
    }

    [EventListener(EventEnum.PlayerDied)]
    void OnPlayerDied(object age){
        StartCoroutine(DelayedPanel((int)age));
    }

    IEnumerator DelayedPanel(int age){
        yield return new WaitForSeconds(2);
        GameOverPanel.ShowResult(age);
    }

    public void TryAgainClick(){
        SceneManager.LoadScene(0);
    }

    public void TwitterClick(){
        Application.OpenURL("https://twitter.com/MarcGrabow");
    }
}
