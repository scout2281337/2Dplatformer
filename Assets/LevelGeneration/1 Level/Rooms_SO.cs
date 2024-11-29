using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Rooms_SO", menuName = "ScriptableObjects/Rooms_SO", order = 1)]
public class Rooms_SO : ScriptableObject
{
    [SerializeField] public GameObject[] smallRoom;
    [SerializeField] public GameObject[] tallRoom;
    [SerializeField] public GameObject[] longRoom;
    [SerializeField] public GameObject[] bigRoom;
    [SerializeField] public GameObject[] bossRoom;
    [SerializeField] public GameObject[] walls;
    [SerializeField] public GameObject[] wallLock;
}
