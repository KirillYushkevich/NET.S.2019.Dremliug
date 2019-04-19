
1. `IFormattable for Book (Day 08).`
    - For Book class objects, implement additional options for string representation. Examples:
      - Jeffrey Richter, CLR via C#
      - Jeffrey Richter, CLR via C#, "Microsoft Press", 2012
      -	ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, "Microsoft Press", 2012, P. 826
      -	Jeffrey Richter, CLR via C#, "Microsoft Press", 2012

2. `IFormatProvider, ICustomFormatter for Book (Day 08).`
    - Without changing the Book class, add additional formatting options that were not originally provided by the class.

3. `Unit tests.`
    - Create unit tests for paragraphs 1. and 2.
    
4. `GCD (Day 04) code refactoring.`
    - Refactor the Euclidean algorithm code. Refactoring is possible only if all methods are in the same class. Use delegates to reduce duplicate code. The class interface should not change.

5. `IComparer, Comparison for Jagged array (Day 05).`
    - Add to the class with the sorting algorithm of a jagged matrix:
      - a method accepting IComparer<int[]> interface as a parameter,
      - a method accepting Comparison<int[]> delegate as a parameter.
    - Implement the class in two ways:
      - the method with the delegate parameter invokes the method with the interface parameter,
      - vice versa.
    - Test matrix sorting using previous comparison criteria (the sum, the maximum, the minimum).
