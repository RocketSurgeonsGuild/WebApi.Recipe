Task("MSBuild")
    .Does(() => 
    {
        // builds web api project and packages for web deploy
        MSBuild("./src/Website.sln", new MSBuildSettings()
            .WithProperty("OutDir", "$(build.stagingDirectory)")
            .WithProperty("DeployOnBuild", "true")
            .WithProperty("WebPublishMethod", "Package")
            .WithProperty("PackageAsSingleFile", "true")
            .WithProperty("SkipInvalidConfigurations", "true"));
    });