var builder = DistributedApplication.CreateBuilder(args);

var bootstrapper = builder.AddProject<Projects.Enscool_Bootstrapper>("enscool-bootstrapper");

builder
    .Build()
    .Run();