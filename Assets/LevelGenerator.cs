using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._42Bytes;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public TextAsset LevelData;

    public PrefabConfig[] Prefabs;

    // Start is called before the first frame update
    void Start()
    {
        var text = LevelData.text.Split(new[]{System.Environment.NewLine}, new StringSplitOptions());
        var z = 0;
        var x = -13;
        foreach(var line in text) {
            Debug.Log(line);
            if(line.StartsWith("---")) break;
            foreach(var c in line) {

                if(!string.IsNullOrWhiteSpace(c+"")) {
                    var prefabConfig = Prefabs.FirstOrDefault(o=>o.Title==c+"");
                    if(prefabConfig!=null){
                        var prefab = prefabConfig.Prefabs.PickRandom();
                        Instantiate(prefab, new Vector3(x, 0.05f, z), prefab.transform.rotation);
                    }else{
                        Debug.LogWarning("couldnt find prefab for "+c);
                    }
                }

                x++;
            }
            x=-13;
            z+=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class PrefabConfig {
    public string Title;
    public GameObject[] Prefabs;
}