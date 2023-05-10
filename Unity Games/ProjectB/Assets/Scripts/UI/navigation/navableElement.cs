using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction{
    up,down,left,right
}

public class navableElement : MonoBehaviour
{
    [SerializeField] protected navableElement up;
    [SerializeField] protected navableElement down;
    [SerializeField] navableElement left;
    [SerializeField] navableElement right;

    protected Dictionary<Direction, navableElement> elements = new Dictionary<Direction, navableElement>();

    private float enlargementValue = 1.3f;
    Button button;

    protected virtual void Start(){
        elements.Add(Direction.up, up);
        elements.Add(Direction.down, down);
        elements.Add(Direction.left, left);
        elements.Add(Direction.right, right);

    }


    public virtual void SetActive(){
        transform.localScale *= enlargementValue;
    }

    public virtual void SetInactive(){
                transform.localScale /= enlargementValue;
    }

    public virtual void Activate(){
        button.onClick.Invoke();
    }

    public virtual navableElement moveTo(Direction direction){
        if(elements[direction] == null)
            return this;

        SetInactive();
        elements[direction].SetActive();

        return elements[direction];

    }
}
