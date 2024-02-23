# Datastructures <!-- omit in toc -->

This library contains a set of useful implementations that can be used in
multiple different projects.

- [1. Features](#1-features)
  - [1.1. CRC Algorithms](#11-crc-algorithms)
  - [1.2. Bit Operations](#12-bit-operations)
  - [1.3. IDeepClonable](#13-ideepclonable)
  - [1.4. Result](#14-result)
  - [1.5. Semantic Versioning](#15-semantic-versioning)
  - [1.6. INamedItem and NamedItemCollection](#16-inameditem-and-nameditemcollection)
  - [1.7. EventLog](#17-eventlog)
- [2. Release History](#2-release-history)
  - [2.1. Version 0.2.1](#21-version-021)
  - [2.2. Version 0.2.0](#22-version-020)

## 1. Features

### 1.1. CRC Algorithms

Provides various values for CRC16 and CRC32 algorithms. It can use precomputed
tables, or algorithms where the user provides the polynomials, seeds and any
inversions.

See [Benchmarks](./docs/BitOperations.md) for performance metrics.

### 1.2. Bit Operations

Support for converting byte buffers into `short`, `ushort`, `int`, `uint`,
`long`, `ulong`, big endian or little endian. Newer .NET Core versions bring
similar algorithms. The implementation provides unsafe (with faster performance).

See [Benchmarks](./docs/BitOperations.md) for performance metrics.

### 1.3. IDeepClonable

Provide an interface that implies an object is deep clonable (as opposed to the
standard .NET `MemberwiseClone` which is a shallow clone).

### 1.4. Result

The `Result` is an interesting datatype that takes inspiration from modern C++20
and Rust, where a result is not a value type, and one normally encodes errors as
either `null` or `-1`, but now one can encode an error, or the correct value,
directly in to the result of a function call.

When defining a function return a `Result<int>` for example, the function can
just return the value, as it would if the return type would be an `int`. If an
exception occurs, the exception would be returned with `FromException`.

```csharp
public Result<int> GetValue(bool isError) {
    if (isError)
        return Result.FromException<int>(new InvalidArgumentException("there's an error"));
    return 42;
}

var r = GetValue(false);
if (!r.HasValue)
    Console.WriteLine($"Error - {r.Error?.Message}");
else
    Console.WriteLine($"Value = {r.Value}");

if (!r.TryGet(out int v)))
    Console.WriteLine($"Error - {r.Error?.Message}");
else
    Console.WriteLine($"Value = {v}");
```

### 1.5. Semantic Versioning

Use `SemVer` or `SemVer2` to manage versions using Semantic Versioning rules.
Version comparisons can be made, and additional version information can be
added.

### 1.6. INamedItem and NamedItemCollection

A collection of objects deriving from `INamedItem`. Derive from
`Collections.Generic.NamedItemCollection` to handle a collection of objects with
a name.

### 1.7. EventLog

Manage an event log, that events can be written as they occur, so typically, a
user interface will be notified of events and can update its controls
accordingly. It implements an `IList<IEvent<T>>, INotifyCollectionChanged`.

It can help by maintaining the maximum severity that is logged, such that
actions can be performed if an error or a critical event was logged.

## 2. Release History

### 2.1. Version 0.2.1

Quality:

- Add README.md reference to NuGet package (DOTNET-811)
- Apply SemVer fixes for CA1861 (DOTNET-833)
- Update .NET 4.5 to 4.6.2 (DOTNET-827, DOTNET-906) and .NET Standard 2.1 to
  .NET 6.0 (DOTNET-936, DOTNET-941, DOTNET-942, DOTNET-945)
- Apply aggressive inlining for .NET Framework 4.6.2 (DOTNET-871)

### 2.2. Version 0.2.0

- Initial Version
