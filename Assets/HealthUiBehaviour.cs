using _42.Events;
using System.Text;
using TMPro;
using UnityEngine;

public class HealthUiBehaviour : MonoBehaviour {

    public enum IconEnum
    {
        Heart,
        Flash,
        Food,
        Water,
        Bullet,
        Age
    }

    public IconEnum Icon = IconEnum.Heart;

    public string ActiveTag = "<alpha=#FF>";
    public string InactiveTag = "<alpha=#66>";
    public string KilledTag = "<color=#FFFF0000>";
    public string Spacer = " ";
    public int MaxPoints = 5;
    public int CurrentPoints = 3;
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

    // [EventListener(Assets.EventEnum.BulletFired)]
    // void SetA () {
    //     if(Icon!=IconEnum.Bullet) return;
    //     CurrentPoints--;
    //     UpdateUi();
	// }

    int age;

    [EventListener(Assets.EventEnum.PlayerLifeRemoved)]
    void SetB (object addedAge) {
        if(Icon!=IconEnum.Age) return;
        age+=(int)addedAge;

        var t = (float)age/(float)MaxPoints;
        CurrentPoints = Mathf.CeilToInt(t);
        Debug.Log(age + " " + t + " " + CurrentPoints);
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
            txt.Append(InactiveTag);
            txt.Append(_icon);
            txt.Append(Spacer);
        }
        _txt.SetText(txt);
    }

    string GetUnicodeIcon()
    {
        switch (Icon)
        {
            case IconEnum.Heart: return "\uf004";
            case IconEnum.Flash: return "\uf0e7";
            case IconEnum.Food: return "\uf2e7";
            case IconEnum.Water: return "\uf043";
            case IconEnum.Bullet: return "I";
            case IconEnum.Age: return "I";
        }
        return "unknown";
    }

}
