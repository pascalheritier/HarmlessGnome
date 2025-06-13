using TMPro;
using UnityEngine;

public abstract class VolumeText : MonoBehaviour
{
    private TextMeshProUGUI textGui;
    protected string persistedVolumeName;

    [SerializeField]
    private string Title;

    private void Awake()
    {
        textGui = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        textGui.text = Title + PlayerPrefs.GetFloat(persistedVolumeName) * 100;
    }
}