using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI;

public class LevelCombiner : MonoBehaviour
{
    [SerializeField] private Tunnel[] tunnels;
    [SerializeField] private Location[] locations;
    [SerializeField] private int locationCount;
    [SerializeField] private Transform startPos;

    public List<LevelPart> partsLocation = new List<LevelPart>();
    public List<LevelPart> partsTunnels = new List<LevelPart>();

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        for (int i = 0; i < locationCount; i++)
        {
            yield return new WaitForSeconds(1);

            Location newLocation = Instantiate(locations[Random.Range(0, locations.Length)]);

            yield return PartLeveling(newLocation, partsTunnels[i].outPoint);

            partsLocation.Add(newLocation);

            yield return new WaitForSeconds(1);

            Tunnel newTunnel = Instantiate(tunnels[Random.Range(0, tunnels.Length)]);
            yield return PartLeveling(newTunnel, newLocation.outPoint);

            partsTunnels.Add(newTunnel); 
        }
    }

    private YieldInstruction PartLeveling(LevelPart levelPart, Transform outPoint)
    {
        levelPart.transform.position = outPoint.position;

        return null;
    }
}
