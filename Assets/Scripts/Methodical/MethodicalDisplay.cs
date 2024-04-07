using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MethodicalDisplay : MonoBehaviour
{
    public MethodicalInformation methodical;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;

    private void Start()
    {
        name.text = methodical.name;
        description.text = methodical.description;
    }
}
