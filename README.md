# Order Management System API

## Overview

The Order Management System (OMS) is a .NET Core API that allows customers to place orders, view their order history, and allows administrators to manage orders. The system includes business logic for tiered discounts, inventory updates, order validation, multiple payment methods, and generating order invoices. It also implements role-based access control (RBAC) to manage user permissions.

## Table of Contents

- [Overview](#overview)
- [Endpoints](#endpoints)
  - [Customer Endpoints](#customer-endpoints)
  - [Order Endpoints](#order-endpoints)
  - [Product Endpoints](#product-endpoints)
  - [Invoice Endpoints](#invoice-endpoints)
  - [User Endpoints](#user-endpoints)
- [Business Logic](#business-logic)
- [Role-Based Access Control (RBAC)](#role-based-access-control-rbac)
- [Testing](#testing)
- [Setup](#setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Endpoints

### Customer Endpoints

- **POST /api/customers**

  - Description: Create a new customer.
  - Request Body:
    ```json
    {
      "name": "string",
      "email": "string",
      "password": "string"
    }
    ```
  - Response:
    ```json
    {
      "id": "int",
      "name": "string",
      "email": "string",
      "appUserId": "guid"
    }
    ```

- **GET /api/customers/{customerId}/orders**
  - Description: Get all orders for a customer.
  - Response:
    ```json
    [
      {
        "orderId": "int",
        "customerId": "int",
        "orderDate": "datetime",
        "totalAmount": "decimal",
        "orderItems": [
            {
                "id": "int",
                "orderId": "int",
                "productId": "int",
                "quantity": "int",
                "unitPrice": "decimal",
                "discount": "decimal",
            }
        ],
        "paymentMethod": "int",
        ...
      }
    ]
    ```

### Order Endpoints

- **POST /api/orders**

  - Description: Create a new order.
  - Request Body:
    ```json
    {
      "paymentMethod": "int",
      "orderItems": [
        {
          "productId": "int",
          "quantity": "int"
        }
      ]
    }
    ```
  - Response:
    ```json
    {
      "orderId": "int",
        "customerId": "int",
        "orderDate": "datetime",
        "totalAmount": "decimal",
        "orderItems": [
            {
                "id": "int",
                "orderId": "int",
                "productId": "int",
                "quantity": "int",
                "unitPrice": "decimal",
                "discount": "decimal",
            }
        ],
        "paymentMethod": "int",
      ...
    }
    ```

- **GET /api/orders/{orderId}**

  - Description: Get details of a specific order.
  - Response:
    ```json
    {
           "orderId": "int",
        "customerId": "int",
        "orderDate": "datetime",
        "totalAmount": "decimal",
        "orderItems": [
            {
                "id": "int",
                "orderId": "int",
                "productId": "int",
                "quantity": "int",
                "unitPrice": "decimal",
                "discount": "decimal",
            }
        ],
        "paymentMethod": "int",
      ...
    }
    ```

- **GET /api/orders**

  - Description: Get all orders (admin only).
  - Response:
    ```json
    [
      {
        "orderId": "guid",
        "orderDate": "datetime",
        "totalAmount": "decimal",
        ...
      }
    ]
    ```

- **PUT /api/orders/{orderId}/status**
  - Description: Update order status (admin only).
  - Request Body:
    ```json
    {
      "status": "string"
    }
    ```
  - Response:
    ```json
    {
      "orderId": "guid",
      "status": "string",
      ...
    }
    ```

### Product Endpoints

- **GET /api/products**

  - Description: Get all products.
  - Response:
    ```json
    [
      {
        "productId": "int",
        "name": "string",
        "price": "decimal",
        ...
      }
    ]
    ```

- **GET /api/products/{productId}**

  - Description: Get details of a specific product.
  - Response:
    ```json
    {
      "productId": "int",
      "name": "string",
      "price": "decimal",
      ...
    }
    ```

- **POST /api/products**

  - Description: Add a new product (admin only).
  - Request Body:
    ```json
    {
      "name": "string",
      "price": "decimal",
      "inventory": "int"
    }
    ```
  - Response:
    ```json
    {
      "productId": "guid",
      "name": "string",
      "price": "decimal",
      ...
    }
    ```

- **PUT /api/products/{productId}**
  - Description: Update product details (admin only).
  - Request Body:
    ```json
    {
      "name": "string",
      "price": "decimal",
      "inventory": "int"
    }
    ```
  - Response:
    ```json
    {
      "productId": "guid",
      "name": "string",
      "price": "decimal",
      ...
    }
    ```

### Invoice Endpoints

- **GET /api/invoices/{invoiceId}**

  - Description: Get details of a specific invoice (admin only).
  - Response:
    ```json
    {
      "invoiceId": "int",
      "orderId": "int",
      "amount": "decimal",
      ...
    }
    ```

- **GET /api/invoices**
  - Description: Get all invoices (admin only).
  - Response:
    ```json
    [
      {
        "invoiceId": "int",
        "orderId": "int",
        "amount": "decimal",
        ...
      }
    ]
    ```

### User Endpoints

- **POST /api/users/register**

  - Description: Register a new user.
  - Request Body:
    ```json
    {
      "username": "string",
      "password": "string",
      "email": "string"
    }
    ```
  - Response:
    ```json
    {
      "userId": "guid",
      "username": "string",
      "email": "string"
    }
    ```

- **POST /api/users/login**
  - Description: Authenticate a user and return a JWT token.
  - Request Body:
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
  - Response:
    ```json
    {
      "token": "string"
    }
    ```

## Business Logic

- **Tiered Discounts**: Discounts applied based on order amount.
- **Inventory Updates**: Inventory is updated when an order is placed.
- **Order Validation**: Ensures order details are correct before processing.
- **Multiple Payment Methods**: Supports various payment methods like credit card, PayPal, etc.
- **Order Invoices**: Generates invoices for orders.

## Role-Based Access Control (RBAC)

- **Admin**: Can manage orders, products, and invoices.
- **Customer**: Can place orders and view their order history.

## Testing

This API is tested using xUnit. Tests cover all endpoints and business logic.

## Setup

1. Clone the repository.
   ```sh
   git clone https://github.com/abdullahmokhtar/Order-Management-System.git
   ```
2. Navigate to the project directory.
   ```sh
   cd order-management-system
   ```
3. Install dependencies.
   ```sh
   dotnet restore
   ```
4. Run the application.
   ```sh
   dotnet run
   ```

## Usage

To use the API, you can use tools like Postman or cURL to send HTTP requests to the endpoints described above.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request.

## License

This project is licensed under the MIT License.
