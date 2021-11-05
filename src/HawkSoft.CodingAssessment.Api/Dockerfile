#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/HawkSoft.CodingAssessment.Api/HawkSoft.CodingAssessment.Api.csproj", "src/HawkSoft.CodingAssessment.Api/"]
COPY ["src/HawkSoft.CodingAssessment/HawkSoft.CodingAssessment.csproj", "src/HawkSoft.CodingAssessment/"]
RUN dotnet restore "src/HawkSoft.CodingAssessment.Api/HawkSoft.CodingAssessment.Api.csproj"
COPY . .
WORKDIR "/src/src/HawkSoft.CodingAssessment.Api"
RUN dotnet build "HawkSoft.CodingAssessment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HawkSoft.CodingAssessment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HawkSoft.CodingAssessment.Api.dll"]