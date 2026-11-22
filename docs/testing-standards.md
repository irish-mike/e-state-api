# Testing standards

Tests are maintained code. Each test must justify its setup, readability, and maintenance cost. Prefer a small, reliable suite that protects important behavior over extensive coverage of trivial code.

Use [engineering principles](engineering-principles.md) for design decisions, [code standards](code-standards.md) for coding conventions, and [AGENTS.md](../AGENTS.md) for workflow and verification requirements.

## The default

**Do not add new tests by default.** Add tests when:

- Fixing an important bug where a focused regression test would prevent it from recurring.
- The user explicitly requests tests.

Do not create new test files or suites without approval. Extend existing tests when they already cover the relevant behavior.

A code change does not automatically require a new test. There is no minimum test count or coverage target.

## Before writing a test

- Identify the observable behavior and a realistic failure that matters to users, business operations, security, or stored data.
- Check whether existing tests already protect that behavior.
- Add a test only when it protects a meaningful risk that existing tests do not adequately cover.
- If expected behavior is unclear, ask rather than turning an assumption into a test.

Judge risk by consequences, not code size. A small authorization change may be important to test, while a large rename may require no new tests.

## What not to test

- **Trivial code:** Avoid testing simple getters, constants, delegation, type declarations, generated code, or basic wiring.
- **Framework behavior:** Do not test guarantees already provided by C#, .NET, ASP.NET Core, EF Core, or other libraries. Test the application's use of those capabilities when there is a meaningful risk.
- **Implementation details:** Test observable outcomes rather than private methods, internal state, or incidental call sequences.
- **Hypothetical edge cases:** Test realistic failure scenarios, not every imaginable condition.
- **Duplicate behavior:** Avoid repeating the same scenario across unit, integration, and API tests unless each protects a distinct risk.
- **Copied implementation:** Do not reproduce production logic to calculate expected results. Use independently known outcomes.
- **Unnecessary text checks:** Do not test documentation, configuration, or log messages merely to demonstrate that something changed. Test them when their contents are an actual requirement.
- **Unrelated behavior:** Keep each test focused on one coherent behavior. Multiple related assertions are fine when they describe the same outcome.

If a test requires excessive setup or complicated mocking, reconsider whether the test is valuable and whether a simpler approach would protect the same behavior.

## Keep tests simple

- Make tests easy to understand at a glance: clear setup, one action, and assertions describing the expected outcome.
- Use minimal, representative inputs that could expose a realistic mistake.
- Keep important preconditions visible rather than hiding them behind elaborate helpers.
- Use fixtures and shared helpers when they remove meaningful repetition without obscuring the scenario.
- Prefer real collaborators when they are simple, fast, and deterministic.
- Name tests so the scenario and expected outcome are clear.
- Keep tests independent and deterministic. Control time, randomness, shared state, and external services when necessary.
- Follow the project's existing test framework, naming conventions, and structure.

## Choose the simplest useful test level

Choose the least expensive test level that reliably demonstrates the relevant failure.

| Level | Use when |
| --- | --- |
| Unit | A business rule, calculation, or transformation determines the result |
| Integration | Correctness depends on persistence, database queries, transactions, or component collaboration |
| API | HTTP behavior, authorization, validation, or response contracts are the actual concern |

Use an isolated test database when behavior depends on real database semantics. Prefer the project's actual database engine when testing queries, transactions, or constraints that may behave differently across providers.

Do not introduce mocks or additional test infrastructure when existing components provide a simpler, reliable test.

## Maintain existing tests

- Do not delete, skip, or weaken valid failing tests merely to make the suite pass.
- Update tests when intended behavior changes or when an existing test is incorrect.
- Treat flaky tests as defects. Fix the cause instead of masking failures with retries or delays.
- Remove redundant tests only when the relevant behavior remains protected and the change is within scope.
- Do not introduce testing frameworks, dependencies, or unnecessary test abstractions without approval.

## Verification

Use the repository's existing test framework and commands. Run the relevant tests and required checks defined in [AGENTS.md](../AGENTS.md).

When finishing:

- Report which checks ran and whether they passed.
- Briefly explain what any new tests protect, or why no new test was needed.
- Do not claim a check passed unless it was actually run.