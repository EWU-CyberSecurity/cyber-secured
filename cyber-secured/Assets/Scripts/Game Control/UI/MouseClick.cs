using UnityEngine;

public class MouseClick : MonoBehaviour
{
    void OnMouseDown()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
