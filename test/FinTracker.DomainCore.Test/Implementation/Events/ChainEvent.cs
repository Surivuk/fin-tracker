using FinTracker.IDomain;

internal record ChainEvent(string Identifier, string HandlerIdentifier) : IDomainEvent;