🇬🇧 **English** | 🇺🇦 [Українська](README.uk.md)

# 🚗 Lab13: Behavioral Patterns in a Car-Sharing Service

A **C# / .NET** console project: two problem situations of "Miskavto" solved with **Command** and **Chain of Responsibility**. Events are logged as `[time] [source] event`.

| Task | Problem | Pattern | Solution |
|---|---|---|---|
| 1 | Remote car commands: retry offline, undo, audit log | **Command** | `ICarCommand`, `CommandCenter` (queue, history, journal) |
| 2 | Trip admission checks in one nested method | **Chain of Responsibility** | `CheckHandler` and one class per check |

**Authors:** Katya, Stas. **Run:** `dotnet build`, `dotnet run`, choose `1`-`2`. Diagrams: `docs/diagrams/`. Peer review: `REVIEW.md`.