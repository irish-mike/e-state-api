using EState.Api.Startup;

var app = ApplicationBootstrapper.Build(args);
await app.RunAsync();
