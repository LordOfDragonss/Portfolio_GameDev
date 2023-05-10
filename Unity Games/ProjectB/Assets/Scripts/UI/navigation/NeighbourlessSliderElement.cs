using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class NeighbourlessSliderElement : navableElement
{
    Slider slider;

    protected override void Start(){
        elements.Add(Direction.up, up);
        elements.Add(Direction.down, down);
        slider = GetComponent<Slider>();
    }
    public override navableElement moveTo(Direction direction)
    {
        if(direction == Direction.up || direction ==  Direction.down)
            return base.moveTo(direction);

        if(direction == Direction.left){
            slider.value--;
        }else{
            slider.value++;
        }
        return this;
    }
}
