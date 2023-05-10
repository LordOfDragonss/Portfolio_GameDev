using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class buttonElement : navableElement
{
    private Button button;
    // Start is called before the first frame update
     protected override void Start()
    {
        base.Start();
        button = GetComponent<Button>();

    }

    public override void Activate()
    {
        button.onClick.Invoke();
    }

}
