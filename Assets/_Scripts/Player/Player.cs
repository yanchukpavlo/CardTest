using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeckHolder))]
public class Player : MonoBehaviour
{    
    public ushort currency { get; private set; }
    public DeckHolder deckHolder { get; private set; }
    

    private void Awake()
    {
        Setup();
    }

    void Setup()
    {
        deckHolder = GetComponent<DeckHolder>();
        deckHolder.Setup();        
        currency = 15;
    }
}
