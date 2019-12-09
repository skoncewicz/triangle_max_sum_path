# Introduction

This project is a solution for recruitment task defined in file "task_definition.pdf".

# Solution

Problem as described in provided file seems to be a good candidate for dynamic programming (https://en.wikipedia.org/wiki/Dynamic_programming).

To be sure that we can use this technique for provided task we need to define following properties for it:

## Optimal Substructure

https://en.wikipedia.org/wiki/Optimal_substructure

Given following triangle:

`a(0, 0)`

`a(1, 0) a(1, 1)`

`...`

`a(n, 0) a(n, 1) ... a(n, n)`

Assuming `a(1, 0)` and `a(1, 1)` are valid next step in path from `a(0, 0)` and that we have calculated maximal path
to `MAX(a(1, 0))` and `MAX(a(1, 1))` we can simply pick bigger of those values as our next step.

To calculate `MAX(a(1, 0))` we simply have to solve the same problem for following triangle:

`a(1, 0)`

`a(2, 0) a(2, 1)`

`...`

`a(n, 0) ... a(n, n - 1)`

To calculate `MAX(a(1, 1))` we have to solve problem for similiar smaller triangle shifted one field to the right side as follows:

`a(1, 1)`

`a(2, 1) a(2, 2)`

`...`

`a(n, 1) a(n, n)`

That way we have reduced problem of calculating max path for triangle with height n to two subproblems of calculating max path for triangles
of height (n-1). We can use that to reduce this all the way until we reach triangles of height 1, and for those triangles solution is trivial.

If there is no valid path from given parent we can simply declare MAX(a(x, y)) = NULL.

## Overlapping subproblems

https://en.wikipedia.org/wiki/Overlapping_subproblems

For triangle:

`a(0, 0)`

`a(1, 0) a(1, 1)`

`a(2, 0) a(2, 1) a(2, 2)`

`...`

`a(n, 0) a(n, 1) ... a(n, n)`

It can clearly be seen that:
- To calculate `MAX(a(1,0))` we have to calculate `MAX(a(2, 0)` and `MAX(a(2, 1))`
- To calculate `MAX(a(1,1))` we have to calculate `MAX(a(2, 1)` and `MAX(a(2, 2))`

For both of this values we have overlapping subproblem of computing `MAX(a(2, 1))` and we can resuse that.

## Solution & Complexity estimation

To get our solution we simply have to calculate max paths starting from the bottom of the triangle and going up. Since we're calculating MAX() for each node
of the triangle only once, we have linear complexity solution `O(n)` with respect to `MAX()` operation.

As for memory, we're allocating two triangles of similiar size plus lists to store results. For each node we're storing `(h - y) + c)`
numbers where c is some contant and y is column. So for the whole triangle where `n` is number of nodes, estimated amount of memory required
would be:

`n + c`

`(n - 1) + c, (n - 1) + c`

`...`

`1 + c, ..., ... , ... , 1 + c`

We can clearly se that if we sum up by columns, k-th column contains `c * (n - k)` constant factors plust sum of natural numbers up to `(n - k)`
Substituting `(n - k)` by `i` we can calculate this sum as:

`i * c + sum(0...n, i(i + 1)/2)` and using wolfram alpha we can write:

`i * c + 1/6 * i(i + 1)(i + 2)`

So memory complexity is `O(n^3)`. We can reduce that to `O(n)` if we stop tracking current path.

## Possible optimizations

To reduce memory footprint we can clear paths that are no longer required (if we're calculated data for 5th row, we don't need data for 6th row anymore).
We could also try to reverse this algorithm to go from top to bottom, then we don't have to keep whole triangle in memory. But this is out of the scope for now.

Memory footprint can be further reduced to linear in respect to the amount of nodes if we store decision to go left or right on each node instead of storing whole path.

## Code quality comment

Since problem solution is really consise only quality improvements worth considering were splitting data structure for triangle and algorithm itself.
Every class is below 100 lines, 3 classes total + tests. Trying to introduce more modularity would be just wasting time (YAGNI principle).
If different model would be required it's easy to just rewrite any of those classes. 