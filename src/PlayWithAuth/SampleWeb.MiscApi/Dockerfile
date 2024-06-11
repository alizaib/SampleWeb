#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#To see what each command does in this file go to [Dockerfile reference] https://docs.docker.com/engine/reference/builder/
#The WORKDIR instruction sets a working directory for other Dockerfile instructions, such as RUN, CMD, and also the working directory 
#for running instances of the container image

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SampleWeb.MiscApi/SampleWeb.MiscApi.csproj", "SampleWeb.MiscApi/"]
RUN dotnet restore "SampleWeb.MiscApi/SampleWeb.MiscApi.csproj"
COPY . .
WORKDIR "/src/SampleWeb.MiscApi"
RUN dotnet build "SampleWeb.MiscApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SampleWeb.MiscApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleWeb.MiscApi.dll"]