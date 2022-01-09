[migration]
dotnet ef --startup-project [startup.csproj] migrations add "[name]" --project [migration.csproj]

[update]
dotnet ef --startup-project [startup.csproj] database update "[name]" --project [migration.csproj]

    migration add
    database update

[tool]
dotnet tool update --global dotnet-ef