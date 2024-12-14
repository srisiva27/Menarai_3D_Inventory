using MessageBrokers;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Controllers
{
    public class ServerController
    {
        private string _url = "https://wadahub.manerai.com/api/inventory/status";
        private string _token = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

         public ServerController()
        {
            MessageBroker.Default
                .Receive<WebRequestMessage>()
                .Subscribe(x => SendDataToServer(x.ItemName));
        }
       
        private async void SendDataToServer(string itemName)
        {
            WWWForm form = new WWWForm();
            form.AddField("item", itemName);
            UnityWebRequest www = UnityWebRequest.Post(_url, form);

            www.SetRequestHeader("authorization", string.Format("Bearer {0}", _token));
            await www.SendWebRequest().AsObservable();

            if (www.isNetworkError)
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text +"  ID   TYPE ::"+ itemName);
            }
        }
    }
}
