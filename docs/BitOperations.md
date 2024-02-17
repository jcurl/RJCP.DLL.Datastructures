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
Results = netcore

BenchmarkDotNet=v0.13.12 OS=Windows 10 (10.0.19045.3930/22H2/2022Update)
Intel Core i7-6700T CPU 2.80GHz (Skylake), 1 CPU(s), 8 logical and 4 physical core(s)
  [HOST] : .NET 6.0.26 (6.0.2623.60508), X64 RyuJIT
```

| Project 'datastructures' Type | Method                  | mean (net48) | stderr   | mean (netcore) | stderr  |
|:------------------------------|:------------------------|-------------:|---------:|---------------:|--------:|
| BitOperationsBenchmark        | Copy16Pointer           | 0.70         | 0.00     | 0.27           | 0.02    |
| BitOperationsBenchmark        | Copy16PointerFrom32     | 0.33         | 0.00     | 0.21           | 0.01    |
| BitOperationsBenchmark        | Copy32Pointer           | 0.34         | 0.00     | 0.43           | 0.00    |
| BitOperationsBenchmark        | Copy64Pointer           | 0.35         | 0.00     | 0.50           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointer      | 0.40         | 0.00     | 0.39           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointer      | 0.41         | 0.00     | 0.50           | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwap       | 0.34         | 0.00     | 0.29           | 0.00    |
| BitOperationsBenchmark        | Copy16PointerSwapFrom32 | 0.34         | 0.00     | 0.31           | 0.00    |
| BitOperationsBenchmark        | Copy32PointerSwap       | 0.36         | 0.01     | 0.43           | 0.00    |
| BitOperationsBenchmark        | Copy64PointerSwap       | 0.36         | 0.00     | 0.49           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatPointerSwap  | 1.81         | 0.03     | 1.23           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatPointerSwap  | 2.34         | 0.02     | 2.16           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLE           | 0.31         | 0.01     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLEFrom32     | 0.27         | 0.01     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftLE           | 0.32         | 0.01     | 0.41           | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftLE           | 1.08         | 0.01     | 0.90           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLE      | 1.09         | 0.01     | 0.89           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLE      | 3.04         | 0.02     | 2.71           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBE           | 0.23         | 0.01     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBEFrom32     | 0.29         | 0.01     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBE           | 0.31         | 0.01     | 0.52           | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftBE           | 1.04         | 0.01     | 1.12           | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShiftBE      | 1.35         | 0.02     | 1.06           | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShiftBE      | 3.10         | 0.02     | 2.88           | 0.02    |
| BitOperationsBenchmark        | Copy16Shift             | 0.27         | 0.01     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftFrom32       | 0.24         | 0.00     | 0.00           | 0.00    |
| BitOperationsBenchmark        | Copy32Shift             | 0.36         | 0.01     | 0.53           | 0.01    |
| BitOperationsBenchmark        | Copy64Shift             | 1.03         | 0.01     | 0.91           | 0.01    |
| BitOperationsBenchmark        | Copy32FloatShift        | 1.25         | 0.01     | 0.89           | 0.01    |
| BitOperationsBenchmark        | Copy64FloatShift        | 3.19         | 0.01     | 2.75           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpan       | -            | -        | 0.14           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftLESpanFrom32 | -            | -        | 0.93           | 0.01    |
| BitOperationsBenchmark        | Copy32ShiftLESpan       | -            | -        | 0.35           | 0.01    |
| BitOperationsBenchmark        | Copy64ShiftLESpan       | -            | -        | 1.03           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftLESpan  | -            | -        | 0.94           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftLESpan  | -            | -        | 2.76           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpan       | -            | -        | 0.25           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftBESpanFrom32 | -            | -        | 0.26           | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftBESpan       | -            | -        | 0.32           | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftBESpan       | -            | -        | 1.02           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftBESpan  | -            | -        | 0.94           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftBESpan  | -            | -        | 2.87           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpan         | -            | -        | 0.11           | 0.00    |
| BitOperationsBenchmark        | Copy16ShiftSpanFrom32   | -            | -        | 0.10           | 0.00    |
| BitOperationsBenchmark        | Copy32ShiftSpan         | -            | -        | 0.34           | 0.00    |
| BitOperationsBenchmark        | Copy64ShiftSpan         | -            | -        | 0.99           | 0.00    |
| BitOperationsBenchmark        | Copy32FloatShiftSpan    | -            | -        | 0.92           | 0.00    |
| BitOperationsBenchmark        | Copy64FloatShiftSpan    | -            | -        | 2.89           | 0.01    |
| CRCBenchmark                  | CRC16                   | 3227820.29   | 15685.03 | 3007736.20     | 2802.17 |
| CRCBenchmark                  | CRC32                   | 2901011.72   | 11058.02 | 2695579.06     | 3628.60 |

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
