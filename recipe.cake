#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "WebApi.Cake.Recipe",
                            repositoryOwner: "RocketSurgeonsGuild",
                            repositoryName: "WebApi.Cake.Recipe",
                            appVeyorAccountName: "RocketSurgeonsGuild",
                            nuspecFilePath: "./WebApi.Cake.Recipe/Rocket.Surgery.WebApi.Cake.Recipe.nuspec");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

BuildParameters.Tasks.CleanTask
    .IsDependentOn("Generate-Version-File");

Task("Generate-Version-File")
    .Does(() => 
    {
        var buildMetaDataCodeGen = TransformText(@"
        public class BuildMetaData
        {
            public static string Date { get; } = ""<%date%>"";
            public static string Version { get; } = ""<%version%>"";
        }",
        "<%",
        "%>")
        .WithToken("date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"))
        .WithToken("version", BuildParameters.Version.SemVersion)
        .ToString();

    System.IO.File.WriteAllText(
        "./WebApi.Cake.Recipe/Content/version.cake",
        buildMetaDataCodeGen
        );
    });

Build.RunNuGet();