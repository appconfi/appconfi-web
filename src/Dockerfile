FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN ls
# Copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
COPY App.sln ./
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Infrastructure/*.csproj ./App.Infrastructure/
COPY App.Service/*.csproj ./App.Service/
COPY App.Web/*.csproj ./App.Web/
COPY App.Web.Core/*.csproj ./App.Web.Core/
COPY App.Service.Email.SendGrid/*.csproj ./App.Service.Email.SendGrid/
COPY App.SharedKernel/*.csproj ./App.SharedKernel/
COPY App.Domain.Test/*.csproj ./App.Domain.Test/
COPY App.Service.Importer/*.csproj ./App.Service.Importer/

# Restore
RUN dotnet restore

# Copy everything else
COPY . .

# Build 
RUN dotnet build "App.Web/App.Web.csproj" -c Release -o /app/build

# Copy everything else and build
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "App.Web.dll"]


