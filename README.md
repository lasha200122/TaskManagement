# TaskManagement

To Run this Project you have to change appSettings.Development configuration.

```json
  "DatabaseSettings": {
    "SQLServer": "Server=Lasha\\SQLEXPRESS;Database=TaskManagement;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
  },
```

Update SQLServer -> Server value to match your Local DB server.

also in this project we use Redis and it is set to default redis credentials, If your system have some other values you have to update this as well

```json
  "RedisSettings": {
    "ConnectionString": "127.0.0.1:6379",
    "SessionTimeout": 60,
    "Password": " "
  },
```

## Project Architecture

### Layers
This Project have 4 layers

- Api
- Application
- Infrastructure
- Domain

  ### Nugets
  Nuget Libraries:
  - MediatR
  - ErrorOr
  - Fluent Validator
 
