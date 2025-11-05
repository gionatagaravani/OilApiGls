
# OilApiGls - Brent Oil Price Trend API

  

A modern .NET 9.0 Web API that provides Brent oil price data through a JSON-RPC 2.0 interface.

  


## ⚡ Quick Start

  

### 📋 Prerequisites

- .NET 9.0 SDK

  

Step by step procedure:

  

```bash

# Clone the repository

git  clone  https://github.com/gionatagaravani/OilApiGls.git

cd  OilApiGls


# Restore dependencies and run

dotnet  restore

dotnet  run

```

  

The API will be available at:


-  **HTTP**: `http://localhost:5006`

-  **Swagger UI**: `http://localhost:5006/swagger`

  


## 📚 API Usage

  

### Endpoint

  

```

POST /api/OilPriceTrend

Content-Type: application/json

```

  

### Request Format (JSON-RPC 2.0)

  

```json

{

"id": 1,

"jsonrpc": "2.0",

"method": "GetOilPriceTrend",

"params": {

"StartDateISO8601": "2023-01-01",

"EndDateISO8601": "2023-01-31"

}

}

```

  

### Response Format

  

**Success Response:**

```json

{

"jsonrpc": "2.0",

"id": 1,

"result": {

"prices": [

{

"date": "2023-01-02",

"price": 85.42

},

{

"date": "2023-01-03",

"price": 86.15

}

]

}

}

```

  

**Error Response:**

```json

{

"jsonrpc": "2.0",

"id": 1,

"error": "Invalid date format. Use ISO 8601 format (ex: '2023-01-01')"

}

```

  

### Testing with cURL

  

```bash

# Test the API with cURL

curl  -X  POST  "https://localhost:7213/api/OilPriceTrend"  \

-H "Content-Type: application/json" \

-d  '{

"id": 1,

"jsonrpc": "2.0",

"method": "GetOilPriceTrend",

"params": {

"StartDateISO8601": "2023-01-01",

"EndDateISO8601": "2023-01-31"

}

}'  \

-k

```

  

### Using Swagger UI

  

1. Navigate to `https://localhost:7213/swagger`

2. Expand the `POST /api/OilPriceTrend` endpoint

3. Click "Try it out"

4. Edit the request body if needed

5. Click "Execute" to see the response

  

## ⚙️ Configuration

  

### Application Settings

  

The API behavior can be configured through `appsettings.json`:

  

```json

{

"Logging": {

"LogLevel": {

"Default": "Information",

"Microsoft.AspNetCore": "Warning"

}

},

"DataUrl": "https://glsitaly-download.s3.eu-central-1.amazonaws.com/MOBILE_APP/BrentDaily/brent-daily.json",

"AllowedHosts": "*"

}

```

 
  


## 🧪 Development Notes

  

### Running Tests

 
```bash

# Run all tests

dotnet  test

 
# Run with detailed output

dotnet  test  --verbosity  normal

```
