# Custom Extension Services Demo

With the new service locator pattern of the Mixed Reality Toolkit, adding your own services is fairly simple and straight forward.

THere are two types of Services that can be generated:

* Services - Base entity that can be registered with the Mixed Reality Toolkit to perform operations.  Enabled through either code or via the Registered Service Provders configuration
* Systems - Unique managers with the ability to have multiple providers for different platforms. Configurable through profiles in the Editor

## Custom Services
Each base service implementation will require the following:

1. [Interface Contract](./CustomService.IDemoCustomService.cs)
2. [Concrete Implementation](./CustomService/DemoCustomService.cs)

Once each of these exist then an service can be added to the Registered Service Provders registry of the Toolkit's "Registered Service Provders" configuration profile.

## Custom Systems
Each service will require the following files:

1. [Interface Contract](./CustomSystem/IDemoCustomSystem.cs)
2. [Concrete Implementation](./CustomSystem/DemoCustomSystem.cs)
3. [Configuration Profile Definition](./CustomSystem/DemoCustomSystemProfile.cs)
4. [Configuration Profile Inspector](./CustomSystem/Inspectors/DemoCustomSystemProfileInspector.cs)
5. [Configuration Profile Asset](./CustomSystem.Profiles/DemoCustomSystemProfile.asset)

Once each of these exist then a system can be added to the Systems registry of the main Mixed Reality Toolkit's configuration profile.
