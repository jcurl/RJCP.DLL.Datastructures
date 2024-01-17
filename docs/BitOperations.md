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

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.3803/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT
```

```text
Results = netcore31

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.3803/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT
```

| Project 'datastructures' Type | Method                  | mean (net48) | stderr  | mean (netcore31) | stderr  |
|:------------------------------|:------------------------|-------------:|--------:|-----------------:|--------:|
| BitOperationsBenchmark        | Copy16Pointer           | 0.33         | 0.00    | 0.35             | 0.00    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 1.35         | 0.00    | 0.33             | 0.00    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.33         | 0.00    | 0.33             | 0.00    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.34         | 0.00    | 0.49             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 3.09         | 0.00    | 0.37             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.40         | 0.00    | 0.64             | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 1.43         | 0.00    | 0.17             | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 1.43         | 0.00    | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 1.44         | 0.00    | 0.37             | 0.00    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 2.94         | 0.00    | 0.33             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.77         | 0.00    | 1.47             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 2.34         | 0.00    | 2.31             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 1.38         | 0.00    | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 1.38         | 0.00    | 0.01             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 1.66         | 0.00    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 3.54         | 0.00    | 0.90             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 2.02         | 0.00    | 0.72             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 4.02         | 0.00    | 2.70             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 1.43         | 0.00    | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 1.38         | 0.00    | 0.00             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 1.66         | 0.00    | 0.29             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 3.47         | 0.00    | 0.89             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 2.06         | 0.00    | 0.67             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 4.02         | 0.00    | 2.78             | 0.00    |
| BitOperationsBenchmark        | Copy16Shift             | 1.72         | 0.00    | 0.01             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 1.72         | 0.00    | 0.00             | 0.00    |
| BitOperationsBenchmark        | Copy32Shift             | 2.04         | 0.00    | 0.30             | 0.00    |
| BitOperationsBenchmark        | Copy64Shift             | 4.00         | 0.00    | 0.89             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShift        | 2.31         | 0.00    | 0.74             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShift        | 4.29         | 0.00    | 2.75             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -       | 0.30             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -       | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -       | 0.34             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -       | 1.03             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -       | 0.93             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -       | 2.87             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -       | 0.27             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -       | 0.30             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -       | 0.61             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -       | 1.08             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -       | 0.94             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -       | 2.88             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -       | 0.28             | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -       | 0.30             | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -       | 0.72             | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -       | 1.08             | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -       | 1.09             | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -       | 2.89             | 0.00    |
| CRCBenchmark                  | CRC16                   | 2979693.54   | 954.01  | 2979509.43       | 1517.01 |
| CRCBenchmark                  | CRC32                   | 2672039.22   | 1112.87 | 2682163.70       | 4254.91 |

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
