using UnityEngine;

public class IslandView : MonoBehaviour
{
    [SerializeField] private Transform slotTop;

    [SerializeField] private Transform slotBottom;

    [SerializeField] private Transform slotLeft;

    [SerializeField] private Transform slotRight;

    public Transform playerSpawn;

    public Transform finishSpawn;

    private int nextSlot = 0;

    public Transform GetSlotTowards(
        Vector2 direction)
    {
        direction.Normalize();

        if (Mathf.Abs(direction.x)
            > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                return slotRight;
            }
            else
            {
                return slotLeft;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return slotTop;
            }
            else
            {
                return slotBottom;
            }
        }
    }
}