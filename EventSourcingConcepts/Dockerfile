FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventSourcingConcepts/EventSourcingConcepts.csproj", "EventSourcingConcepts/"]
RUN dotnet restore "EventSourcingConcepts/EventSourcingConcepts.csproj"
COPY . .
WORKDIR "/src/EventSourcingConcepts"
RUN dotnet build "EventSourcingConcepts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventSourcingConcepts.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventSourcingConcepts.dll"]
