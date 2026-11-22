# AGENTS.md

Instructions for coding agents working on the e-state API. See [README](README.md) for setup and development commands.

## Instruction precedence

Follow platform and security restrictions first, then explicit user instructions, repository rules, and general defaults. When instructions conflict or requirements are unclear, ask for clarification if the difference matters.

## Before starting

- Inspect the relevant code, tests, and configuration before making changes.
- Consult the relevant sections of [backend architecture](docs/backend-architecture.md) for project structure, module boundaries, and design decisions.
- Consult [code standards](docs/code-standards.md) and [engineering principles](docs/engineering-principles.md) for relevant conventions.
- Follow existing logging patterns when modifying logs.
- Use project files, the tool manifest, and Makefile to determine versions and commands.

## Scope and implementation

- Implement only what the request requires. Avoid speculative features, unnecessary abstractions, and unrelated refactoring.
- Keep changes focused and proportional to the problem.
- Follow existing patterns unless there is a clear reason to improve them.
- Preserve unrelated code and work in progress.
- Prefer existing or built-in dependencies. Before adding a dependency, explain why it is needed, consider alternatives, and get approval. Prefer small, maintained open-source packages.
- Ask when missing information affects behavior, scope, or risk. Make reasonable decisions independently for routine implementation details.

## Code quality

- Prefer simple, readable code over clever or overly compact solutions.
- Use descriptive names, avoid unnecessary nesting, and separate distinct operations with blank lines.
- Prefer early returns when they simplify control flow.
- Follow [`.editorconfig`](.editorconfig) and the project's analyzer rules.
- Fix all analyzer errors. Review warnings, but do not remove intentional work-in-progress code solely to eliminate warnings.
- Do not suppress diagnostics or weaken formatting, linting, or build rules to make checks pass.
- Review automatically formatted code for readability.

## Development and compatibility

- The API is in development, and the owner controls its consumers. Update API contracts and data models directly when needed.
- Do not add backward-compatibility layers, deprecated routes, or legacy-data fallbacks unless requested.
- All project databases are disposable during development. Data can be deleted, schemas recreated, and sample data reseeded as needed.
- Update EF Core models and development seeds when changing the schema.
- Use `make db-reset` and `make db-seed` when needed.
- Do not introduce migrations until preserving existing data becomes necessary.

## Testing and verification

- Follow the [testing standards](docs/testing-standards.md). Do not add new tests by default. Add focused regression tests for important bug fixes or add tests when explicitly requested. Ask before creating new test files or suites.
- Update existing tests when intended behavior changes and their expectations no longer apply.
- Run the project's formatting checks, build, and relevant existing tests before completing work.
- Fix failures caused by your changes. Run checks as configured and keep unrelated code intact.
- Report which checks ran and any failures or checks that could not be completed.

## Documentation

- Keep documentation concise, practical, and accurate.
- Update existing documentation when behavior, setup, commands, or conventions change.
- Preserve important configuration, operational instructions, and safeguards.
- Keep each topic in one place and link to it instead of duplicating information.
- Use examples that demonstrate reusable patterns.
- Keep feature status, roadmaps, and bug notes in planning material. Use Git history for past changes and decisions.
- Do not hard-wrap Markdown prose.

## Git workflow

- Work directly on `main` during development. Do not create branches or pull requests unless explicitly requested.
- Do not commit, push, merge, or rewrite history unless explicitly requested.
- Keep commits focused and include only changes relevant to the task.
- Use commit messages in the format `(<type>) <description>`.
- Allowed types: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`.
- Example: `(docs) clarify module boundaries`.
- Do not add agent attribution to commits or commit trailers.

## Safety

- Project database resets and reseeding are permitted during development.
- Get approval before destructive actions affecting resources outside the project.
- Never expose secrets or commit credentials, `.env` files, or build artifacts.
- Do not contact people or modify external systems unless explicitly requested.
- Preserve existing hooks, CI checks, security controls, and test coverage.