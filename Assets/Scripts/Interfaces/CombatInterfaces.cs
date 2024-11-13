using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void Push(Vector2 forceVector, float forceStrength);
}

public interface IDamagable
{
    void TakeDamage(float damage);
}

