using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counterUI;

    [SerializeField]
    private PlayerAttack playerAttack;

    private void Update()
    {
        counterUI.text = playerAttack.GetCurrentAttackPoints().ToString("0000");
    }
}