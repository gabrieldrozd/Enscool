var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Enscool_Bootstrapper>("enscool-bootstrapper");

builder
    .Build()
    .Run();