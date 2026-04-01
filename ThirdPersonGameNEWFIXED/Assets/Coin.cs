using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float floatAmplitude = 0.3f;
    [SerializeField] private float floatFrequency = 2f;

    private Vector3 startPosition;
    private Coroutine floatCoroutine;
    private Coroutine rotateCoroutine;

    public System.Action<Coin> OnCollected;

    void Start()
    {
        startPosition = transform.position;
        StartAnimations();
        Debug.Log("Монетка создана, анимации запущены"); 
    }

    void StartAnimations()
    {
        floatCoroutine = StartCoroutine(FloatingAnimation());
        rotateCoroutine = StartCoroutine(RotationAnimation());
    }

    IEnumerator FloatingAnimation()
    {
        while (true)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
            yield return null;
        }
    }

    IEnumerator RotationAnimation()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Collect()
    {
        Debug.Log("Collect вызван!"); 

        if (floatCoroutine != null) StopCoroutine(floatCoroutine);
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);

        StartCoroutine(CollectAnimation());
    }

    IEnumerator CollectAnimation()
    {
        float elapsed = 0;
        Vector3 originalScale = transform.localScale;

        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.3f;
            transform.localScale = originalScale * (1 - t);
            yield return null;
        }

        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
}