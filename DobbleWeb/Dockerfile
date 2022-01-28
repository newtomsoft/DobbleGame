#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DobbleWeb/DobbleWeb.csproj", "DobbleWeb/"]
COPY ["SignalR/SignalR.csproj", "SignalR/"]
COPY ["DobbleManager/DobbleManager.csproj", "DobbleManager/"]
RUN dotnet restore "DobbleWeb/DobbleWeb.csproj"
COPY . .
WORKDIR "/src/DobbleWeb"
RUN dotnet build "DobbleWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DobbleWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DobbleWeb.dll"]