using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinCount = 20;
    [SerializeField] private Vector2 areaMin = new Vector2(-12, -8);
    [SerializeField] private Vector2 areaMax = new Vector2(12, 8);
    [SerializeField] private float height = 1f;

    private List<Coin> activeCoins = new List<Coin>();

    void Start()
    {
        SpawnAll();
    }

    void SpawnAll()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(areaMin.x, areaMax.x),
                height,
                Random.Range(areaMin.y, areaMax.y)
            );

            GameObject obj = Instantiate(coinPrefab, pos, Quaternion.identity);
            Coin coin = obj.GetComponent<Coin>();
            coin.OnCollected += HandleCollected;
            activeCoins.Add(coin);
        }
    }

    void HandleCollected(Coin coin)
    {
        if (activeCoins.Contains(coin))
        {
            activeCoins.Remove(coin);
            coin.OnCollected -= HandleCollected;
        }

        GameManager.Instance?.AddCoin();
    }
}