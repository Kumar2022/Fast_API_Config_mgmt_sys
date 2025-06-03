using System.Collections.Generic;

namespace VirtualFileSystem;

internal class DirectoryNode
{
    public Dictionary<string, DirectoryNode> Directories { get; } = new();
    public Dictionary<string, string> Files { get; } = new();
}

public class InMemoryFileSystem : IFileSystem
{
    private readonly DirectoryNode _root = new();

    private static string[] SplitPath(string path) =>
        path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);

    private DirectoryNode GetDirectory(string path, bool create = false)
    {
        var segments = SplitPath(path);
        var current = _root;
        foreach (var segment in segments)
        {
            if (!current.Directories.TryGetValue(segment, out var next))
            {
                if (!create)
                    throw new DirectoryNotFoundException($"Directory not found: {path}");
                next = new DirectoryNode();
                current.Directories[segment] = next;
            }
            current = next;
        }
        return current;
    }

    private (DirectoryNode dir, string name) GetParent(string path, bool create)
    {
        var directoryPath = Path.GetDirectoryName(path) ?? string.Empty;
        var name = Path.GetFileName(path);
        var dir = GetDirectory(directoryPath, create);
        return (dir, name);
    }

    public void CreateDirectory(string path) => GetDirectory(path, create: true);

    public void DeleteFile(string path)
    {
        var (dir, name) = GetParent(path, create: false);
        if (!dir.Files.Remove(name))
            throw new FileNotFoundException($"File not found: {path}");
    }

    public void DeleteDirectory(string path, bool recursive)
    {
        if (string.IsNullOrEmpty(path.Trim('/')))
            throw new IOException("Cannot delete root directory");
        var segments = SplitPath(path);
        var current = _root;
        DirectoryNode? parent = null;
        string? name = null;
        foreach (var segment in segments)
        {
            if (!current.Directories.TryGetValue(segment, out var next))
                throw new DirectoryNotFoundException($"Directory not found: {path}");
            parent = current;
            name = segment;
            current = next;
        }
        if (!recursive && (current.Directories.Count > 0 || current.Files.Count > 0))
            throw new IOException("Directory is not empty");
        parent!.Directories.Remove(name!);
    }

    public bool FileExists(string path)
    {
        var (dir, name) = GetParent(path, create: false);
        return dir.Files.ContainsKey(name);
    }

    public bool DirectoryExists(string path)
    {
        if (string.IsNullOrEmpty(path.Trim('/')))
            return true;
        try
        {
            GetDirectory(path, create: false);
            return true;
        }
        catch (DirectoryNotFoundException)
        {
            return false;
        }
    }

    public void WriteAllText(string path, string contents)
    {
        var (dir, name) = GetParent(path, create: true);
        dir.Files[name] = contents;
    }

    public string ReadAllText(string path)
    {
        var (dir, name) = GetParent(path, create: false);
        if (!dir.Files.TryGetValue(name, out var data))
            throw new FileNotFoundException($"File not found: {path}");
        return data;
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        var dir = GetDirectory(path, create: false);
        foreach (var kv in dir.Files.Keys)
            yield return kv;
    }

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        var dir = GetDirectory(path, create: false);
        foreach (var kv in dir.Directories.Keys)
            yield return kv;
    }
}
