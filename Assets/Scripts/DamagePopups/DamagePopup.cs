using UnityEngine;
using UnityEngine.Rendering;

public class DamagePopup : GenericPooledObject
{
    public TMPro.TextMeshPro textMeshPro;
    public void SetValue(float value)
    {
        textMeshPro.text = value.ToString();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke(nameof(Deactivate), 0.75f);
    }

    protected override void Reset()
    {
        base.Reset();
    }
}
