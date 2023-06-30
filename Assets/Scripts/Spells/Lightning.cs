using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightningSearch))]
public class Lightning : Spell
{
    LightningSearch lightningSearch;
    [SerializeField] float range;

    protected override void Awake()
    {
        base.Awake();
        lightningSearch = GetComponent<LightningSearch>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnCast(Vector2 startPos)
    {
        base.OnCast(startPos);
        var targets = lightningSearch.
            SearchForTargetNextTargetIsStartPos<Enemy>(startPos, range, 5);

        StartCoroutine(CastLightning(targets));
    }

    private IEnumerator CastLightning(List<Vector3> targets)
    {
        _particleSystem.Emit(1);
        var particles = new ParticleSystem.Particle[targets.Count];
        var count = _particleSystem.GetParticles(particles);

        for (int i = 0; i < targets.Count; i++)
        {
            var nextTarget = targets[i];

            particles[0].position = nextTarget;
            _particleSystem.SetParticles(particles);
            //allow trail to render
            yield return new WaitForFixedUpdate();
        }
    }
}
