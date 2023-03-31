using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CryptoInfoApp.Core;

public static class DispatcherHelper
{
    public static Dispatcher Dispatcher { get; set; }

    public static Task BeginInvoke(Action action)
    {
        return Dispatcher.BeginInvoke(action).Task;
    }

    public static void Invoke(Action action)
    {
        Dispatcher.Invoke(action);
    }

    public static bool CheckAccess()
    {
        return Dispatcher.CheckAccess();
    }
}