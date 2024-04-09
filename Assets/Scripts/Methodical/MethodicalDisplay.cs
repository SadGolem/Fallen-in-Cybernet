
using TMPro;
using UnityEngine;

public class MethodicalDisplay : MonoBehaviour
{
    public MethodicalInformation methodical;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;

    public void GetMethodical(MethodicalInformation methodicalInformation)
    {
        methodical = methodicalInformation;
        UpdateText();
    }

    private void UpdateText()
    {
        name.text = methodical.name;
        description.text = methodical.description;
    }
}
