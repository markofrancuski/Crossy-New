using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Value", menuName = "Bool Value")]
public class BoolValue : ScriptableObject
{
    public string str = "bool";
    public bool statValue;

    private void OnEnable() 
    {
        if(PlayerPrefs.GetString(str) != null)
        {
            string Str = PlayerPrefs.GetString(str);
            if(Str == "True") statValue = true;
            else statValue = false;
        }
        
    }

    private void OnDisable() 
    {
        PlayerPrefs.SetString(str, statValue.ToString());
    }
}
