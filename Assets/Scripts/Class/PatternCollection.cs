using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatternCollection : MonoBehaviour
{
    public List<PatternItemInCollection> patterns;

    void Start()
    {
        patterns = new List<PatternItemInCollection>();
    }
}