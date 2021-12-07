# Bit Operations

The Datastructures library provides some functions that support reading bytes in
an array to native types. This is useful for marshalling data, especially from
streams.

## Performance

Performance results are obtained with microbenchmarking Using
[BenchmarkDotNet](https://benchmarkdotnet.org/).

### Results

```text
Results = net48

BenchmarkDotNet=v0.13.1 OS=Windows 10.0.19043.1348 (21H1/May2021Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
```

```text
Results = netcore31

BenchmarkDotNet=v0.13.1 OS=Windows 10.0.19043.1348 (21H1/May2021Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT
```

| Project 'datastructures' Type | Method                  | mean (net48) | stderr  | mean (netcore31) | stderr  |
|:------------------------------|:------------------------|-------------:|--------:|-----------------:|--------:|
| BitOperationsBenchmark        | Copy16Pointer           | 0.31         | 0.00    | 0.18             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 1.19         | 0.01    | 0.19             | 0.00    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.30         | 0.01    | 0.30             | 0.01    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.43         | 0.00    | 0.51             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 0.43         | 0.01    | 0.36             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.41         | 0.00    | 0.49             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 1.17         | 0.01    | 0.33             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 1.19         | 0.01    | 0.46             | 0.01    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 1.31         | 0.01    | 0.40             | 0.01    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 3.01         | 0.01    | 0.51             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.85         | 0.01    | 1.52             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 2.75         | 0.01    | 2.38             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 1.17         | 0.01    | 0.25             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 1.15         | 0.01    | 0.26             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 1.57         | 0.00    | 0.09             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 3.56         | 0.02    | 0.96             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 2.15         | 0.01    | 0.71             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 4.14         | 0.02    | 2.80             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 1.21         | 0.00    | 0.01             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 1.19         | 0.01    | 0.00             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 1.55         | 0.01    | 0.30             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 3.59         | 0.02    | 0.96             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 2.13         | 0.01    | 0.86             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 4.17         | 0.01    | 2.81             | 0.02    |
| BitOperationsBenchmark        | Copy16Shift             | 1.59         | 0.01    | 0.03             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 1.56         | 0.01    | 0.28             | 0.01    |
| BitOperationsBenchmark        | Copy32Shift             | 1.82         | 0.01    | 0.32             | 0.01    |
| BitOperationsBenchmark        | Copy64Shift             | 4.13         | 0.02    | 0.90             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShift        | 2.44         | 0.01    | 0.71             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShift        | 4.44         | 0.02    | 2.84             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -       | 0.55             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -       | 0.29             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -       | 0.66             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -       | 2.58             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -       | 1.00             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -       | 2.92             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -       | 0.29             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -       | 0.55             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -       | 0.62             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -       | 1.05             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -       | 1.01             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -       | 2.94             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -       | 0.57             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -       | 0.32             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -       | 0.50             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -       | 1.06             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -       | 0.94             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -       | 2.97             | 0.02    |

### Interpretation

The `DangerousCopyXXPointer` methods are by far the fastest, as they perform
type casts and copy the data with the native type. The operations done without
type casting are sub-nanosecond speeds.

The test case `Copy16Pointer` and `Copy16PointerFrom32` only differ that the
input is a `short` or an `int`, where the `int` is slower. This indicates that
one should be careful to use the correct type for an input.

Otherwise, for the non-dangerous methods, we can see that for .NET 4.0:

* 16-bit and 32-bit are the same performance impact.
* 64-bit is twice as slow as 32-bit.
* The BigEndian and LittleEndian make no difference, as expected as each byte is
  read and converted manually
* The conditional version is 0.3ns slower, which is the cost of doing an `if`
  check

Aggressive inline has made a significant difference in performance for .NET Core.
