# Runnin the project

* Install AWS .NET Mock Lambda Test Tool

    ```shell
    dotnet tool install -g Amazon.Lambda.TestTool-3.1
    ```
	
	Note: use `dotnet tool update -g Amazon.Lambda.TestTool-3.1` to update your version.

* Running:

    ```shell
    dotnet lambda-test-tool-3.1 --path <absolute-directory> --port <port-number> --no-launch-window 
    ```

    Note: You can use `--payload <payload>` to execute auto file
	Note: In vs code use F5 and choose the profile project to start