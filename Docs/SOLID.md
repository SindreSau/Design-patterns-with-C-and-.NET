# SOLID Principles

SOLID is an acronym for five design principles in object-oriented programming that help create more maintainable,
flexible, and scalable software.

## Single Responsibility Principle (SRP)

A class should have only one reason to change, meaning it should have only one responsibility.

**Example:** Classes are separated by their specific responsibilities rather than combining multiple functionalities
into one class.

## Open/Closed Principle (OCP)

Software entities should be open for extension but closed for modification.

**Example:** New functionality is added by creating new derived classes rather than modifying existing code.

## Liskov Substitution Principle (LSP)

Objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the
program.

**Example:** A `Square` class that inherits from `Rectangle` may only require one parameter in its constructor when its
height and width are equal. This should still allow the `Area()` method to function correctly for both classes and the
`Square` should still maintains the `width` and `height` properties.

## Interface Segregation Principle (ISP)

No client should be forced to depend on methods it does not use.

**Example:** Breaking down `IMultiFunctionDevice` into smaller interfaces (`IPrinter`, `IScanner`, `IFax`) so classes
only implement what they need.

## Dependency Inversion Principle (DIP)

High-level modules should not depend on low-level modules. Both should depend on abstractions.

**Example:** `Research` class depends on the `IRelationshipBrowser` interface rather than directly on the
`Relationships` implementation, making the system more flexible and testable.