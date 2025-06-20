using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelToolSpawner : MonoBehaviour
{
    [SerializeField]
    private List<ToolCollectible> toolCollectibles;


    [SerializeField]
    private List<Transform> toolLocationCandidates;

    private void Awake()
    {
        if (toolLocationCandidates.Count < toolCollectibles.Count)
            throw new NotSupportedException("Cannot spawn tool collectibles, not enough locations provided!");

        System.Random rng = new();
        List<Transform> shuffledLocations = toolLocationCandidates.OrderBy(_ => rng.Next()).ToList();

        for (int i = 0; i < toolCollectibles.Count; i++)
            toolCollectibles[i].transform.position = shuffledLocations[i].transform.position;
    }
}
