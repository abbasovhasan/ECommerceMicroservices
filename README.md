---

# ECommerce Microservices

This project implements a modular e-commerce platform using a microservices architecture. Built with .NET, it offers a range of services typical of an online store, such as catalog management, ordering, and user authentication via an identity server. Each service is designed to be self-contained, enabling scalability and ease of management.

## Key Features

- **Service-oriented Architecture**: Each feature is separated into independent services.
- **Identity and Authentication**: Includes an identity server for user authentication and authorization.
- **Basket and Order Management**: Supports shopping cart functionality and order processing.
- **Catalog and Product Management**: Manages product listings and details.
- **Photo Stock Management**: Handles product image storage and retrieval.

## Project Structure

- **IdentityServer**: Manages user identities, authentication, and authorization.
- **Services**:
  - **Basket**: Manages shopping cart data and operations.
  - **Catalog**: Handles product catalog information, including categories, prices, and inventory.
  - **Order**: Processes orders and manages order history and status.
  - **PhotoStock**: Manages image storage for product photos.

- **Solution File** (`Code_Academy_Final_Project.sln`): Main solution file to manage all services within the project.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) - Version 5.0 or later recommended
- Docker (optional) for containerized services
- Database for each service as specified in individual configurations (e.g., SQL Server, PostgreSQL)

## Getting Started

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd ECommerceMicroservices
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Run Each Microservice**:
   Each service can be run independently or as part of the entire solution using Docker Compose or Kubernetes. To run each service individually:
   - Navigate to the service directory:
     ```bash
     cd Services/Catalog
     ```
   - Start the service:
     ```bash
     dotnet run
     ```

4. **Database Migrations** (if applicable):
   If the service requires a database, ensure migrations are applied:
   ```bash
   dotnet ef database update
   ```

5. **Running the Identity Server**:
   Identity Server must be running to authenticate and authorize users. Navigate to the `IdentityServer` directory and start the service:
   ```bash
   cd IdentityServer
   dotnet run
   ```

6. **Testing API Endpoints**:
   Each service exposes its own set of API endpoints for managing the respective e-commerce functions. You can use Postman or similar tools to interact with each service.

## Microservices Overview

### Identity Server

- **Authentication & Authorization**: Provides OAuth2 and OpenID Connect for secure user login.
- **Endpoints**: Manages user registration, login, and role-based access.

### Catalog Service

- **Product Management**: Allows CRUD operations on products.
- **API Endpoints**: 
  - `GET /products`: List all products
  - `POST /products`: Add a new product
  - `PUT /products/{id}`: Update product details
  - `DELETE /products/{id}`: Delete a product

### Basket Service

- **Shopping Cart Management**: Enables users to add, update, and delete items in their shopping cart.
- **API Endpoints**:
  - `GET /basket/{userId}`: Retrieve basket for a user
  - `POST /basket`: Add item to basket
  - `PUT /basket/{userId}/item/{itemId}`: Update item quantity
  - `DELETE /basket/{userId}/item/{itemId}`: Remove item from basket

### Order Service

- **Order Processing**: Manages order placement, status, and history.
- **API Endpoints**:
  - `POST /order`: Place an order
  - `GET /order/{userId}`: Get orders by user
  - `GET /order/status/{orderId}`: Retrieve order status

### PhotoStock Service

- **Image Management**: Handles uploading, storing, and retrieving product images.
- **API Endpoints**:
  - `POST /photo`: Upload a new image
  - `GET /photo/{photoId}`: Retrieve a photo by ID

## Configuration

- **appsettings.json**: Each service has its own `appsettings.json` for configuration, including database connection strings, API keys, and other settings.
- **Docker**: To use Docker, refer to individual `Dockerfile` configurations for each service (if provided).

## Running with Docker Compose

You can use Docker Compose to start all services together:
1. Make sure Docker is running.
2. Run the following command:
   ```bash
   docker-compose up
   ```

This will start each service in a separate container and link them according to the configuration in `docker-compose.yml` (if available).

## API Documentation

If Swagger or similar tools are set up, you can access API documentation for each service at:
- `http://localhost:<port>/swagger`

Replace `<port>` with the actual port for each service as specified in its configuration.

## Contributing

Contributions are welcome! Please fork the repository, create a branch for your feature or fix, and submit a pull request with a detailed description.

---
