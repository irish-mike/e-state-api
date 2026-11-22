# Engineering principles

These principles guide how we design and maintain the e-state API. Good code is **easy to understand and easy to change**. Favor solutions that a future developer can understand and modify safely.

These are principles, not strict rules. Apply them with judgment and within the requested scope.

Use [code standards](code-standards.md) for concrete coding conventions, [backend architecture](backend-architecture.md) for project structure and boundaries, and [AGENTS.md](../AGENTS.md) for task scope, approvals, and workflow rules.

## Keep complexity proportional (KISS and YAGNI)

Every line, dependency, and abstraction has a cost to understand, maintain, and verify. Prefer the simplest solution that fully satisfies the current requirements.

- Solve the actual problem, not hypothetical future requirements.
- Avoid speculative abstractions, configuration, flexibility, and unnecessary dependencies.
- Do not invent edge cases or add defensive code for scenarios that cannot realistically occur under the current contracts.
- Handle genuine failure cases, particularly at external boundaries involving untrusted input, security, or unreliable dependencies.
- Prefer existing project, library, or framework capabilities over reinventing functionality.
- Optimize for fewer concepts and moving parts, not fewer lines. Explicit code, validation, and focused tests can add lines while reducing complexity.

Think ahead when a decision is expensive to reverse, but do not build infrastructure for requirements that do not exist.

## Give knowledge one owner (DRY)

Each business rule, shared convention, or important fact should have one authoritative definition so that changes do not require coordinated edits in multiple places.

- Avoid duplicating knowledge that must remain consistent.
- Extract shared behavior when a genuine common responsibility emerges.
- Do not force superficially similar code into a shared abstraction.
- Prefer a little straightforward duplication over unnecessary coupling.

DRY is about avoiding duplicated knowledge, not eliminating every repeated line of code.

## Keep responsibilities cohesive (SRP)

A module, class, or method should have a clear, coherent responsibility and as few independent reasons to change as practical.

- Keep related behavior together, especially when it changes for the same reason.
- Separate responsibilities when they are genuinely independent.
- Avoid combining unrelated functionality in one component.
- Do not split code into unnecessary fragments that must always be read and changed together.

The goal is cohesive, understandable code, not the maximum number of small classes or methods.

## Organize code around related responsibilities

Group related code by feature or domain so that components that work and change together remain easy to find.

- Keep related behavior, types, and tests close together where practical.
- Aim for high cohesion within modules and loose coupling between them.
- Place genuinely shared or cross-cutting functionality in an appropriate common location.
- Do not introduce shared modules or abstractions for functionality that belongs to a single feature.

Follow the project structure and module boundaries defined in the backend architecture.

## Manage complexity behind meaningful boundaries

Encapsulation should make code easier to understand by hiding implementation details while keeping behavior clear.

- Express operations in meaningful terms so callers do not need to understand their internal mechanics.
- Isolate external dependencies, volatile business rules, and other genuine sources of complexity where useful.
- Introduce interfaces, adapters, or additional layers when they protect a meaningful boundary or enable useful substitution.
- Avoid wrappers and indirection that merely forward calls without providing a useful responsibility or boundary.
- Keep boundaries consistent with the backend architecture.

A boundary should simplify understanding and maintenance, not introduce ceremony.

## Make behavior and dependencies explicit

Code should make its inputs, outputs, dependencies, state changes, and important failure modes understandable.

- Prefer clear control flow over clever expressions or hidden behavior.
- Avoid surprising mutation, implicit dependencies, and silently ignored errors.
- Keep state with the object or operation that owns it.
- Keep side effects deliberate and predictable.
- Encapsulate incidental details without hiding important behavior.

A reader should be able to understand an operation without tracing through unrelated parts of the system.

## Prefer reversible decisions

When several approaches satisfy the requirement, prefer the one that is easier to change, remove, or replace.

- Avoid premature commitments to unnecessary complexity or dependencies.
- Keep decisions local when they do not need to affect the wider application.
- Record the reasoning behind consequential decisions when it will help future maintainers.
- Do not introduce abstractions solely to make every decision theoretically reversible.

A straightforward implementation is often easier to change than a flexible framework designed for hypothetical requirements.

## Improve within the requested scope

Leave code clear and maintainable without turning focused work into unrelated cleanup.

- Make small, safe improvements when they directly support the requested change.
- Simplify unnecessary complexity or misleading names when they interfere with the work.
- Preserve unrelated code, behavior, and intentional work in progress.
- Do not expand a task into speculative improvements or broad refactoring.
- Suggest worthwhile improvements outside the requested scope rather than implementing them without approval.

The goal is to complete the requested work well, not to improve everything encountered along the way.