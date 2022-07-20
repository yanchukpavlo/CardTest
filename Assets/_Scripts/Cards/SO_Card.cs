using UnityEngine;

[CreateAssetMenu(fileName = "Card name", menuName = "ScriptableObjects/Card", order = 1)]
public class SO_Card : ScriptableObject
{
    [field: SerializeField] public Card card { get; private set; }
}
