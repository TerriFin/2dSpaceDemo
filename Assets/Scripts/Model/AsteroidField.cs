using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {
    public GameObject smallAsteroid;
    public GameObject mediumAsteroid;
    public GameObject largeAsteroid;

    public int maxSmallAsteroids;
    public int maxMediumAsteroids;
    public int maxLargeAsteroids;

    private int currentSmallAsteroids;
    private int currentMediumAsteroids;
    private int currentLargeAsteroids;

    public bool rectangular;
    public bool circural;

    public float rectangularWidth;
    public float rectangularHeight;
    public float circuralRadius;

    public float maxAsteroidSpawnTime;
    public float minAsteroidSpawnTime;
    private float asteroidSpawnTimer;

    private void Start() {
        if (rectangular) {
            while (currentSmallAsteroids < maxSmallAsteroids) {
                SpawnAsteroidInRandomPosRec(smallAsteroid);
                currentSmallAsteroids++;
            }

            while (currentMediumAsteroids < maxMediumAsteroids) {
                SpawnAsteroidInRandomPosRec(mediumAsteroid);
                currentMediumAsteroids++;
            }

            while (currentLargeAsteroids < maxLargeAsteroids) {
                SpawnAsteroidInRandomPosRec(largeAsteroid);
                currentLargeAsteroids++;
            }
        } else if (circural) {
            while (currentSmallAsteroids < maxSmallAsteroids) {
                SpawnAsteroidInRandomPosCirc(smallAsteroid);
                currentSmallAsteroids++;
            }

            while (currentMediumAsteroids < maxMediumAsteroids) {
                SpawnAsteroidInRandomPosCirc(mediumAsteroid);
                currentMediumAsteroids++;
            }

            while (currentLargeAsteroids < maxLargeAsteroids) {
                SpawnAsteroidInRandomPosCirc(largeAsteroid);
                currentLargeAsteroids++;
            }
        }

        asteroidSpawnTimer = Time.time + Random.Range(minAsteroidSpawnTime, maxAsteroidSpawnTime);
    }

    private void Update() {
        if (Time.time >= asteroidSpawnTimer) {
            asteroidSpawnTimer = Time.time + Random.Range(minAsteroidSpawnTime, maxAsteroidSpawnTime);

            float asteroidRandomiserNumber = Random.value;

            if (rectangular) {
                if (asteroidRandomiserNumber < 0.25f && currentLargeAsteroids < maxLargeAsteroids) {
                    SpawnAsteroidInRandomPosRec(largeAsteroid);
                    currentLargeAsteroids++;
                } else if (asteroidRandomiserNumber < 0.60f && currentMediumAsteroids < maxMediumAsteroids) {
                    SpawnAsteroidInRandomPosRec(mediumAsteroid);
                    currentMediumAsteroids++;
                } else if (currentSmallAsteroids < maxSmallAsteroids) {
                    SpawnAsteroidInRandomPosRec(smallAsteroid);
                    currentSmallAsteroids++;
                }
            } else if (circural) {
                if (asteroidRandomiserNumber < 0.25f && currentLargeAsteroids < maxLargeAsteroids) {
                    SpawnAsteroidInRandomPosCirc(largeAsteroid);
                    currentLargeAsteroids++;
                } else if (asteroidRandomiserNumber < 0.60f && currentMediumAsteroids < maxMediumAsteroids) {
                    SpawnAsteroidInRandomPosCirc(mediumAsteroid);
                    currentMediumAsteroids++;
                } else if (currentSmallAsteroids < maxSmallAsteroids) {
                    SpawnAsteroidInRandomPosCirc(smallAsteroid);
                    currentSmallAsteroids++;
                }
            }
        }
    }

    public void HandleAsteroidDestruction(Asteroid destroyed) {
        if (destroyed.smallAsteroid) {
            currentSmallAsteroids--;
        } else if (destroyed.mediumAsteroid) {
            currentMediumAsteroids--;
        } else if (destroyed.largeAsteroid) {
            currentLargeAsteroids--;
        }
    }

    private void SpawnAsteroidInRandomPosRec(GameObject asteroid) {
        GameObject spawnedAsteroid = Instantiate(asteroid);

        spawnedAsteroid.GetComponentInChildren<HealthBar>().ManuallyStart();

        spawnedAsteroid.transform.position = transform.position + GetRandomPosInRectangle();
        spawnedAsteroid.transform.Rotate(0, 0, Random.Range(0, 360));
        spawnedAsteroid.GetComponent<Asteroid>().field = this;
    }

    private Vector3 GetRandomPosInRectangle() {
        return new Vector2(Random.Range(-rectangularWidth, rectangularWidth), Random.Range(-rectangularHeight, rectangularHeight));
    }

    private void SpawnAsteroidInRandomPosCirc(GameObject asteroid) {
        GameObject spawnedAsteroid = Instantiate(asteroid);

        spawnedAsteroid.GetComponentInChildren<HealthBar>().ManuallyStart();

        spawnedAsteroid.transform.position = transform.position + GetRandomPosInCircle();
        spawnedAsteroid.transform.Rotate(0, 0, Random.Range(0, 360));
        spawnedAsteroid.GetComponent<Asteroid>().field = this;
    }

    private Vector3 GetRandomPosInCircle() {
        float randomCirclePoint = Random.Range(0, 360);

        float randomPointWidth = Mathf.Cos(randomCirclePoint);
        float randomPointHeight = Mathf.Sin(randomCirclePoint);

        float scalar = Random.Range(0, circuralRadius);

        return new Vector2(randomPointWidth * scalar, randomPointHeight * scalar);
    }
}
