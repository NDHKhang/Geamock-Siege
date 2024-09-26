using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float scrollSpeed = 10f;
    [SerializeField] float minY = 40f;
    [SerializeField] float maxY = 120f;

    //[SerializeField] float maxX = ;
    //[SerializeField] float maxY

    [SerializeField] float minX = -40f;
    [SerializeField] float maxX = 90f;
    [SerializeField] float minZ = -50f;
    [SerializeField] float maxZ = 50f;

    float zoomFactor;

    void Update()
    {
        MoveCamera();        
    }

    void MoveCamera()
    {
        //if (gameOver)
        //{
        //    this.enabled = false;
        //    return;
        //}

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // factor based on zoom level
        zoomFactor = 0.4f + (1 - (pos.y - minY) / (maxY - minY));


        pos.x = Mathf.Clamp(pos.x, minX * zoomFactor, maxX * zoomFactor);
        pos.z = Mathf.Clamp(pos.z, minZ * zoomFactor, maxZ * zoomFactor);

        // Apply new position
        transform.position = pos;
    }
}
