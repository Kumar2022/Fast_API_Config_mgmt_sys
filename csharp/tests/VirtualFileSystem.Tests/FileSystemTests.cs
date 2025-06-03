using VirtualFileSystem;

namespace VirtualFileSystem.Tests;

public class FileSystemTests
{
    [Fact]
    public void WriteAndReadFile_Works()
    {
        var fs = new InMemoryFileSystem();
        fs.CreateDirectory("/config");
        fs.WriteAllText("/config/settings.json", "{}");
        Assert.True(fs.FileExists("/config/settings.json"));
        Assert.Equal("{}", fs.ReadAllText("/config/settings.json"));
    }

    [Fact]
    public void EnumerateDirectoriesAndFiles_Works()
    {
        var fs = new InMemoryFileSystem();
        fs.CreateDirectory("/a/b");
        fs.WriteAllText("/a/b/file.txt", "demo");

        var dirs = fs.EnumerateDirectories("/a").ToList();
        Assert.Single(dirs);
        Assert.Equal("b", dirs[0]);

        var files = fs.EnumerateFiles("/a/b").ToList();
        Assert.Single(files);
        Assert.Equal("file.txt", files[0]);
    }
}
