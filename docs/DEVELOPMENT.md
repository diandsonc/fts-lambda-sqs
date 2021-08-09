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

    Note: You can use `--payload <payload>` to execute auto file or in vs code use `ctrl + shift + D` and choose the profile project to start

* Testing:

    ```shell
    dotnet test --no-restore --logger trx //p:CollectCoverage=true //p:CoverletOutputFormat=opencover /p:Exclude=[xunit.*]*
    ```

* Deploy lambda:

    ```shell
    dotnet lambda deploy-function <my-function>
    ```