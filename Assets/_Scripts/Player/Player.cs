using UnityEngine;
using UnityEngine.EventSystems;
using System;

public enum ActionState : byte
{
    None,
    Update,
    Destroy
}

[RequireComponent(typeof(DeckHolder))]
public class Player : MonoBehaviour
{
    public static Action<ActionState> OnChangeState;
    public static Action<int> OnChangeCurrency;
    public static ActionState ActionState { get; private set; } = ActionState.None;
    public static int Currency { get; private set; }

    [SerializeField] PointerLine pointerLine;

    PointerLine line = null;
    bool block;

    public DeckHolder deckHolder { get; private set; }

    public static ActionState SetActionState { set { ActionState = value; } }

    public static void ChangeCurrency(int amount)
    {
        Currency += amount;
        OnChangeCurrency?.Invoke(Currency);
    }

    void ChangeState(ActionState newState)
    {
        ActionState = newState;
        OnChangeState?.Invoke(ActionState);
        Debug.Log("New player state - " + newState);

        if (line)
        {
            Destroy(line.gameObject);
            line = null;
        }
        
        switch (newState)
        {
            case ActionState.None:

                break;

            case ActionState.Update:
                line = Instantiate(pointerLine);
                break;

            case ActionState.Destroy:
                line = Instantiate(pointerLine);
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), newState);
                break;
        }
    }

    //button event
    public void StateUpdate(bool isBlock)
    {
        block = isBlock;
        ChangeState(ActionState.Update);
    }

    //button event
    public void StateDestroy(bool isBlock)
    {
        block = isBlock;
        ChangeState(ActionState.Destroy);
    }

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (block && EventSystem.current.IsPointerOverGameObject()) block = false;
            else ChangeState(ActionState.None);
        }
    }

    void Setup()
    {
        deckHolder = GetComponent<DeckHolder>();
        var d = deckHolder.Setup();
        d.OnCardDestroyed += CardDestroyed;
        d.OnCardUpgraded += CardUpgraded;

        ChangeCurrency(15);
    }

    void CardUpgraded(byte cost)
    {
        ChangeState(ActionState.None);
        ChangeCurrency(cost * -1);
    }

    void CardDestroyed(byte amount)
    {
        ChangeState(ActionState.None);
        ChangeCurrency(amount);
    }
}
