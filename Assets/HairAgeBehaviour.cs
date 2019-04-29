using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HairAgeBehaviour : MonoBehaviour
{

    public Transform Mask;
    private Vector3 origScale;
    private int age;
    public int MaxAge = 100;
    public int AgeToGrow = 5;
    public int AgeToWhite = 60;
    public int AgeToFade = 80;
    // Start is called before the first frame update
    void Start()
    {
        origScale = Mask.transform.localScale;   
        Mask.transform.DOScaleY(0,0);
        InvokeRepeating("AddAge", 1, 0.21f);
    }

    // Update is called once per frame
    void AddAge()
    {
        age++;

        if(age>AgeToFade){
            //todo later if bored
        }
        var lerp = Mathf.InverseLerp(0, MaxAge, age);
        var lerp2 = Mathf.Lerp(0, origScale.y, lerp);
        Debug.Log("age "+age+" lerp "+lerp+ " lerp2 "+lerp2);
        Mask.transform.DOScaleY(lerp2, 0.25f);
    }
}
