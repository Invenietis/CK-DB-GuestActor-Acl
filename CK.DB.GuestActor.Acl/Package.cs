using CK.Setup;
using CK.SqlServer.Setup;

namespace CK.DB.GuestActor.Acl
{
    [SqlPackage( Schema = "CK", ResourcePath = "Res" )]
    [Versions( "1.0.0" )]
    [SqlObjectItem( "transform:sGuestActorDestroy" )]
    public class Package : SqlPackage
    {
        internal void StObjConstruct( CK.DB.GuestActor.Package guestActor, CK.DB.Acl.Package acl ) { }
    }
}
