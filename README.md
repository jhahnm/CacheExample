# Technical Test: Cache Example
## By Julio Hahn Monroy
### This is a dotnet ASP Core API application that contains the three requirements as follows:

*Prerequisites:*

Had installed the following:

.NETCore 7

Docker Desktop

*To run the project after cloning:*

```
$: cd API/
$: docker compose up -d
$: dotnet watch
```


>Write a program that accepts two lines (x1,x2) and (x3,x4) on the
   x-axis and returns whether they overlap. As an example, (1,5) and (2,6) overlaps but not (1,5)
   and (6,8).
>
Please refer to UtilsController -> CheckOverlap method:

As an example to use it:

```
Overlapping Line: http://localhost:5100/api/util/overlap?x1=1&x2=5&x3=2&x4=6

Not Overlapping Line: http://localhost:5100/api/util/overlap?x1=1&x2=5&x3=6&x4=8
```

>write a software library that accepts 2 version string as input and
   returns whether one is greater than, equal, or less than the other. As an example: “1.2” is
   greater than “1.1”. Please provide all test cases you could think of.
>
Example to use it:

```
http://localhost:5100/api/util/compare?version1=1.2&version2=1.2.0.1
```

There is a full set of test cases in tests/API.UnitTests
> write a new
   library that can be integrated to the Ormuco stack. Dealing with network issues everyday,
   latency is our biggest problem. Thus, your challenge is to write a new Geo Distributed LRU (Least
   Recently Used) cache with time expiration. This library will be used extensively by many of our
   services so it needs to meet the following criteria:
   - 1 - Simplicity. Integration needs to be dead simple.
   - 2 - Resilient to network failures or crashes.
   - 3 - Near real time replication of data across Geolocation. Writes need to be in real time.
   - 4 - Data consistency across regions
   - 5 - Locality of reference, data should almost always be available from the closest region
   - 6 - Flexible Schema
   - 7 - Cache can expire

To try to achieve the requirements, the following has been done:
1. Use a mongodb container to achieve the Schema flexibility
2. Use redis container to achieve having a distributed caching mechanism. The principal benefit of pointing to an external Cache service like redis, is that we can have as many instances of this service as required, and if some instance is crashed for any reason, the caching is preserved between instances.
3. When adding new cars, the cache is cleared to avoid having the cache outdated.
4. Please notice Expiring mechanism is also included (Please refer to CachedCarsRepository for details).

Please consider this is just a working example for assessment purposes, but is not an attempt to have all in place as integration tests, distribution configuration between other details has been omitted.

