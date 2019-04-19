1. `Polynomial.`
    - Create a sealed class Polynomial to work with floating-point polynomial in one variable. (... + 3.6x^4 - 0.78x^3 + ... + 8).
As an internal structure for storing coefficients use sz-array (single-dimensioned, zero-based array).
For created class:
    - override the virtual methods of the Object class,
    - overload operations defined for polynomials (excluding the division of a polynomial by a polynomial), including "==" and "!=".

2. `Jagged array sort.`
    - Implement the Bubble sort algorithm for a non-rectangular (jagged) integer array.
(!) Do not use sorting methods of the System.Array class.
Add ways to sort the rows of the matrix:
        - ascending (descending) by the sum of the elements of a row,
        - ascending (descending) by the maximum element a row,
        - ascending (descending) by the minimum element a row.
