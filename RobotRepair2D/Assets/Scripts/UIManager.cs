using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scrapTotal, waveInfo, score;

    private void Awake()
    {
        Instance = this;
    }
}
