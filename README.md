# Nuget-Unity
![img](https://raw.githubusercontent.com/Alquimiaware/Nuget-Unity/master/Nuget-Unity32.png)

Editor Extension for Unity to let users consume nuget repositorires and benefit from the usage of a complete versioned package provider that also downloads indirect dependencies of the installed code.

# How to use
- Open Nuget window from Window -> Nuget
- Search for the library you want to consume ( ie. newtonsoft.json )
- Install the one you prefer ( it blocks the main thread until completes for now)
- Your package and its dependencies will be installed on Packages/Runtime or Packages/Editor depending on the refereces of the assembly

# Notes
Right now the libraries downloaded are targetting .net35. As you might know there is no guarantee that a library built against that target will completely work with unity. 
A complete solution would be to have a package repository with libraries built against unity specific build targets. I'll share with you soon a little more about the specifics of how this could be done.

Thanks a lot all of you who try this, I also would love to get feedback in the form of well described issues ^^

# Download 
[Alpha-Preview](https://github.com/Alquimiaware/Nuget-Unity/raw/master/NuGetUnity.unitypackage)

### Credits
<div>Icons made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a>             is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0">CC BY 3.0</a></div>
