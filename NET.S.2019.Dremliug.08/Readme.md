1. Book

    - Create the **Book** class (ISBN, author, title, publisher, year of publication, number of pages, price), *override* all necessary methods of Object.
    - Implement *ordering and equality* for instances of Book class (use *interfaces*).
    - Create the **BookListService** class as a service to work with a *collection* of books; implement functionality:
      - AddBook (adds a book if not exists in the collection, otherwise throws an exception),
      - RemoveBook (removes a book if it exists, otherwise throws an exception),
      - FindBookByTag (finds a book by a given criterion),
      - SortBooksByTag(sorts books by a given criterion)
    - To perform basic operations with a list of books, which can be loaded and/or saved in some **BookListStorage** storage, create the **BookListService** class.
    - Demonstrate how classes work on the example of a console application.
    - Use a binary file as storage. Only BinaryReader and BinaryWriter are allowed to work with the storage. The storage may change later.
    - **(!)** do not use delegates
    
2. Bank account

    - Develop a type system describing work with a bank account.
    - The state of the account is determined by its ID, account holder (name, surname), balance and some bonus points. Bonus points increase / decrease each time the account is credited / debited by values ​​different for adding and withdrawal and are calculated depending on some "value" of the balance and a "value" of adding. These "values" are integers and depend on accuont status which may be Base, Gold, Platinum.
    - To work with the account implement:
      - add funds,
      - withdraw funds,
      - open a new account,
      - close an existing account.
    - Data must be stored in a binary file.
    - Demonstrate the result as a console application.
    
    