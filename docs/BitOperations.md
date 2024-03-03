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

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.4046/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT
```

```text
Results = net6

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.4046/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET 6.0.27 (6.0.2724.6912), X64 RyuJIT
```

```text
Results = net8

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.4046/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT
```

| Project 'datastructures' Type | Method                  | mean (net48) | stderr  | mean (net6) | stderr  | mean (net8) | stderr  |
|:------------------------------|:------------------------|-------------:|--------:|------------:|--------:|------------:|--------:|
| BitOperationsBenchmark        | Copy16Pointer           | 0.34         | 0.00    | 0.06        | 0.00    | 0.07        | 0.00    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 0.34         | 0.00    | 0.06        | 0.00    | 0.08        | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 0.34         | 0.00    | 0.31        | 0.00    | 0.06        | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 0.33         | 0.00    | 0.32        | 0.00    | 0.06        | 0.00    |
| BitOperationsBenchmark        | Copy16Shift             | 0.24         | 0.00    | 0.04        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 0.24         | 0.00    | 0.03        | 0.00    | 0.00        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 0.26         | 0.00    | 0.03        | 0.00    | 0.00        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -       | 0.29        | 0.00    | 0.02        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -       | 0.30        | 0.00    | 0.71        | 0.04    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 0.24         | 0.00    | 0.04        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 0.26         | 0.00    | 0.06        | 0.00    | 0.00        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 0.26         | 0.00    | 0.07        | 0.00    | 0.00        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -       | 0.29        | 0.00    | 0.01        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -       | 0.29        | 0.00    | 0.01        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -       | 0.08        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -       | 0.08        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 0.41         | 0.00    | 0.31        | 0.00    | 0.30        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.62         | 0.01    | 1.23        | 0.01    | 0.32        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShift        | 1.15         | 0.01    | 0.67        | 0.00    | 0.05        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 1.24         | 0.00    | 0.92        | 0.00    | 0.34        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -       | 0.97        | 0.00    | 0.37        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 0.96         | 0.01    | 0.77        | 0.00    | 0.33        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -       | 0.95        | 0.00    | 0.37        | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -       | 0.96        | 0.00    | 0.36        | 0.00    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.34         | 0.00    | 0.06        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 0.33         | 0.00    | 0.06        | 0.00    | 0.29        | 0.00    |
| BitOperationsBenchmark        | Copy32Shift             | 1.20         | 0.00    | 0.08        | 0.00    | 0.31        | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 0.29         | 0.00    | 0.08        | 0.00    | 0.30        | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -       | 0.37        | 0.00    | 0.23        | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 0.28         | 0.00    | 0.07        | 0.00    | 0.30        | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -       | 0.40        | 0.00    | 0.16        | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -       | 0.42        | 0.00    | 0.30        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.42         | 0.00    | 0.31        | 0.00    | 0.34        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 2.62         | 0.09    | 2.04        | 0.01    | 0.34        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShift        | 2.92         | 0.01    | 2.79        | 0.00    | 0.79        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 2.88         | 0.00    | 2.79        | 0.00    | 0.80        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -       | 2.95        | 0.01    | 0.90        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 2.80         | 0.01    | 2.72        | 0.01    | 0.78        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -       | 2.92        | 0.01    | 0.90        | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -       | 2.96        | 0.00    | 0.91        | 0.00    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.36         | 0.00    | 0.05        | 0.00    | 0.33        | 0.00    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 0.35         | 0.00    | 0.33        | 0.00    | 0.42        | 0.00    |
| BitOperationsBenchmark        | Copy64Shift             | 0.93         | 0.01    | 0.91        | 0.00    | 0.79        | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 0.94         | 0.00    | 0.89        | 0.00    | 0.78        | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -       | 1.01        | 0.00    | 0.97        | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 0.92         | 0.00    | 0.89        | 0.00    | 0.79        | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -       | 1.04        | 0.00    | 0.97        | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -       | 1.00        | 0.00    | 0.97        | 0.00    |
| CRCBenchmark                  | CRC16                   | 3061826.59   | 6447.77 | 3060003.78  | 6303.48 | 2750417.94  | 1351.10 |
| CRCBenchmark                  | CRC32                   | 2754320.18   | 3652.39 | 2754865.18  | 2775.44 | 2450069.32  | 2927.49 |

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
