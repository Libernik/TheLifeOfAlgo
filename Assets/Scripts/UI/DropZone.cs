using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public PersonItem currentPerson;

    public void OnDrop(PointerEventData eventData)
    {
        var person = eventData.pointerDrag.GetComponent<PersonItem>();

        if (person != null)
        {
            if (currentPerson != null)
            {
                currentPerson.transform.SetParent(currentPerson.startParent);
                currentPerson.transform.localPosition = Vector3.zero;
            }

            currentPerson = person;

            person.transform.SetParent(transform);
            person.transform.localPosition = Vector3.zero;
        }
    }
}