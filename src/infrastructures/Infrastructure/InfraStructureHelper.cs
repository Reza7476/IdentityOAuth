namespace Infrastructure;

public  class InfraStructureHelper
{
    public static string GetInfrastructureDirectory()
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        DirectoryInfo? parentDir = Directory.GetParent(currentDirectory);

        var slnFile = parentDir.GetFiles("*.sln");

        parentDir = parentDir.Parent;

        string infrastructureProjectName = "infrastructures\\Infrastructure";

        var infrastructureDirectory = parentDir + "\\" + infrastructureProjectName;

        return infrastructureDirectory;
    }
}
