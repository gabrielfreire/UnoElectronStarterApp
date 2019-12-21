# Uno Platform + Electron
## Electron
![Electron Picture](Assets/Electron_wasm.PNG)
## UWP
![UWP Picture](Assets/UWP.PNG)

This repository is composed of the default Uno Platform Starter template project and an extra Electron.NET that wraps the Uno WASM dist files. 

This project aims to be a Quick Starter of Electron Apps in .NET that wraps an Uno Platform WebAssembly Project.

This way we can distribute UWP apps to Mac/Linux/Windows as cross-platform Desktop Applications.
# Build
The `UnoTest.Wasm` project build must be triggered mannually before building `UnoTest.Electron`.

The `UnoTest.Electron` project uses a batch script (`prebuild.bat`) that gets executed from the project's `.csproj` file.
This script copies all the `dist` files from `UnoTest.Wasm` to `UnoTest.Electron/wwwroot` before build.

# TODO
- Automate WebAssembly dist files copy/pre-build/build process.
- Maybe there is a better way to do it (thinking...).