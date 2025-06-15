using TMPro;
using UnityEngine;

public class ToolCollectibleCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counterUI;

    [SerializeField]
    private PlayerCollect playerCollect;

    private void Update()
    {
        counterUI.text = playerCollect.GetCurrentToolCollectibleCount().ToString("000");
    }
}