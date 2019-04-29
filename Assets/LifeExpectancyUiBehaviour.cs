using _42.Events;
using Assets;
using System.Text;
using TMPro;
using UnityEngine;

public class LifeExpectancyUiBehaviour : MonoBehaviour {

 string ActiveTag = "<alpha=#FF>";
 string InactiveTag = "<alpha=#66>";
 string KilledTag = "<color=#990000>";
    public string Spacer = "";
    public int MaxPoints = 100;
    public int CurrentPoints = 0;
    public int KilledPoints = 0;
    private TextMeshProUGUI _txt;
    private string _icon;

    void Awake () {
        Game.Events.Register(this);
        _txt = GetComponent<TextMeshProUGUI>();
        _icon = GetUnicodeIcon();
	}

    private void Start()
    {
        UpdateUi();
    }

    [EventListener(Assets.EventEnum.PlayerLifeRemoved)]
    void OnRemoveLife (object removedYears) {
        KilledPoints+=(int)removedYears;
        UpdateUi();
	}

    [EventListener(Assets.EventEnum.PlayerGainedLife)]
    void OnAddLife (object addedYears) {
        MaxPoints+=(int)addedYears;
        UpdateUi();
	}

    [EventListener(Assets.EventEnum.PlayerAged)]
    void OnAged (object addedYears) {
        CurrentPoints+=(int)addedYears;
        UpdateUi();
	}

    public void UpdateUi()
    {
        var txt = new StringBuilder();
        for (int i = 0; i < CurrentPoints; i++)
        {
            txt.Append(ActiveTag);
            txt.Append(_icon);
            txt.Append(Spacer);
        }
        for (int i = CurrentPoints; i < MaxPoints; i++)
        {
            var killedPoint = (i >= MaxPoints - KilledPoints);
            txt.Append(killedPoint ? KilledTag : InactiveTag);
            txt.Append(_icon);
            txt.Append(Spacer);
        }
        _txt.SetText(txt);
        CheckIfIShouldBeDead();
    }

    bool dead;

    public void CheckIfIShouldBeDead(){
        if(!dead && CurrentPoints>=MaxPoints-KilledPoints){
            dead =true;
            Game.Events.Post(this, EventEnum.PlayerDied, CurrentPoints);
        }
    }

    string GetUnicodeIcon()
    {
        return "I";
    }

}
