using BattleShips.BusinessLayer;
using Unity;

namespace Battleships
{
    class UnityDiContainerProvider
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IIoHelper, IoHelper>();
            container.RegisterType<IIoHelperBoard, IoHelperBoard>();
            container.RegisterType<IIoHelperShot, IoHelperShot>();
            container.RegisterType<IShotService, ShotService>();

            container.RegisterSingleton<IShipService, ShipService>();

            return container;
        }
    }
}
