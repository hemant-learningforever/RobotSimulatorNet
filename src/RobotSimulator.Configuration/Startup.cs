using RobotSimulator.Contracts;
using RobotSimulator.Implementation;
using System;
using Unity;

namespace RobotSimulator.Configuration
{
    public class Startup:IDisposable
    {
        IUnityContainer _container;

        public Startup()
        {
            _container = new UnityContainer();
            Register();
        }

        private void Register()
        {
            _container.RegisterType<IMoveValidator, MoveValidator>();
            _container.RegisterType<IMove, Move>();

        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
