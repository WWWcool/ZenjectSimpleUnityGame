using System;
using Blocks;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Game/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public BlocksSettings blocks;
        public GameInstaller.Settings gameInstaller;

        [Serializable]
        public class BlocksSettings
        {
            public BlockManager.Settings Spawner;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(blocks.Spawner);
            Container.BindInstance(gameInstaller);
        }
    }
}

