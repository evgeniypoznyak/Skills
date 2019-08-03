FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Skills.API/Skills.API.csproj Skills.API/
COPY Skills.Domain/Skills.Domain.csproj Skills.Domain/
COPY Skills.Infrastructure/Skills.Infrastructure.csproj Skills.Infrastructure/
COPY Skills.Infrastructure.Tests/Skills.Infrastructure.Tests.csproj Skills.Infrastructure.Tests/
RUN dotnet restore Skills.API/Skills.API.csproj
COPY . .
WORKDIR /src/Skills.API
RUN dotnet build Skills.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Skills.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skills.API.dll"]
