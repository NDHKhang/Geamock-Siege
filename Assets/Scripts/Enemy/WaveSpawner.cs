using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaveSpawner : MonoBehaviour
{
    [Header("Time Attribute")]
    [Tooltip("Initialize time")]
    [SerializeField] [Min(0f)] float countdown = 2f;
    [SerializeField] [Min(0f)] float timeBetweenWaves = 5f;
    [Tooltip("Time between spawning each enemy in a wave")]
    [SerializeField] [Min(0f)]float spawnTimer = 1f;

    [SerializeField] TextMeshProUGUI timeCountdownText;

    [SerializeField] Transform startPosition;

    public Transform StartPostition {  get { return startPosition; } }
 
    int waveIndex = 0;
    bool isWaveDone = true;

    void Update()
    {
        HandleSpawn();
    }

    void HandleSpawn()
    {
        if (countdown <= 0f)
        {
            countdown = timeBetweenWaves;
            isWaveDone = false;
            StartCoroutine(SpawnWave());
        }

        if (isWaveDone)
            countdown -= Time.deltaTime;

        textCountdown();
    }

    void SpawnEnemy()
    {
        GameObject enemy = ObjectPool.SharedInstance.GetPooledObject();

        // Set position and rotation for starting
        enemy.transform.position = startPosition.position;
        enemy.transform.rotation = startPosition.rotation;

        if (enemy != null)
            enemy.SetActive(true);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }
        isWaveDone = true;
    }

    void textCountdown()
    {
        string time = Mathf.Round(countdown).ToString();
        timeCountdownText.text = $"Next Wave: {time}";
    }
}
