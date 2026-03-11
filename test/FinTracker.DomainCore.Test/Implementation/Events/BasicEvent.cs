using FinTracker.IDomain;

internal record BasicEvent(string Identifier) : IDomainEvent;