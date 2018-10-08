using System.ServiceModel;
using System.ServiceModel.Web;

namespace Service
{
    [ServiceContract]
    interface IHelloWorld
    {
        [OperationContract, WebGet]
        string Hello();

        [OperationContract]
        string Goodbye();
    }
}
