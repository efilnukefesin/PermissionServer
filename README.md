# PermissionServer
Demo implementation of a Permission system using IdentityServer4.

Project Status: WIP (early)

Build Status:
master: [![Build status](https://ci.appveyor.com/api/projects/status/e3lctnp2ax5qgao1/branch/master?svg=true)](https://ci.appveyor.com/project/efilnukefesin/permissionserver)
develop: [![Build status](https://ci.appveyor.com/api/projects/status/e3lctnp2ax5qgao1/branch/develop?svg=true)](https://ci.appveyor.com/project/efilnukefesin/permissionserver)

Necessary .NET Core SDK: [NET Core 3 SDK Preview 5](<https://dotnet.microsoft.com/download/dotnet-core/3.0>)

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
- leverage .NET Standard 3
- provide real life examples of above scenarios
- MVVM (I'm trying not to overengineer [VMs for UserControls, seperated Projects in all detail, ...])
- ...

So let's introduce the members of the play:
- Bob (he's the user)
- Nigel (he's the boss/permission owner)
- Admin (he's the admin, coming from hell)

We probably need some pieces of software, too:
- a client app which uses the MegaImportantFunction (tm) (ClientApp)
- a console test app (ConsoleTestApp)
- the server having this function (SuperHotFeatureServer)
- an IdentityServer4 implementation (IdentityServer)
- a Permission dealing Permission Server (PermissionServer)
- an admin app (AdminApp)
- some model and shared libs

## underlying principles
To keep this undertaking understandable and not too abstract, there is a quite simple model of understanding behind:
1. a USER keeps the identity information. A USER can have 0..n ROLES
2. a ROLE is a logical "folder" for PERMISSIONS
2.5. a ROLE could need an in-between layer called FUNCTIONS to bundle PERMISSIONS but thats not clear yet
3. a PERMISSION is holding the information if something is allowed
4. (a POLICY is taking some of the above information and telling the app if or if not the USER may do something)

## enhanced principles
With further thinking applied, one can come to the following conclusions:
- never should a ROLE be checked in a POLICY
- a ROLE could have 0..n child ROLES
- a PERMISSION should not be assigned to a USER directly
- a PERMISSION must not have child PERMISSIONS
- a human understandable permission system should use additive PERMISSIONS, no explicit denials
- a substitute for a USER should always know that his work is required
- processes should be clear and transparent
- if a PERMISSION is revoked, this should hit immediately

##TODOs
As this solution is still in a very early stage, there are some open TODOs, of course:
- integrate translations
- Add Process Layer in Role Model
- Add persistance of data
- add logging
- do the clients
- do all the end points
- ...