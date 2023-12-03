using System.Windows;
using ClipNote.Models;
using ClipNote.Views;
using Prism.Ioc;

namespace ClipNote
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            using var db = new DatabaseContext();
            db.Database.EnsureCreated();
        }
    }
}