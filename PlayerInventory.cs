using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Referință la textul de scor
    public int NumberOfCoins { get; private set; }

    void Start()
    {
        // Caută obiectul cu numele "CoinText" în Canvas
        if (coinText == null)
        {
            GameObject textObject = GameObject.Find("CoinText");
            if (textObject != null)
            {
                coinText = textObject.GetComponent<TextMeshProUGUI>();
            }
        }

        UpdateUI();
    }

    public void CoinCollected()
    {
        NumberOfCoins++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + NumberOfCoins;
        }
    }
}
