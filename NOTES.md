# Unit Testing an ASP.NET Core 6 Web API

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
  - 