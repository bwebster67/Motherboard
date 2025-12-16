using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDeathEffect : GenericPooledObject
{
    public ParticleSystem particles;
    protected override void OnEnable()
    {
        base.OnEnable();
        particles.Play();
        Invoke(nameof(Deactivate), 0.75f);
    }

    protected override void Reset()
    {
        base.Reset();
        particles.Stop();
    }
}
