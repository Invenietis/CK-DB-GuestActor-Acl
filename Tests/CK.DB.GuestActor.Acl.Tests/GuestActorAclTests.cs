using System;
using CK.Core;
using CK.DB.Acl;
using CK.DB.Actor;
using CK.DB.Auth;
using CK.SqlServer;
using FluentAssertions;
using NUnit.Framework;

using static CK.Testing.MonitorTestHelper;

namespace CK.DB.GuestActor.Tests
{
    [TestFixture]
    public class GuestActorAclTests
    {
        [Test]
        public void when_guest_actor_was_granted_on_acl_it_can_be_destroyed()
        {
            var guestActorTable = SharedEngine.Map.StObjs.Obtain<GuestActorTable>();
            var aclTable = SharedEngine.Map.StObjs.Obtain<AclTable>();

            using( var ctx = new SqlStandardCallContext( TestHelper.Monitor ) )
            {
                var createResult = guestActorTable.CreateGuestActor( ctx, 1, DateTime.Now + TimeSpan.FromMinutes( 5 ), true );
                var aclId = aclTable.CreateAcl( ctx, 1 );
                aclTable.AclGrantSet(ctx, 1, aclId, createResult.GuestActorId, "", (byte) GrantLevel.Viewer );
                guestActorTable
                   .Invoking( sut => sut.DestroyGuestActor( ctx, 1, createResult.GuestActorId ) )
                   .Should()
                   .NotThrow();
            }
        }

        [Test]
        public void when_guest_actor_is_a_user_and_was_granted_on_acl_it_can_be_destroyed()
        {
            var userTable = SharedEngine.Map.StObjs.Obtain<UserTable>();
            var guestActorTable = SharedEngine.Map.StObjs.Obtain<GuestActorTable>();
            var aclTable = SharedEngine.Map.StObjs.Obtain<AclTable>();

            using( var ctx = new SqlStandardCallContext( TestHelper.Monitor ) )
            {
                var userId = userTable.CreateUser( ctx, 1, Guid.NewGuid().ToString() );
                var payload = guestActorTable.CreatePayload();
                var createResult =  guestActorTable.CreateOrUpdateGuestActor( ctx, 1, userId, payload as IGuestActorInfo, UCLMode.CreateOnly );
                createResult.OperationResult.Should().Be( UCResult.Created );
                var aclId = aclTable.CreateAcl( ctx, 1 );
                aclTable.AclGrantSet(ctx, 1, aclId, userId, "", (byte) GrantLevel.Viewer );
                userTable
                   .Invoking( sut => sut.DestroyUser( ctx, 1, userId ) )
                   .Should()
                   .NotThrow();
            }
        }
    }
}
