# docker build -t evgeniy/dotnet-skills .
# docker run -p 1111:80 -d --env-file ./.env --name evgeniy-dotnet-skills  --network evgeniy_poznyaks_com  evgeniy/dotnet-skills

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Skills/Skills.csproj Skills/
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
