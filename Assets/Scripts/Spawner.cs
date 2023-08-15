using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

    //второй вариант создания и движения труб через корутину
    /*void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.3f);
            float rand = Random.Range(-1f, 4f);
            GameObject newPrefab = Instantiate(prefab, new Vector3(12, rand, 0), Quaternion.identity);
            Destroy(newPrefab, 10);
        }
    }
    public float speed; // скорость движения труб

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
    */
}
