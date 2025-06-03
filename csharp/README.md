# C# Libraries

This directory contains example C# libraries organised into a .NET solution. It demonstrates a simple configuration loader and a minimal in-memory filesystem implementation inspired by projects like `System.IO.Abstractions`.

## Structure

- `src/ConfigManager` – class library containing the `Configuration` class.
- `src/VirtualFileSystem` – class library implementing an in-memory filesystem.
- `tests/ConfigManager.Tests` – tests for the configuration library.
- `tests/VirtualFileSystem.Tests` – tests for the filesystem library.

## Building and Testing

Ensure the [.NET SDK](https://dotnet.microsoft.com/download) is installed. Then run:

```bash
cd csharp
 dotnet test FileSystem.sln
```

This will build all projects and execute their tests.
