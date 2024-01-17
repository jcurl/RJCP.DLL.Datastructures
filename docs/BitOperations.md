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

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.3930/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT
```

```text
Results = netcore31

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.3930/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT
```

| Project 'datastructures' Type | Method                  | mean (net48) | stderr  | mean (netcore31) | stderr  |
|:------------------------------|:------------------------|-------------:|--------:|-----------------:|--------:|
| BitOperationsBenchmark        | Copy16Pointer           | 0.34         | 0.00    | 0.49             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 0.69         | 0.00    | 0.40             | 0.01    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.34         | 0.00    | 0.45             | 0.01    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.35         | 0.00    | 0.86             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 0.41         | 0.00    | 0.58             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.41         | 0.00    | 0.70             | 0.01    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 0.34         | 0.00    | 0.36             | 0.02    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 0.33         | 0.00    | 0.38             | 0.01    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 0.34         | 0.00    | 0.42             | 0.01    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 0.35         | 0.00    | 0.38             | 0.01    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.54         | 0.00    | 1.55             | 0.01    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 3.21         | 0.08    | 2.40             | 0.01    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 0.23         | 0.01    | 0.00             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 0.18         | 0.01    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 0.34         | 0.01    | 0.33             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 0.99         | 0.01    | 0.93             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 1.05         | 0.01    | 0.75             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 2.93         | 0.01    | 2.78             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 0.25         | 0.00    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 0.28         | 0.00    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 0.31         | 0.00    | 0.31             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 1.00         | 0.01    | 0.93             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 1.34         | 0.02    | 0.95             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 3.05         | 0.02    | 2.79             | 0.00    |
| BitOperationsBenchmark        | Copy16Shift             | 0.25         | 0.00    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 0.25         | 0.00    | 0.00             | 0.00    |
| BitOperationsBenchmark        | Copy32Shift             | 0.36         | 0.01    | 0.31             | 0.00    |
| BitOperationsBenchmark        | Copy64Shift             | 1.05         | 0.01    | 0.91             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShift        | 1.28         | 0.01    | 0.76             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShift        | 3.20         | 0.03    | 2.79             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -       | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -       | 0.31             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -       | 0.34             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -       | 1.07             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -       | 0.97             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -       | 2.96             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -       | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -       | 0.31             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -       | 0.34             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -       | 1.02             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -       | 1.01             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -       | 2.93             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -       | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -       | 0.30             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -       | 0.75             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -       | 1.10             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -       | 1.12             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -       | 2.88             | 0.00    |
| CRCBenchmark                  | CRC16                   | 3123028.43   | 872.30  | 3022825.99       | 3316.74 |
| CRCBenchmark                  | CRC32                   | 2802209.65   | 1588.20 | 2712745.31       | 2542.83 |

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
