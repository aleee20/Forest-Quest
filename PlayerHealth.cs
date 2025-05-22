using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;

    public Image[] hearts;              // inimile din UI
    public Sprite heartFull;
    public Sprite heartEmpty;

    void Start()
    {
        if (hearts.Length == 0)
        {
            hearts = new Image[3];
            hearts[0] = GameObject.Find("Heart1").GetComponent<Image>();
            hearts[1] = GameObject.Find("Heart2").GetComponent<Image>();
            hearts[2] = GameObject.Find("Heart3").GetComponent<Image>();
        }

        UpdateHeartsUI();
    }


    public void TakeDamage()
    {
        lives--;

        Debug.Log("Ai fost prins! Vieți rămase: " + lives);

        UpdateHeartsUI(); // ❤️ actualizează vizual

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                Debug.LogWarning($"Heart[{i}] este NULL!");
                continue;
            }

            hearts[i].sprite = i < lives ? heartFull : heartEmpty;
        }
    }

}
