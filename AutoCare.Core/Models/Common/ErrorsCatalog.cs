using System.Reflection;
using System.Text.Json.Serialization;

namespace AutoCare.Core.Models.Common;

public class ErrorsCatalog : Enumeration
{
    [JsonIgnore]
    public Exception Exception { get; set; }
    public static Dictionary<string, IList<ErrorsCatalog>> GetErrorsList()
    {
        Dictionary<string, IList<ErrorsCatalog>> all = new Dictionary<string, IList<ErrorsCatalog>>();

        //get all inner classes
        var classes = typeof(ErrorsCatalog).GetNestedTypes(BindingFlags.Static | BindingFlags.Public);
        // get errors that placed in the inner classes
        foreach (var collection in classes)
        {
            all.Add(collection.Name, collection.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                .Select(f => f.GetValue(null))
                                                .Cast<ErrorsCatalog>()
                                                .ToList());
        }
        return all;
    }


    public override string ToString()
    {
        return $"ErrorCode : {Id} ,ErrorDetails : {Name}";
    }
    public ErrorsCatalog(int id, string name) : base(id, name, ' ')
    {

    }

    public readonly static ErrorsCatalog FaildToAddUser = new ErrorsCatalog(1001, "Failed To Add User");
    public readonly static ErrorsCatalog FaildToAddService = new ErrorsCatalog(1002, "Faild To Add Service");
    public readonly static ErrorsCatalog FaildToAddServiceDetail = new ErrorsCatalog(1003, "Faild To Add Service Detail");
    public readonly static ErrorsCatalog FaildToRetrieveServiceDetails = new ErrorsCatalog(1004, "Faild To Retrieve Service Details");
    public readonly static ErrorsCatalog FaildToDeleteSubService = new ErrorsCatalog(1005, "Faild To Delete SubService");
    public readonly static ErrorsCatalog FaildToDeleteSubServices = new ErrorsCatalog(1006, "Faild To Delete SubServices");
    public readonly static ErrorsCatalog FaildToDeleteCustomer = new ErrorsCatalog(1007, "Faild To Delete Customer");
    public readonly static ErrorsCatalog FaildToDeleteService = new ErrorsCatalog(1008, "Faild To Delete Service");
    public readonly static ErrorsCatalog FaildToUpdateSubService = new ErrorsCatalog(1009, "Faild To Update SubService");
    public readonly static ErrorsCatalog FaildToUpdateTotalPaidAndPrice = new ErrorsCatalog(1010, "Faild To Update Total Paid And Price");

    public readonly static ErrorsCatalog FailedToGetAllItems = new ErrorsCatalog(1011, "Failed To Get All Items");
    public readonly static ErrorsCatalog FailedToSaveItem = new ErrorsCatalog(1012, "Failed To Save Item");
    public readonly static ErrorsCatalog FailedToUpdateItem = new ErrorsCatalog(1013, "Failed To Update Item");
    public readonly static ErrorsCatalog FailedToDeleteItem = new ErrorsCatalog(1014, "Failed To Delete Item");
    public readonly static ErrorsCatalog FailedToSearchItem = new ErrorsCatalog(1015, "Failed To Search Item");

}