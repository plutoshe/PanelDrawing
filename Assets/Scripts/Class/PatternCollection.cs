using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PatternCollection
{
    public Dictionary<int, PatternItemInCollection> patterns;

    public PatternCollection()
    {
        patterns = new Dictionary<int, PatternItemInCollection>();
    }

    void Start()
    {
        
    }
}