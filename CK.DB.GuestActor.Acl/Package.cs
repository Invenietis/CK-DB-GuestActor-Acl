using CK.Core;

namespace CK.DB.GuestActor.Acl
{
    /// <summary>
    /// Package that adds acl support to <see cref="GuestActorTable"/>.
    /// </summary>
    [SqlPackage( Schema = "CK", ResourcePath = "Res" )]
    [Versions( "1.0.0" )]
    [SqlObjectItem( "transform:sGuestActorDestroy" )]
    public class Package : SqlPackage
    {
        internal void StObjConstruct( CK.DB.GuestActor.Package guestActor, CK.DB.Acl.Package acl ) { }
    }
}
