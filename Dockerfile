#################################################################################
# Globals
#################################################################################
ARG DOTNET_STARTUP_PROJECT="./Src/WebApi/WebApi.csproj"

#################################################################################
# Stage 1: Base Runtime
# This image contains only what is needed to run the app
#################################################################################
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
ARG DOTNET_STARTUP_PROJECT

WORKDIR /app
EXPOSE 8080

RUN echo -e "Variablen-Check:\n \
    DOTNET_STARTUP_PROJECT: ${DOTNET_STARTUP_PROJECT}\n"

#################################################################################
# Stage 2: Build
# This image contains the full SDK to compile the code
#################################################################################
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG DOTNET_STARTUP_PROJECT

RUN echo -e "Variablen-Check:\n \
    DOTNET_STARTUP_PROJECT: ${DOTNET_STARTUP_PROJECT}\n"

WORKDIR /src
# Copy files, restore dependencies and build
COPY . .
# Remove the clear step after everything is working to speed up subsequent builds
# RUN dotnet nuget locals all --clear
RUN dotnet restore ${DOTNET_STARTUP_PROJECT}
RUN dotnet build ${DOTNET_STARTUP_PROJECT} -c Release -o /app/build

#################################################################################
# Stage 3: Publish
#################################################################################
FROM build AS publish
ARG DOTNET_STARTUP_PROJECT

RUN echo -e "Variablen-Check:\n \
    DOTNET_STARTUP_PROJECT: ${DOTNET_STARTUP_PROJECT}\n"

RUN dotnet publish ${DOTNET_STARTUP_PROJECT} -c Release -o /app/publish /p:UseAppHost=false 

#################################################################################
# Stage 4: Final Image
#################################################################################
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]