﻿using System;
 using Blocks;
 using LeaderBoard;
 using UnityEngine;
 using Zenject;

 namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallBlocks();
            Container.Bind<LeaderBoardManager>().AsSingle();
        }

        void InstallBlocks()
        {
            Container.Bind<BlockManager>().AsSingle();
            Container.BindFactory<Block, Block.Factory>()
                .FromComponentInNewPrefab(_settings.BlockPrefab)
                .WithGameObjectName("Block")
                .UnderTransformGroup("Blocks");
        }

        [Serializable]
        public class Settings
        {
            public GameObject BlockPrefab;
        }
    }
}

