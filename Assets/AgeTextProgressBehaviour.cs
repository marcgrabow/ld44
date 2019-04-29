using System.Collections;
using System.Collections.Generic;
using _42.Events;
using Assets;
using TMPro;
using UnityEngine;

public class AgeTextProgressBehaviour : MonoBehaviour
{
    private TextMeshProUGUI txt;
    private int age;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        age=0;
        Game.Events.Register(this);
    }

    [EventListener(EventEnum.PlayerAged)]
    void Set(object addedAge)
    {
        age+=(int)addedAge;
        txt.text = "Age: "+age+"y";
    }
}
