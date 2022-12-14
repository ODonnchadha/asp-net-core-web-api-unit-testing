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
  - Categorizing:
    - Out of the box, tests are grouped by class. Categorize tests in order to divide/run a subset.
  - SUMMARY:
    - Approaches for sharing test context:
      - Constructor & dispose.
      - Class fixture.
      - Collection fixture.
    - Integrating test context with ASP.NET Core's dependency injection system.
    - Use [Trait] to categorize tests. [Skip] to skip tests. ITestOutputHelper to log additional diagnostics info.

- WORKING WITH DATA-DRIVEN TESTS:
  - [Fact] versus [Theory]:
    - A fact is a test that is always true. It tests invariant conditions.
    - A theory is a test which is only true for a particular set of data. 
    - And provide data to the theory:
      - Inline data:
      - Member data: Share data across tests.
      - Class data:
      - NOTE: Use [TheoryData] for type-safe data. Working for objects. No compile-time checks on place.
  - Data-driven tests:
    - Type-safe approach:
    - Data from an external source: Other people can manage this data.
  - SUMMARY:
    - [Theory] enables data-driven tests. Inline data. Member data. Class data.
    - Strongly-typed data with TheoryData. Getting data from an external source.

- ISOLATING UNIT TESTS WITH ASP.NET CORE TECHNIQUES & MOCKING:
  - Unit tests should be isolated from other components of the system. And other dependencies. e.g.: factories. repositories.
  - By isolating a test you can ensure that when it passes/fails it is the cause of the SUT.
  - Test double: A generic term for any case where you replace a production object for testing purpose.
    - Fakes: A working implementation not suitable for production use. e.g.: SQLLite in-memory.
    - Dummies: A test double that's never accessed or used. e.g.: "constructing."
    - Stubs: A test double that provides fake data to the SUT.
    - Spies: A test double capable of capturing indirect output and providing indirect input as needed.
      - A subclass of the class being tested, addining additional behavior.
    - Mocks: A test double that implements the expected behavior.
  - Test isolation options:
    - Manually creating test doubles.
    - Using built-in framework or library functionality to create test doubles.
    - Using a mocking framewrk to create test doubles.
    - NOTE: Different types of test doubles and different approaches are often combined. 
      - Focus on the fact that the test is isolated, no matter how.
  - Test isolation with EF Core.
    - In principle, EF Core is used for calling into databases.
    - Avoid real db calls. 
      - Use in-memory for simple scenarios. Not the best option.
      - SQLLite in-memory mode  is the best compatibility with a real database.
  - Test isolation with HttpClient.
    - Tests must be isolated from network calls. A custom message handler can short-circut the actual call.
    - e.g.: HttpClient -> HttpRequestMessage (Message Handler) -> API -> HttpResponseMessage (Message Handler) -> HttpClient.
  - Test isolation with Moq: Defacto standard in .NET. (ver 4.17.2)
    - Creating:
    - Configurating:
    - Mocking an interface:
    - Mocking async code:
  - Which approach?
    - Consider: Reliabiility. Effort. Available knowledge.
  - SUMMARY:
    - Test doubles. Framework techniques. Mocking framework.

- UNIT TESTING ASP.NET CORE API Controllers:
  - Controllers. Test the behavior you yourself coded.
  - Code coverage. 100% can be counterproductive. ROI on final 10% can be counterproductive.
  - Controller types:
    - Thick (fat) controllers: Actions with logic that implement expected behavior. Model state. COnditional code. Mapping code.
    - Thin (skinny) controller: Delegate the actual implementation of the behavior to other components. e.g.: Command pattern. Mediator.
      - One type, by definition, is not better than the other.
  - Test isolation is important. Avoid model binding, filters, routing. DO test custom middleware.
  - Test: Expected return type. Expected type of return data. Expected data of returned data. Other action logic. (Non framework-related.)
    - Mocking controller dependencies.
    - Working with ModelState.
    - Dealing with HttpContext.
    - Work with HtppClient calls.
    - If we are not testing the mapping code itself, use a mocked mapper.
    - Mapping code can be seen as part of the SUT, so you can use an actual mapper.
  - Combining controller action asserts in one unit test & testing maping code.
  - Controller: Removing the [ApiController()] attribute for ModelState/BadRequest:
    ```csharp
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
    ```
    - Newing up an errant DTO is useless. ModelBinder does not execute. ModelState is invalid during the binding process.
    - We need to invalidate ModelState in order to test and validate n isolation:
      ```csharp
        controller.ModelState.AddModelError("FirstName", "Required");
      ```
  - HttpContext: An object which encapsulates all HTTP-specific information about an individual HTTP request. A container for a single request.
    - Request.
    - Response.
    - Features. (Connection, Server Information, etc.)
    - User.
    - Testing with HttpContext:
      - Use the built-in default implementation: DefaultHttpContext. (All properties that are not read-only can be changed.)
      - Use Moq: Mock<HttpContext>
  - SUMMARY:
    - Testing API Controller. ActionResult<T>, DTO Models, ModelState, HttpCOntext.

- UNIT TESTING ASP.NET CORE MIDDLEWARE, FILTERS, & SERVICE REGISTRATIONS:
  - Unit testing middleware. Custom only. Unit or even integration. 
    - Especially if dependenceis are tough to mock and test isolation becaomes impossible.
    - Mock the HttpContext. Or use DefaultHttpContext. Handle the RequestDelegate.
    - We need to avoid this:
    ```csharp
      await _next(context);
    ```
  - Filters: Custom filters often handle cross-cutting concerns.
    - e.g.: Error handling. Caching.
    - Used to avoid code duplication as well.
    - Filters run in the ASP.NET Core action invocation pipeline.
    - Filters:
      - Action filter. Unit test. 
        - Runs immediately before/after an action method is called.
        - Can modify the arguments passed into an action.
        - Can modify the result returned from the action.
      - Authorization filter.
      - Resource filter.
      - Exception filter.
      - Result filter.
  - Unit testing service registrations:
    - Services are registered on ASP.NET Core's IoC container.
      - These registrations can be unit tested.
      - Approach: Create an IServiceCollection.Register. Build IServiceProvider. Verify service registration.
  - SUMMARY:
    - Challenges with middleware & filter testing are related to test isolation.
    - Test service registrations by building an IServiceProvider and testing whether you can get a service instance.

- INTEGRATING UNIT TESTS IN DEVELOPMENT & RELEASE FLOWS:
  - Running tests with the CLI: PowerShell. Bash. etc
    ```javascript
      dotnet test
    ```
    - Builds the solution and runs whatever test projects it finds. MSTest. NUnit. XUnit. Success? 0 as an exit code. Else, 1.
  - Test runners with test frameworks:
    - XUnit versus the runner.
    - Test runner: The program, or perhaps the 3rd party plugin to a program, that is responsible for looking for 1:M assemblies with tests in them and activating the test framework(s) that it finds in those assemblies.
    - Test framework: The code that has detailed knowledge of how to discover & run int tests.
    - Running tests in parallel: Allows a set of tests to finish faster, locally and on your build server.
      - Test runner: Can support running different test assembies in parallel.
      - Test framework: an support running tests within a single assembly in parallel.
  - Testing in parallel:
    - Tests within the same class do not run in parallel. Different test classes run in parallel.
    - Concept used: Test collections. Each test within a class is considered a collection.
    - Can use the [Collection("NAME")] attribute.
  - Testing against multiple target frameworks:
    - e.g.: .NET 6.x. .NET 5.x. Testing a class library that is referenced. Not all may target the same framework.
    - Useful when developing frameworks or libraries.
    ```xml
      <PropertyGroup>
        <TargetFrameworks>net6.0;net5.0</TargetFrameworks>
      </PropertyGroup>
    ```
    - Test explorer will dispay each target framework.
  - Integrating testing in your CI/CD pipeline:
  - Summary:
    - Test runner: Program responsible for identifying 1:More assemblies with tests in them.
    - Test framework: Code that has detailed knowledge of how to discover & run unit tests.
    - Via the CLI: dotnet test. (Also executed on the build server.)
    - Parallel and run in different frameworks.