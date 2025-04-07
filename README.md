# Stock Management API

## Overview
The Stock Management API is designed to manage products, including creating, updating, retrieving, and deleting product details. It also includes functionality to manage product stock levels.

## Getting Started

### Prerequisites
- .NET Core SDK
- SQL Server

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/stock-management-api.git
2. Navigate to project 
cd stock-management-api

3.Restore the dependencies:
dotnet restore

4.Usage
Running the Application

dotnet run

5.API Endpoints

Create Product
Endpoint: POST /api/products
Example Request:
{
    "ProductId": 212556,
    "ProductName": "Product2",
    "Quantity": 20,
    "Priceperunit": 50
}
Example Response:
{
    "ProductId": 212556,
    "ProductName": "Product2",
    "Quantity": 20,
    "Priceperunit": 50
}

Get Products

Endpoint: GET /api/products
Example Request:
GET /api/products

Example Response:
[
    {
        "ProductId": 1,
        "ProductName": "Product1",
        "Quantity": 10,
        "Priceperunit": 50
    },
    {
        "ProductId": 2,
        "ProductName": "Product2",
        "Quantity": 20,
        "Priceperunit": 50
    }
]

Get Product
Endpoint: GET /api/products/{id}

Example Request:
GET /api/products/1

Example Response:
{
    "ProductId": 1,
    "ProductName": "Product1",
    "Quantity": 10,
    "Priceperunit": 50
}

Update Product

Endpoint: PUT /api/products/{id}
Example Request:
{
    "ProductId": 1,
    "ProductName": "Updated Product",
    "Quantity": 50,
    "Priceperunit": 20.0
}
Example Response:
{
    "ProductId": 1,
    "ProductName": "Updated Product",
    "Quantity": 50,
    "Priceperunit": 20.0
}

Delete Product
Endpoint: DELETE /api/products/{id}

Example Request:
DELETE /api/products/2
Example Response:
HTTP/1.1 200 OK

Decrement Stock
Endpoint: PUT /api/products/decrement-stock/{id}/{quantity}

Example Request:
PUT /api/products/decrement-stock/1/5
Example Response:
{
    "ProductId": 1,
    "ProductName": "Product1",
    "Quantity": 5,
    "Priceperunit": 50
}

Add To Stock

Endpoint: PUT /api/products/add-to-stock/{id}/{quantity}
Example Request:

PUT /api/products/add-to-stock/2/5
Example Response:
{
    "ProductId": 2,
    "ProductName": "Product2",
    "Quantity": 25,
    "Priceperunit": 50
}




