# Nuget-Unity
![img](https://raw.githubusercontent.com/Alquimiaware/Nuget-Unity/master/Nuget-Unity32.png)

Editor Extension for Unity to let users consume nuget repositories and benefit from the usage of a complete versioned package provider that also downloads indirect dependencies of the installed code.

# Why using a package manager to share code?
As you keep making one project after another you start observing that lots of the problems being solved are almost exacly the same. 
At that point you may consider isolating those features as middleware so it can be reused later.
You might find in your way that the features you are isolating depend on other features, that in turn depend on other subfeatures. 
You can follow the rabbit hole until you hit a leaf, which is code that only depends on features from the framework, which is available to anyone running on a fixed target platform. 

## Let's see it through an example.
We want use a *Sphere* type that helps us working with spherical volumes.
To define a *Sphere* behavior we need to provide:
- Center (*Vector*) 
- Radius (*float*)

In the way Sphere is described, its definition won't be complete unless a Vector and float types that conforms to every feature Sphere is demanding is in scope.
Components that are required to be in scope for a given component to work are called dependencies, from the point of view of the code that needs them.

To define a *Vector* we need:
- 3 coordinates (*float*)

So in order to install *Sphere* component, we also need to install *Vector*. We don't need to install *float* because it's part of the platform framework. The way Unity works right now **there is not automatic way for a component to download it's dependencies** so they must be installed by hand or it must be distributed with copies of their dependencies. This is a reason a lot of people share code just by copy pasting all the common code ( assuming they can extract the middleware without bringing the whole system, which is another topic altogether) or by unity packages.

## Why not sharing all common code together?
There are lots of reasons, including but not limited to:
- You will be always compiling common code that will barely change and probably lots of code that you are not using on the specific project, so your build times will be slower
- When you fix an issue on common code, you have manually to propagate the changes to all the copies.
- There is not ordered stable releases so you probably will have lots of almost equivalent (but not quite) versions that will be hell to mantain.
- If common code contains a type already present in the project, there will be conflicts
- Anything that depends on common will need to bring all that code, or join the pack

## How to share in a better way?
Shared code can provide the value of reusability without the overheads described in previous section, it just needs to be distributed complying with the following properties.
- It is organized in versioned packages.
- It is seamless to install or update a package into the editor.
- There is a form of repository for package distribution.
- Direct and indirect dependencies of a package are automatically installed too. 

# How does Nuget-Unity help?
Based on top of the already established [NuGet](https://www.nuget.org/) package manager, we already get versioned packages, repositories and automatic dependency installing. What Nuget-Unity helps with is making it seamless to use with the editor while also solving the issues preventing people from using it by command line, which are:
- Unity uses outdated mono version
- Unity is not 100% compatible with .net35 target, so each module must me verified against it. 
- It has different framework target profiles depending on whether the code is runtime or editor and also depending on whether full or partial framework is included.
- Nuget command line downloads all target framework versions so the apropiate one must be selected. 

# How to use
- Open Nuget window from Window -> Nuget
- Search for the library you want to consume ( ie. newtonsoft.json )
- Install the one you prefer ( it blocks the main thread until completes for now)
- Your package and its dependencies will be installed on Packages/Runtime or Packages/Editor depending on the refereces of the assembly

# Notes
Right now the libraries downloaded are targetting .net35. As you might know there is no guarantee that a library built against that target will completely work with unity. 

A complete solution would be to have a package repository with libraries built against unity specific build targets. I'll share with you soon a little more about the specifics of how this could be done.

The project right now is focusing on consuming and upgrading packages. For package publishing I'll write some articles with options for managing your package repositories and how to properly create the packages.

Thanks a lot all of you who try this, I also would love to get feedback in the form of well described issues ^^

# Download 
[Alpha-Preview](https://github.com/Alquimiaware/Nuget-Unity/raw/master/NuGetUnity.unitypackage)

### Credits
<div>Icons made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a>             is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0">CC BY 3.0</a></div>
