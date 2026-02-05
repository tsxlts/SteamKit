namespace SteamKit
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate Task AsyncEventHandler<in TEventArgs>(object? sender, TEventArgs e) where TEventArgs : allows ref struct;
}
