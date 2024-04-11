using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DropsMiniGameController : MonoBehaviour
{
    public GameObject itemPrefab; // префаб пойманных предметов
    public GameObject parent; // префаб предметов, которые нельзя поймать
    public GameObject badItemPrefab; // префаб предметов, которые нельзя поймать
    public Transform spawnPoint; // точка спавна для предметов
    public Transform player; // игрок
    public Button left; // кнопка
    public Button right; // кнопка
    public float spawnInterval = 1.0f; // интервал спавна новых предметов
    public float moveSpeed = 0.06f; // скорость движения предметов вниз
    public float destroyY = -6.0f; // предмет уничтожается, если достигает этой координаты по Y
    [SerializeField] List<string> attackList = new List<string>();
    [SerializeField] List<string> simleText = new List<string>();
    [SerializeField] TextMeshProUGUI counter;
    [SerializeField] TextMeshProUGUI counterHeart;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameWin;
    private int count = 0;
    private int countHeart = 3;
    private static int achievementCounter;

    void Start()
    {
        InvokeRepeating("SpawnItem", 0, spawnInterval);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            GoLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            GoRight();
    }

    private void FixedUpdate()
    {
        MoveItems();
    }

    void SpawnItem()
    {
        Vector3 randomSpawnPoint = new Vector3(Random.Range(-8.4f, 8.4f), spawnPoint.position.y, spawnPoint.position.z);

        GameObject item;
        if (Random.Range(0, 101) <= 50)
        {
            item = Instantiate(badItemPrefab, randomSpawnPoint, Quaternion.identity, parent.transform);
            var text = item.GetComponentInChildren<TextMeshProUGUI>();
            text.text = GetRandomSimpleText();
        }
        else
        {
            item = Instantiate(itemPrefab, randomSpawnPoint, Quaternion.identity, parent.transform);
            var text = item.GetComponentInChildren<TextMeshProUGUI>();
            text.text = GetRandomBadText();
        }
        Destroy(item, 10.0f); // уничтожаем предмет спустя 10 секунд
    }

    string GetRandomBadText()
    {
        int randomIndex = Random.Range(0, attackList.Count);
        return attackList[randomIndex];
    }

    string GetRandomSimpleText()
    {
        int randomIndex = Random.Range(0, simleText.Count);
        return simleText[randomIndex];
    }

    void MoveItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            item.transform.Translate(Vector3.down * moveSpeed * Time.timeScale);
        }
    }

    void CheckItems()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Item"))
        {
            if (item.transform.position.y < destroyY)
            {
                Destroy(item);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void Counter()
    {
        count++;
        counter.text = count.ToString();

        if (count == 20)
        {
            Time.timeScale = 0f;
            gameWin.SetActive(true);
        }
    }

    public void UnCounter()
    {
        --countHeart;
        counterHeart.text = countHeart.ToString();
        if (countHeart == 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }
    }

    public void GoRight()
    {
        if (player.localPosition.x < 8.12)
            player.Translate(Vector3.right * moveSpeed /** Time.timeScale*/); // Двигаем персонажа вправо
    }

    public void Restart()
    {
        ++achievementCounter;
        Time.timeScale = 1f;
        var i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i);
    }

    public void GoLeft()
    {
        if (player.localPosition.x > -8.12)
            player.Translate(Vector3.left * moveSpeed /** Time.timeScale*/); // Двигаем персонажа влево 
    }
}
