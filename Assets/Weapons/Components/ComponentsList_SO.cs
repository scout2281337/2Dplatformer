using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComponentsList", menuName = "ScriptableObjects/Weapon/ComponentsList", order = 1)]
public class ComponentsList_SO : ScriptableObject
{
    public List<GameObject> weapons = new();
    public List<BaseShotTypeComponent> shotTypes = new();
    public List<BaseFireComponent> fireTypes = new();
    public List<BaseActiveComponent> activeTypes = new();
    public List<BaseImpactComponent> impactTypes = new();
}
