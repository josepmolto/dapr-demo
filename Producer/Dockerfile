FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY ./Producer/src ./Producer/src
COPY ./Common ./Common 
WORKDIR /app/Producer/src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/Producer/src/out
COPY --from=build-env /app/Producer/src/out .
ENTRYPOINT ["dotnet", "Producer.dll"]
EXPOSE 80 