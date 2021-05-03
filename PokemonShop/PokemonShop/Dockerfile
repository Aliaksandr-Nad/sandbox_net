# syntax=docker/dockerfile:experimental
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app
    
COPY *.sln nuget.config  ./
COPY *.csproj ./

RUN dotnet restore PokemonShop.sln

ARG CONFIGURATION=Release
COPY . ./
RUN dotnet build \
    --no-restore \
    --configuration ${CONFIGURATION} \
    PokemonShop.sln
RUN dotnet publish \
    PokemonShop.csproj \
    --no-restore \
    --configuration ${CONFIGURATION} \
    --output /app/out 
    
FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0 AS runtime
WORKDIR /app

EXPOSE 5000
COPY --from=build /app/out .
CMD [ "dotnet", "PokemonShop.dll" ]