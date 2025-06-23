
using Microsoft.Extensions.DependencyInjection;

using MovilAsis.Services;

namespace MovilAsis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

    }
}