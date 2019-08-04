FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Skills/Skills.csproj Skills/
COPY Skills.Domain/Skills.Domain.csproj Skills.Domain/
COPY Skills.Infrastructure/Skills.Infrastructure.csproj Skills.Infrastructure/
COPY Skills.Infrastructure.Tests/Skills.Infrastructure.Tests.csproj Skills.Infrastructure.Tests/
RUN dotnet restore Skills/Skills.csproj
COPY . .
WORKDIR /src/Skills
RUN dotnet build Skills.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Skills.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skills.dll"]
