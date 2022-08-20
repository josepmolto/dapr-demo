FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY ./Book/src ./Book/src
COPY ./Common ./Common 
WORKDIR /app/Book/src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/Book/src/out .
ENTRYPOINT ["dotnet", "Book.dll"]
EXPOSE 80 