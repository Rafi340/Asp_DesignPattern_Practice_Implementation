FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
RUN apt update && apt install -y nodejs
COPY ["Demo.Web/Demo.Web.csproj", "Demo.Web/"]
COPY ["Demo.Domain/Demo.Domain.csproj", "Demo.Domain/"]
COPY ["Demo.Application/Demo.Application.csproj", "Demo.Application/"]
COPY ["Demo.Infrastructure/Demo.Infrastructure.csproj", "Demo.Infrastructure/"]
RUN dotnet restore "Demo.Web/Demo.Web.csproj"


COPY . .
WORKDIR "/src/Demo.Web"
RUN dotnet build "Demo.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT [ "dotnet", "Demo.Web.dll" ]
