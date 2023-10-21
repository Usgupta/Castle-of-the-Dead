using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/IntVariable", order = 4)]
public class IntVariable : Variable<int>
{
    public override void SetValue(int value)
    {
        _value = value;
    }
    // overload
    public void SetValue(IntVariable value)
    {
        SetValue(value.Value);
    }

    public void ApplyChange(int amount)
    {
        this.Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        ApplyChange(amount.Value);
    }

}