# Code standards

Rules for writing readable, maintainable C# backend code.

[Engineering principles](engineering-principles.md) explains the design principles. [Backend architecture](backend-architecture.md) defines project structure, module boundaries, and persistence responsibilities.

[`.editorconfig`](../.editorconfig) and project configuration are authoritative for formatting and analyzer rules. [AGENTS.md](../AGENTS.md) defines task scope, approvals, Git workflow, and verification requirements.

## Naming and values

- Use clear, descriptive names. Short names such as `result`, `filtered`, or `listing` are fine when the surrounding code provides enough context. Avoid repeating information unnecessarily.
- Follow C# naming conventions: PascalCase for types and members, camelCase for local variables and parameters, and `_camelCase` for private instance fields.
- Name classes according to their responsibility. Names such as `Service`, `Repository`, `Manager`, `Helper`, and `Utils` are appropriate when they describe the class's purpose.
- Keep class responsibilities cohesive. Scope helpers and utilities to a meaningful domain or purpose rather than collecting unrelated functionality.
- Name methods for what they do. Preserve words that clarify an operation's scope, such as `ResetOverrides()` instead of `Reset()` when only overrides are reset.
- Make lookup behavior clear to callers. Use nullable results, exceptions, `TryGet` methods, or the Null Object pattern when appropriate.
- Name booleans as predicates, such as `isActive`, `hasAccess`, and `shouldRetry`.
- Prefer full words, allowing familiar abbreviations such as `id` and `db` when their meaning is clear.
- Use American English in identifiers and technical terms unless the domain requires otherwise.
- Keep simple, self-explanatory literals inline. Name calculations, conditions, and unexplained values when doing so improves clarity.
- Keep business constants with the code that owns them. Share constants when they represent genuine shared conventions.

## Types and files

- Name C# files after their primary type, such as `ListingService.cs` or `ListingRepository.cs`.
- Organize related types according to the project's existing structure.
- Use classes, records, interfaces, and other C# types according to their purpose. Avoid introducing abstractions without a clear benefit.
- Give publicly accessible types names that remain meaningful outside their immediate context.

## Control flow and readability

- Prefer straightforward, readable code over clever or unnecessarily compact expressions.
- Use guard clauses and early returns when they reduce nesting.
- Avoid nested ternary expressions and overly complex conditions.
- Break dense expressions or long method chains into named steps when intermediate values have useful meaning. Keep short, conventional expressions together.
- Keep methods focused on one coherent operation. Extract methods when they represent meaningful steps or make the caller easier to follow.
- Keep related logic together rather than scattering one operation across unnecessary methods or classes.
- Make complex operations read like a sequence of clearly named steps.
- Keep related validation, decisions, and result construction together.
- Choose the loop or collection operation that expresses the behavior most clearly.
- Prefer readability over insignificant performance improvements. Optimize when there is a demonstrated need.
- Represent missing, optional, invalid, and empty states intentionally.
- Prefer deriving values from existing information over storing redundant state, unless storing them serves a clear purpose.
- Avoid hidden mutation, unnecessary side effects, and custom machinery for simple problems.

## Classes and state

- Keep classes focused on cohesive responsibilities. Split them when they acquire genuinely separate responsibilities.
- Keep related behavior together so readers can understand an operation without navigating unnecessary files or layers.
- Place behavior on domain objects when it naturally belongs to them.
- Prefer local variables and method parameters for values used during a single operation.
- Use fields for state and dependencies that belong to the object.
- Avoid mutable request-specific state in shared services.
- Respect dependency-injection lifetimes and resource ownership.
- Prefer immutable state when it improves clarity or prevents unintended changes.
- Use static methods for independent operations when instance state or dependencies are unnecessary.

## Keep code minimal

- Implement what current requirements need. Avoid speculative features, options, abstractions, and configuration.
- Prefer existing capabilities over introducing new infrastructure.
- Avoid wrappers and forwarding methods that add no meaningful responsibility or boundary.
- Keep each behavior configured in one place where practical.
- Extract repeated logic when doing so gives the behavior a clear owner.
- Avoid storing information that can be derived easily from existing state.
- Review unused code before removing it. Preserve intentional work in progress and avoid unrelated cleanup.
- Do not introduce backward-compatibility layers for previous API contracts or database schemas during the current development phase unless requested.

## Async and cancellation

- Use asynchronous APIs for I/O and avoid blocking asynchronous operations.
- Propagate cancellation tokens through operations that support cancellation.

## Error handling and resources

- Handle errors where they can be meaningfully recovered from, translated, or reported. Otherwise, let them propagate.
- Use exceptions for exceptional failures rather than normal application outcomes.
- Keep error messages useful without exposing secrets or sensitive information.
- Avoid catching exceptions solely to log and rethrow them when another layer already handles logging.
- Manage resources according to their ownership and lifetime. Dispose of resources you own and let dependency injection manage resources it owns.
- Validate required configuration at startup when practical.
- Avoid unexpected I/O or other side effects in constructors and property getters.

## Persistence and API boundaries

- Follow the module and persistence boundaries defined in the backend architecture.
- Keep database-specific concerns within the appropriate persistence boundaries.
- Use EF Core's supported APIs rather than recreating database infrastructure.
- Respect `DbContext` lifetimes and avoid concurrent operations on the same context.
- Keep transactions scoped to operations that require atomicity.
- Keep HTTP-specific concerns, such as response codes and response formatting, at the API boundary.
- Keep request validation, business rules, and persistence responsibilities clear.
- Follow the project's development database and seeding workflow when changing data models.

## Logging

- Use the project's established logging conventions and `ILogger` integration.
- Use structured logging with constant message templates and named placeholders.
- Log information that helps diagnose behavior or failures.
- Choose log levels according to the importance and expected frequency of events.
- Avoid logging the same exception at multiple layers without adding useful context.
- Never log secrets, credentials, or unnecessary sensitive data.
- Avoid excessive logging of routine successful operations.

## Comments and documentation

Most code should explain itself through names, types, and structure. Add comments when they explain something the code cannot communicate clearly.

- Explain non-obvious business rules, constraints, workarounds, or design decisions.
- Explain why something is necessary rather than restating what the code does.
- Keep comments short unless a complex algorithm or protocol requires more detail.
- Use XML documentation when callers need information that the method signature cannot express, such as important preconditions, side effects, or failure behavior.
- Avoid documenting obvious parameters, return values, or straightforward operations.
- Avoid decorative comment banners and unnecessary section headings.
- Remove commented-out code. Update or remove comments when the implementation changes.
- Keep change history, dates, and authorship in Git rather than source comments.
- Use `TODO` or `FIXME` comments when they clearly describe unfinished work and why it remains.

## Libraries and generated code

- Follow established repository patterns and official documentation for the installed library versions.
- Prefer supported public APIs over internal, deprecated, or undocumented behavior.
- Before implementing a capability, check existing project code, .NET, and installed dependencies for a suitable solution.
- Do not recreate functionality that the current stack already provides adequately.
- Prefer a few lines of clear code over adding a dependency that provides little value.
- When existing practice conflicts with newer guidance, identify a concrete reason to change it rather than refactoring solely to adopt a newer pattern.
- Modify the source, schema, or generator for generated output. Regenerate the result rather than editing generated files manually.
- Follow the dependency approval rules in AGENTS.md.

## Reviewing code

- Identify correctness and maintainability problems before minor style preferences.
- Suggest removing unnecessary complexity, duplication, or state when doing so makes the implementation easier to understand.
- Suggest extracting a method when a block is difficult to follow, not merely because it is long.
- Suggest renaming identifiers when they hide the meaning or responsibility of the code.
- Respect intentional work-in-progress code and the requested scope.
- Explain findings in plain language, with the most important issues first.