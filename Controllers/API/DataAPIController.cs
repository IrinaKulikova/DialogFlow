using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDialog.Models;
using WebDialog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Google.Cloud.Dialogflow.V2;

namespace WebDialog.Controllers
{
    public class DataAPIController : ControllerBase
    {
        private readonly IDataRepository _repository;
        public DataAPIController(IDataRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<dynamic> GetAll([FromBody] WebhookRequest dialogflowRequest)
        {
            var intentName = dialogflowRequest.QueryResult.Intent.DisplayName;
            var actualQuestion = dialogflowRequest.QueryResult.QueryText;
            var datas = await _repository.GetAll();
            var testAnswer = "";
            datas.ToList().ForEach(c => testAnswer += c.SomeParameter.ToString() + ", ");
            var parameters = dialogflowRequest.QueryResult.Parameters;
            var dialogflowResponse = new WebhookResponse
            {
                FulfillmentText = testAnswer,
                FulfillmentMessages =
                { new Intent.Types.Message
                    { SimpleResponses = new Intent.Types.Message.Types.SimpleResponses
                        { SimpleResponses_ =
                            { new Intent.Types.Message.Types.SimpleResponse
                                {
                                   DisplayText = testAnswer,
                                   TextToSpeech = testAnswer,
                                }
                            }
                        }
                    }
                }
            };
            var jsonResponse = dialogflowResponse.ToString();
            return new ContentResult { Content = jsonResponse, ContentType = "application/json" }; ;
        }
    }
}
