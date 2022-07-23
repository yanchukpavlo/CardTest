using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class PointerLine : MonoBehaviour
{
    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, EventSystem.current.currentSelectedGameObject.transform.position);
        line.SetPosition(1, MousePos());
        line.enabled = true;
    }
    
    private void Update()
    {
        line.SetPosition(1, MousePos());
    }

    Vector3 MousePos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
