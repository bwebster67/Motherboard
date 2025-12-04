using System.Collections;
using TMPro;
using UnityEngine;

public class ByteBlastProjectile : PlayerProjectile 
{
    public TextMeshPro textMeshPro;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void Reset()
    {
        base.Reset();
        float rand = Random.value;
        if (rand < 0.5) textMeshPro.text = "0";
        else textMeshPro.text = "1";
    }

    public IEnumerator Blast(Vector3 velocityVector)
    {
        float timeElapsed = 0f;

        while (timeElapsed < 3)
        {
            transform.position = transform.position + velocityVector * Time.deltaTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        } 

        Deactivate();

    }
}
