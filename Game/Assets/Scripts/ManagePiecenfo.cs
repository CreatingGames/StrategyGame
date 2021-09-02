using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Gs2.Core.Model.Region;
//using Gs2.Core.Model.BasicGs2Credential;
//using Gs2.Core.Net.Gs2RestSession;
//using Gs2.Core.Exception.Gs2Exception;
//using Gs2.Core.AsyncResult;
//using Gs2.Gs2Formation.Gs2FormationRestClient;
//using Gs2.Gs2Formation.Request.CreateNamespaceRequest;
//using Gs2.Gs2Formation.Result.CreateNamespaceResult;
using Gs2.Core;
using Gs2.Gs2Formation.Model;
using Gs2.Gs2Formation.Result;
using Gs2.Sample.Core;

/*
public class ManagePiecenfo : MonoBehaviour
{
    [SerializeField]
    public Gs2Client gs2Client;
    public IEnumerator LoadData()
    {
        AsyncResult<EzMoldModel> asyncResult = null;
        yield return gs2Client.client.Formation.GetMoldModel(
            new EzGetMoldModelResult()
                .withNamespaceName("namespace1")
                .withMoldName(""),
            r => asyncResult = r
        );
        if (asyncResult.Error != null)
        {
            throw asyncResult.Error;
        }
        var result = asyncResult.Result;
        var item = result.Item;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
