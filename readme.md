# Entity Framework 6 in .NET Framework app, developed in VSCode - PoC

This repository contains a simple example on how to set up and work with a .NET Framework 4.8 application from VSCode, utilizing EF6. dotnet is capable of building apps to run on the legacy .NET FW runtime.
TypeSafeEnum Role class and the way it can be used within EF6 is from https://ardalis.com/persisting-the-type-safe-enum-pattern-with-ef-6/

## Installation steps

1. Install the toolchain as depicted here: https://www.coderedcorp.com/blog/using-vs-code-with-a-legacy-net-project/
    - VSCode, C# extension, VS Build tools, .NET Framework, nuget needs to be installed

2. Install Powershell extension in VSCode

3. Install EntityFramework by running: 
    - nuget install EntityFramework

The EF6 toolchain is deployed to the folder where the nuget command was run. E.g. if you run the nuget command from ```c:\repos\ef6```,
the EF toolchain will be deployed into ```c:\repos\ef6\EntityFramework.<major>.<minor>.<patch>``` (in my case this was ```c:\repos\ef6\EntityFramework.6.5.1```)

Let's call this ```<EntityFramework_toolchain_path>```

## General EF6 information and gotchas
To run EF6 commands, use the following executable:
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe 
```

Please note that the DbContext need to be initialized with the Connection String name only, like:
```public BloggingContext() : base("BloggingContext") { }```

It does not work with 
```public BloggingContext() : base("name=BloggingContext") { }```
as it throws "No connection string named 'BloggingContext' could be found in the application config file." error during ef6 commands. See https://github.com/dotnet/ef6/issues/1603 for more information. 

As a probable side effect, the --connection-string an --connection-provider parameters need to be used explicitly, as EF6 tool does not seem to find the connection string in the config file, even if there's only one defined in there.

Also, check the csproj file containing the migrations and add the following Property to the project file (into a PropertyGroup node):
```<EmbeddedResourceUseDependentUponConvention>true</EmbeddedResourceUseDependentUponConvention>```

Otherwise, the ef6 update database command might throw the following error:
```
Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "<your_migration_name>.resources" was correctly embedded or linked into assembly "<your_assembly_name>" at compile time, or that all the satellite assemblies required are loadable and fully signed.
```

Since the EF6 migration tool looks for the migration definition in the executable, don't forget to build the application after you've added a new migration to the application.

## EF6 migration commands

### Enable migrations
To enable migrations for your project, run:
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe migrations enable --project-dir <your_project_dir> --assembly <path_to_built_assembly_file> --config <path_to_assembly_config_file>
```
Example: 
```
.\toolchain\EntityFramework.6.5.1\tools\net45\any\ef6.exe migrations enable --project-dir .\main --assembly .\main\bin\Debug\net481\main.exe --config .\main\bin\Debug\net481\main.exe.config
```

### Add migrations
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe migrations add <Migration_Name> --project-dir <your_project_dir> --assembly <path_to_built_assembly_file> --config <path_to_assembly_config_file>
```
Example: 
```
.\toolchain\EntityFramework.6.5.1\tools\net45\any\ef6.exe migrations add AddRoleForPost --project-dir .\main --assembly .\main\bin\Debug\net481\main.exe --config .\main\bin\Debug\net481\main.exe.config
```

When the new migration is not added because of a allegedly pending explicit migrations but you're sure there is none, add the --connection-string and --connection-provider params to the command, like:
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe migrations add <Migration_Name> --project-dir <your_project_dir> --assembly <path_to_built_assembly_file> --config <path_to_assembly_config_file>  --connection-string <DB_connection_string> --connection-provider <connection_provider>
```
Example:
```
.\toolchain\EntityFramework.6.5.1\tools\net45\any\ef6.exe migrations add AddRoleForPost --project-dir .\main --assembly .\main\bin\Debug\net481\main.exe --config .\main\bin\Debug\net481\main.exe.config --connection-string "data source=.;initial catalog=Blogging;User Id=sa;Password=Password12;MultipleActiveResultSets=True;App=EntityFramework" --connection-provider "System.Data.SqlClient"
```

### List available migrations
To list migrations created for your project, run:
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe migrations list --project-dir <your_project_dir> --assembly <path_to_built_assembly_file> --config <path_to_assembly_config_file> --connection-string <DB_connection_string> --connection-provider <connection_provider>
```
Example:
```
.\toolchain\EntityFramework.6.5.1\tools\net45\any\ef6.exe migrations enable --project-dir .\main --assembly .\main\bin\Debug\net481\main.exe --config .\main\bin\Debug\net481\main.exe.config --connection-string "data source=.;initial catalog=Blogging;User Id=sa;Password=Password12;MultipleActiveResultSets=True;App=EntityFramework" --connection-provider "System.Data.SqlClient"
```

### Update database
```
.\<EntityFramework_toolchain_path>\tools\net45\any\ef6.exe database update --project-dir <your_project_dir> --assembly <path_to_built_assembly_file>  --config <path_to_assembly_config_file> --connection-string <DB_connection_string> --connection-provider <connection_provider>
```
Example:
```
.\toolchain\EntityFramework.6.5.1\tools\net45\any\ef6.exe database update --project-dir .\main --assembly .\main\bin\Debug\net481\main.exe --config .\main\bin\Debug\net481\main.exe.config --verbose --connection-string "data source=.;initial catalog=Blogging;User Id=sa;Password=Password12;MultipleActiveResultSets=True;App=EntityFramework" --connection-provider "System.Data.SqlClient"
```

--target switch allows selecting the migration explicitly, e.g. ```--target 202504131531442_AddRoleForPost```



