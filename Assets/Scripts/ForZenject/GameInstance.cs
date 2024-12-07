
using UnityEngine;
using Zenject;

public class GameInstance : MonoInstaller
{
    [SerializeField] private GameObject player;
    
    public override void InstallBindings()
    {
        Container.Bind<Singleton<PlayerManager>>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelGenerator>().FromComponentInHierarchy().AsSingle();

        Container.Bind<GameObject>().FromInstance(player).AsSingle();

        Container.Bind<PlayerInteraction>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerCombat>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerHealth>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerStats>().FromComponentInHierarchy().AsSingle();

    }
}
