using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float timeCost = 5f;

    private bool playerInside;

    private LevelTimer timer;

    [SerializeField] private TMP_Text costLabel;

    void Start()
    {
        timer =
            FindFirstObjectByType<LevelTimer>();
    }

    void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject player =
                GameObject.FindGameObjectWithTag(
                    "Player");

            player.transform.position =
                target.position;

            timer.SpendTime(timeCost);
        }
    }

    void OnTriggerEnter2D(
        Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(
        Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    public void Setup(Transform targetPortal, int cost)
    {
        target = targetPortal;

        timeCost = cost;

        costLabel.text =
            cost.ToString();
    }
}