using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaveSpawner : MonoBehaviour
{
    [Header("Time Attribute")]
    [Tooltip("Initialize time")]
    [SerializeField] [Min(0f)] float countdown = 2f;
    [Tooltip("Time between each wave")]
    [SerializeField] [Min(0f)] float timeBetweenWaves = 5f;
    [Tooltip("Time between spawning each enemy in a wave")]
    [SerializeField] [Min(0f)]float spawnTimer = 1f;

    [SerializeField] TextMeshProUGUI timeCountdownText;

    [SerializeField] Transform startPosition;

    [HideInInspector] public static int EnemiesAlive = 0;
    [SerializeField] Wave[] waves;
 
    int waveIndex = 0;

    void Update()
    {
        HandleSpawn();
    }

    void HandleSpawn()
    {
        if (EnemiesAlive > 0) return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        textCountdown();
        countdown -= Time.deltaTime;
    }

    void SpawnEnemy(GameObject enemy)
    {
        enemy = ObjectPool.SharedInstance.GetPooledObject(enemy.gameObject.name);

        if (enemy == null) return;

        // Set position and rotation for starting
        enemy.transform.position = startPosition.position;
        enemy.transform.rotation = startPosition.rotation;

        enemy.SetActive(true);
        EnemiesAlive++;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(spawnTimer / wave.rate);
        }
        waveIndex++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("Won");
            this.enabled = false;
            yield return null;
        }
    }

    void textCountdown()
    {
        //string time = Mathf.Round(countdown).ToString();
        //timeCountdownText.text = $"Next Wave: {time}";
        timeCountdownText.text = $"Next Wave: {string.Format("{0:00.00}", countdown)}";
    }
}
