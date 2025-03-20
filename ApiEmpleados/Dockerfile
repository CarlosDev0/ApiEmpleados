# Use official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy and restore dependencies:  
COPY *.csproj ./   #Line for render
#COPY /ApiEmpleados/*.csproj ./   #Line for local Docker
RUN dotnet restore

# Copy everything else and build: RUN dotnet publish -c Release -o out
COPY . ./
WORKDIR "/app/ApiEmpleados"

#RUN dotnet build "ApiEmpleados.csproj" -c $BUILD_CONFIGURATION -o /app/build
#FROM build AS publish
#ARG $BUILD_CONFIGURATION
#RUN dotnet publish "ApiEmpleados.csproj" -c $BUILD_CONFIGURATION -o /app/publish

RUN dotnet publish -c Release -o out

# Use runtime image to run the API
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port (make sure this matches the API's configured port) 8080
EXPOSE 5178

# Run the API
CMD ["dotnet", "ApiEmpleados.dll"]
