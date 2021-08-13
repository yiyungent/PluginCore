#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["examples/AspNetCore3_1/AspNetCore3_1.csproj", "examples/AspNetCore3_1/"]
# 其实可以不复制以下Project, 已配置 Release 时通过 nuget包
COPY ["src/PluginCore/PluginCore.csproj", "src/PluginCore/"]
COPY ["src/PluginCore.IPlugins/PluginCore.IPlugins.csproj", "src/PluginCore.IPlugins/"]
RUN dotnet restore "examples/AspNetCore3_1/AspNetCore3_1.csproj"
COPY . .
WORKDIR "/src/examples/AspNetCore3_1"
RUN dotnet build "AspNetCore3_1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCore3_1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCore3_1.dll"]