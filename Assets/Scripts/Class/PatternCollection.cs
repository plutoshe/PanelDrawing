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
    public void Set(PatternCollection p)
    {
        patterns.Clear();
        foreach (var kvs in p.patterns)
        {
            var newPattern = new PatternItemInCollection();
            newPattern.Set(kvs.Value);
            patterns.Add(kvs.Key, newPattern);
        }
    }

    void Start()
    {
        
    }
}