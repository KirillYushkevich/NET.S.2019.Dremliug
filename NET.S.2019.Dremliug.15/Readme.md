1. `Bank account.`

    - Develop a type system describing work with a bank account.
    - The state of the account is determined by its ID, account holder (name, surname), balance and some bonus points. Bonus points increase / decrease each time the account is credited / debited by values ​​different for adding and withdrawal and are calculated depending on some "value" of the balance and a "value" of the credit / debit amount. These "values" are integers and depend on account rank which may be Base, Gold, Platinum.
    - To work with the account implement the following features:
      - add funds,
      - withdraw funds,
      - open a new account,
      - close an existing account.
    - Data must be stored in a binary file.
    - Demonstrate the result as a console application.
    
    - The type system should allow the following future extension:
      - adding new account ranks,
      - adding/changing account storages,
      - changing the bonus points calculation logic,
      - changing the account ID generation logic.
      
    - Use "The Stairway pattern" to organize classes and interfaces. (AccountSystemDemo.7z example is attached)
    - Test the BL layer using NUnit and Moq frameworks.