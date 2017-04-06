using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Allwin.Sitecore.Modules.LiveList.Hubs
{
    /// <summary>
    /// SignalR LiveList hub
    /// </summary>
    public class LiveListHub : Hub
    {
        /// <summary>
        /// Client join to specific groups
        /// </summary>
        /// <param name="groupName">The group name that the connection belongs to from now on</param>
        /// <returns>A task as it is a asynchron call</returns>
        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }
    }
}