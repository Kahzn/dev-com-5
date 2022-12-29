using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    public Image easterEggImage = null;
    private readonly KeyCode[] trigger = new KeyCode[] { KeyCode.N, KeyCode.E, KeyCode.C, KeyCode.R, KeyCode.O };
    private int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(trigger[index]))
        {
            ++index;
        }        
        if (index == trigger.Length)
        {
            easterEggImage.enabled = !easterEggImage.enabled;
            index = 0;
        }
    }
}
