using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWHelper.Plugins.Input
{
    public abstract class BaseController : IDisposable
    {
        protected IPageController PageController { get; private set; }

        public void Dispose()
        {
            OnDisposing();
        }

        protected virtual void OnDisposing()
        {
        }

        public void Start(IPageController pageController)
        {
            PageController = pageController;
            OnStart();
        }

        public void Stop()
        {
            OnStop();
        }

        protected virtual void OnStop()
        {
        }

        protected virtual void OnStart()
        {
        }
    }
}
