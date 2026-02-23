namespace FinTracker.Domain.Presentation.Model;

public class Category(string id, string title, string description)
{
    public string Id { get; private init; } = id;

    public string Title { get; private set; } = title;

    public string Description { get; private set; } = description;

    public void ChangeTitle(string newTitle) => Title = newTitle;

    public void ChangeDescription(string newDescription) => Description = newDescription;
}