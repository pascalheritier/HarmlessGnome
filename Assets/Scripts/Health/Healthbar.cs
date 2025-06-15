using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Image totalHealthBar;
    [SerializeField] Image currentHealthBar;

    public void Start()
    {
        totalHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }

    public void Update()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}
