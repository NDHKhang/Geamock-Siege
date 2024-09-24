using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    [SerializeField] Vector3 positionOffset;

    WaveSpawner waveSpawner;

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        waveSpawner = GetComponent<WaveSpawner>();
    }

    void OnEnable()
    {
        FindPath();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        // Clear path if exist then add new path
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();

            if (waypoint != null) 
                path.Add(child.GetComponent<Tile>());
        }
    }

    void SpawnFromStart()
    {
        //transform.position = path[0].transform.position + positionOffset;
    }

    void FinishPath()
    {
        enemy.LostGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            endPos += positionOffset;
            float travelPercent = 0f;

            float rotationDegrees = degreesToRotate(transform.forward, startPos, endPos);
            // Change direction base on path

            while(travelPercent < 1f)
            {
                if (travelPercent < 0.25f)
                {
                    // mutiple by 4 to rotate it within the 1/4 between 2 waypoints
                    float rotationAngle = 4 * rotationDegrees * Time.deltaTime * speed;
                    transform.Rotate(new Vector3(0, rotationAngle, 0));
                }
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    // Get the signed angle between 2 vector current director and target director
    float degreesToRotate(Vector3 currentDir, Vector3 currentPos, Vector3 endPos)
    {
        Vector3 targetDir = endPos - currentPos;
        return Vector3.SignedAngle(currentDir, targetDir, Vector3.up);
    }
}
