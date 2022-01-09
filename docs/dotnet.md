[migration]
dotnet ef --startup-project [startup.csproj] migrations add "[name]" --project [migration.csproj]

[update]
dotnet ef --startup-project [startup.csproj] database update "[name]" --project [migration.csproj]

    migration add
    database update

dotnet ef --startup-project .\src\Presentation\Server\ migrations add "sqlite" --project .\src\Infrastructure\

dotnet ef --startup-project .\src\Presentation\Server\ database update "sqlite" --project .\src\Infrastructure\

[tool]
dotnet tool update --global dotnet-ef