# PermissionServer
Demo implementation of a Permission system using IdentityServer4.

This repository aims to show possible approaches to the following scenarios.
You might/should/must want to:
- Identify a User
- check the permissions of this user in a WPF project
- manage permissions of this user in a WPF project
- let this user try to get new permissions
- let the permission owner decide whether to grant these
- let the permission owner have an overview of users using his permissions
- ...

This solution follows these principles:
- seperate Identity from Permissions
- KISS (hopefully ;-))
- leverage .NET Standard
- provide real life examples of above scenarios
- MVVM (I'm trying not to overengineer [VMs for UserControls, seperated Projects in all detail, ...])
- ...

So let's introduce the members of the play:
- Bob (he's the user)
- Nigel (he's the boss/permission owner)
- Dom (he's the admin, a particular nice one)

We probably need some pieces of software, too:

- a client app with the MegaImportantFunction (tm)
- an IdentityServer4 implementation
- a Permission dealing Permission Server
- an admin app