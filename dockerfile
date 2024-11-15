FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./star-wars.csproj", "STAR-WARS/"]
RUN dotnet restore "STAR-WARS/star-wars.csproj"
COPY . .
RUN dotnet build "star-wars.csproj" -c Release -o /app/build -r linux-x64

# FROM build AS publish
# RUN dotnet publish "star-wars.csproj" -c Release -o /app/publish -r linux-x64

FROM base AS final

WORKDIR /app
COPY --from=build /app/build .
COPY ./wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "star-wars.dll"]