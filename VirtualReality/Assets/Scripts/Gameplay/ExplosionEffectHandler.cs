using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectHandler : MonoBehaviour
{

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float radius = 1;
    public float Radius { get { return this.radius; } set { this.radius = value; ChangeParticleShapeRadius(); } }
    void Start()
    {
        if (particles == null)
        {
            this.particles = GetComponentInChildren<ParticleSystem>();
        }
        ChangeParticleShapeRadius();
    }

    void ChangeParticleShapeRadius()
    {
        ParticleSystem.ShapeModule sm = particles.shape;
        sm.radius = radius;
    }

}
