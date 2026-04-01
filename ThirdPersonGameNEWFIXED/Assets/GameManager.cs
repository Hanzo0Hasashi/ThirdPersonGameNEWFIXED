using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Text coinText;
    private int coins = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (coinText != null) coinText.text = "Coins: 0";
    }

    public void AddCoin()
    {
        coins++;
        if (coinText != null) coinText.text = "Coins: " + coins;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("EntryPoint");
        }
    }
}