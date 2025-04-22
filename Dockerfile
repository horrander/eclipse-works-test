FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.sln .
COPY ./source ./source
RUN dotnet restore 
RUN dotnet publish --no-restore -c Release --property:PublishDir=/app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "EclipseWorks.WebApi.dll"]
