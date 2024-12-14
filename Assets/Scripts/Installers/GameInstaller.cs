using System;
using System.Collections.Generic;
using Controllers;
using Controllers.Game;
using Controllers.Inputs;
using Controllers.UI;
using UnityEngine;
using Views.UI;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        private List<BaseController> _baseControllers = new List<BaseController>();

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();

            Container.Bind<IInput>().To<MouseInput>().AsSingle().NonLazy();
            Container.Bind<DragAndDropController>().AsSingle().NonLazy();
            Container.Bind<WorldController>().AsSingle().NonLazy();
            Container.Bind<InventoryController>().AsSingle().NonLazy();
            Container.Bind<ItemsController>().AsSingle().NonLazy();
            Container.Bind<InventoryWindowController>().AsSingle().NonLazy();
            Container.Bind<ServerController>().AsSingle().NonLazy();

            Container.Bind<BaseController>().To<MonoBehaviourManager>().AsSingle().NonLazy();
            _baseControllers.AddRange(Container.ResolveAll<BaseController>());
        }

        private void Update()
        {
            foreach (var controller in _baseControllers)
            {
                controller.Update(Time.deltaTime);
            }
        }
    }
}
