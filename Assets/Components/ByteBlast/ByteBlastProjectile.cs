using System.Collections;
using TMPro;
using UnityEngine;

public class ByteBlastProjectile : PlayerProjectile 
{
    public TextMeshPro textMeshPro;
    public Vector3 velocityVector = new Vector3(0, 0, 0);

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }

    protected override void Reset()
    {
        base.Reset();
        float rand = Random.value;
        if (rand < 0.5) textMeshPro.text = "0";
        else textMeshPro.text = "1";
    }

    protected override void Update()
    {
        base.Update();
        if (enabled)
        {
            transform.position = transform.position + velocityVector * Time.deltaTime;
        }
    }

}
