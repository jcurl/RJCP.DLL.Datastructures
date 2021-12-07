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
| BitOperationsBenchmark        | Copy16Pointer           | 0.30         | 0.01    | 0.40             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 1.18         | 0.01    | 1.46             | 0.01    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.34         | 0.01    | 0.50             | 0.01    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.41         | 0.01    | 0.50             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 0.41         | 0.00    | 0.52             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.39         | 0.01    | 0.38             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 1.15         | 0.01    | 1.24             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 1.17         | 0.01    | 1.30             | 0.01    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 1.38         | 0.02    | 1.65             | 0.01    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 3.03         | 0.01    | 2.92             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.83         | 0.01    | 1.84             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 2.75         | 0.01    | 2.76             | 0.02    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 1.17         | 0.01    | 1.46             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 1.16         | 0.01    | 1.05             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 1.38         | 0.01    | 1.62             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 3.56         | 0.02    | 3.61             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 2.17         | 0.01    | 1.58             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 4.11         | 0.02    | 4.12             | 0.02    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 1.18         | 0.01    | 1.03             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 1.19         | 0.01    | 1.05             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 1.56         | 0.01    | 1.48             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 3.59         | 0.02    | 3.93             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 2.14         | 0.01    | 1.90             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 4.15         | 0.02    | 4.11             | 0.02    |
| BitOperationsBenchmark        | Copy16Shift             | 1.58         | 0.01    | 1.44             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 1.57         | 0.01    | 1.16             | 0.01    |
| BitOperationsBenchmark        | Copy32Shift             | 1.80         | 0.01    | 1.57             | 0.01    |
| BitOperationsBenchmark        | Copy64Shift             | 4.09         | 0.02    | 3.61             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShift        | 2.43         | 0.01    | 1.56             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShift        | 4.44         | 0.02    | 4.13             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -       | 2.29             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -       | 2.15             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -       | 2.44             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -       | 3.58             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -       | 2.43             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -       | 4.17             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -       | 2.34             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -       | 2.22             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -       | 2.46             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -       | 3.70             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -       | 2.36             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -       | 4.16             | 0.02    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -       | 1.88             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -       | 1.82             | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -       | 2.17             | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -       | 3.57             | 0.02    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -       | 2.26             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -       | 4.13             | 0.02    |

### Interpretation

The `DangerousCopyXXPointer` methods are by far the fastest, as they perform
type casts and copy the data with the native type. The operations done without
type casting are sub-nanosecond speeds.

The test case `Copy16Pointer` and `Copy16PointerFrom32` only differ that the
input is a `short` or an `int`, where the `int` is slower. This indicates that
one should be careful to use the correct type for an input.

Otherwise, for the non-dangerous methods, we can see that:

* 16-bit and 32-bit are the same size.
* 64-bit is twice as slow.
* The BigEndian and LittleEndian make no difference, as expected as each byte is
  read and converted manually
* The conditional version is 0.3ns slower, which is the cost of doing an `if`
  check.

When comparing the arrays `byte[]` with `Span<byte>`, the arrays are slightly
faster.
