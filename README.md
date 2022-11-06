# Matrix Web Server

## How to run

1. install [dotnet 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. git clone
3. to build the solution, run ```dotnet build league_matrix.sln```
4. to run the web api, run ```dotnet run --project ./web/web.csproj```
5. to execute the unit tests, run ```dotnet test```

## Sample curl requests

* curl --insecure -F 'file=@./Test/Data/matrix.csv' "https://localhost:7295/echo"
* curl --insecure -F 'file=@./Test/Data/matrix.csv' "https://localhost:7295/invert"
* curl --insecure -F 'file=@./Test/Data/matrix.csv' "https://localhost:7295/flatten"
* curl --insecure -F 'file=@./Test/Data/matrix.csv' "https://localhost:7295/sum"
* curl --insecure -F 'file=@./Test/Data/matrix.csv' "https://localhost:7295/multiply"