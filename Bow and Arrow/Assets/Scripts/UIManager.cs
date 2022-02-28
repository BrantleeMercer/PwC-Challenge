using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _hitCounter;

    private void Start() 
    {
        _hitCounter.text = "Hits: 0";
    }
    
    public void UpdateHitCounter(int hits)
    {
        _hitCounter.text = $"Hits: {hits}";
    }
}
