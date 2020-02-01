using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI ScrapTotalText, WaveInfoText, ScoreText, ShipRepairStatusText;

    private void Awake()
    {
        Instance = this;
    }
}
