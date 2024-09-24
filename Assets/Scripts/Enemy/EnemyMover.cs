using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // Movement speed
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();
    
    [SerializeField] Vector3 positionOffset;

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void Awake()
    {
        enemy = GetComponent<Enemy>();

        gridManager = GridManager.instance;
        pathFinder = FindAnyObjectByType<PathFinder>();
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
        
        if (pathFinder != null) 
            path = pathFinder.GetPath();
    }

    void FinishPath()
    {
        enemy.LostGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            endPos += positionOffset;

            // Handle smooth rotating and moving
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
