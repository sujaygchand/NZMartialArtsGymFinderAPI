#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./NZMartialArtsGymFinderAPI.csproj", "./"]
RUN dotnet restore "./NZMartialArtsGymFinderAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./NZMartialArtsGymFinderAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./NZMartialArtsGymFinderAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "NZMartialArtsGymFinderAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet NZMartialArtsGymFinderAPI.dll