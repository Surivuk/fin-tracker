namespace FinTracker.IDomain;

public interface IDomainQuery<RequestData, ResponseData>
{
    public Task<ResponseData> Execute(RequestData request);
}

public interface IDomainQuery<ResponseData>
{
    public Task<ResponseData> Execute();
}