FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY ./Consumer/src ./Consumer/src
COPY ./Common ./Common 
WORKDIR /app/Consumer/src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/Consumer/src/out
COPY --from=build-env /app/Consumer/src/out .
ENTRYPOINT ["dotnet", "Consumer.dll"]
EXPOSE 80 