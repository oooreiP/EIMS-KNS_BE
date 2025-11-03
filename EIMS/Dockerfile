# Stage 1: Build the application
# Use the official .NET SDK image, specifying the version (8.0)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and restore dependencies first for layer caching
# Copy only the .sln file and project files first
COPY *.sln .
COPY EIMS.API/*.csproj ./EIMS.API/
COPY EIMS.Application/*.csproj ./EIMS.Application/
COPY EIMS.Infrastructure/*.csproj ./EIMS.Infrastructure/
COPY EIMS.Domain/*.csproj ./EIMS.Domain/
# Add any other project files if necessary

# Restore dependencies for the entire solution
RUN dotnet restore EIMS.sln

# Copy the rest of the source code
COPY . .

# Build and publish the API project in Release configuration
WORKDIR /app/EIMS.API
RUN dotnet publish -c Release -o /app/out --no-restore

# Stage 2: Create the final runtime image
# Use the official ASP.NET Core runtime image, smaller than the SDK image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/out .

# Expose the port the application will listen on (ASP.NET Core defaults to 8080 in containers)
EXPOSE 8080

# (Optional but Recommended) Add a non-root user for security
RUN adduser --system --group appuser
USER appuser

# Define the entry point for the container
ENTRYPOINT ["dotnet", "EIMS.API.dll"]
