using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropDownElement : navableElement
{
    TMP_Dropdown dropdown;
    Scrollbar scrollbar;
    float incrementValue;
    bool selected = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Update(){
        if(selected)
        dropdown.Show();
    }

    public override void Activate()
    {
        incrementValue = 1f / (float)dropdown.options.Count();
        selected = !selected;
        Update();
        if(selected){
            scrollbar = GetComponentInChildren<Scrollbar>();
        }
    }

    public override navableElement moveTo(Direction direction)
    {   
        if(!selected)
            return base.moveTo(direction);

        switch (direction){
        case Direction.up:
                scrollbar.SetValueWithoutNotify(scrollbar.value - incrementValue);
                break;

        case Direction.down:
                scrollbar.value += incrementValue;
                Debug.Log(incrementValue);
                //scrollbar.SetValueWithoutNotify(scrollbar.value + incrementValue);
                break;

        }
        Debug.Log(scrollbar.value);

        return this;
    }
}
