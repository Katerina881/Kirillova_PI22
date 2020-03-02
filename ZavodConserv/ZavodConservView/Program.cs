using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using ZavodConservbusinessLogic.BusinessLogics;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservFileImplement.Implements;

namespace ZavodConservView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer(); 
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager()); 
            currentContainer.RegisterType<IConservLogic, ConservLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
