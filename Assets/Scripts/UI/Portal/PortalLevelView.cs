using UnityEngine;
using UnityEngine.UI;
using System;

public class PortalLevelView : MonoBehaviour
{
    [SerializeField] private GameObject islandPrefab;

    [SerializeField] private GameObject portalPrefab;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject finishPrefab;

    [SerializeField] private Transform worldRoot;

    private IslandView[] islandViews;

    public Button startButton;

    public event Action FinishReached;

    public PlayerMovement Build(PortalLevelData data)
    {
        ClearWorld();

        startButton.gameObject.SetActive(true);

        // ----- ISLANDS -----

        islandViews =
            new IslandView[
                data.nodes.Count];

        for (int i = 0;
            i < data.nodes.Count;
            i++)
        {
            PortalNodeData node =
                data.nodes[i];

            GameObject island =
                Instantiate(
                    islandPrefab,
                    node.position,
                    Quaternion.identity,
                    worldRoot);

            islandViews[i] =
                island.GetComponent<
                    IslandView>();
        }

        // ----- PORTALS -----

        foreach (PortalEdgeData edge
            in data.edges)
        {
            CreatePortalPair(edge);
        }

        // ----- PLAYER -----

        Vector2 startPos =
            data.nodes[
                data.startNode]
                .position;

        GameObject playerObj = Instantiate(
            playerPrefab,
            islandViews[
                data.startNode]
                .playerSpawn.position,
            Quaternion.identity,
            worldRoot);

        PlayerMovement player = playerObj.GetComponent<PlayerMovement>();
        // ----- FINISH -----

        Vector2 finishPos =
            data.nodes[
                data.finishNode]
                .position;

        GameObject finish = Instantiate(
            finishPrefab,
            islandViews[
                data.finishNode]
                .finishSpawn.position,
            Quaternion.identity,
            worldRoot);

        FinishZone finishZone =
            finish.GetComponent<FinishZone>();

        finishZone.OnPlayerEntered += HandleFinishReached;

        return player;
    }

    void ClearWorld()
    {
        foreach (Transform child
            in worldRoot)
        {
            Destroy(child.gameObject);
        }
    }

    void CreatePortalPair(PortalEdgeData edge)
    {
        IslandView fromIsland =
            islandViews[edge.from];

        IslandView toIsland =
            islandViews[edge.to];

        Vector2 dir =
            toIsland.transform.position
            - fromIsland.transform.position;

        Transform fromSlot =
            fromIsland.GetSlotTowards(
                dir);

        Transform toSlot =
            toIsland.GetSlotTowards(
                -dir);

        GameObject a =
            Instantiate(
                portalPrefab,
                fromSlot.position,
                Quaternion.identity,
                worldRoot);

        GameObject b =
            Instantiate(
                portalPrefab,
                toSlot.position,
                Quaternion.identity,
                worldRoot);

        Portal pa =
            a.GetComponent<Portal>();

        Portal pb =
            b.GetComponent<Portal>();

        pa.Setup(
            b.transform,
            edge.cost);

        pb.Setup(
            a.transform,
            edge.cost);

        ApplyPortalVisual(
            a,
            edge.type);

        ApplyPortalVisual(
            b,
            edge.type);
    }

    void ApplyPortalVisual(GameObject portal, PortalType type)
    {
        SpriteRenderer sr = portal.GetComponent<SpriteRenderer>();

        switch (type)
        {
            case PortalType.Fast:
                sr.color = Color.green;
                break;

            case PortalType.Medium:
                sr.color = Color.yellow;
                break;

            case PortalType.Slow:
                sr.color = Color.black;
                break;
        }
    }

    void HandleFinishReached()
    {
        FinishReached?.Invoke();
    }
}