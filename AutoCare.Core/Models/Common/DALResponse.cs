namespace AutoCare.Core.Models.Common;
public class DALResponse<T>
{

    public ErrorsCatalog Error { get; set; }
    public T Value { get; set; }
    public bool Success => Error == null;

    public DALResponse(ErrorsCatalog error, T value)
    {
        Value = value;
        Error = error;
    }
    public DALResponse()
    {
        Error = null;
    }
}
