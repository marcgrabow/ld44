using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventFireButtonBehaviour : MonoBehaviour {
    
    public EventEnum Event;
    public bool UseEventNameAsBtnText = true;

    private void Awake()
    {
        if (UseEventNameAsBtnText)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = Event + "";
        }
    }

    public void Fire () {
        Game.Events.Post(this, Event);
	}
	
}
