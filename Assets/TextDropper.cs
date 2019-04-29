using System.Collections;
using System.Collections.Generic;
using _42.Events;
using Assets;
using TMPro;
using UnityEngine;

public class TextDropper : MonoBehaviour
{

    public GameObject Prefab;
    public Transform SpawnTarget;
    public Vector3 SpawnOffset;

    [Multiline]
    public string StartText;

    // Start is called before the first frame update
    void Start()
    {
        Game.Events.Register(this);    

        var go = Instantiate(Prefab, transform.position, Prefab.transform.rotation);
        go.GetComponentInChildren<TextMeshPro>().text = StartText;
    }

    [EventListener(EventEnum.TextEvent)]
    void OnTextEventHappened(object txt)
    {
        var go = Instantiate(Prefab, SpawnTarget.position+SpawnOffset, Prefab.transform.rotation);
        go.GetComponentInChildren<TextMeshPro>().text = txt+"";
    }

    [EventListener(EventEnum.BoughtWeapon)]
    void OnBoughtWeapon()
    {
        OnTextEventHappened("lost 5 years by buying a weapon");
    }

    
}
