using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class Spell : MonoBehaviour, ICastable
{
    [SerializeField] protected ParticleSystem _particleSystem;

    protected virtual void Awake() { }

    protected virtual void Start() { }

    public virtual void OnCast(Vector2 startPos)
    { 
    }
}
