internal class TestRepository(TestUnitOfWork unit)
{
    public void Save(string identifier) => unit.CurrentUnit.RecordExecution(identifier);
}