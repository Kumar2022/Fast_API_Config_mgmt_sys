namespace VirtualFileSystem;

public interface IFileSystem
{
    void CreateDirectory(string path);
    void DeleteFile(string path);
    void DeleteDirectory(string path, bool recursive);
    bool FileExists(string path);
    bool DirectoryExists(string path);
    void WriteAllText(string path, string contents);
    string ReadAllText(string path);
    IEnumerable<string> EnumerateFiles(string path);
    IEnumerable<string> EnumerateDirectories(string path);
}
