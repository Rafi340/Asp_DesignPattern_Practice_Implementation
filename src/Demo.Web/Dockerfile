FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
RUN apt update && apt install -y nodejs
COPY ["Demo.Web/Demo.Web.csproj", "Demo.Web/"]
COPY ["Demo.Domain/Demo.Domain.csproj", "Demo.Domain/"]
COPY ["Demo.Application/Demo.Application.csproj", "Demo.Application/"]
COPY ["Demo.Infrastructure/Demo.Infrastructure.csproj", "Demo.Infrastructure/"]
RUN dotnet restore "Demo.Web/Demo.Web.csproj"

