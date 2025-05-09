# Use official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

#COPY . .

# Create tool manifest and install EF Core tools
#RUN dotnet new tool-manifest
#RUN dotnet tool install --local dotnet-ef --version 7.0.0

# Install EF Core CLI tools
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"

# Copy and restore dependencies
COPY *.csproj ./
#COPY . .

RUN dotnet restore
#RUN dotnet restore ApiEmpleados.csproj

# Install Entity Framework Core CLI Tools
#RUN dotnet new tool-manifest
#RUN dotnet tool install --local dotnet-ef --version 7.0.0
#RUN dotnet tool restore  # Restore the installed tools

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
#RUN dotnet publish ApiEmpleados.csproj -c Release -o out

# Use runtime image to run the API (aspnet:7.0)
# Keep the SDK in the final container (so 'dotnet ef' works)
FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
#COPY --from=build /app .
COPY --from=build /app/out .

# Copy tool manifest to runtime container
#COPY --from=build /app/.config /app/.config

# Install the EF Core Tools
RUN dotnet tool install --global dotnet-ef --version 7.0.0

# Ensure EF tools are available
ENV PATH="$PATH:/root/.dotnet/tools"

# Expose port (make sure this matches the API's configured port)
EXPOSE 5000

# Run the API
CMD ["dotnet", "ApiEmpleados.dll"]
