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
        // Only spawn more enemy if current wave is done
        if (EnemiesAlive > 0) return;

        // Win level if survive after number of waves
        if (waveIndex == waves.Length)
        {
            GameManager.instance.Win();
            this.enabled = false;
        }

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
        PlayerStats.instance.rounds++;

        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;  
    }

    void textCountdown()
    {
        timeCountdownText.text = $"Wave {PlayerStats.instance.rounds}/{waves.Length}: {string.Format("{0:00.00}", countdown)}";
    }
}
