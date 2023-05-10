using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTracker : MonoBehaviour
{
    public Text text;
    public static int TargetsKilled;
    public int TargetsCount;

    private void FixedUpdate()
    {
        text.text = "Targets" + TargetsKilled + "/" + TargetsCount;
    }
    private void Reset()
    {
        TargetsKilled = 0;
    }
}
