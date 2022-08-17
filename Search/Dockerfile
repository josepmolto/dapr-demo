FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY ./Search/src ./Search/src
COPY ./Common ./Common 
WORKDIR /app/Search/src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/Search/src/out
COPY --from=build-env /app/Search/src/out .
ENTRYPOINT ["dotnet", "Search.dll"]
EXPOSE 80 