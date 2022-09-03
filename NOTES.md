# Unit Testing an ASP.NET Core 6 Web API
- Unit testing your ASP.NET Core 6 Web API helps with improving its reliability.
- This course will teach you the ins and outs of unit testing with xUnit in detail.

- OVERVIEW:
  - ASP.NET Core 6.0. XUnit 2.4.1. Moq 4.17.2. .NET 6.0.
  - Arrange, Act, and Assert. Integrating within pipeline(s).

- INTRODUCTION:
  - A unit test is an automated test that tests a small piece of behavior.
    - Often just part of a method of a class. And, potentially, functionality related behavior across classes.
  - Unit Tests:
    - Unit tests should (1) have low complexitity. (2) Be fast. (3) Be well encapsulated. (Isolated.)
    - Reasons? Improved reliability at a relatively low cost. Write once and use without additional cost.
      - Bugs are discovered quicker. Easier. Cheaper to fix.
  - Integration Tests: Automated test whether or not two or more components work together correctly.
    - Can test full request/response. Can be created with same frameworks. Optionally: TestHost & TestServer.
    - Less isolated. Medium complexity. Not well encapsulated. Relatively slow.
  - Functional (End-to-end) Tests: Automated. e.g.: Selenium. Postman. TestHost & TestServer.
    - High complexity. Slow. Poorly encapsulated.
  ```javascript
    update-database
  ```
  - Add xUnit Test Project.
  ```xml
      <Nullable>enable</Nullable>
	      <ImplicitUsings>true</ImplicitUsings>
    ```
  - Naming guideline:
    - CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500
      - A name for the unit that's being tested. e.g.: "CreateEmployee."
      - The scenario under which the unit is being tested. e.g.: "ConstructInternalEmployee."
      - The expected behavior when the scenario in onvoked. e.g.: "SalaryMustBe2500."
  - Arrange, Act, Assert.
    - Setting up the test. Initializing objects. Testing dependencies.
    - Executing the actual test. e.g.: Calling a method. Setting a property.
    - Verifying the executed action.
  - xUnit, nUnit, and MSTest:
    - xUnit: .NET de facto. Built with .NET Core. Built from scratch.
    - MSTest: Microsoft built-in. Carries technical debt.
    - nUnit: A port of jUnit. Carries technical debt.
  - SUMMARY:
    - A unit test is an automated test that tests a small piece of behavior, oten simply testing ythe methods of a class.
      - Improves application reliability at a much lower cost that manual testing.
    - [Fact] signafies a unit test method.

- BASIC UNIT TESTING SCENARIOS:
  - Assert: A boolean expression that should evaluate 'true.' A test can contain one or more asserts.
  - xUnit provides asserts for all common core testing scenarios.
  - "A unit test should only contain one assert."
    - NOTE: Multiple assertions in a test is acceptable provided they assert the same behavior.
  - Strings. Case sensitive. Note the inline regular expression "sounds like."
    ```csharp
      Assert.Matches("Lu(c|s|z)ia Shel(t|d)on", employee.FullName);
    ```
  - Floating point: float. double. decimal. salary is of type decimal.
  - Asserting on arrays and collections.
  - Asynchronous assertions.
  - Asserting on exceptions.
    - ThrowsAny(Async)<T> takes derived versions into consideration. Throws(Async)<T> does not.
  - Asserting on events.
  - Assert on object types.
  - Asserting on private methods.
    - A private method is an implementation detail that doesn't exist in isolation.
    - Test the behavior of the method that uses the private method.
    - Making a private method public just to be able to test it breaks encapsulation.
      - [InternalVisible] is a slightly less poor alternative.
  - SUMMARY:
    - Alerts allow evalution and verification of the test outcome.
      - Fails when one or more asserts fail.
      - Passes when all asserts pass.

- SETTING UP TESTS & CONTROLLING TEST EXECUTAION:
  - Set-up & clean-up code. And you don't have to execute each and every test every single time.
  - Setting up tests and sharing test content:
    - Constructor & dispose.
      - Set up test context in the constructor, potentially clean up in .Dispose() method.
        - Context is recreated for each test. Test class instance is not shared.
    - Class fixture.
      - Create a single test context shared among all tests in the class.
        - Context is cleaned up after all tests in the class have finished.
        - Use when context creation and clean-up are expensive.
    - NOTE: Do not allow a test to depend on changes made to the context by another test. Isolate your tests.
    - Collection fixture.
      - Create a single test context shared among tests in several test classes.
      - Context is cleaned up after all tests across classes have finished.
      - Use when context creation and clean-up are expensive.
  - Integrating test context with ASP.NET Core's dependency injection system.
    - In ASP.NET Core, dependencies are often resolved via the built-in IoC container.
      - Can this be integrated with a unit test?
        - Newing up dependencies is the preferred approach. Simple. Fast. Concise.
      - You might want to integrate with the DI system.
        - If the class has got a lot of dependencies. Or a large dependency tree.