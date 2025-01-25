# ScopedLogging

ScopedLogging makes it possible to easily write scoped parameters to all log messages within a specific scope.  
It provides extension methods for `ILogger<T>` to simplify adding contextual information to log entries.

---

## Features

- **Scoped logging:** Attach contextual parameters to all log messages within a defined scope.
- **Flexible initialization:** Initialize scopes with zero, one, or multiple parameters.
- **Automatic cleanup:** Ensures proper disposal of logging scopes.
- **Simple API:** Easy-to-use extension methods for `ILogger<T>`.

---

## Installation

You can install the package via NuGet (replace with actual package name if published):

```sh
  dotnet add package KudAndrii.Logger.ScopedLogging
```

Then, include the necessary namespace in your project:

```csharp
using KudAndrii.Logger.ScopedLogging.Extensions;
```

---

## Usage

### Basic Usage

```csharp
using (var scopesBag = logger.InitScopes())
{
    scopesBag.AppendScope("UserId", 12345);
    logger.LogInformation("User authenticated.");
    // Logs will include UserId.

    scopesBag.AppendScope("Age", 18);
    logger.LogInformation("Process completed.");
    // Logs will include UserId along with Age.
}
```

### Initializing with Parameters

```csharp
using (var scopesBag = logger.InitScopes("UserId", 12345))
{
    logger.LogInformation("User logged in.");
    // Logs will include UserId.

    scopesBag.AppendScope("SessionId", "XYZ789");
    logger.LogInformation("Session started.");
    // Logs will include UserId and SessionId.
}
```

### Initializing with Multiple Parameters

```csharp
using (var scopesBag = logger.InitScopes(("UserId", 12345), ("Role", "Admin")))
{
    logger.LogInformation("User dashboard accessed.");
    // Logs will include UserId and Role.
}
```

---

## API Reference

### `ScopesBag<TCategoryName>` Class

#### Overview

A helper class for managing logging scopes, allowing the addition of contextual parameters to log messages.

#### Methods

- **`AppendScope(string propertyName, object? propertyValue)`**  
  Adds a single scoped parameter to all subsequent log messages.  
  _Example:_
  ```csharp
  scopesBag.AppendScope("UserId", 12345);
  ```

- **`AppendScope(params (string PropertyName, object? PropertyValue)[] args)`**  
  Adds multiple scoped parameters.  
  _Example:_
  ```csharp
  scopesBag.AppendScope(("UserId", 12345), ("Role", "Admin"));
  ```

---

## Extension Methods

The ScopedLogging library provides the following extension methods for `ILogger<T>`:

### `InitScopes<T>(this ILogger<T> logger)`

Creates a new `ScopesBag<T>` instance to manage log scopes.

```csharp
using (var scopesBag = logger.InitScopes())
{
    logger.LogInformation("Log with default scope");
}
```

---

### `InitScopes<T>(this ILogger<T> logger, string propertyName, object? propertyValue)`

Creates a new `ScopesBag<T>` and adds an initial scope parameter.

```csharp
using (var scopesBag = logger.InitScopes("UserId", 12345))
{
    logger.LogInformation("Log with UserId scope");
}
```

---

### `InitScopes<T>(this ILogger<T> logger, params (string PropertyName, object? PropertyValue)[] args)`

Creates a new `ScopesBag<T>` and adds multiple initial scope parameters.

```csharp
using (var scopesBag = logger.InitScopes(("UserId", 12345), ("Role", "Admin")))
{
    logger.LogInformation("Log with UserId and Role scopes");
}
```

---

## Best Practices

- **Use short-lived scopes:**  
  Always wrap scopes in `using` statements to ensure proper disposal.

- **Minimize scope size:**  
  Avoid adding too many properties to prevent performance overhead.

- **Consistent naming:**  
  Use a consistent naming pattern for scoped properties to improve log readability.

---

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch with your feature or bug fix.
3. Submit a pull request.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
