using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerActionState : byte
{
    None,
    Update,
    Destroy
}

[RequireComponent(typeof(DeckHolder))]
public class Player : MonoBehaviour
{
    public static Action<PlayerActionState> OnChangeState;
    public static PlayerActionState PlayerActionState { get; private set; } = PlayerActionState.None;
    public DeckHolder deckHolder { get; private set; }

    bool block;

    public static void ChangeState(PlayerActionState newState)
    {
        PlayerActionState = newState;
        OnChangeState?.Invoke(PlayerActionState);
        Debug.Log("New player state - " + newState);
    }

    public void StateUpdate(bool isBlock)
    {
        if (isBlock)
        {
            block = true;
            Debug.Log("Set Block");
        }
        ChangeState(PlayerActionState.Update);
    }

    public void StateDestroy(bool isBlock)
    {
        block = isBlock;
        ChangeState(PlayerActionState.Destroy);
    }

    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (block) block = false;
            else ChangeState(PlayerActionState.None);
        }
    }

    void Setup()
    {
        deckHolder = GetComponent<DeckHolder>();
        deckHolder.Setup(15);
    }
}
