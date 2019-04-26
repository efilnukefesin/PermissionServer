using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Enums
{
    public enum PermissionFlowType
    {
        ClientSide, //Token is being held on client
        ServerSide  //Token is request each time at the Permission Server
    }
}
