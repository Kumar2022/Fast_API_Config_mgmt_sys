using ConfigManager;

namespace ConfigManager.Tests;

public class ConfigurationTests
{
    [Fact]
    public void LoadFromFile_ShouldLoadSettings()
    {
        var jsonFile = Path.GetTempFileName();
        File.WriteAllText(jsonFile, "{\"Key\":\"Value\"}");
        try
        {
            var config = new Configuration();
            config.LoadFromFile(jsonFile);
            Assert.Equal("Value", config["Key"]);
        }
        finally
        {
            File.Delete(jsonFile);
        }
    }
}
