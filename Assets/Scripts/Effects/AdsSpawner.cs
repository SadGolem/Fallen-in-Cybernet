using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsSpawner : MonoBehaviour
{
    public List<Sprite> adsToSpawn;
    public GameObject prefab;
    public Canvas canvas;

    private Ad ad;
    private void Start()
    {
        StartCoroutine(SpawnObjectsOverTime(10f)); // Вызываем метод, который спавнит объекты в течение 10 секунд
    }

    private IEnumerator SpawnObjectsOverTime(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            SpawnObject();
            yield return new WaitForSeconds(0.5f);
            timer += 0.2f;
        }
        yield return null;
    }

    private void SpawnObject()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, canvas.pixelRect.width - 200f), Random.Range(0, canvas.pixelRect.height - 200f));
        GameObject newObject = SetPictureToPrefabWindow();
        newObject.transform.position = randomPosition;
    }

    private GameObject SetPictureToPrefabWindow()
    {
        GameObject objectAd = Instantiate(prefab, Vector3.zero, Quaternion.identity, canvas.transform);
        Image adImage = objectAd.GetComponentInChildren<Image>();
        adImage.sprite = ChooseAds();
        /*images.Add(adImage);*/
        return objectAd;
    }

    private Sprite ChooseAds()
    {
        int randomIndex = Random.Range(0, adsToSpawn.Count);
        return adsToSpawn[randomIndex];
    }
}

